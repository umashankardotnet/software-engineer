# Complete Guide to Trees in Data Structures and Algorithms (DSA) - C# Focus

---

## 1. What is a Tree?

A **Tree** is a **non-linear, hierarchical data structure** consisting of nodes connected by edges.

### Key Characteristics:

* One node is the **Root**.
* Every node has **0 or more child nodes**.
* Nodes with no children are called **Leaf Nodes**.
* Trees are inherently **recursive**.

---

## 2. Basic Terminology

| Term   | Meaning                              |
| ------ | ------------------------------------ |
| Node   | Element of the tree                  |
| Root   | Top node                             |
| Child  | Node directly connected below a node |
| Parent | Node directly above another node     |
| Leaf   | Node without children                |
| Edge   | Connection between two nodes         |
| Height | Longest path from node to leaf       |
| Depth  | Distance from root to node           |

---

## 3. Tree Types

### 3.1 General Tree (N-ary Tree)

* Nodes can have any number of children.
* Example: File Systems

```csharp
public class NaryTreeNode
{
    public string Value;
    public List<NaryTreeNode> Children = new List<NaryTreeNode>();

    public NaryTreeNode(string value)
    {
        Value = value;
    }
}
```

### 3.2 Binary Tree

* Each node has at most two children.

```csharp
public class BinaryTreeNode
{
    public int Value;
    public BinaryTreeNode Left, Right;

    public BinaryTreeNode(int value)
    {
        Value = value;
    }
}

// Example: Creating a Binary Tree
BinaryTreeNode root = new BinaryTreeNode(1);
root.Left = new BinaryTreeNode(2);
root.Right = new BinaryTreeNode(3);
```

### 3.3 Full Binary Tree

* Every node has 0 or 2 children.

### 3.4 Perfect Binary Tree

* All internal nodes have two children, all leaves are at the same level.

### 3.5 Complete Binary Tree

* All levels are filled except possibly the last, filled left to right.

### 3.6 Balanced Binary Tree

* The height difference between left and right subtree is minimal.

### 3.7 Binary Search Tree (BST)

* Left child < Parent < Right child.

```csharp
public class BSTNode
{
    public int Value;
    public BSTNode Left, Right;

    public BSTNode(int value)
    {
        Value = value;
    }
}

public BSTNode Insert(BSTNode root, int key)
{
    if (root == null) return new BSTNode(key);
    if (key < root.Value)
        root.Left = Insert(root.Left, key);
    else
        root.Right = Insert(root.Right, key);
    return root;
}

// Example: Building BST
BSTNode bstRoot = null;
bstRoot = Insert(bstRoot, 8);
bstRoot = Insert(bstRoot, 3);
bstRoot = Insert(bstRoot, 10);
```

### 3.8 Self-Balancing Trees

* AVL Tree
* Red-Black Tree

### 3.9 Heap (Min-Heap / Max-Heap)

```csharp
var pq = new PriorityQueue<string, int>();
pq.Enqueue("Task1", 2);
pq.Enqueue("Task2", 1);

while (pq.Count > 0)
{
    Console.WriteLine(pq.Dequeue());
}
```

### 3.10 Trie (Prefix Tree)

```csharp
public class TrieNode
{
    public Dictionary<char, TrieNode> Children = new();
    public bool EndOfWord = false;
}

public class Trie
{
    private TrieNode root = new();

    public void Insert(string word)
    {
        var node = root;
        foreach (char c in word)
        {
            if (!node.Children.ContainsKey(c))
                node.Children[c] = new TrieNode();
            node = node.Children[c];
        }
        node.EndOfWord = true;
    }

    public bool Search(string word)
    {
        var node = root;
        foreach (char c in word)
        {
            if (!node.Children.ContainsKey(c))
                return false;
            node = node.Children[c];
        }
        return node.EndOfWord;
    }
}
```

---

## 4. Tree Traversals

### 4.1 Depth-First Search (DFS)

* InOrder (Left, Root, Right)
* PreOrder (Root, Left, Right)
* PostOrder (Left, Right, Root)

```csharp
public void InOrder(BinaryTreeNode node)
{
    if (node == null) return;
    InOrder(node.Left);
    Console.Write(node.Value + " ");
    InOrder(node.Right);
}

public void PreOrder(BinaryTreeNode node)
{
    if (node == null) return;
    Console.Write(node.Value + " ");
    PreOrder(node.Left);
    PreOrder(node.Right);
}

public void PostOrder(BinaryTreeNode node)
{
    if (node == null) return;
    PostOrder(node.Left);
    PostOrder(node.Right);
    Console.Write(node.Value + " ");
}
```

### 4.2 Breadth-First Search (BFS) (Level Order)

```csharp
public void LevelOrder(BinaryTreeNode root)
{
    if (root == null) return;

    Queue<BinaryTreeNode> queue = new Queue<BinaryTreeNode>();
    queue.Enqueue(root);

    while (queue.Count > 0)
    {
        var node = queue.Dequeue();
        Console.Write(node.Value + " ");

        if (node.Left != null) queue.Enqueue(node.Left);
        if (node.Right != null) queue.Enqueue(node.Right);
    }
}
```

---

## 5. Why Recursion for Trees?

* Trees are inherently recursive (subtree of a tree is itself a tree).
* Simplifies code by avoiding manual stack management.
* Elegant for traversal and decision-making problems.

---

## 6. Common Interview Problems

| Problem                                | Concept                  | LeetCode ID  |
| -------------------------------------- | ------------------------ | ------------ |
| InOrder, PreOrder, PostOrder Traversal | DFS, recursion           | 94, 144, 145 |
| Level Order Traversal                  | BFS                      | 102          |
| Maximum Depth                          | Recursion                | 104          |
| Symmetric Tree                         | Recursion                | 101          |
| Path Sum                               | DFS + Backtracking       | 112          |
| Lowest Common Ancestor (LCA)           | DFS + Recursion          | 236          |
| Validate BST                           | DFS + Min/Max Constraint | 98           |
| Diameter of Binary Tree                | DFS + Height Calculation | 543          |
| Kth Smallest in BST                    | InOrder + Counter        | 230          |
| Serialize and Deserialize Tree         | BFS/DFS                  | 297          |
| Flatten Binary Tree to Linked List     | PreOrder Traversal       | 114          |
| Max Path Sum                           | Recursion + DFS          | 124          |

---

## 7. Summary & Key Takeaways

* Master **traversals** first.
* Understand **BST rules and operations**.
* Learn recursion as the **natural fit for tree algorithms**.
* Practice **problem-solving on trees in C#**.
* Use LeetCode, GeeksforGeeks for hands-on exercises.

---

This guide now includes code examples alongside each concept for better understanding and practical implementation.
