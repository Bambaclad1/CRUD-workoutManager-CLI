namespace ConsoleApp1.util
{
    internal class CheckIfValidIntFromString
    {
        public static int Check(string input, int Min, int Max)
        {
            if (!int.TryParse(input, out int result))
            {
                Console.WriteLine("\nInvalid answer. Please answer in a valid integer.");
                return 409;
            }

            if (result >= Min && result <= Max)
            {
                return result;
            }
            else
            {
                Console.Write($"\nAnswer must be between values {Min} and {Max}, " + result + " is invalid.");
                return 409;
            }
        }
    }
}