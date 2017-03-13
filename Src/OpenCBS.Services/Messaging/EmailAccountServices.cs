using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Messaging;
using OpenCBS.Manager.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCBS.Services.Messaging
{
    public partial class EmailAccountServices : Services
    {                
        private static List<EmailAccount> _emailAccounts;
        private readonly EmailAccountManager _manager;

        public EmailAccountServices(User user)
        {
            _manager = new EmailAccountManager(user);
        }

        private void LoadEmailAccounts()
        {
            //if (_emailAccounts != null) return;
            _emailAccounts = _manager.SelectAll();
        }

        public EmailAccount FindById(int id)
        {
            LoadEmailAccounts();
            return _emailAccounts.Find(item => item.Id == id);
        }

        public EmailAccount FindByEmail(string email)
        {
            LoadEmailAccounts();
            return _emailAccounts.Find(item => item.Email == email.Trim());
        }

        public List<EmailAccount> LoadAll()
        {
            //if (_emailAccounts != null) return _emailAccounts;
             
            _emailAccounts = _manager.SelectAll(); return _emailAccounts;
        }

        public void Add(EmailAccount emailAccount) 
        {
            _manager.Add(emailAccount);
        }

        public void Update(EmailAccount emailAccount)
        {
            _manager.Update(emailAccount);
        }

        /// <summary>
        /// Deletes an email account
        /// </summary>
        /// <param name="emailAccount">Email account</param>
        public void Delete(EmailAccount emailAccount)
        {
            if (emailAccount == null)
                throw new ArgumentNullException("emailAccount");

            if (_manager.SelectAll().Count == 1)
                throw new Exception("You cannot delete this email account. At least one account is required.");

            _manager.Delete(emailAccount);            
        }                        
    }
}
