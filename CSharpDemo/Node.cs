using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForTest
{
    /// <summary>
    ///  结构点框架
    /// </summary>
    public abstract class Node
    {
        public long ID { get; set; }

        public string Name { get; set; }
    }

    // 树结构节点
    public abstract class TreeNode
    {
        public long ID { get; set; }

        public string Name { get; set; }

        // 树节点的父节点只有一个
        public TreeNode Parent { get; set; }

        // 子节点集合
        public List<TreeNode> Childs { get; set; }

        public bool IsRoot
        {
            get { throw new NotImplementedException(); }
            set { IsRoot = this.Parent == null ? true : false; }
        }

        public bool IsLeaf
        {
            get { throw new NotImplementedException(); }
            set { IsLeaf = this.Childs == null || this.Childs.Count == 0 ? true : false; }
        }

        // Methods
        public abstract void Add(TreeNode node);

    }

    /// <summary>
    /// <see cref="ForTestMvc.Controllers.Home.Index"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNode<T> : TreeNode
    {
        public T target { set; get; }


        public override void Add(TreeNode node)
        {
            try
            {
                this.Childs.Add(node);
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

    }

    public class TreeInfo
    {
        public long TID { get; set; }

        public string Name { get; set; }
    }


}
