public static class Arrays
{
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.For each 
    /// of the example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer that is greater than 0.
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Step 1: Create a new array that has the required length.
        // Step 2: Use loop to go through each position in the array.
        // Step 3: Multiply the number by the current position plus 1 as addition.
        // Step 4: Store the result in the array.
        // Step 5: Return the completed array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// Rotate the data to the right by the given amount. For example, when the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.

    public static void RotateListRight(List<int> data, int amount)
    {
        // Step 1: Compute the split print as data.count-amount.
        // Step 2: Then slice the last 'elements' into a new list (endPoint).
        // Step 3: Also, get the remaining values from the beginning of the list.
        // Step 4: Clear the original list.
        // Step 5: Finally, add the rotated values back into the original list.

        int splitPoint = data.Count - amount;

        List<int> endPart = data.GetRange(splitPoint, amount);
        List<int> beginningPart = data.GetRange(0, splitPoint);

        data.Clear();

        data.AddRange(endPart);
        data.AddRange(beginningPart);
    }
}