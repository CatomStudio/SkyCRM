using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Timers;

namespace ForTest.Data
{
    /// <summary>
    ///  计时对象。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class TimerObject<T> : ISerializable
    {
        private DateTime _birthday;

        public TimerObject()
        {
            _birthday = DateTime.Now;
        }

        public T Value { get; set; }

        #region 核心函数

        public void ResetBirthday()
        {
            _birthday = DateTime.Now;
        }

        public virtual DateTime Birthday
        {
            get { return _birthday; }
            protected set { _birthday = value; }
        }

        // 时间跨度
        public virtual TimeSpan Elapsed
        {
            get { return DateTime.Now - _birthday; }
        }

        // 跨度时间
        public virtual double ElapsedMilliseconds
        {
            get { return Elapsed.TotalMilliseconds; }
        }

        public virtual long ElapsedTicks
        {
            get { return Elapsed.Ticks; }
        }

        public static implicit operator TimerObject<T>(T value)
        {
            return new TimerObject<T> { Value = value };
        }

        public static implicit operator T(TimerObject<T> value)
        {
            return value == null ? default(T) : value.Value;
        }

        #endregion

        #region ISerializable 成员

        protected TimerObject(SerializationInfo info, StreamingContext context)
        {
            _birthday = info.GetDateTime("$birthday");
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("$birthday", _birthday);
        }

        #endregion
    }

    /// <summary>
    /// 对象池
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    [Serializable]
    public sealed class ObjectPool<T> : IDisposable, ISerializable where T : class
    {
        public ObjectPool(Func<T> creater, short maxCoun = 32)
        {
            if (maxCoun <= 0)
                throw new ArgumentException("参数maxCount为空");
            if (creater == null)
                throw new ArgumentException("参数creater为空");
            maxCount = maxCoun;
            Creater = creater;
        }

        /// <summary>
        /// 内部类：指明一种池中的对象。
        /// </summary>
        private class Catfish<R>
        {
            public TimerObject<R> Fish;

            public double Life;

            private bool isFree;

            /// <summary>
            /// 是否空闲
            /// </summary>
            public bool IsFree
            {
                get
                {
                    if (isFree)
                        return true;
                    else
                        return Fish.ElapsedMilliseconds > Life;
                }
                set
                {
                    isFree = value;
                    if (!value)
                    {
                        if (Fish != null)
                            Fish.ResetBirthday();
                    }
                }
            }

        }

        // 异步操作的封锁对象。
        private readonly object Sync = new object();

        private readonly short maxCount;
        private bool isInTimer = false;
        /// <summary>
        /// 动态或手动释放的对象集合
        /// </summary>
        private Hashtable table;

        private Hashtable Table
        {
            get
            {
                if (table == null)
                {
                    lock (Sync)
                    {
                        if (table == null)
                        {
                            table = Hashtable.Synchronized(new Hashtable());
                        }
                    }
                }
                return table;
            }
        }

        private void Add(int id, Catfish<T> fish)
        {
            Table.Add(id, fish);
        }
        private void Remove(int id)
        {
            Table.Remove(id);
        }
        private int CurrentThreadID
        {
            get
            {
                return System.Threading.Thread.CurrentThread.ManagedThreadId;
            }
        }
        public short Count
        {
            get
            {
                return (short)Table.Count;
            }
        }

        private Func<T> Creater { get; set; }

        private Func<T, bool> predicate;

        /// <summary>
        /// 设置对象的自动判定方式
        /// </summary>
        public Func<T, bool> Predicate
        {
            get
            {
                if (predicate == null)
                {
                    lock (Sync)
                    {
                        if (predicate == null)
                        {
                            predicate = r => false;
                        }
                    }
                }
                return predicate;
            }
            set
            {
                if (value != null)
                    predicate = value;
            }
        }

        /// <summary>
        /// 达到指定时间，系统自动回收对象
        /// 因为.NET中没有真正的异步处理方式
        /// 所有的异步都是基于线程池来做的
        /// 所以系统会以每个托管线程的线程ID来标示对象
        /// 如果一个线程对某个对象借取时间超过借取时间
        /// 系统则会自动回收对象
        /// </summary>
        /// <param name=”life”>借取时间，毫秒为单位,单个线程</param>
        /// <returns>对象</returns>
        public T Borrow(double life = 3000)
        {
            if (life < 0)
                throw new ArgumentException("参数life不能小于0");
            if (disposing)
                return null;
            var id = CurrentThreadID;
            Catfish<T> obj = null;
            if (Table.ContainsKey(id))
            {
                obj = Table[id] as Catfish<T>;
                obj.IsFree = false;
                obj.Life = life;
                //obj.Fish.ResetBirthday();
                return obj.Fish;
            }
            else if (Count < maxCount)
            {
                T item = Creater();
                obj = new Catfish<T>
                {
                    Fish = new TimerObject<T>
                    {
                        Value = item
                    },
                    IsFree = false,
                    Life = life
                };
                Add(id, obj);
                return item;
            }
            else
            {
                return FindFreeObject(life, id, ref obj);
            }
        }

        private T FindFreeObject(double life, int id, ref Catfish<T> obj)
        {
            T item = null;
            var tb = Table;
            int? invalidID = null;
            foreach (DictionaryEntry entry in tb)
            {
                obj = entry.Value as Catfish<T>;
                var value = obj.Fish.Value;
                if (obj.IsFree)//手动释放或超时的
                {
                    item = value;
                    invalidID = (int)entry.Key;
                    break;
                }
                if (Predicate(value))//状态设置的
                {
                    item = value;
                    invalidID = (int)entry.Key;
                    break;
                }
            }
            if (item == null)
                return null;// ThrowHelper.Throw<InvalidOperationException, T>();
            if (invalidID.HasValue)
            {
                Remove(invalidID.Value);
                Add(id, new Catfish<T>
                {
                    IsFree = false,
                    Life = life,
                    Fish = item
                });
            }
            return item;
        }
        /// <summary>
        /// 归还对象到池中
        /// </summary>
        public void GiveBack()
        {
            var id = CurrentThreadID;
            Catfish<T> obj = null;
            if (Table.ContainsKey(id))
            {
                obj = Table[id] as Catfish<T>;
                obj.IsFree = true;
            }
        }

        #region IDisposable 成员
        private DisposeMode? disposeMode;
        public DisposeMode DisposeMode
        {
            get
            {
                return disposeMode.HasValue ? disposeMode.Value : DisposeMode.Noraml;
            }
            set
            {
                if (disposeMode == null)
                {
                    lock (Sync)
                    {
                        if (disposeMode == null)
                        {
                            disposeMode = value;
                        }
                    }
                }
            }
        }
        private Timer disposeTimer;
        private Timer DisposeTimer
        {
            get
            {
                if (disposeTimer == null)
                {
                    lock (Sync)
                    {
                        if (disposeTimer == null)
                        {
                            disposeTimer = new Timer(100);
                            disposeTimer.Elapsed += disposeTimer_Elapsed;
                        }
                    }
                }
                return disposeTimer;
            }
        }

        private void disposeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isInTimer)
                return;
            isInTimer = true;
            Dispose();
        }
        ~ObjectPool()
        {
            Dispose(false);
        }

        private Boolean disposed;
        private bool disposing = false;
        private void Dispose(bool disposing)
        {
            this.disposing = true;
            if (disposed)
                return;
            if (disposing)
            {
                if (DisposeMode == DisposeMode.Noraml)
                {
                    if (table != null)
                    {
                        var tb = Table;
                        var found = false;
                        foreach (DictionaryEntry item in tb)
                        {
                            var value = item.Value as Catfish<T>;
                            if (value == null)
                                continue;
                            if (!value.IsFree)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (found)
                        {
                            if (!DisposeTimer.Enabled)
                            {
                                DisposeTimer.Enabled = true;
                            }
                            isInTimer = false;
                            return;
                        }
                    }
                }
            }

            lock (Sync)
            {
                isInTimer = false;
                if (disposeTimer != null)
                {
                    disposeTimer.Enabled = false;
                    disposeTimer.Dispose();
                }
                if (table != null)
                    table.Clear();
            }
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #region ISerializable 成员

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("$table", Table);
            info.AddValue("$maxcount", maxCount);
        }
        private ObjectPool(SerializationInfo info, StreamingContext context)
        {
            table = info.GetValue("$table", typeof(Hashtable)) as Hashtable;
            maxCount = short.Parse(info.GetString("$maxcount"));
        }

        #endregion
    }


    /// <summary>
    /// 枚举DisposeMode代码：
    /// 对象借取之后的归还方式
    /// </summary>
    public enum DisposeMode : byte
    {
        /// <summary>
        /// 正常方式
        /// 不容许借取新对象
        /// 将等待池中所有对象被释放后在释放对象
        /// </summary>
        Noraml = 1,

        /// <summary>
        /// 强制关闭
        /// 不容许借取新对象
        /// 将断掉当前所有对象连接
        /// </summary>
        Force = 2
    }

}
