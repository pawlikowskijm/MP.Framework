using MP.ReportingServices.RSService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MP.ReportingServices
{
    public class ReportClient
    {
        public ReportClient()
        {

        }

        public ReportClient(string reportServerPath)
        {
            ReportServerPath = reportServerPath;
            Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

        }

        public string ReportServerPath { get; }
        public NetworkCredential Credentials { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportServerPath">ex. http://desktop-51p1738/ReportServer_SSRS</param>
        /// <param name="reportPath">ex. ReportFormats.Pdf</param>
        /// <param name="reportFormats">ex. BasicReport or BusinessReports/BasicReport</param>
        /// <param name="reportParams">Dictionary, where [parameterName, parameterValue]></param>
        /// <returns>Report bytes in chosen format.</returns>
        public byte[] GetReportBytes(string reportPath, ReportFormats reportFormat, Dictionary<string, string> reportParams)
        {
            using (Report report = new Report(ReportServerPath, reportPath, reportFormat, Credentials, reportParams))
            {
                using (var ms = new MemoryStream())
                {
                    report.Render().CopyTo(ms);
                    return ms.ToArray();
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportServerPath">ex. http://desktop-51p1738/ReportServer_SSRS</param>
        /// <param name="reportPath">ex. ReportFormats.Pdf</param>
        /// <param name="reportFormats">ex. BasicReport or BusinessReports/BasicReport</param>
        /// <param name="credentials">ex. new NetworkCredential("log", "pass")</param>
        /// <param name="reportParams">Dictionary, where [parameterName, parameterValue]></param>
        /// <returns>Report bytes in chosen format.</returns>
        public byte[] GetReportBytes(string reportPath, ReportFormats reportFormat, NetworkCredential credentials, Dictionary<string, string> reportParams)
        {
            using (Report report = new Report(ReportServerPath, reportPath, reportFormat, credentials, reportParams))
            {
                using (var ms = new MemoryStream())
                {
                    report.Render().CopyTo(ms);
                    return ms.ToArray();
                }

            }
        }

        public List<ReportInfo> GetReportsInfo()
        {
            var service = new ReportingService2010();
            service.Credentials = Credentials;
            CatalogItem[] items = service.ListChildren("/", true);
            var reportsInfo = new List<ReportInfo>();
            foreach (var item in items)
            {
                if (item.TypeName != "Report") continue;
                var parameters = service.GetItemParameters(item.Path, null, true, null, null);
                reportsInfo.Add(new ReportInfo
                {
                    Name = item.Name,
                    Path = item.Path,
                    Parameters = parameters.Select(p => new ReportParameter
                    {
                        Name = p.Name,
                        Prompt = p.Prompt,
                        ValueType = Type.GetType($"System.{p.ParameterTypeName}", true, true).GetType(),
                        DefaultValue = p.DefaultValues?.FirstOrDefault()
                    }).ToList()
                });
            }
            return reportsInfo;
        }
    }
}
