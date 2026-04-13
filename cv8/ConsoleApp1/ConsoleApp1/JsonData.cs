using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class JsonData
    {
        public string Init { get; set; }
        public List<JsonDataseries> Dataseries { get; set; }
    }

    public class JsonDataseries
    {
        public int Timepoint { get; set; }
        public int Temp2m { get; set; }
    }
}
