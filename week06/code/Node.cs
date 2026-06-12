public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1

        // PROBLEM 1 FIX: If value already exists, do nothing
        if (value == Data)
            return;

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2

        // Base case: found the value
        if (value == Data)
            return true;

        // Search in the left subtree if value is smaller
        if (value < Data)
        {
            if (Left is null)
                return false;
            return Left.Contains(value);
        }

        // Search in the right subtree if value is larger
        if (Right is null)
            return false;
        return Right.Contains(value);
    }

    public int GetHeight()
    {
        // TODO Start Problem 4

        // Calculate height of left and right subtrees
        int leftHeight = Left?.GetHeight() ?? 0;   // If Left is null, height is 0
        int rightHeight = Right?.GetHeight() ?? 0; // If Right is null, height is 0

        // Height = 1 + max(leftHeight, rightHeight)
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}