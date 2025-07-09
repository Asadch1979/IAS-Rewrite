using System.Linq;
using System.Threading.Tasks;

namespace AIS
    {
    public static class EmailNotification
        {
        public static async Task SendJoiningNotificationAsync(string toEmail, string ccEmail, string auditEntity, string teamLead, string teamMembers)
            {
            string emailSubject = $"IAS~Notification: Audit Team has Joined for {auditEntity}";
            string emailBody = $@"Dear Sir,

This is to notify you that the audit team has officially joined for the audit of {auditEntity}.

Please coordinate with the Audit Team for any necessary assistance during the audit process.

Audit Team Details
{teamLead}  {teamMembers}

You must provide all information desired by the Audit Team during the course of Audit.

Best Regards,
Internal Audit System (IAS)
";
            EmailConfiguration econ = new EmailConfiguration();
            await econ.ConfigEmailAsync(toEmail, ccEmail, emailSubject, emailBody);
            }

        public static void SendPasswordResetSuccess(string userFullName, string ppNumber, string pass, string userEmail, string userCCEmail)
            {
            string subject = "IAS~ Password Reset Successful";
            string body = $@"
Dear {userFullName},

Your password has been successfully reset. Please find your new login details below:

Username: {ppNumber}
Password: {pass}

For security reasons, we recommend that you change this password immediately after logging in.

If you did not request this password reset, please contact our support team immediately.

Best Regards,

Internal Audit System (IAS)
";
            EmailConfiguration email = new EmailConfiguration();
            email.ConfigEmail(userEmail, userCCEmail, subject, body);
            }

        public static void NotifyAuditSampleIssue(string engagementId, string toEmail, string ccEmail)
            {
            string subject = $"IAS~Notification: Issue in Audit Sample for Engagement ID: {engagementId}";
            string body = $@"
                    Dear Sir,

                    This is to notify you that the issue has been identified in Audit Sample Creation Please check and fix.

                    Best Regards,
                    Internal Audit System (IAS)
                    ";
            EmailConfiguration econ = new EmailConfiguration();
            econ.ConfigEmail(toEmail, ccEmail, subject, body);
            }

        public static void NotifyAuditExceptionIssue(string engagementId, string toEmail, string ccEmail)
            {
            string subject = $"IAS~Notification: Issue in Audit Exception for Engagement ID: {engagementId}";
            string body = $@"
 Dear Sir,

This is to notify you that the issue has been identified while creating exception reports Please check and fix.

Best Regards,
Internal Audit System (IAS)
                    ";
            EmailConfiguration econ = new EmailConfiguration();
            econ.ConfigEmail(toEmail, ccEmail, subject, body);
            }

        public static void NotifyAuditCriteriaSubmission(string toEmail, string ccEmail, string subject, string body)
            {
            EmailConfiguration email = new EmailConfiguration();
            email.ConfigEmail(toEmail, ccEmail, subject, body);
            }

        public static void NotifyParaStatus(string paraNo, string paraStatus, string paraGist, string toEmail, string ccEmail, string cc2Email)
            {
            string subject = $"IAS~Notification: Para No: {paraNo} is marked {paraStatus}";
            string body = $@"
Dear Sir,

This is to notify you that Para No. {paraNo} has been marked as {paraStatus}.

Gist of Para:
{paraGist}

Best Regards,
Internal Audit System (IAS)
            ";
            string ccCombined = string.Join(";", new[] { ccEmail, cc2Email }.Where(e => !string.IsNullOrWhiteSpace(e)));
            EmailConfiguration econ = new EmailConfiguration();
            econ.ConfigEmail(toEmail, ccCombined, subject, body);
            }
        }
    }