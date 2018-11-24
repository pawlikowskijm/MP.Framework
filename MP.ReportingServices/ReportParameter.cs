using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ReportingServices
{
    public class ReportParameter
    {
        public string Name { get; set; }
        public string Prompt { get; set; }
        public string Value { get; set; }
        public Type ValueType { get; set; }
        public string DefaultValue { get; set; }
    }
}
