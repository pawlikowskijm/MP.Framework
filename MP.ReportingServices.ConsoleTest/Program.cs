using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ReportingServices.ConsoleTest
{
    class Program
    {
        static ReportClient Client { get; set; }

        static void Main(string[] args)
        {
            Client = new ReportClient("http://desktop-51p1738/ReportServer_SSRS");
            //GetReportTest();
            GetReportNamesWithPaths();


            Console.ReadKey();
        }

        static void GetReportTest()
        {
            var reportParameters = new Dictionary<string, string>();
            reportParameters.Add("PracownikEmail", "ghi@poczta.jakas.agh");
            var reportBytes = Client.GetReportBytes("ZestawienieBasic", ReportFormats.Pdf, reportParameters);
            File.WriteAllBytes(@"C:\@\ZestawienieBasic.pdf", reportBytes);
        }

        static void GetReportNamesWithPaths()
        {
            var info = Client.GetReportsInfo();
        }
    }
}
