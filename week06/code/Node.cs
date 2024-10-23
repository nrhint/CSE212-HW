public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value) {
        if (value == Data) {
            return;
        } else if (value < Data) {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        } else {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value) {
        if (value == Data) {
            return true;
        } else if (value < Data) {
            if (Left is null) {
                return false;
            } else {
                return Left.Contains(value);
            }
        } else { // Right must be higher.
            if (Right is null) {
                return false;
            } else {
                return Right.Contains(value);
            }
        }
    }

    // Recursive function: Base case will be if both the children
    // nodes are null then it will return 1. Otherwise return the
    // higher value between the left and right nodes in the tree
    public int GetHeight()
    {
        int left = 0;
        int right = 0;
        // Base return case. If both children are null then this is a end of branch.
        if (Left is null && Right is null) {
            return 1;
        }
        // Get the left value if it is not null
        if (!(Left is null)) {
            left = Left.GetHeight()+1;
        }
        // Get the right value if it is not null
        if (!(Right is null)) {
            right = Right.GetHeight()+1;
        }
        // Return whichever value is higher. If they are equal then the right will be returned.
        if (left > right) {
            return left;
        } else {
            return right;
        }
    }
}
