# DFS 

DFS (Depth First Search) is a graph/tree traversal algorithm that explores as far as possible along each branch before backtracking. Let me explain with simple examples:

### 1. Basic Concept
Think of DFS like exploring a maze - you keep going down one path until you hit a dead end, then backtrack and try another path.

### 2. Simple Tree Example
```csharp
public class TreeNode 
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    
    public TreeNode(int value) 
    {
        val = value;
    }
}

/*
Consider this tree:
       1
      / \
     2   3
    / \
   4   5

DFS will visit: 1 → 2 → 4 → 5 → 3
*/
```

### 3. Basic DFS Implementation
```csharp
public class Solution 
{
    // Recursive DFS
    public void DFS(TreeNode root) 
    {
        if (root == null) return;
        
        // Process current node
        Console.Write(root.val + " ");
        
        // Visit left subtree
        DFS(root.left);
        
        // Visit right subtree
        DFS(root.right);
    }
}
```

### 4. Real-World Analogy
Imagine exploring a family tree:
```csharp
public class FamilyMember 
{
    public string name;
    public List<FamilyMember> children;
    
    public void ExploreFamily(FamilyMember person) 
    {
        if (person == null) return;
        
        // Visit current person
        Console.WriteLine($"Visiting: {person.name}");
        
        // Visit all children
        foreach(var child in person.children) 
        {
            ExploreFamily(child);
        }
    }
}
```

### 5. Practical Example: Finding a Path in a Maze
```csharp
public class MazeSolver 
{
    private int[,] maze;
    private bool[,] visited;
    
    public bool FindPath(int x, int y, int endX, int endY) 
    {
        // Check boundaries and walls
        if (x < 0 || y < 0 || x >= maze.GetLength(0) || 
            y >= maze.GetLength(1) || maze[x,y] == 1 || visited[x,y])
            return false;
            
        // Mark as visited
        visited[x,y] = true;
        
        // Found destination
        if (x == endX && y == endY)
            return true;
            
        // Try all directions
        if (FindPath(x+1, y, endX, endY)) return true;  // Down
        if (FindPath(x-1, y, endX, endY)) return true;  // Up
        if (FindPath(x, y+1, endX, endY)) return true;  // Right
        if (FindPath(x, y-1, endX, endY)) return true;  // Left
        
        return false;
    }
}
```

### 6. When to Use DFS
```csharp
/*
DFS is useful for:
1. Finding paths in a graph/maze
2. Detecting cycles
3. Topological sorting
4. Solving puzzles with only one solution
*/
```

### 7. Visual Step-by-Step Example
```csharp
/*
Tree:       1
          /   \
         2     3
        / \   
       4   5   

Step 1: Visit 1
Stack: [1]
Visited: 1

Step 2: Visit 2
Stack: [1, 2]
Visited: 1, 2

Step 3: Visit 4
Stack: [1, 2, 4]
Visited: 1, 2, 4

Step 4: Backtrack to 2, Visit 5
Stack: [1, 2, 5]
Visited: 1, 2, 4, 5

Step 5: Backtrack to 1, Visit 3
Stack: [1, 3]
Visited: 1, 2, 4, 5, 3
*/
```

### 8. Iterative vs Recursive
```csharp
// Iterative DFS
public void DFSIterative(TreeNode root) 
{
    if (root == null) return;
    
    Stack<TreeNode> stack = new Stack<TreeNode>();
    stack.Push(root);
    
    while (stack.Count > 0) 
    {
        TreeNode current = stack.Pop();
        Console.Write(current.val + " ");
        
        // Push right first (so left is processed first)
        if (current.right != null)
            stack.Push(current.right);
        if (current.left != null)
            stack.Push(current.left);
    }
}

// Recursive DFS (same as before)
public void DFSRecursive(TreeNode root) 
{
    if (root == null) return;
    Console.Write(root.val + " ");
    DFSRecursive(root.left);
    DFSRecursive(root.right);
}
```

### 9. Common Applications
```csharp
// Example: Finding if a path exists between two nodes
public bool PathExists(TreeNode root, int target) 
{
    if (root == null) return false;
    
    // Found target
    if (root.val == target) return true;
    
    // Look in left and right subtrees
    return PathExists(root.left, target) || 
           PathExists(root.right, target);
}
```

### 10. Time and Space Complexity
```csharp
/*
Time Complexity: O(V + E)
- V = number of vertices/nodes
- E = number of edges

Space Complexity: 
- O(H) for recursive (H = height of tree)
- O(V) for iterative
*/
```

DFS is fundamental in computer science and is used in:
1. Web crawlers
2. Finding connected components
3. Solving puzzles
4. Game AI
5. Social networking

Understanding DFS is crucial for solving many graph and tree-related problems in programming.

## How it works?
Let me explain DFS (Depth First Search) with a simple example using a binary tree traversal.

```csharp
// First, let's define a basic TreeNode class
public class TreeNode 
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    
    public TreeNode(int value) 
    {
        val = value;
        left = null;
        right = null;
    }
}
```

Here are three ways to perform DFS traversal:

### 1. Simple Recursive DFS (Pre-order traversal)
```csharp
public class Solution 
{
    public void DFS(TreeNode root) 
    {
        // Base case: if node is null, return
        if (root == null) return;
        
        // Process current node
        Console.Write(root.val + " "); // Visit the node
        
        // Recursively traverse left subtree
        DFS(root.left);
        
        // Recursively traverse right subtree
        DFS(root.right);
    }
}
```

### 2. DFS using Stack (Iterative approach)
```csharp
public class Solution 
{
    public void DFSIterative(TreeNode root) 
    {
        // If tree is empty
        if (root == null) return;
        
        // Create a stack for DFS
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root);
        
        while (stack.Count > 0) 
        {
            // Pop a node from stack
            TreeNode current = stack.Pop();
            
            // Process current node
            Console.Write(current.val + " ");
            
            // Push right child first (so left will be processed first)
            if (current.right != null)
                stack.Push(current.right);
                
            if (current.left != null)
                stack.Push(current.left);
        }
    }
}
```

### Let's see how it works with an example:
```csharp
/*
Consider this binary tree:
       1
      / \
     2   3
    / \
   4   5

*/

public class Program 
{
    public static void Main() 
    {
        // Create the tree
        TreeNode root = new TreeNode(1);
        root.left = new TreeNode(2);
        root.right = new TreeNode(3);
        root.left.left = new TreeNode(4);
        root.left.right = new TreeNode(5);
        
        Solution solution = new Solution();
        
        Console.WriteLine("Recursive DFS:");
        solution.DFS(root);  // Output: 1 2 4 5 3
        
        Console.WriteLine("\nIterative DFS:");
        solution.DFSIterative(root);  // Output: 1 2 4 5 3
    }
}
```

### Step-by-Step Explanation of How DFS Works:

1. **Recursive Approach Steps:**
```csharp
// Starting at root (1):
DFS(1)
    Print 1
    DFS(2)  // Go to left child
        Print 2
        DFS(4)  // Go to left child
            Print 4
            DFS(null)  // Return
            DFS(null)  // Return
        DFS(5)  // Go to right child
            Print 5
            DFS(null)  // Return
            DFS(null)  // Return
    DFS(3)  // Go to right child
        Print 3
        DFS(null)  // Return
        DFS(null)  // Return
```

2. **Iterative Approach Steps:**
```csharp
// Using stack:
Initial stack: [1]

Step 1: pop 1, print 1
        push 3, push 2
        Stack: [2, 3]

Step 2: pop 2, print 2
        push 5, push 4
        Stack: [4, 5, 3]

Step 3: pop 4, print 4
        Stack: [5, 3]

Step 4: pop 5, print 5
        Stack: [3]

Step 5: pop 3, print 3
        Stack: []
```

### Different Types of DFS Traversals:
```csharp
public class Solution 
{
    // Pre-order traversal (Root -> Left -> Right)
    public void PreOrder(TreeNode root) 
    {
        if (root == null) return;
        Console.Write(root.val + " ");  // Root
        PreOrder(root.left);            // Left
        PreOrder(root.right);           // Right
    }
    
    // In-order traversal (Left -> Root -> Right)
    public void InOrder(TreeNode root) 
    {
        if (root == null) return;
        InOrder(root.left);             // Left
        Console.Write(root.val + " ");  // Root
        InOrder(root.right);            // Right
    }
    
    // Post-order traversal (Left -> Right -> Root)
    public void PostOrder(TreeNode root) 
    {
        if (root == null) return;
        PostOrder(root.left);           // Left
        PostOrder(root.right);          // Right
        Console.Write(root.val + " ");  // Root
    }
}
```

### Common Use Cases for DFS:
1. Finding a path between two nodes
2. Detecting cycles in a graph
3. Topological sorting
4. Solving maze problems
5. Tree traversal problems

### Time and Space Complexity:
```csharp
/*
Time Complexity: O(N) where N is number of nodes
- We visit each node exactly once

Space Complexity: 
- Recursive: O(H) where H is height of tree (due to recursion stack)
- Iterative: O(N) in worst case (for skewed tree)
*/
```

This should give you a good understanding of how DFS works. The key is to understand that DFS explores as far as possible along each branch before backtracking.