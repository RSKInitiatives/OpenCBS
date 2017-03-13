using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.Messaging;
using OpenCBS.Manager.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCBS.Services.Messaging
{
    public partial class QueuedSMSServices : Services
    {        
        private static List<QueuedSMS> _queuedSMS;
        private readonly QueuedSMSManager _manager;

        public QueuedSMSServices(User user) 
        {
            _manager = new QueuedSMSManager(user);
        }

        private void LoadQueuedSMSs()
        {
            //if (_queuedSMSs != null) return;
            _queuedSMS = _manager.SelectAll();
        }

        public QueuedSMS FindById(int id)
        {
            LoadQueuedSMSs();
            return _queuedSMS.Find(item => item.Id == id);
        }

        public List<QueuedSMS> LoadAll()
        {
            //if (_queuedSMSs != null) return _queuedSMSs;

            _queuedSMS = _manager.SelectAll(); return _queuedSMS;
        }

        public void Add(QueuedSMS queuedSMS)
        {
            _manager.Add(queuedSMS);
        }

        public void Update(QueuedSMS queuedSMS)
        {
            _manager.Update(queuedSMS);
        }

        /// <summary>
        /// Deletes an SMS account
        /// </summary>
        /// <param name="queuedSMS">SMS account</param>
        public void Delete(QueuedSMS queuedSMS)
        {
            if (queuedSMS == null)
                throw new ArgumentNullException("queuedSMS");

            if (_manager.SelectAll().Count == 1)
                throw new Exception("You cannot delete this SMS account. At least one account is required.");

            _manager.Delete(queuedSMS);
        }      

        /// <summary>
        /// Get queued SMSs by identifiers
        /// </summary>
        /// <param name="queuedSMSIds">queued SMS identifiers</param>
        /// <returns>Queued SMSs</returns>
        public virtual IList<QueuedSMS> GetByIds(int[] queuedSMSIds)
        {
            if (queuedSMSIds == null || queuedSMSIds.Length == 0)
                return new List<QueuedSMS>();

            var query = from qe in _manager.SelectAll()
                        where queuedSMSIds.Contains(qe.Id)
                        select qe;
            var queuedSMSs = query.ToList();
            //sort by passed identifiers
            var sortedQueuedSMSs = new List<QueuedSMS>();
            foreach (int id in queuedSMSIds)
            {
                var queuedSMS = queuedSMSs.Find(x => x.Id == id);
                if (queuedSMS != null)
                    sortedQueuedSMSs.Add(queuedSMS);
            }
            return sortedQueuedSMSs;
        }

        /// <summary>
        /// Gets all queued SMSs
        /// </summary>
        /// <param name="fromSMS">From SMS</param>
        /// <param name="recipients">To SMS</param>
        /// <param name="startTime">The start time</param>
        /// <param name="endTime">The end time</param>
        /// <param name="loadNotSentItemsOnly">A value indicating whether to load only not sent SMSs</param>
        /// <param name="sendTriesStart">Maximum send tries</param>
        /// <param name="loadNewest">A value indicating whether we should sort queued SMS descending; otherwise, ascending.</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>SMS item list</returns>
        public virtual IList<QueuedSMS> SearchSMSs(string fromSMS, 
            string recipients, DateTime? startTime, DateTime? endTime, 
            bool loadNotSentItemsOnly, int sendTriesStart, int sendTriesEnd,
            bool loadNewest, int pageIndex, int pageSize)
        {
            fromSMS = (fromSMS ?? String.Empty).Trim();
            recipients = (recipients ?? String.Empty).Trim();
            
            var query = _manager.SelectAll();
            if (!String.IsNullOrEmpty(fromSMS))
                query = query.FindAll(qe => qe.From.Contains(fromSMS));
            if (!String.IsNullOrEmpty(recipients))
                query = query.FindAll(qe => qe.Recipient.Contains(recipients));
            if (startTime.HasValue)
                query = query.FindAll(qe => qe.CreatedOnUtc >= startTime);
            if (endTime.HasValue)
                query = query.FindAll(qe => qe.CreatedOnUtc <= endTime);
            if (loadNotSentItemsOnly)
                query = query.FindAll(qe => !qe.SentOnUtc.HasValue);
            query = query.FindAll(qe => qe.SentTries >= sendTriesStart && qe.SentTries < sendTriesEnd);
            //query = query.OrderByDescending(qe => qe.Priority).ToList();
            query = loadNewest ?
                query.OrderByDescending(qe => qe.CreatedOnUtc).ToList() : query;
                //((IOrderedQueryable<QueuedSMS>)query).ThenByDescending(qe => qe.CreatedOnUtc) :
                //((IOrderedQueryable<QueuedSMS>)query).ThenBy(qe => qe.CreatedOnUtc);

            var queuedSMSs = new List<QueuedSMS>(query/*.Skip(pageIndex * pageSize).Take(pageSize)*/);            

            return queuedSMSs;
        }


        public virtual IList<QueuedSMS> GetUncharged()
        {         
            var query = _manager.SelectAll().FindAll(sms => sms.SentOnUtc.HasValue && sms.Charged != true);

            var queuedSMSs = new List<QueuedSMS>(query);

            return queuedSMSs;
        }

        public virtual List<QueuedSMS> FindByContractId(int contractId)
        {
            var query = _manager.SelectAll()
                .FindAll(sms => sms.ContractId.HasValue && sms.ContractId == contractId)
                .OrderByDescending(sms=>sms.CreatedOnUtc);

            var queuedSMSs = new List<QueuedSMS>(query);

            return queuedSMSs;
        }
    }
}
