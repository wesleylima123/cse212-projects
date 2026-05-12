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
        
        // PLAN:
        // 1. Create an array of doubles with size = 'length'
        // 2. For each position i in the array (from 0 to length-1):
        //    - The value at position i will be: number * (i + 1)
        // 3. Return the populated array
        
        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result; // replace this return statement with your own
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

        // PLAN:
        // Example: data = {1,2,3,4,5,6,7,8,9}, amount = 3
        // Expected result: {7,8,9,1,2,3,4,5,6}
        //
        // STEPS:
        // 1. Calculate the split point: splitIndex = data.Count - amount
        // 2. Get the last 'amount' elements: data.GetRange(splitIndex, amount)
        // 3. Remove the last 'amount' elements: data.RemoveRange(splitIndex, amount)
        // 4. Insert the elements at the beginning: data.InsertRange(0, lastElements)

        int splitIndex = data.Count - amount;
        List<int> lasts = data.GetRange(splitIndex, amount);
        data.RemoveRange(splitIndex, amount);
        data.InsertRange(0, lasts);
    }
}
