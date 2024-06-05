using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagmentSystem.Services
{
    public class GVAR
    {
        public ConcurrentDictionary<string, ConcurrentDictionary<string, string>> DicOfDic { get; set; } = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();
        public ConcurrentDictionary<string, DataTable> DicOfDT { get; set; } = new ConcurrentDictionary<string, DataTable>();

        public GVAR()
        {
            DicOfDic = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();
            DicOfDT = new ConcurrentDictionary<string, DataTable>();
        }
    }

}
