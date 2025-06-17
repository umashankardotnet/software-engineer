using System;

namespace TreePractice
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree tree = new BinaryTree();
            tree.Insert(50);
            tree.Insert(30);
            tree.Insert(20);
            tree.Insert(40);
            tree.Insert(70);
            tree.Insert(60);
            tree.Insert(80);

            // Print inorder traversal of the BST
            tree.InOrderTraversal();

            Console.WriteLine("Hello World!");
        }
    }
}
