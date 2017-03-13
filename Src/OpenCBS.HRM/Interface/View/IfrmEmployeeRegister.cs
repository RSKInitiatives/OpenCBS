using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCBS.HRM.Interface.View
{
    public interface IfrmEmployeeRegister
    {
        bool Enabled { get; set; }

        void Show();
        void Gridfill();
        void Close();
    }
}
