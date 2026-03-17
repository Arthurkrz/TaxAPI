namespace RegistroNF.API.Core.Common
{
    public class EmailMessage
    {
        public List<string> To { get; set; } = new();

        public string Subject { get; set; } = String.Empty;

        public string Content { get; set; } = String.Empty;

        public bool IsHtml { get; set; } = true;
    }
}
