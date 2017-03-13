using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace OpenCBS.Manager.Messaging
{
    public class MessageTemplateManager : Manager
    {
        private User user;
        
        public MessageTemplateManager(User user)
            : base(user)
        {
            this.user = user;
        }

        public void Delete(MessageTemplate messageTemplate)
        {
            const string q = @"UPDATE dbo.MessageTemplates
            SET Deleted = 1 WHERE Id = @Id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@Id", messageTemplate.Id);
                c.ExecuteNonQuery();
            }
        }

        public int Add(MessageTemplate messageTemplate)
        {
            const string q = @"INSERT INTO dbo.MessageTemplates
                              (Name, BccEmailAddresses, Subject, Body, EmailBody, IsActive, SendEmail, SendSMS, EmailAccountId, IsDefault, Deleted)
                              VALUES (@Name, @BccEmailAddresses, @Subject, @Body, @EmailBody, @IsActive, @SendEmail, @SendSMS, @EmailAccountId, @IsDefault, @Deleted)
                              SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@Name", messageTemplate.Name);
                c.AddParam("@BccEmailAddresses", messageTemplate.BccEmailAddresses);
                c.AddParam("@Subject", messageTemplate.Subject);
                c.AddParam("@Body", messageTemplate.Body);
                c.AddParam("@EmailBody", messageTemplate.EmailBody);
                c.AddParam("@SendEmail", messageTemplate.SendEmail);
                c.AddParam("@SendSMS", messageTemplate.SendSMS);
                c.AddParam("@IsActive", messageTemplate.IsActive);
                c.AddParam("@EmailAccountId", messageTemplate.EmailAccountId);
                c.AddParam("@IsDefault", messageTemplate.IsDefault);
                c.AddParam("@Deleted", false);
                return int.Parse(c.ExecuteScalar().ToString());
            }
        }

        public bool Update(MessageTemplate messageTemplate)
        {
            bool updateOk = false;
            try
            {
                const string q = @"UPDATE dbo.MessageTemplates
                              SET [Name] = @Name, 
                                  [BccEmailAddresses] = @BccEmailAddresses, 
                                  [Subject] = @Subject,
                                  [Body] = @Body, 
                                  [EmailBody] = @EmailBody, 
                                  [SendEmail] = @SendEmail, 
                                  [SendSMS] = @SendSMS, 
                                  [IsActive] = @IsActive, [EmailAccountId] = @EmailAccountId, 
                                  [IsDefault] = @IsDefault, [Deleted] = @Deleted
                              WHERE Id = @Id";


                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@Id", messageTemplate.Id);
                    c.AddParam("@Name", messageTemplate.Name);
                    c.AddParam("@BccEmailAddresses", messageTemplate.BccEmailAddresses);
                    c.AddParam("@Subject", messageTemplate.Subject);
                    c.AddParam("@Body", messageTemplate.Body);
                    c.AddParam("@EmailBody", messageTemplate.EmailBody);
                    c.AddParam("@SendEmail", messageTemplate.SendEmail);
                    c.AddParam("@SendSMS", messageTemplate.SendSMS);
                    c.AddParam("@IsActive", messageTemplate.IsActive);
                    c.AddParam("@EmailAccountId", messageTemplate.EmailAccountId);
                    c.AddParam("@IsDefault", messageTemplate.IsDefault.HasValue ? messageTemplate.IsDefault.Value : false);
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

        public MessageTemplate SelectByName(string name)
        {            
            string q =
                @"SELECT * FROM dbo.MessageTemplates WHERE Name LIKE '%{0}%' AND Deleted <> 1";
            q = string.Format(q, name);

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                if (r.Empty) return null;
                if (!r.Read()) return null;
                var messageTemplate = new MessageTemplate
                {

                    Id = r.GetInt("Id"),
                    Name = r.GetString("Name"),
                    BccEmailAddresses = r.GetString("BccEmailAddresses"),
                    Subject = r.GetString("Subject"),
                    Body = r.GetString("Body"),
                    EmailBody = r.GetString("EmailBody"),
                    SendEmail = r.GetNullBool("SendEmail"),
                    SendSMS = r.GetNullBool("SendSMS"),
                    IsActive = r.GetBool("IsActive"),
                    EmailAccountId = r.GetInt("EmailAccountId"),
                    IsDefault = r.GetBool("IsDefault"),
                    Deleted = r.GetBool("Deleted"),
                };

                if (messageTemplate.EmailAccountId > 0)
                {
                    EmailAccountManager _emailAccountManager = new EmailAccountManager(user);
                    messageTemplate.EmailAccount = _emailAccountManager.SelectById(messageTemplate.EmailAccountId);

                }

                return messageTemplate;
            }            
        }

        public List<MessageTemplate> SelectAll()
        {
            List<MessageTemplate> messageTemplates = new List<MessageTemplate>();
            const string q =
                @"SELECT * FROM dbo.MessageTemplates where Deleted <> 1";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                if (r.Empty) return messageTemplates;

                while (r.Read())
                {
                    var messageTemplate = new MessageTemplate
                    {
                        Id = r.GetInt("Id"),
                        Name = r.GetString("Name"),
                        BccEmailAddresses = r.GetString("BccEmailAddresses"),
                        Subject = r.GetString("Subject"),
                        Body = r.GetString("Body"),
                        EmailBody = r.GetString("EmailBody"),
                        SendEmail = r.GetNullBool("SendEmail"),
                        SendSMS = r.GetNullBool("SendSMS"),
                        IsActive = r.GetBool("IsActive"),
                        EmailAccountId = r.GetInt("EmailAccountId"),
                        IsDefault = r.GetBool("IsDefault"),
                        Deleted = r.GetBool("Deleted"),
                    };

                    if (messageTemplate.EmailAccountId > 0)
                    {
                        EmailAccountManager _emailAccountManager = new EmailAccountManager(user);
                        messageTemplate.EmailAccount = _emailAccountManager.SelectById(messageTemplate.EmailAccountId);

                    }
                    messageTemplates.Add(messageTemplate);
                }
            }
            return messageTemplates;
        }
    }
}
