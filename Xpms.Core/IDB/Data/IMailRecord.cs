namespace Xpms.Core.IDB.Data
{
    public interface IMailRecord
    {
        string From { get; set; }
        string To { get; set; }
        string Cc { get; set; }
        string Bcc { get; set; }
        string Subject { get; set; }
        string HtmlBody { get; set; }
        string TextBody { get; set; }
    }
}