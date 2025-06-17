using System;

namespace TreePractice
{

    public class BinaryTree
    {
        Node root;
        public class Node
        {
            public int Data;
            public Node Left, Right;
            public Node(int data)
            {
                Data = data;
                Left = Right = null;
            }
        }

        public BinaryTree()
        {
            root = null;
        }


        /// <summary>
        /// insert item in tree
        /// </summary>
        /// <param name="data"></param>
        public void Insert(int item)
        {
           Insert(root, item);
        }

        private Node Insert(Node root, int data)
        {
            // root
            if (root == null)
            {
                root = new Node(data);
                return root;
            }

            // left child
            if (data < root.Data)
            {
                root.Left = Insert(root.Left, data);
            }
            // right child
            else if (data > root.Data)
            {
                root.Right = Insert(root.Right, data);
            }

            return root;
        }

        /// <summary>
        /// InOrder Traversal of Tree
        /// </summary>
        /// <param name="root">Root Node of Tree</param>
        public void InOrderTraversal()
        {
            InOrderTraverse(root);
        }

        private void InOrderTraverse(Node root)
        {
            if (root != null)
            {
                InOrderTraverse(root.Left);
                Console.WriteLine(root.Data);
                InOrderTraverse(root.Right);
            }
        }
    }

}
