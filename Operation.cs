namespace Sectra{
    /// <summary>
    /// Class that holds data to perform an operation
    /// _operationType would be add, subtract, multiply
    /// _amount would be the integer value or the register
    /// to use for the operation
    /// </summary>
    public class Operation {
        string _operationType;
        string _amount;

        public Operation(string operationType, string amount){
            _amount = amount;
            _operationType = operationType;
        }

        public int GetAmountIntegerValue(){
            int.TryParse(_amount, out int value);
            return value;
        }

        public bool IsAmountNumber => int.TryParse(_amount, out _);

        public string OperationType => _operationType;

        public string Amount => _amount;
    }
}