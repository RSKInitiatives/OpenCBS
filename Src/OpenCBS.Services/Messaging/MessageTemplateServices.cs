using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Messaging;
using OpenCBS.Manager.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCBS.Services.Messaging
{
    [Serializable]
    public class MessageTemplateServices : Services
    {
        #region Constants

        private const string MESSAGETEMPLATES_ALL_KEY = "opencbs.messagetemplate.all-0";//{0}";
        private const string MESSAGETEMPLATES_BY_NAME_KEY = "opencbs.messagetemplate.name-{0}-0";//{0}-{1}";
        private const string MESSAGETEMPLATES_PATTERN_KEY = "opencbs.messagetemplate.";

        #endregion

        private static List<MessageTemplate> _messageTemplates;
        private readonly MessageTemplateManager _manager;

        public MessageTemplateServices(User user)
        {
            _manager = new MessageTemplateManager(user);
        }

        #region Methods
        /// <summary>
        /// Delete a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public void Delete(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            _manager.Delete(messageTemplate);

            //_cacheManager.RemoveByPattern(MESSAGETEMPLATES_PATTERN_KEY);
        }

        /// <summary>
        /// Inserts a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public void Add(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            _manager.Add(messageTemplate);

            //_cacheManager.RemoveByPattern(MESSAGETEMPLATES_PATTERN_KEY);
        }

        /// <summary>
        /// Updates a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public void Update(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            _manager.Update(messageTemplate);

            //_cacheManager.RemoveByPattern(MESSAGETEMPLATES_PATTERN_KEY);
        }

        public MessageTemplate FindById(int id)
        {
            LoadMessageTemplates();
            return _messageTemplates.Find(item => item.Id == id);
        }

        public List<MessageTemplate> LoadAll()
        {
            //if (_messageTemplates != null) return _messageTemplates;
            
            _messageTemplates = _manager.SelectAll();
            return _messageTemplates;
        }

        private void LoadMessageTemplates()
        {
            //if (_messageTemplates != null) return;
            _messageTemplates = _manager.SelectAll();
        }

        /// <summary>
        /// Gets a message template
        /// </summary>
        /// <param name="messageTemplateName">Message template name</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Message template</returns>
        public MessageTemplate FindByName(string messageTemplateName)
        {
            if (string.IsNullOrWhiteSpace(messageTemplateName))
                throw new ArgumentException("messageTemplateName");

            //string key = string.Format(MESSAGETEMPLATES_BY_NAME_KEY, messageTemplateName);

            MessageTemplate messageTemplate = _manager.SelectByName(messageTemplateName);                        
            return messageTemplate;

        }
        /*
        /// <summary>
        /// Gets all message templates
        /// </summary>
		/// <returns>Message template list</returns>
		public override IEnumerable<MessageTemplate> GetAll()
        {
			string key = MESSAGETEMPLATES_ALL_KEY;
            var query = GetAll().OrderBy(t => t.Name);
			
            /*return _cacheManager.Get(key, () =>
            {
				var query = GetAll().OrderBy(t => t.Name);				

				return query.ToList();
            });
            return query;
        }
        */

        /// <summary>
        /// Create a copy of message template with all depended data
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        /// <returns>Message template copy</returns>
        public MessageTemplate Copy(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            var mtCopy = new MessageTemplate()
            {
                Name = messageTemplate.Name,
                BccEmailAddresses = messageTemplate.BccEmailAddresses,
                Subject = messageTemplate.Subject,
                Body = messageTemplate.Body,
                IsActive = messageTemplate.IsActive,
                EmailAccountId = messageTemplate.EmailAccountId
            };

            _manager.Add(mtCopy);

            return mtCopy;
        }

        #endregion

    }
}
