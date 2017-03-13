using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using OpenCBS.ArchitectureV2.Interface.Repository;
using OpenCBS.ArchitectureV2.Model;
using Dapper;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.CoreDomain.Clients;
using System.Data.SqlClient;
using OpenCBS.Services;
using OpenCBS.Enums;

namespace OpenCBS.ArchitectureV2.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly ClientServices _clientService;
        

        public ClientRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
            _clientService  = ServicesProvider.GetInstance().GetClientServices();
        }

        public List<Alert> GetAlerts(DateTime date, int userId)
        {
            List<Alert> clientAlerts = _clientService.SelectAllPersons()
                .Select(p => new Alert() 
                {
                    Id = p.Id,
                    Kind = Alert.AlertKind.Client,
                    ClientStatus = p.IsNew.HasValue && p.IsNew.Value ? OClientStatus.New 
                    : p.IsUpdated.HasValue && p.IsUpdated.Value ? OClientStatus.Updated 
                    : p.Status,
                    Date = p.CreationDate,
                    ClientName = p.Name,
                    Address = p.Address,
                    Phone = p.PersonalPhone,
                    BranchName = p.Branch != null ? p.Branch.Name : ""
                })
                .ToList();

            return clientAlerts;
        }
        
    }
}
