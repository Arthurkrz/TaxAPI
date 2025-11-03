namespace RegistroNF.Core.Common
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string error)
        {
            Error = error;
        }

        public string Error { get; }
    }
}
