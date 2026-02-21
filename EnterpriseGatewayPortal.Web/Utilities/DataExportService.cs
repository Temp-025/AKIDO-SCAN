using ClosedXML.Excel;
using CsvHelper;
using DinkToPdf;
using DinkToPdf.Contracts;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.Exceptions;
using EnterpriseGatewayPortal.Web.ViewModel.AdminLogReports;
using System.Data;
using System.Globalization;
using System.Net;
using System.Text;

namespace EnterpriseGatewayPortal.Web.Utilities
{
    public class DataExportService
    {
        private readonly IAdminLogReportsService _adminlogReportService;
        private readonly IAdminLogService _adminLogService;
        private readonly IEmailSenderService _emailSender;
        private readonly IClientService _clientService;
        private readonly IRazorRendererHelper _razorRendererHelper;
        private readonly IConverter _converter;
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _environment;
        private readonly ILogger<DataExportService> _logger;
        private readonly IEmailSender _emailSenderNew;
        private const string FOLDER_PATH = "REPORTS";

        public DataExportService(IAdminLogReportsService adminlogReportService, IAdminLogService adminLogService,
            IEmailSenderService emailSender,
            IEmailSender emailSenderNew,
            IClientService clientService,
            IConverter converter,
            IConfiguration configuration,
            IWebHostEnvironment environment,
            ILogger<DataExportService> logger,
            IRazorRendererHelper razorRendererHelper)
        {
            _adminlogReportService = adminlogReportService;
            _emailSender = emailSender;
            _emailSenderNew = emailSenderNew;
            _clientService = clientService;
            _converter = converter;
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
            _razorRendererHelper = razorRendererHelper;
            _adminLogService = adminLogService;
        }

        public async Task ExportAdminReportToFile(string exportType, string name, string email, string startDate, string endDate,
            string userName = null, string moduleName = null, int totalCount = 0)
        {
            _logger.LogInformation("Admin Report Export start");
            _logger.LogError("Admin Report Export");

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[] {
                                        new DataColumn("_Id"),
                                        new DataColumn("User Name"),
                                        new DataColumn("Module Name"),
                                        new DataColumn("Service Name"),
                                        new DataColumn("Activity Name"),
                                        new DataColumn("Log Message"),
                                        new DataColumn("Status"),
                                        new DataColumn("Data Transformation"),
                                        new DataColumn("Time Stamp"),
                                         //new DataColumn("Integrity"),
                                        });

            //var logReports = await _adminlogReportService.GetAdminLogReportAsync(startDate,
            //    endDate,
            //    userName,
            //    moduleName,
            //    perPage: totalCount);

            var logReports = await _adminLogService.GetLocalAdminLogReportsAsync(startDate,
                endDate,
                userName,
                moduleName,
                perPage: totalCount);

            if (logReports == null)
            {
                _logger.LogError("Failed to export admin reports");

                return;
            }

            //foreach (var report in logReports)
            //{
            //    report.IsChecksumValid = _adminlogReportService.VerifyChecksum(report).Success;
            //    dt.Rows.Add(report._id, report.UserName, report.ModuleName,
            //        report.ServiceName, report.ActivityName, report.LogMessage, report.LogMessageType,
            //        report.DataTransformation, report.Timestamp, report.IsChecksumValid ? "Valid" : "Invalid");
            //}
            foreach (var report in logReports)
            {
                dt.Rows.Add(report._id, report.UserName, report.ModuleName,
                    report.ServiceName, report.ActivityName, report.LogMessage, report.LogMessageType,
                    report.DataTransformation, report.Timestamp);
            }

            string fileName = string.Empty;
            switch (exportType.ToLower())
            {
                case "excel":
                    fileName = ExportToExcel(dt);
                    break;

                case "csv":
                    fileName = ExportToCSVUsingCSVWriter(dt);
                    break;
                case "pdf":
                    AdminLogReportsPdfViewModel viewModel = new AdminLogReportsPdfViewModel();
                    viewModel.AdminLogReports = logReports;
                    var partialName = "/Views/AdminLogReports/AdminLogReportsPdfView.cshtml";
                    fileName = await ExportToPdf(viewModel, partialName);
                    break;
            }

            _logger.LogInformation($"Successfully exported admin reports");

            _logger.LogInformation("Sending email...");

            string issuer = _configuration["PortalLoginUrl"];
            string encodedUrl = WebUtility.UrlEncode($"fileName={fileName}");
            string downloadLink = string.Format("{0}FileManager/Download?value={1}", issuer, encodedUrl);

            int hours = Convert.ToInt32(_configuration["ReportExpirationHours"]);
            string content = string.Format($"Hi {name},\nPlease click the below link to download the activity report. The link is valid for {hours} hours\n{downloadLink}\n");
            Message message = new Message(new string[] { email }, "Admin report download link", content);
            //   if (await _emailSender.SendEmail(message) != 0)
            if (await _emailSenderNew.SendEmail(message) != 0)

            {
                _logger.LogError("Failed to send email");
            }
            else
            {
                _logger.LogInformation("Email sent successfully");
            }

            // Delete the expired files
            DeleteExpiredFiles(Convert.ToInt32(_configuration["ReportExpirationHours"]));

            _logger.LogInformation("Admin Report Export end");
        }

        public async Task<string> ExportToPdf<TModel>(TModel viewModel, string partialName)
        {
            //FileStream writeStream = null;

            string fileName = string.Format(@"{0}.pdf", Guid.NewGuid());
            var htmlContent = _razorRendererHelper.RenderPartialToString(partialName, viewModel);
            byte[] pdfBytes = GeneratePdf(htmlContent);

            string path = Path.Combine(_environment.WebRootPath, FOLDER_PATH);

            using (FileStream fs = File.Create(Path.Combine(path, fileName)))
            {
                await fs.WriteAsync(pdfBytes);
            }

            //writeStream = System.IO.File.Create(Path.Combine(path, fileName));
            //writeStream.WriteAsync(pdfBytes);
            //writeStream.Close();

            return fileName;
        }

        public string ExportToExcel(DataTable dataTable)
        {
            _logger.LogInformation("ExportToExcel start...");

            string path = Path.Combine(_environment.WebRootPath, FOLDER_PATH);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = string.Format(@"{0}.xlsx", Guid.NewGuid());
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dataTable);
                ws.Columns().AdjustToContents();
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    wb.SaveAs(stream);
                }
            }

            _logger.LogInformation("ExportToExcel end...");

            return fileName;
        }

        public string ExportToCSVUsingCSVWriter(DataTable dataTable)
        {
            _logger.LogInformation("ExportToCSVUsingCSVWriter start...");

            string path = Path.Combine(_environment.WebRootPath, FOLDER_PATH);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = string.Format(@"{0}.csv", Guid.NewGuid());
            using (StreamWriter textWriter = new StreamWriter(new FileStream(Path.Combine(path, fileName), FileMode.CreateNew)))
            {
                using (CsvWriter csv = new CsvWriter(textWriter, CultureInfo.CurrentCulture))
                {
                    // Write columns
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        csv.WriteField(column.ColumnName);
                    }
                    csv.NextRecord();

                    // Write row values
                    foreach (DataRow row in dataTable.Rows)
                    {
                        for (var i = 0; i < dataTable.Columns.Count; i++)
                        {
                            csv.WriteField(row[i]);
                        }
                        csv.NextRecord();
                    }
                }
            }

            _logger.LogInformation("ExportToCSVUsingCSVWriter end...");

            return fileName;
        }

        public string ExportToCSVUsingStringBuilder(DataTable dataTable)
        {
            _logger.LogInformation("ExportToCSVUsingStringBuilder start...");

            string path = Path.Combine(_environment.WebRootPath, FOLDER_PATH);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = string.Format(@"{0}.csv", Guid.NewGuid());

            StringBuilder data = new StringBuilder();

            //Taking the column names.
            for (int column = 0; column < dataTable.Columns.Count; column++)
            {
                //Making sure that end of the line, shoould not have comma delimiter.
                if (column == dataTable.Columns.Count - 1)
                    data.Append(dataTable.Columns[column].ColumnName.ToString().Replace(",", ";"));
                else
                    data.Append(dataTable.Columns[column].ColumnName.ToString().Replace(",", ";") + ',');
            }

            data.Append(Environment.NewLine);//New line after appending columns.

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                for (int column = 0; column < dataTable.Columns.Count; column++)
                {
                    ////Making sure that end of the line, shoould not have comma delimiter.
                    if (column == dataTable.Columns.Count - 1)
                        data.Append(dataTable.Rows[row][column].ToString().Replace(",", ";"));
                    else
                        data.Append(dataTable.Rows[row][column].ToString().Replace(",", ";") + ',');
                }

                //Making sure that end of the file, should not have a new line.
                if (row != dataTable.Rows.Count - 1)
                    data.Append(Environment.NewLine);
            }

            File.WriteAllText(Path.Combine(path, fileName), data.ToString());

            _logger.LogInformation("ExportToCSVUsingStringBuilder end...");

            return fileName;
        }



        public byte[] DownloadFile(string fileName, int fileExpirationHours)
        {
            _logger.LogInformation("DownloadFile start...");

            string path = Path.Combine(_environment.WebRootPath, FOLDER_PATH);
            if (Directory.Exists(path))
            {
                string filePath = $"{path}//{fileName}";
                if (File.Exists(filePath))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    int differenceHours = (int)(DateTime.Now - fileInfo.CreationTime).TotalHours;
                    if (differenceHours > fileExpirationHours)
                    {
                        _logger.LogError($"File {fileName} has expired");
                        throw new NotFoundException("File doesn't exists");
                    }

                    _logger.LogInformation("DownloadFile end...");

                    return File.ReadAllBytes(filePath);
                }

                _logger.LogError($"File {fileName} not found");
                throw new NotFoundException("File not found");
            }

            _logger.LogError($"Directory {path} not found");
            throw new NotFoundException("Directory not found");
        }

        public byte[] GeneratePdf(string htmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 18, Bottom = 18 },
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                //HeaderSettings = { FontSize = 10, Right = "Page [page] of [toPage]", Line = true },
                //FooterSettings = { FontSize = 8, Center = "PDF demo from JeminPro", Line = true },
            };

            var htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            return _converter.Convert(htmlToPdfDocument);
        }

        private void DeleteExpiredFiles(int time)
        {
            _logger.LogInformation("DeleteExpiredFiles start...");

            string path = Path.Combine(_environment.WebRootPath, FOLDER_PATH);
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        DateTime creationTime = fileInfo.CreationTime;
                        if (creationTime < DateTime.Now.AddHours(-time))
                        {
                            File.Delete(file);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }
                }
            }

            _logger.LogInformation("DeleteExpiredFiles end...");
        }
    }
}
