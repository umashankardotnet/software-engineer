namespace LinkedListPractice
{
    /// <summary>
    /// singly linked list node
    /// </summary>
    class Node<T>
    {
        public T data;
        public Node<T> Next;

        public Node(T value)
        {
            data = value;
            Next = null;
        }
    }


    class SinglyLinkedList<T>
    {
        public Node<T> Head, Tail;
        int count = 0;

        public int Count
        {
            get
            {
                return count;
            }
        }

        public SinglyLinkedList()
        {
            Head = null;
            Tail = null;
        }

        /// <summary>
        /// Add item to the front of linked list in O(1) time complexity
        /// </summary>
        /// <param name="item"></param>
        public void InsertFront(T item)
        {
            Node<T> node = new Node<T>(item);
            node.Next = Head;
            count++;
            // make node as Head
            Head = node;
            if (count == 1)
            {
                Tail = node;
            }
        }

        /// <summary>
        /// add item to end of linked list in O(1) time complexity where n = number of nodes
        /// </summary>
        /// <param name="item"></param>
        public void InserLast(T item)
        {
            Node<T> node = new Node<T>(item);
            if (count == 0)
            {
                Head = node;
            }
            else
            {
                Tail.Next = node;
            }
            Tail = node;
            count++;
        }

        /// <summary>
        /// insert a node after a given node in O(1) time complexity
        /// </summary>
        /// <param name="prevNode"></param>
        /// <param name="item"></param>
        public void InsertAfter(Node<T> prevNode, T item)
        {
            if (prevNode == null)
                return;

            var node = new Node<T>(item);
            node.Next = prevNode.Next;
            prevNode.Next = node;
            count++;
        }

        /// <summary>
        /// remove item from front from linked list in O(1) time complexity
        /// </summary>
        public void RemoveFront()
        {
            if (count == 0)
                return;

            Head = Head.Next;
            count--;
            if (count == 0)
                Tail = null;
        }

        /// <summary>
        /// remove item from last of linked list
        /// </summary>
        public void RemoveLast()
        {
            if (count == 0)
                return;

            if (count == 1)
            {
                Head = Tail = null;
            }
            else
            {

                var current = Head;
                while (current.Next != Tail)
                {
                    current = current.Next;
                }
                current.Next = null;
                Tail = current;
            }
            count--;
        }

        /// <summary>
        /// removes an item after a given node from linkedlist in O(n) time complexity
        /// </summary>
        /// <param name="prevNode"></param>
        public void RemoveAfter(Node<T> prevNode)
        {
            if (count == 0)
                return;

            if (prevNode.Next == null)
            {
                return;
            }
            else
            {
                var current = Head;
                while (current != prevNode)
                {
                    current = current.Next;
                }
                var nodeToBeDeleted = current.Next;
                current.Next = nodeToBeDeleted.Next;

                // reset the tail // case when node te be deleted is last node
                if (current.Next == null)
                {
                    Tail = current;
                }
                count--;
            }
        }
    }
}
