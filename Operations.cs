namespace Sectra
{
    /// <summary>
    /// Holds functions and dictionary to perform operations
    /// </summary>
    public class Constants
    {
        private static int Addition(int a, int b) => a + b;
        private static int Subtraction(int a, int b) => a - b;
        private static int Multiply(int a, int b) => a * b;
        public static Dictionary<string, Func<int,int, int>> operationToFunction =
            new Dictionary<string, Func<int,int, int>> {
                {"add", Addition},
                {"+", Addition},
                {"subtract", Subtraction},
                {"-", Subtraction},
                {"multiply", Multiply},
                {"*", Multiply}
            };
    }
}