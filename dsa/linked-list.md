# Complete guide to Linked Lists
Here’s a **complete guide to Linked Lists** with real-world **use cases**, **C# implementation**, and **commonly asked interview problems**.

A **Linked List** is a **linear data structure** where elements (called **nodes**) are stored **non-contiguously** in memory and each node points to the **next** node.

### Structure of a Node

```csharp
public class Node
{
    public int data;
    public Node next;

    public Node(int val)
    {
        data = val;
        next = null;
    }
}
```


## Types of Linked Lists

| Type                     | Description                                        |
| ------------------------ | -------------------------------------------------- |
| **Singly Linked List**   | Each node points to the next. No previous pointer. |
| **Doubly Linked List**   | Each node has `next` and `prev` pointers.          |
| **Circular Linked List** | Last node points back to the first node.           |


## Real-World Use Cases

| Use Case                                    | Description                                       |
| ------------------------------------------- | ------------------------------------------------- |
| **Browser history**                         | Doubly linked list to move forward and backward.  |
| **Undo/Redo functionality**                 | Doubly or singly linked list.                     |
| **Music Playlist**                          | Circular linked list to play in a loop.           |
| **HashMap chaining (collision resolution)** | Uses linked lists in buckets.                     |
| **Memory-efficient queue/stack**            | Linked list based stack/queue for dynamic sizing. |


## 🛠️ C# Implementation of Singly Linked List

```csharp
public class SinglyLinkedList
{
    public Node head;

    public void AddLast(int data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            head = newNode;
            return;
        }
        Node temp = head;
        while (temp.next != null)
            temp = temp.next;

        temp.next = newNode;
    }

    public void Print()
    {
        Node temp = head;
        while (temp != null)
        {
            Console.Write(temp.data + " -> ");
            temp = temp.next;
        }
        Console.WriteLine("null");
    }
}
```


## Common Operations

| Operation         | Time Complexity | Description           |
| ----------------- | --------------- | --------------------- |
| Insertion at head | O(1)            | New node becomes head |
| Insertion at tail | O(n)            | Traverse till end     |
| Deletion by value | O(n)            | Traverse to find node |
| Search            | O(n)            | Traverse through list |


## Common Interview Problems (with approaches)

### 1. **Reverse a Linked List**

* **Approach**: Iterative or Recursive

```csharp
public Node Reverse(Node head)
{
    Node prev = null;
    Node curr = head;
    while (curr != null)
    {
        Node nextNode = curr.next;
        curr.next = prev;
        prev = curr;
        curr = nextNode;
    }
    return prev;
}
```


### 2. **Detect Cycle in a Linked List**

* **Approach**: Floyd’s Cycle Detection (Tortoise & Hare)

```csharp
public bool HasCycle(Node head)
{
    Node slow = head, fast = head;
    while (fast != null && fast.next != null)
    {
        slow = slow.next;
        fast = fast.next.next;
        if (slow == fast)
            return true;
    }
    return false;
}
```


### 3. **Find the Middle of Linked List**

* **Approach**: Slow and Fast Pointer

```csharp
public Node FindMiddle(Node head)
{
    Node slow = head, fast = head;
    while (fast != null && fast.next != null)
    {
        slow = slow.next;
        fast = fast.next.next;
    }
    return slow;
}
```


### 4. **Merge Two Sorted Linked Lists**

```csharp
public Node MergeSorted(Node l1, Node l2)
{
    Node dummy = new Node(-1);
    Node curr = dummy;

    while (l1 != null && l2 != null)
    {
        if (l1.data < l2.data)
        {
            curr.next = l1;
            l1 = l1.next;
        }
        else
        {
            curr.next = l2;
            l2 = l2.next;
        }
        curr = curr.next;
    }

    curr.next = (l1 != null) ? l1 : l2;
    return dummy.next;
}
```


### 5. **Remove N-th Node from End**

* **Approach**: Two pointers (maintain a gap of N)

```csharp
public Node RemoveNthFromEnd(Node head, int n)
{
    Node dummy = new Node(0);
    dummy.next = head;
    Node fast = dummy, slow = dummy;

    for (int i = 0; i <= n; i++)
        fast = fast.next;

    while (fast != null)
    {
        fast = fast.next;
        slow = slow.next;
    }

    slow.next = slow.next.next;
    return dummy.next;
}
```


### 6. **Palindrome Linked List**

* **Approach**: Reverse second half and compare

```csharp
public bool IsPalindrome(Node head)
{
    if (head == null || head.next == null) return true;

    Node mid = FindMiddle(head);
    Node secondHalf = Reverse(mid);
    Node firstHalf = head;

    while (secondHalf != null)
    {
        if (firstHalf.data != secondHalf.data)
            return false;

        firstHalf = firstHalf.next;
        secondHalf = secondHalf.next;
    }

    return true;
}
```


## Techniques Used in Linked List Problems

| Technique           | Description                                                     |
| ------------------- | --------------------------------------------------------------- |
| Two Pointer         | Used in detecting cycle, finding middle, removing N-th from end |
| Dummy Node          | Helps in simplifying edge cases (deletion, merging)             |
| Recursion           | Reversal, merge sort                                            |
| Stack               | Used in palindrome or reverse k-group                           |
| Fast & Slow Pointer | Efficient traversal                                             |


## Final Tips for Interviews

1. **Explain tradeoffs**: Array vs LinkedList (contiguous vs dynamic memory, O(1) access vs O(1) insert/delete).
2. **Write clean and modular code**.
3. **Practice edge cases**: empty list, single node, head/tail deletion.
4. **Use visual diagrams** to explain pointer manipulation.
5. **Know how GC works in C#**, especially if dealing with memory leaks in linked structures.


## Doubly Linked List (DLL)

A **Doubly Linked List** is a linear data structure where each node contains:

* `data`
* a pointer to the **next** node
* a pointer to the **previous** node

### Node Structure in C\#

```csharp
public class DoublyNode
{
    public int data;
    public DoublyNode next;
    public DoublyNode prev;

    public DoublyNode(int val)
    {
        data = val;
        next = null;
        prev = null;
    }
}
```


### Use Cases of DLL

| Use Case            | Why DLL?                                    |
| ------------------- | ------------------------------------------- |
| **Browser history** | You can go `back` and `forward` easily.     |
| **Undo/Redo**       | Navigate in both directions.                |
| **MRU/LRU Cache**   | Efficient removal/insertion from both ends. |
| **Media Players**   | Skipping forward/backward between songs.    |


### Basic Operations in C\#

```csharp
public class DoublyLinkedList
{
    public DoublyNode head;

    public void AddToEnd(int data)
    {
        var newNode = new DoublyNode(data);
        if (head == null)
        {
            head = newNode;
            return;
        }

        var temp = head;
        while (temp.next != null)
            temp = temp.next;

        temp.next = newNode;
        newNode.prev = temp;
    }

    public void PrintForward()
    {
        var temp = head;
        while (temp != null)
        {
            Console.Write(temp.data + " <-> ");
            temp = temp.next;
        }
        Console.WriteLine("null");
    }

    public void PrintBackward()
    {
        if (head == null) return;

        var temp = head;
        while (temp.next != null)
            temp = temp.next;

        while (temp != null)
        {
            Console.Write(temp.data + " <-> ");
            temp = temp.prev;
        }
        Console.WriteLine("null");
    }
}
```


### Key Operations & Time Complexities

| Operation           | Time Complexity         |
| ------------------- | ----------------------- |
| Insert at Head/Tail | O(1)                    |
| Delete Node         | O(1) (if node is given) |
| Search Node         | O(n)                    |


### 💡 Common Interview Problems (DLL)

1. **Implement LRU Cache** – Most common use case.
2. **Flatten a multilevel DLL** – Used in nested linked structures.
3. **Reverse a DLL** – Just swap `next` and `prev` pointers.

```csharp
public DoublyNode Reverse(DoublyNode head)
{
    DoublyNode temp = null;
    DoublyNode current = head;

    while (current != null)
    {
        temp = current.prev;
        current.prev = current.next;
        current.next = temp;
        current = current.prev;
    }

    return temp?.prev; // new head
}
```


## Circular Linked List (CLL)

A **Circular Linked List** is a variation where:

* The **last node points back to the head**
* Can be **singly** or **doubly** linked

### Node Structure (Same as singly/doubly node)


### Use Cases of CLL

| Use Case                      | Why CLL?                      |
| ----------------------------- | ----------------------------- |
| **Round Robin Scheduling**    | Cycle through tasks endlessly |
| **Circular Buffer (Queue)**   | Automatically wrap around     |
| **Game players (turn-based)** | Rotate players circularly     |


### C# Implementation – Singly CLL

```csharp
public class CircularLinkedList
{
    public Node head;

    public void AddToEnd(int data)
    {
        Node newNode = new Node(data);

        if (head == null)
        {
            head = newNode;
            newNode.next = head;
            return;
        }

        Node temp = head;
        while (temp.next != head)
            temp = temp.next;

        temp.next = newNode;
        newNode.next = head;
    }

    public void Print()
    {
        if (head == null) return;

        Node temp = head;
        do
        {
            Console.Write(temp.data + " -> ");
            temp = temp.next;
        } while (temp != head);

        Console.WriteLine("(head)");
    }
}
```


### Operations in CLL

| Operation           | Complexity | Notes                      |
| ------------------- | ---------- | -------------------------- |
| Insert at end       | O(n)       | Traverse till last node    |
| Insert at beginning | O(1)       | With tail pointer, O(1)    |
| Delete node         | O(n)       | Search and update pointers |


### Interview-Oriented Problems

1. **Josephus Problem (Hot Potato)**

   * Eliminate every `k-th` node in circular fashion
   * Solve using simulation on CLL or use recursion.

2. **Split a Circular Linked List into two halves**

3. **Check if a linked list is circular**

```csharp
public bool IsCircular(Node head)
{
    if (head == null) return true;

    Node temp = head.next;
    while (temp != null && temp != head)
        temp = temp.next;

    return temp == head;
}
```


## 🔃 Comparison Summary

| Feature           | Singly LL    | Doubly LL     | Circular LL              |
| ----------------- | ------------ | ------------- | ------------------------ |
| Prev pointer      | ❌            | ✅             | Optional                 |
| Traverse backward | ❌            | ✅             | ✅ (in CDLL)              |
| Circular nature   | ❌            | ❌             | ✅                        |
| Memory per node   | Less         | More          | Same                     |
| Use cases         | Simple lists | History, Undo | Round Robin, Game cycles |


## ✅ Final Notes

* **Singly LL**: Best when only forward traversal is needed.
* **Doubly LL**: Prefer when you need back-and-forth traversal.
* **Circular LL**: Ideal when you need to cycle through nodes (like scheduling).
