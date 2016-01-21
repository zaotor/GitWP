using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public class elem
    {
        public string value { get; set; }
    }

    public class medicalBookAt
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string fields { get;  set; }
        public string size { get; set; }
        public string weight { get; set; }
        public string agreement { get; set; }
        public string address_id { get; set; }
        public ObservableCollection<elem> fieldlist { get; set; }
    }
}
