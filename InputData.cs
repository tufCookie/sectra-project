using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace Sectra{
    /// <summary>
    /// This class handles the input data passed from the file. From the input
    /// it will update the print statements or the register values.
    /// </summary>
    public class RegisterData{

        /// <summary>
        /// Dictionary containing a list of operations
        /// The operation list should go from first operation to last added operation
        /// </summary>
        private Dictionary<string, List<Operation>> _registerDictionary = new();

        public RegisterData() {}

        /// <summary>
        /// Validates the operation
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsOperationValid(string[] strings, string input){
            string register = strings[0];
            string operation = strings[1];

            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            if(!rg.IsMatch(register)){
                Console.WriteLine($"Error: {register} does not have alphanumeric characters in: {input}.");
                return false;
            }
            if(!Constants.operationToFunction.ContainsKey(operation)){
                Console.WriteLine($"Error: incorrect value {operation} for: {input}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates the print information
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsPrintValid(string[] strings, string input){
            string print = strings[0];
            string register = strings[1];
            if (!print.Trim().Equals("print"))
            {
                ConsoleWriteUnknowInput(input);
                return false;
            }
            if(!_registerDictionary.ContainsKey(register)){
                ConsoleWriteRegisterNotFound(register);
                return false;
            }
            return true;
        }

        private void ConsoleWriteRegisterNotFound(string register){
            Console.WriteLine($"Error: Register {register} does not exist.");
        }

        private void ConsoleWriteUnknowInput(string input) {
            Console.WriteLine($"Error: Unknown input: \n\n\t {input} \n\n " +
                     "Valid inputs are:" 
                    + "\n\t1. <register> <operation> <value>"
                    + "\n\t2. print <register>"
                    + "\n\t3. quit");
        }

        /// <summary>
        /// Stores the operation data to the dictionary
        /// </summary>
        /// <param name="strings"></param>
        private void HandleOperation(string[] strings){
            Operation operation = new Operation(strings[1], strings[2]);
            if(!_registerDictionary.ContainsKey(strings[0])){
                List<Operation> list = new()
                {
                    operation
                };
                _registerDictionary.Add(strings[0], list);
            }
            else{
                _registerDictionary[strings[0]].Add(operation);
            }
        }

        /// <summary>
        /// Prints the register value
        /// </summary>
        /// <param name="strings"></param>
        private void HandlePrint(string[] strings){
            int registerValue = GetRegisterValue(strings[1]);
            if(registerValue != int.MaxValue){
                Console.WriteLine(registerValue);
            }
            else{
                Console.WriteLine($"Register value {strings[1]} can not be computed.");
            }
        }

        /// <summary>
        /// Recursive method to get the specified register
        /// If a register is not found it return the max integer value
        /// </summary>
        /// <param name="key">The register</param>
        /// <returns></returns>
        private int GetRegisterValue(string key){
            if(!_registerDictionary.ContainsKey(key)){
                return int.MaxValue;
            }

            int value = 0;
            foreach(Operation operation in _registerDictionary[key]){
                Func<int, int, int> func = Constants.operationToFunction[operation.OperationType];
                int amount = 0;
                amount = operation.IsAmountNumber ? 
                    operation.GetAmountIntegerValue() :             
                    GetRegisterValue(operation.Amount);
                if (amount == int.MaxValue){
                    return int.MaxValue;
                }

                value = func(value, amount);
            }
            return value;
        }

        /// <summary>
        /// Reads input and determines to print or to store the operation
        /// in the dictionary
        /// </summary>
        /// <param name="input"></param>
        public void ReadInput(string input){
            string[] strings = input.Split(' ');
            if(strings.Count() == 3 && IsOperationValid(strings, input)){
                HandleOperation(strings);
            }
            else if(strings.Count() == 2 && IsPrintValid(strings, input)){
                HandlePrint(strings);
            }
        }
    }
}