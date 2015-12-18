using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForTest.Base
{
    public abstract class A
    {
        // 1. abstract methods must declare in abstract class.
        // 2. abstract methods must have not a body.
        // 3. SubClass must override abstract methods. 
        // * abstract methods often like interfaces.
        public abstract void ATodo1();

        // 1. virtual is not used for class.
        // 2. virtual methos must have a body.
        // 3. SubClass must be public or protected.
        // * virtual methods often like shared common methods.
        public virtual void ATodo2()
        {
            Console.WriteLine("This is virtual method Todo2 in a abstract class A.");
        }

    }

    public class B : A
    {
        public override void ATodo1()
        {
            base.ATodo2();
        }

        public void BTodo2()
        {
            Console.WriteLine("This is virtual method Todo2 in a abstract class B.");
        }
    }

    public class C : B
    {

    }

    public class Entry
    {
        public static void Main1()
        {
            // 1. 子类转型为父类后，实例对象的数据存储，按父类所对应的【成员数据】存储。
            // eg. C extends B，B extends A.
            //       A a = new C();
            // 实例对象 a 存储的数据中包括 A 类的所有成员，而不包括 B 中的超出 A 的成员。
            A a = new C();
            a.ATodo2();
            //a.BTodo2(); // instance a doesn't contain this class B method.
        }
    }

}
