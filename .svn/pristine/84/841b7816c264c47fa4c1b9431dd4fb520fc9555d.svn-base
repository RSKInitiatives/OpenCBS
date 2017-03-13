using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Messaging;
using OpenCBS.Manager.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCBS.Services.Messaging
{
    public partial class QueuedEmailServices : Services
    {        
        private static List<QueuedEmail> _queuedEmails;
        private readonly QueuedEmailManager _manager;

        public QueuedEmailServices(User user) 
        {
            _manager = new QueuedEmailManager(user);
        }

        private void LoadQueuedEmails()
        {
            //if (_queuedEmails != null) return;
            _queuedEmails = _manager.SelectAll();
        }

        public QueuedEmail FindById(int id)
        {
            LoadQueuedEmails();
            return _queuedEmails.Find(item => item.Id == id);
        }

        public List<QueuedEmail> LoadAll()
        {
            //if (_queuedEmails != null) return _queuedEmails;

            _queuedEmails = _manager.SelectAll(); return _queuedEmails;
        }

        public void Add(QueuedEmail queuedEmail)
        {
            _manager.Add(queuedEmail);
        }

        public void Update(QueuedEmail queuedEmail)
        {
            _manager.Update(queuedEmail);
        }

        /// <summary>
        /// Deletes an email account
        /// </summary>
        /// <param name="queuedEmail">Email account</param>
        public void Delete(QueuedEmail queuedEmail)
        {
            if (queuedEmail == null)
                throw new ArgumentNullException("queuedEmail");

            if (_manager.SelectAll().Count == 1)
                throw new Exception("You cannot delete this email account. At least one account is required.");

            _manager.Delete(queuedEmail);
        }      

        /// <summary>
        /// Get queued emails by identifiers
        /// </summary>
        /// <param name="queuedEmailIds">queued email identifiers</param>
        /// <returns>Queued emails</returns>
        public virtual IList<QueuedEmail> GetByIds(int[] queuedEmailIds)
        {
            if (queuedEmailIds == null || queuedEmailIds.Length == 0)
                return new List<QueuedEmail>();

            var query = from qe in _manager.SelectAll()
                        where queuedEmailIds.Contains(qe.Id)
                        select qe;
            var queuedEmails = query.ToList();
            //sort by passed identifiers
            var sortedQueuedEmails = new List<QueuedEmail>();
            foreach (int id in queuedEmailIds)
            {
                var queuedEmail = queuedEmails.Find(x => x.Id == id);
                if (queuedEmail != null)
                    sortedQueuedEmails.Add(queuedEmail);
            }
            return sortedQueuedEmails;
        }

        /// <summary>
        /// Gets all queued emails
        /// </summary>
        /// <param name="fromEmail">From Email</param>
        /// <param name="toEmail">To Email</param>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="loadNotSentItemsOnly">A value indicating whether to load only not sent emails</param>
        /// <param name="sendTriesStart">Maximum send tries</param>
        /// <param name="loadNewest">A value indicating whether we should sort queued email descending; otherwise, ascending.</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Email item list</returns>
        public virtual IList<QueuedEmail> SearchEmails(string fromEmail, 
            string toEmail, DateTime? startTime, DateTime? endTime, 
            bool loadNotSentItemsOnly, int sendTriesStart, int sendTriesEnd,
            bool loadNewest, int pageIndex, int pageSize)
        {
            fromEmail = (fromEmail ?? String.Empty).Trim();
            toEmail = (toEmail ?? String.Empty).Trim();
            
            var query = _manager.SelectAll();
            if (!String.IsNullOrEmpty(fromEmail))
                query = query.FindAll(qe => qe.From.Contains(fromEmail));
            if (!String.IsNullOrEmpty(toEmail))
                query = query.FindAll(qe => qe.To.Contains(toEmail));
            if (startTime.HasValue)
                query = query.FindAll(qe => qe.CreatedOnUtc >= startTime);
            if (endTime.HasValue)
                query = query.FindAll(qe => qe.CreatedOnUtc <= endTime);
            if (loadNotSentItemsOnly)
                query = query.FindAll(qe => !qe.SentOnUtc.HasValue);
            query = query.FindAll(qe => qe.SentTries >= sendTriesStart && qe.SentTries < sendTriesEnd);
            query = query.OrderByDescending(qe => qe.Priority).ToList();
            query = loadNewest ?
                query.OrderByDescending(qe => qe.CreatedOnUtc).ToList() : query;
                //((IOrderedQueryable<QueuedEmail>)query).ThenByDescending(qe => qe.CreatedOnUtc) :
                //((IOrderedQueryable<QueuedEmail>)query).ThenBy(qe => qe.CreatedOnUtc);

            var queuedEmails = new List<QueuedEmail>(query/*.Skip(pageIndex * pageSize).Take(pageSize)*/);            

            return queuedEmails;
        }
        
    }
}
