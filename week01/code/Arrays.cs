using System.Diagnostics;


public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.


        // 0: Create a results list
        double[] results = new double[length];
        // 1: Loop for the length times
        for (int ii = 0; ii < length; ii++) {
            // 2: Append to the result of the number times the iterator for the list
            results[ii] = number*(ii+1);
        }
        // 3: Return the list
        return results;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // 0: Create result list
        List<int> left = new List<int> ();
        List<int> right = new List<int> ();
        
        // 1: Get the first range and add it to the result list (The right side)
        left = data.GetRange(data.Count - amount, amount);
        // 2: Get the second range and add it to the result list (The rest)
        right = data.GetRange(0, data.Count - amount);
        data.RemoveRange(0, data.Count);
        data.InsertRange(0, left);
        data.InsertRange(left.Count, right);
    }
}
