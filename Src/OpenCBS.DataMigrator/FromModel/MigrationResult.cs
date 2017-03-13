using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.DataMigrator.FromModel
{
    public class MigrationResult
    {
        public int cid_no;
        public string surname;
        public string first_name;
        public string full_name;
        public string migrated = "no";
        public string type = "";
        public string failure_reason = "";

    }
}
