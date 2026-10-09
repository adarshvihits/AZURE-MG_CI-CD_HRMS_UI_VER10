using Hrms.Lite.Shared.DataBank.DTO;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Streaming;
using System.IO;
using System.Net;
using System.Data;

using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Editing;
//using Telerik.Documents.Primitives;
using Microsoft.AspNetCore.Hosting;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Fonts;

using System;
using Telerik.Documents.Primitives;
using System.Linq;
using Kendo.Mvc.Extensions;
using Spire.Pdf.Exporting.XPS.Schema;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EmployeeDocumentDownloadController : Controller
    {
      
        private readonly IDropdownService _dropdownService;
        private readonly IDocumentUploadService _documentUploadService;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        private readonly IEmployeeDocumentDownloadService _employeeDocumentDownload;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;

        public EmployeeDocumentDownloadController(IEmployeeDocumentDownloadService employeeDocumentDownload, IBlob blob, IWebHostEnvironment env)
        {
            _employeeDocumentDownload = employeeDocumentDownload;
           
            _blob = blob;
            _environment = env;

        }

        public IActionResult Index()
        {
            return View();
        }

        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = true
            }
        };
        public async Task<IActionResult> GetEmployeesList([DataSourceRequest] DataSourceRequest request)
        {

            EmployeeDocumentDownload model = new EmployeeDocumentDownload();
            List<EmployeeDocDownload_List> modellist = new List<EmployeeDocDownload_List>();

            model = await _employeeDocumentDownload.GetEmployeeList();

            modellist = model.EmployeeProfileList;

            var dataResult1 = modellist.ToDataSourceResult(request);

            //var data = await _employeeDocumentDownload.GetEmployeeList();



            return Json(dataResult1, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });

        }
       

        public async Task<IActionResult> Orders_ValueMapper(int[] values)
        {


            EmployeeDocumentDownload model = new EmployeeDocumentDownload();
            List<EmployeeDocDownload_List> modellist = new List<EmployeeDocDownload_List>();

            model = await _employeeDocumentDownload.GetEmployeeList();

            modellist = model.EmployeeProfileList;

            var indices = new List<int>();

            if (values != null && values.Any())
            {
                var index = 0;

                foreach (var order in modellist)
                {
                    if (values.Contains(order.employeeCode))
                    {
                        indices.Add(index);
                    }

                    index += 1;
                }
            }

            return Json(indices);
        }


        public async Task<ActionResult> MergeAndDownload(int employee)
        {
            

            DataSet ds = await _employeeDocumentDownload.GetEmployeeDocument(employee);//46058

            // Get the list of file URLs from your data source
            List<string> fileUrls = GetFileUrls(ds);

           

            // Merge the PDF files
            using (MemoryStream mergedStream = new MemoryStream())
            {
                using (PdfStreamWriter fileWriter = new PdfStreamWriter(mergedStream))
                {
                    foreach (string fileUrl in fileUrls)
                    {
                        using (WebClient webClient = new WebClient())
                        {
                            byte[] fileBytes = await webClient.DownloadDataTaskAsync(fileUrl);
                            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
                            {
                                memoryStream.Seek(0, SeekOrigin.Begin);

                                using (PdfFileSource fileToMerge = new PdfFileSource(memoryStream))
                                {
                                    foreach (PdfPageSource pageToMerge in fileToMerge.Pages)
                                    {
                                        fileWriter.WritePage(pageToMerge);
                                    }
                                }
                            }
                        }
                    }
                }
 //string path = Path.Combine(this._environment.WebRootPath, "Uploads", "fmerged.pdf");
               
                // Prepare the merged file for download
                byte[] mergedBytes = mergedStream.ToArray();


                string folderPath = System.IO.Path.Combine(this._environment.WebRootPath, "Uploads");

                string[] files = Directory.GetFiles(folderPath);

                foreach (string filePath in files)
                {
                   System.IO.File.Delete(filePath);
                   
                }


                return File(mergedBytes, "application/pdf", "merged.pdf");
              



            }
        }

      

        // Dummy method to retrieve the list of file URLs (Replace with your own logic)
        private List<string> GetFileUrls(DataSet ds)
        {
         double defaultLeftIndent = 50;
        
            string path = "";
            string filePath = "";
            int i = 0;
            List<string> strDetailIDList = new List<string>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                i++;
                filePath = row["absoluteUri"].ToString();
                byte[] renderedBytes = null;
                RadFixedDocument document = new RadFixedDocument();
                RadFixedPage page = document.Pages.AddPage();
                page.Size = new Size(600, 750);
                FixedContentEditor editor = new FixedContentEditor(page);
                editor.Position.Translate(defaultLeftIndent, 50);
                string url = filePath;
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;

                if (row["fileExtension"].ToString() == ".jpg")
                {
                    path = System.IO.Path.Combine(this._environment.WebRootPath, "Uploads", "filename" + i.ToString() + ".jpg");

                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(url, path);
                    }

                    using (FileStream fs = new FileStream(path,FileMode.Open, FileAccess.Read))
                    {
                        editor.DrawImage(fs);
                    }


                    PdfFormatProvider formatProvider = new PdfFormatProvider();
                    formatProvider.ExportSettings.ImageQuality = ImageQuality.High;
                   
                    using (MemoryStream msImage = new MemoryStream())
                    {

                        formatProvider.Export(document, msImage);
                        renderedBytes = msImage.ToArray();
                    }
                    path = System.IO.Path.Combine(this._environment.WebRootPath, "Uploads", "filename" + i.ToString() + ".pdf");
                    System.IO.File.WriteAllBytes(path, renderedBytes);

                }
               
                if (row["fileExtension"].ToString() == ".pdf")
                {
                    path = System.IO.Path.Combine(this._environment.WebRootPath, "Uploads", "filename" + i.ToString() + ".pdf");
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(url, path);
                    }
                  
                }
                path = System.IO.Path.Combine(this._environment.WebRootPath, "Uploads", "filename" + i.ToString() + ".pdf");
                MemoryStream ms = new MemoryStream();
                RadFixedPage foregroundContentOwner = GenerateForegroundTextContent(row["name"].ToString());
               
                using (Stream stream = System.IO.File.OpenRead(path))
                {
                  
                    using (PdfFileSource fileSource = new PdfFileSource(stream))
                    {
                        using (PdfStreamWriter fileWriter = new PdfStreamWriter(ms, true))
                        {
                            foreach (PdfPageSource pageSource in fileSource.Pages)
                            {
                                using (PdfPageStreamWriter pageWriter = fileWriter.BeginPage(pageSource.Size, pageSource.Rotation))
                                {

                                    pageWriter.WriteContent(pageSource);

                                    using (pageWriter.SaveContentPosition())
                                    {
                                        double xCenteringTranslation = pageSource.Size.Width / 2; //(pageSource.Size.Width - foregroundContentOwner.Size.Width) / 2;
                                        double yCenteringTranslation = 5;//pageSource.Size.Height / 2; //(pageSource.Size.Height - foregroundContentOwner.Size.Height) / 2;
                                        pageWriter.ContentPosition.Translate(xCenteringTranslation, yCenteringTranslation);
                                        pageWriter.WriteContent(foregroundContentOwner);
                                    }
                                }
                            }
                        }
                    }
                    renderedBytes = ms.ToArray();

                    path = System.IO.Path.Combine(this._environment.WebRootPath, "Uploads", "filename" + i.ToString() + ".pdf");

                    System.IO.File.WriteAllBytes(path, renderedBytes);

                    // Do your work with the document inside the using statement. 
                }


                strDetailIDList.Add(path);
            }
               return strDetailIDList;

            // Example list of file URLs
           
        }


        private static RadFixedPage GenerateForegroundTextContent(string text)
        {
            Block block = new Block();
            //block.BackgroundColor = new RgbColor(50, 0, 0, 0);
            //block.GraphicProperties.FillColor = new RgbColor(255, 0, 0);
            block.TextProperties.FontSize = 15;//120;
            block.TextProperties.Font = FontsRepository.HelveticaBold;
            block.InsertText(text);
            Size horizontalTextSize = block.Measure();
            double rotatedTextSquareSize = (horizontalTextSize.Width + horizontalTextSize.Height) / Math.Sqrt(2);

            RadFixedPage foregroundContentOwner = new RadFixedPage();
           // foregroundContentOwner.Size = new Size(rotatedTextSquareSize, rotatedTextSquareSize);
            FixedContentEditor foregroundEditor = new FixedContentEditor(foregroundContentOwner);
            foregroundEditor.Position.Translate(horizontalTextSize.Height / Math.Sqrt(2), 0);
           // foregroundEditor.Position.Rotate(45);
            foregroundEditor.DrawBlock(block);

            return foregroundContentOwner;
        }


        private static void DrawDescription(FixedContentEditor editor, double maxWidth,string text)
        {
            Block block = new Block();
            block.GraphicProperties.FillColor = RgbColors.Black;
            block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;

            block.TextProperties.Font = FontsRepository.HelveticaBoldOblique;
            block.InsertText(text);
            block.TextProperties.Font = FontsRepository.Helvetica;
            // block.InsertText(" is a document processing library that enables your application to import and export files to and from PDF format. The document model is entirely independent from UI and allows you to generate sleek documents with differently formatted text, images, shapes and more.");

            editor.DrawBlock(block, new Size(maxWidth, double.PositiveInfinity));
        }




    }
}
