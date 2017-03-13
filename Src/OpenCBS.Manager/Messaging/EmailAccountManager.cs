using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Messaging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace OpenCBS.Manager.Messaging
{
    public class EmailAccountManager : Manager
    {
        private CoreDomain.User user;

        public EmailAccountManager(User user)
            : base(user)
        {
            this.user = user;
        }

        public EmailAccount SelectById(int Id)
        {
            const string q =
                @"SELECT * FROM dbo.EmailAccounts where Id = @Id AND Deleted <> 1";


            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@Id", Id);

                using (OpenCbsReader r = c.ExecuteReader())
                {
                    if (r.Empty) return null;
                    if (!r.Read()) return null;

                    return new EmailAccount
                        {
                            Id = r.GetInt("Id"),
                            Email = r.GetString("Email"),
                            DisplayName = r.GetString("DisplayName"),
                            Host = r.GetString("Host"),
                            Port = r.GetInt("Port"),
                            Username = r.GetString("Username"),
                            Password = r.GetString("Password"),
                            EnableSsl = r.GetBool("EnableSsl"),
                            UseDefaultCredentials = r.GetBool("UseDefaultCredentials"),
                            IsDefaultEmailAccount = r.GetBool("IsDefaultEmailAccount"),
                            Deleted = r.GetBool("Deleted"),
                        };

                }
            }
        }

        public List<EmailAccount> SelectAll()
        {
            List<EmailAccount> emailAccounts = new List<EmailAccount>();
            const string q =
                @"SELECT * FROM dbo.EmailAccounts where Deleted <> 1";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            using (OpenCbsReader r = c.ExecuteReader())
            {
                if (r.Empty) return emailAccounts;

                while (r.Read())
                {
                    var b = new EmailAccount
                    {
                        Id = r.GetInt("Id"),
                        Email = r.GetString("Email"),
                        DisplayName = r.GetString("DisplayName"),
                        Host = r.GetString("Host"),
                        Port = r.GetInt("Port"),
                        Username = r.GetString("Username"),
                        Password = r.GetString("Password"),
                        EnableSsl = r.GetBool("EnableSsl"),
                        UseDefaultCredentials = r.GetBool("UseDefaultCredentials"),
                        IsDefaultEmailAccount = r.GetBool("IsDefaultEmailAccount"),
                        Deleted = r.GetBool("Deleted"),
                    };
                    emailAccounts.Add(b);
                }
            }
            return emailAccounts;
        }

        public void Delete(EmailAccount emailAccount)
        {
            const string q = @"UPDATE dbo.EmailAccounts
            SET Deleted = 1 WHERE Id = @Id";
            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@Id", emailAccount.Id);
                c.ExecuteNonQuery();
            }
        }

        public int Add(EmailAccount emailAccount)
        {
            const string q = @"INSERT INTO dbo.EmailAccounts
                              (Email, DisplayName, Host, Port, Username, Password, EnableSsl, UseDefaultCredentials, IsDefaultEmailAccount, Deleted)
                              VALUES (@email, @displayName, @host, @port, @userName, @password, @enableSSL, @useDefaultCred, @isDefault, @deleted)
                              SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = GetConnection())
            using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
            {
                c.AddParam("@email", emailAccount.Email);
                c.AddParam("@displayName", emailAccount.DisplayName);
                c.AddParam("@host", emailAccount.Host);
                c.AddParam("@port", emailAccount.Port);
                c.AddParam("@userName", emailAccount.Username);
                c.AddParam("@password", emailAccount.Password);
                c.AddParam("@enableSSL", emailAccount.EnableSsl);
                c.AddParam("@useDefaultCred", emailAccount.UseDefaultCredentials);
                c.AddParam("@isDefault", emailAccount.IsDefaultEmailAccount);
                c.AddParam("@deleted", false);
                return int.Parse(c.ExecuteScalar().ToString());
            }
        }

        public bool Update(EmailAccount emailAccount)
        {
            bool updateOk = false;
            try
            {
                const string q = @"UPDATE dbo.EmailAccounts
                              SET [Email] = @email, 
                                  [DisplayName] = @displayName, 
                                  [Host] = @host,
                                  [Port] = @port, 
                                  [Username] = @userName, [Password] = @password, [EnableSsl] = @enableSSL, 
                                  [UseDefaultCredentials] = @useDefaultCred, [IsDefaultEmailAccount] = @isDefault, [Deleted] = @deleted
                              WHERE Id = @Id";


                using (SqlConnection conn = GetConnection())
                using (OpenCbsCommand c = new OpenCbsCommand(q, conn))
                {
                    c.AddParam("@Id", emailAccount.Id);
                    c.AddParam("@email", emailAccount.Email);
                    c.AddParam("@displayName", emailAccount.DisplayName);
                    c.AddParam("@host", emailAccount.Host);
                    c.AddParam("@port", emailAccount.Port);
                    c.AddParam("@userName", emailAccount.Username);
                    c.AddParam("@password", emailAccount.Password);
                    c.AddParam("@enableSSL", emailAccount.EnableSsl);
                    c.AddParam("@useDefaultCred", emailAccount.UseDefaultCredentials);
                    c.AddParam("@isDefault", emailAccount.IsDefaultEmailAccount);
                    c.AddParam("@deleted", false);
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
    }
}
