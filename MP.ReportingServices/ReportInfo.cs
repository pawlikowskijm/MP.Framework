using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ReportingServices
{
    public class ReportInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<ReportParameter> Parameters { get; set; }
    }
}
