using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace OpenCBS.Manager.Messaging
{
    public class QueuedEmailManager : Manager
    {
        private CoreDomain.User user;

        public QueuedEmailManager(User user) : base(user)
        {
            this.user = user;
        }

        public List<QueuedEmail> SelectAll()
        {
            List<QueuedEmail> qeuedEmails = new List<QueuedEmail>();
            const string q =
                @"SELECT * FROM dbo.QueuedEmails where Deleted <> 1";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                if (r.Empty) return qeuedEmails;

                while (r.Read())
                {
                    var queuedEmail = new QueuedEmail
                    {
                        Id = r.GetInt("Id"),
                        Priority = r.GetInt("Priority"),
                        From = r.GetString("From"),
                        FromName = r.GetString("FromName"),
                        To = r.GetString("To"),
                        ToName = r.GetString("ToName"),
                        ReplyTo = r.GetString("ReplyTo"),
                        ReplyToName = r.GetString("ReplyToName"),
                        CC = r.GetString("CC"),
                        Bcc = r.GetString("Bcc"),
                        Body = r.GetString("Body"),
                        CreatedOnUtc = r.GetDateTime("CreatedOnUtc"),
                        SentOnUtc = r.GetNullDateTime("SentOnUtc"),
                        EmailAccountId = r.GetInt("EmailAccountId"),
                        SentTries = r.GetInt("SentTries"),
                        Subject = r.GetString("Subject"),
                        Deleted = r.GetBool("Deleted"),
                    };

                    if (queuedEmail.EmailAccountId > 0)
                    {
                        EmailAccountManager _emailAccountManager = new EmailAccountManager(user);
                        queuedEmail.EmailAccount = _emailAccountManager.SelectById(queuedEmail.EmailAccountId);

                    }
                    qeuedEmails.Add(queuedEmail);
                }
            }
            return qeuedEmails;
        }

        public void Delete(QueuedEmail queuedEmail)
        {
            const string q = @"UPDATE dbo.QueuedEmails
            SET Deleted = 1 WHERE Id = @Id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@Id", queuedEmail.Id);
                c.ExecuteNonQuery();
            }
        }

        public bool Update(QueuedEmail queuedEmail)
        {                                    
            bool updateOk = false;
            try
            {
                const string q = @"UPDATE dbo.QueuedEmails
                              SET [Priority] = @Priority, 
                                  [From] = @From, 
                                  [FromName] = @FromName,
                                  [To] = @To, 
                                  [ToName] = @ToName, 
                                  [ReplyTo] = @ReplyTo, 
                                    [ReplyToName] = @ReplyToName, 
                                    [CC] = @CC, 
                                    [Bcc] = @Bcc, 
                                    [Body] = @Body, 
                                    [CreatedOnUtc] = @CreatedOnUtc, 
                                    [SentOnUtc] = @SentOnUtc, 
                                    [SentTries] = @SentTries, 
                                    [EmailAccountId] = @EmailAccountId, 
                                    [Subject] = @Subject, 
                                  [Deleted] = @Deleted
                              WHERE Id = @Id";


                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@Id", queuedEmail.Id);
                    c.AddParam("@Priority", queuedEmail.Priority);
                    c.AddParam("@From", queuedEmail.From);
                    c.AddParam("@FromName", queuedEmail.FromName);
                    c.AddParam("@To", queuedEmail.To);
                    c.AddParam("@ToName", queuedEmail.ToName);
                    c.AddParam("@ReplyTo", queuedEmail.ReplyTo);
                    c.AddParam("@ReplyToName", queuedEmail.ReplyToName);
                    c.AddParam("@CC", queuedEmail.CC);
                    c.AddParam("@Bcc", queuedEmail.Bcc);
                    c.AddParam("@Body", queuedEmail.Body);
                    c.AddParam("@CreatedOnUtc", queuedEmail.CreatedOnUtc);
                    c.AddParam("@SentOnUtc", queuedEmail.SentOnUtc);
                    c.AddParam("@EmailAccountId", queuedEmail.EmailAccountId);
                    c.AddParam("@SentTries", queuedEmail.SentTries);
                    c.AddParam("@Subject", queuedEmail.Subject);                    
                    c.AddParam("@Deleted", false);
                    c.ExecuteNonQuery();
                    updateOk = true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return updateOk;
        }

        public int Add(QueuedEmail queuedEmail)
        {
            const string q = @"INSERT INTO dbo.QueuedEmails
                              ([Priority], [From], [FromName], [To], [ToName], [ReplyTo], [ReplyToName], [CC], [Bcc], [Body], CreatedOnUtc, SentOnUtc, EmailAccountId, SentTries, Subject, Deleted)
                              VALUES (@Priority, @From, @FromName, @To, @ToName, @ReplyTo, @ReplyToName, @CC, @Bcc, @Body, @CreatedOnUtc, @SentOnUtc, @EmailAccountId, @SentTries, @Subject, @Deleted)
                              SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@Priority", queuedEmail.Priority);
                c.AddParam("@From", queuedEmail.From);
                c.AddParam("@FromName", queuedEmail.FromName);
                c.AddParam("@To", queuedEmail.To);
                c.AddParam("@ToName", queuedEmail.ToName);
                c.AddParam("@ReplyTo", queuedEmail.ReplyTo);
                c.AddParam("@ReplyToName", queuedEmail.ReplyToName);
                c.AddParam("@CC", queuedEmail.CC);
                c.AddParam("@Bcc", queuedEmail.Bcc);
                c.AddParam("@Body", queuedEmail.Body);
                c.AddParam("@CreatedOnUtc", queuedEmail.CreatedOnUtc);
                c.AddParam("@SentOnUtc", queuedEmail.SentOnUtc);
                c.AddParam("@EmailAccountId", queuedEmail.EmailAccountId);
                c.AddParam("@SentTries", queuedEmail.SentTries);
                c.AddParam("@Subject", queuedEmail.Subject);
                c.AddParam("@Deleted", false);
                return int.Parse(c.ExecuteScalar().ToString());
            }
        }
    }
}
