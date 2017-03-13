using System.Collections.Generic;
using System.Linq;
using OpenCBS.ArchitectureV2.Interface;
using OpenCBS.ArchitectureV2.Interface.Repository;
using OpenCBS.ArchitectureV2.Model;
using Dapper;

namespace OpenCBS.ArchitectureV2.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public SearchRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public List<SearchResult> Search(string keywords)
        {
            const string query = @"
                select
	                v.id
	                , 'Village Bank' [Type]
                    , t.active Active
	                , v.name
	                , d.name District
	                , t.city City
                from 
	                dbo.Villages v
                left join
	                dbo.Tiers t on v.id = t.id
                left join
	                dbo.Districts d on d.id = t.district_id
                where
	                v.name like @keywords
                order by
                    v.name
            ";
            using (var connection = _connectionProvider.GetConnection())
            {
                keywords = "%" + keywords + "%";
                return connection.Query<SearchResult>(query, new { keywords }).ToList();
            }
        }
    }
}
