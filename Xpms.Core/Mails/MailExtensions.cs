using System;
using System.Net.Mail;

namespace Xpms.Core.Mails
{
    public static class MailExtensions
    {
        public static MailAddress ToMailAddress(this string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Invalid email string.");

            return new MailAddress(address);
        }

        public static void AddMailAddresses(this MailAddressCollection collection, string addresses)
        {
            if (string.IsNullOrEmpty(addresses))
                throw new ArgumentException("Invalid emails string.");

            var emails = addresses.Split(new[] { ',', ';' }
                , StringSplitOptions.RemoveEmptyEntries
                );

            foreach (var email in emails)
            {
                collection.Add(email.ToMailAddress());
            }
        }

        public static MailMessage Message(this MailBase mail, string from = null, string to = null, string subject = null, string body = null)
        {
            var msg = new MailMessage(
                from ?? mail.From,
                to ?? mail.To,
                subject ?? mail.Subject,
                body ?? mail.HtmlBody ?? mail.TextBody
            );

            return msg;
        }
    }
}