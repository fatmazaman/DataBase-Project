using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Net;
using System.Collections.ObjectModel;
using System.Threading;
using System.Text.RegularExpressions;

namespace iSharpToolkit.Net.Mail
{
    public class iSharpMail
    {
        private static Mutex IsLocked = new Mutex();

        private static Regex emailSeparator = new Regex(@"\s*[;,]\s*", RegexOptions.Compiled);

        /// <summary>
        /// Sends email using the specified parameters.
        /// </summary>
        /// <param name="sToName">To Name</param>
        /// <param name="sToEmail">To Email</param>
        /// <param name="sCCName">CC Name</param>
        /// <param name="sCCEmail">CC Email</param>
        /// <param name="sBCCName">BCC Name</param>
        /// <param name="sBCCEMail">BCC EMail</param>
        /// <param name="sFromName">From Name</param>
        /// <param name="sFromEMail">From EMail</param>
        /// <param name="sReplyToEMail">Reply To EMail</param>
        /// <param name="sSubject">Subject</param>
        /// <param name="sBody">Body</param>
        /// <param name="sAttachments">Attachments</param>
        /// <param name="Priority">MailPriority</param>
        /// <param name="bHTML">HTML</param>
        /// <returns>true if email is send, false otherwise</returns>
        public static bool Send(string sToName, string sToEmail, string sCCName, string sCCEmail,
                                string sBCCName, string sBCCEMail, string sFromName, string sFromEMail,
                                string sReplyToEMail, string sSubject, string sBody, IEnumerable<string> sAttachments,
                                MailPriority Priority, bool bHTML)
        {
            IsLocked.WaitOne();
            MailMessage message = new MailMessage();

            // Create the file attachment for this e-mail message.
            if (sAttachments != null)
            {
                foreach (string sAttachment in sAttachments)
                {
                    Attachment data = new Attachment(sAttachment, MediaTypeNames.Application.Octet);
                    // Add time stamp information for the file.
                    ContentDisposition disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(sAttachment);
                    disposition.ModificationDate = File.GetLastWriteTime(sAttachment);
                    disposition.ReadDate = File.GetLastAccessTime(sAttachment);
                    message.Attachments.Add(data);
                }
            }

            message.Priority = Priority;
            message.From = new MailAddress(sFromEMail, sFromName);
            message.Subject = sSubject;
            message.Body = sBody;

            foreach (MailAddress item in SplitEmails(sToEmail, sToName))
                message.To.Add(item);

            foreach (MailAddress item in SplitEmails(sBCCEMail, sBCCName))
                message.Bcc.Add(item);

            foreach (MailAddress item in SplitEmails(sCCEmail, sCCName))
                message.CC.Add(item);

            if (!String.IsNullOrEmpty(sReplyToEMail))
            {
                MailAddress ReplyAdress = new MailAddress(sReplyToEMail, sFromName);
                message.ReplyTo = ReplyAdress;
            }
            else
            {
                MailAddress ReplyAddress = new MailAddress(sFromEMail, sFromName);
                message.ReplyTo = ReplyAddress;
            }

            message.IsBodyHtml = bHTML;

            SmtpClient client = new SmtpClient();
            try
            {
                client.Send(message);
                IsLocked.ReleaseMutex();
                return true;
            }
            catch (Exception ex)
            {
                IsLocked.ReleaseMutex();
                return false;
            }

        }
        /// <summary>
        /// Sends email using the specified parameters.
        /// </summary>
        /// <param name="sToName">To Name</param>
        /// <param name="sToEmail">To Email</param>
        /// <param name="sCCName">CC Name</param>
        /// <param name="sCCEmail">CC Email</param>
        /// <param name="sBCCName">BCC Name</param>
        /// <param name="sBCCEMail">BCC EMail</param>
        /// <param name="sFromName">From Name</param>
        /// <param name="sFromEMail">From EMail</param>
        /// <param name="sReplyToEMail">Reply To EMail</param>
        /// <param name="sSubject">Subject</param>
        /// <param name="sBody">Body</param>
        /// <param name="sAttachments">Attachments</param>
        /// <param name="bHTML">HTML</param>
        /// <returns>true if email is send, false otherwise</returns>
        public static bool Send(string sToName, string sToEmail, string sCCName, string sCCEmail, string sBCCName, string sBCCEMail, string sFromName, string sFromEMail, string sReplyToEMail, string sSubject, string sBody, IEnumerable<string> sAttachments, bool bHTML)
        {
            return Send(sToName, sToEmail, sCCName, sCCEmail, sBCCName, sBCCEMail, sFromName, sFromEMail, sReplyToEMail, sSubject, sBody, sAttachments, MailPriority.Normal, bHTML);
        }
        /// <summary>
        /// Sends email using the specified parameters.
        /// </summary>
        /// <param name="sToEmail">To Email</param>
        /// <param name="sFromEMail">From EMail</param>
        /// <param name="sSubject">Subject</param>
        /// <param name="sBody">Body</param>
        /// <param name="sAttachments">Attachments</param>
        /// <param name="bHTML">HTML</param>
        /// <returns>true if email is send, false otherwise</returns>
        public static bool Send(string sToEmail, string sFromEMail, string sSubject, string sBody, string[] sAttachments, bool bHTML)
        {
            return Send(sToEmail, sToEmail, string.Empty, string.Empty, string.Empty, string.Empty, sFromEMail, sFromEMail, string.Empty, sSubject, sBody, sAttachments, MailPriority.Normal, bHTML);
        }
        /// <summary>
        /// Sends email using the specified parameters.
        /// </summary>
        /// <param name="sToEmail">To Email</param>
        /// <param name="sFromEMail">From EMail</param>
        /// <param name="sSubject">Subject</param>
        /// <param name="sBody">Body</param>
        /// <param name="sAttachments">Attachments</param>
        /// <returns>true if email is send, false otherwise</returns>
        public static bool Send(string sToEmail, string sFromEMail, string sSubject, string sBody, string[] sAttachments)
        {
            return Send(sToEmail, sToEmail, string.Empty, string.Empty, string.Empty, string.Empty, sFromEMail, sFromEMail, string.Empty, sSubject, sBody, sAttachments, MailPriority.Normal, false);
        }
        /// <summary>
        /// Sends email using the specified parameters.
        /// </summary>
        /// <param name="sToEmail">To Email</param>
        /// <param name="sFromEMail">From EMail</param>
        /// <param name="sSubject">Subject</param>
        /// <param name="sBody">Body</param>
        /// <param name="bHTML">HTML</param>
        /// <returns>true if email is send, false otherwise</returns>
        public static bool Send(string sToEmail, string sFromEMail, string sSubject, string sBody, bool bHTML)
        {
            return Send(sToEmail, sToEmail, string.Empty, string.Empty, string.Empty, string.Empty, sFromEMail, sFromEMail, string.Empty, sSubject, sBody, null, MailPriority.Normal, bHTML);
        }
        /// <summary>
        /// Sends email using the specified parameters.
        /// </summary>
        /// <param name="sToEmail">To Email</param>
        /// <param name="sFromEMail">From EMail</param>
        /// <param name="sSubject">Subject</param>
        /// <param name="sBody">Body</param>
        /// <returns>true if email is send, false otherwise</returns>
        public static bool Send(string sToEmail, string sFromEMail, string sSubject, string sBody)
        {
            return Send(sToEmail, sToEmail, string.Empty, string.Empty, string.Empty, string.Empty, sFromEMail, sFromEMail, string.Empty, sSubject, sBody, null, MailPriority.Normal, false);
        }
        /// <summary>
        /// Sends email using the specified parameters.
        /// </summary>
        /// <param name="sToEmail">To Email</param>
        /// <param name="sFromEMail">From EMail</param>
        /// <param name="sBCCEMail">BCCEMail</param>
        /// <param name="sSubject">Subject</param>
        /// <param name="sBody">Body</param>
        /// <param name="sAttachments">Attachments</param>
        /// <param name="bHTML">HTML</param>
        /// <returns>true if email is send, false otherwise</returns>
        public static bool SendWithBCC(string sToEmail, string sFromEMail, string sBCCEMail, string sSubject, string sBody, string[] sAttachments, bool bHTML)
        {
            return Send(sToEmail, sToEmail, string.Empty, string.Empty, sBCCEMail, sBCCEMail, sFromEMail, sFromEMail, string.Empty, sSubject, sBody, sAttachments, MailPriority.Normal, bHTML);
        }
        /// <summary>
        /// Sends email using the specified parameters.
        /// </summary>
        /// <param name="sToEmail">To Email</param>
        /// <param name="sFromEMail">From EMail</param>
        /// <param name="sBCCEMail">BCCEMail</param>
        /// <param name="sSubject">Subject</param>
        /// <param name="sBody">Body</param>
        /// <param name="bHTML">HTML</param>
        /// <returns>true if email is send, false otherwise</returns>
        public static bool SendWithBCC(string sToEmail, string sFromEMail, string sBCCEMail, string sSubject, string sBody, bool bHTML)
        {
            return Send(sToEmail, sToEmail, string.Empty, string.Empty, sBCCEMail, sBCCEMail, sFromEMail, sFromEMail, string.Empty, sSubject, sBody, null, MailPriority.Normal, bHTML);
        }
        /// <summary>
        /// Sends email using the specified parameters.
        /// </summary>
        /// <param name="sToEmail">To Email</param>
        /// <param name="sFromEMail">From EMail</param>
        /// <param name="sCCEMail">CC Email</param>
        /// <param name="sReplyToEMail">Reply To Email</param>
        /// <param name="sSubject">Subject</param>
        /// <param name="sBody">Body</param>
        /// <param name="sAttachments">Attachments</param>
        /// <param name="bHTML">HTML</param>
        /// <returns>true if email is send, false otherwise</returns>
        public static bool SendWithCC(string sToEmail, string sFromEMail, string sCCEMail, string sReplyToEMail, string sSubject, string sBody, string[] sAttachments, bool bHTML)
        {
            return Send(sToEmail, sToEmail, sCCEMail, sCCEMail, string.Empty, string.Empty, sFromEMail, sFromEMail, string.Empty, sSubject, sBody, sAttachments, MailPriority.Normal, bHTML);
        }
        /// <summary>
        /// Sends email using the specified parameters.
        /// </summary>
        /// <param name="sToEmail">To Email</param>
        /// <param name="sFromEMail">From EMail</param>
        /// <param name="sCCEMail">CC EMail</param>
        /// <param name="sSubject">Subject</param>
        /// <param name="sBody">Body</param>
        /// <param name="bHTML">HTML</param>
        /// <returns>true if email is send, false otherwise</returns>
        public static bool SendWithCC(string sToEmail, string sFromEMail, string sCCEMail, string sSubject, string sBody, bool bHTML)
        {
            return Send(sToEmail, sToEmail, sCCEMail, sCCEMail, string.Empty, string.Empty, sFromEMail, sFromEMail, string.Empty, sSubject, sBody, null, MailPriority.Normal, bHTML);
        }

        /// <summary>
        /// creates email list collection based on the given email addesses and names.
        /// <remarks>uses comma and semicolon as the delimiter to create the collection.</remarks>
        /// </summary>
        /// <param name="emails">emails list ',' or ';' seperated</param>
        /// <param name="names">name list ',' or ';' seperated</param>
        /// <returns>Email collection</returns>
        private static Collection<MailAddress> SplitEmails(string emails, string names)
        {
            Collection<MailAddress> collection = new Collection<MailAddress>();
            if (String.IsNullOrEmpty(emails))
                return collection;

            string[] NameList = null;
            if (!String.IsNullOrEmpty(names))
                NameList = emailSeparator.Split(names);

            string[] EmailList = emailSeparator.Split(emails);
            for (int i = 0; i < EmailList.Length; i++)
            {
                if (String.IsNullOrEmpty(EmailList[i]))
                    continue;

                if (NameList != null && NameList.Length > i)
                    collection.Add(new MailAddress(EmailList[i].Trim(), NameList[i].Trim()));
                else
                    collection.Add(new MailAddress(EmailList[i].Trim()));
            }
            return collection;
        }
    }
}
