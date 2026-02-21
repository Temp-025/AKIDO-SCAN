//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
//using EnterpriseGatewayPortal.Core.Domain.Services;
//using EnterpriseGatewayPortal.Core.DTOs;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;

//using ClosedXML.Excel;
//using iTextSharp.text;
//using iTextSharp.text.pdf;

//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http.HttpResults;

//namespace EnterpriseGatewayPortal.Core.Services
//{
//    public class ConvertToPdfService : IConvertToPdfService
//    {
//        private const string LibreOfficePath = "C:\\Program Files\\LibreOffice\\program\\soffice.exe";
//        private HttpClient _client;

//        public ConvertToPdfService(HttpClient client, IConfiguration configuration)
//        {
//            _client = client;

//            _client = new HttpClient { BaseAddress = new Uri(configuration.GetValue<string>("Config:PdfConverterBaseAddress")) };
//            _client.Timeout = TimeSpan.FromMinutes(5);

//        }

//        public FileContentResult? ConvertDocToPdf(IFormFile file)
//        {
//            try
//            {
//                if (file == null || Path.GetExtension(file.FileName).ToLower() != ".docx")
//                {
//                    throw new ArgumentException("Invalid file format. Please upload a DOCX file.");
//                }

//                string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
//                Directory.CreateDirectory(tempDir);

//                string inputFilePath = Path.Combine(tempDir, file.FileName);
//                string outputFilePath = Path.Combine(tempDir, Path.GetFileNameWithoutExtension(file.FileName) + ".pdf");

//                using (var stream = new FileStream(inputFilePath, FileMode.Create))
//                {
//                    file.CopyTo(stream);
//                }

//                ProcessStartInfo startInfo = new ProcessStartInfo
//                {
//                    FileName = LibreOfficePath,
//                    Arguments = $"--headless --convert-to pdf \"{inputFilePath}\" --outdir \"{tempDir}\"",
//                    RedirectStandardOutput = true,
//                    RedirectStandardError = true,
//                    UseShellExecute = false,
//                    CreateNoWindow = true
//                };

//                using (Process process = new Process { StartInfo = startInfo })
//                {
//                    process.Start();
//                    process.WaitForExit();
//                }

//                if (!File.Exists(outputFilePath))
//                {
//                    throw new Exception("PDF conversion failed.");
//                }

//                byte[] fileBytes = File.ReadAllBytes(outputFilePath);
//                Directory.Delete(tempDir, true);

//                return new FileContentResult(fileBytes, "application/pdf")
//                {
//                    FileDownloadName = Path.GetFileName(outputFilePath)
//                };
//            }
//            catch
//            {
//                return null;
//            }
//        }

//        public FileContentResult ConvertExcelToPdf(IFormFile file)
//        {
//            try
//            {
//                if (file == null || Path.GetExtension(file.FileName).ToLower() != ".xlsx")
//                {
//                    return null;
//                }

//                string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
//                using (var stream = new FileStream(tempFilePath, FileMode.Create))
//                {
//                    file.CopyTo(stream);
//                }

//                using (var workbook = new XLWorkbook(tempFilePath))
//                {
//                    var worksheet = workbook.Worksheet(1);
//                    worksheet.AutoFilter.Clear();

//                    var range = worksheet.RangeUsed();
//                    var headers = range.Row(1).CellsUsed().Select(c => c.Value.ToString()).ToList();
//                    var dataRows = range.RowsUsed().Skip(1);

//                    float[] columnWidths = new float[headers.Count];
//                    for (int i = 0; i < headers.Count; i++)
//                    {
//                        float headerWidth = headers[i].Length;
//                        float maxWidth = headerWidth;
//                        foreach (var row in dataRows)
//                        {
//                            var cellValue = row.Cell(i + 1).Value.ToString();
//                            if (cellValue.Length > maxWidth)
//                            {
//                                maxWidth = cellValue.Length;
//                            }
//                        }
//                        columnWidths[i] = (Math.Max(maxWidth, headerWidth) + 3) * 5.2f;
//                    }

//                    float totalWidth = columnWidths.Sum();
//                    iTextSharp.text.Rectangle pageSize = totalWidth < PageSize.A4.Width ? PageSize.A4 :
//                                         totalWidth < PageSize.A4.Rotate().Width ? PageSize.A4.Rotate() :
//                                         totalWidth < PageSize.A3.Width ? PageSize.A3 :
//                                         totalWidth < PageSize.A3.Rotate().Width ? PageSize.A3.Rotate() :
//                                         totalWidth < PageSize.A2.Width ? PageSize.A2 :
//                                         totalWidth < PageSize.A2.Rotate().Width ? PageSize.A2.Rotate() :
//                                         totalWidth < PageSize.A1.Width ? PageSize.A1 :
//                                         totalWidth < PageSize.A1.Rotate().Width ? PageSize.A1.Rotate() :
//                                         totalWidth < PageSize.A0.Width ? PageSize.A0 :
//                                         totalWidth < PageSize.A0.Rotate().Width ? PageSize.A0.Rotate() :
//                                         new iTextSharp.text.Rectangle(totalWidth, totalWidth * (float)Math.Sqrt(2));

//                    MemoryStream pdfStream = new MemoryStream();
//                    Document pdfDoc = new Document(pageSize, 20, 20, 20, 20);
//                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, pdfStream);
//                    writer.CloseStream = false;
//                    pdfDoc.Open();

//                    PdfPTable pdfTable = new PdfPTable(headers.Count)
//                    {
//                        WidthPercentage = 100
//                    };
//                    pdfTable.SetWidths(columnWidths);

//                    foreach (var header in headers)
//                    {
//                        PdfPCell headerCell = new PdfPCell(new Phrase(header))
//                        {
//                            Border = iTextSharp.text.Rectangle.NO_BORDER
//                        };
//                        pdfTable.AddCell(headerCell);
//                    }

//                    foreach (var row in dataRows)
//                    {
//                        foreach (var cell in row.CellsUsed())
//                        {
//                            PdfPCell cellElement = new PdfPCell(new Phrase(cell.Value.ToString()))
//                            {
//                                Border = iTextSharp.text.Rectangle.NO_BORDER
//                            };
//                            pdfTable.AddCell(cellElement);
//                        }
//                    }

//                    pdfDoc.Add(pdfTable);
//                    pdfDoc.Close();
//                    pdfStream.Position = 0;

//                    System.IO.File.Delete(tempFilePath);
//                    return new FileContentResult(pdfStream.ToArray(), "application/pdf")
//                    {
//                        FileDownloadName = Path.GetFileNameWithoutExtension(file.FileName) + ".pdf"
//                    };
//                }
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        public async Task<ServiceResult> ConvertToPdf(IFormFile file)
//        {
//            try
//            {
//                if (file == null || file.Length == 0)
//                    throw new ArgumentException("Input file is empty or null.", nameof(file));



//                HttpResponseMessage response = null;

//                using (var form = new MultipartFormDataContent())
//                {
//                    using MemoryStream memoryStream = new();

//                    await file.CopyToAsync(memoryStream);

//                    memoryStream.Position = 0;

//                    var streamContent = new StreamContent(memoryStream);

//                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

//                    form.Add(streamContent, "file", file.FileName);

//                    response = await _client.PostAsync("convert", form);
//                    response.EnsureSuccessStatusCode();

//                }

//                var contentType = response.Content.Headers.ContentType?.MediaType;
//                if (contentType != "application/pdf")
//                    throw new InvalidOperationException("Expected PDF file in response.");

//                var pdfBytes = await response.Content.ReadAsByteArrayAsync();

//                var fileContent = new FileContentResult(pdfBytes, "application/pdf")
//                {
//                    FileDownloadName = Path.ChangeExtension(file.FileName, ".pdf")
//                };



//                return new ServiceResult(true, "File converted to PDF successfully", fileContent);
//            }
//            catch (Exception ex)
//            {
//                // You may want to return an error message instead of null.
//                return new ServiceResult(false, "PDF conversion failed: " + ex.Message, null);
//            }
//        }
//        public async Task<ServiceResult> AddCommentsToPdf(CommentrequestDTO request)
//        {
//            try
//            {
//                if (request.File == null || request.File.Length == 0)
//                    throw new ArgumentException("Input file is empty or null.", nameof(request.File));



//                HttpResponseMessage response = null;

//                using (var form = new MultipartFormDataContent())
//                {
//                    using MemoryStream memoryStream = new();

//                    await request.File.CopyToAsync(memoryStream);

//                    memoryStream.Position = 0;

//                    var streamContent = new StreamContent(memoryStream);

//                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

//                    form.Add(streamContent, "file", request.File.FileName);
//                    form.Add(new StringContent(request.Comments ?? ""), "Comments");


//                    response = await _client.PostAsync("annotate", form);
//                    response.EnsureSuccessStatusCode();

//                }

//                var contentType = response.Content.Headers.ContentType?.MediaType;
//                if (contentType != "application/pdf")
//                    throw new InvalidOperationException("Expected PDF file in response.");

//                var pdfBytes = await response.Content.ReadAsByteArrayAsync();

//                var fileContent = new FileContentResult(pdfBytes, "application/pdf")
//                {
//                    FileDownloadName = Path.ChangeExtension(request.File.FileName, ".pdf")
//                };



//                return new ServiceResult(true, "File converted to PDF successfully", fileContent);
//            }
//            catch (Exception ex)
//            {
//                // You may want to return an error message instead of null.
//                return new ServiceResult(false, "PDF conversion failed: " + ex.Message, null);
//            }
//        }

//        public async Task<ServiceResult> InitialWatermark(InitialWatermarkDTO request, Dictionary<string, IFormFile> imageFiles)
//        {
//            try
//            {
//                if (request.Pdf == null || request.Pdf.Length == 0)
//                    throw new ArgumentException("Input file is empty or null.", nameof(request.Pdf));

//                HttpResponseMessage response;

//                using var form = new MultipartFormDataContent();
//                var disposableStreams = new List<MemoryStream>();

//                try
//                {
//                    // PDF
//                    var memoryStream = new MemoryStream();
//                    await request.Pdf.CopyToAsync(memoryStream);
//                    memoryStream.Position = 0;
//                    disposableStreams.Add(memoryStream);

//                    var streamContent = new StreamContent(memoryStream);
//                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
//                    form.Add(streamContent, "pdf", request.Pdf.FileName);

//                    // Strings
//                    if (!string.IsNullOrWhiteSpace(request.Params))
//                        form.Add(new StringContent(request.Params), "params");

//                    if (!string.IsNullOrWhiteSpace(request.Text))
//                        form.Add(new StringContent(request.Text), "watermark_text");

//                    if (!string.IsNullOrWhiteSpace(request.Font_size))
//                        form.Add(new StringContent(request.Font_size), "font_size");

//                    // Images
//                    foreach (var kvp in imageFiles)
//                    {
//                        string imageFieldName = kvp.Key;
//                        IFormFile imageFile = kvp.Value;

//                        var imgStream = new MemoryStream();
//                        await imageFile.CopyToAsync(imgStream);
//                        imgStream.Position = 0;
//                        disposableStreams.Add(imgStream);

//                        var imageContent = new StreamContent(imgStream);
//                        imageContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);
//                        form.Add(imageContent, imageFieldName, imageFile.FileName);
//                    }

//                    // Send
//                    response = await _client.PostAsync("embed", form);
//                    response.EnsureSuccessStatusCode();
//                }
//                finally
//                {
//                    // Dispose all memory streams AFTER sending the request
//                    foreach (var stream in disposableStreams)
//                        stream.Dispose();
//                }

//                var contentType = response.Content.Headers.ContentType?.MediaType;
//                if (contentType != "application/pdf")
//                    throw new InvalidOperationException("Expected PDF file in response.");

//                var pdfBytes = await response.Content.ReadAsByteArrayAsync();

//                var fileContent = new FileContentResult(pdfBytes, "application/pdf")
//                {
//                    FileDownloadName = Path.ChangeExtension(request.Pdf.FileName, ".pdf")
//                };

//                return new ServiceResult(true, "File converted to PDF successfully", fileContent);
//            }
//            catch (Exception ex)
//            {
//                return new ServiceResult(false, "PDF conversion failed: " + ex.Message, null);

//            }
//        }

//    }
//}
