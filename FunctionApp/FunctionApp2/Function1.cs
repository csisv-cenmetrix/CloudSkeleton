using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Newtonsoft.Json;
using System.Collections.Generic;
using PdfCreator.Models;

namespace PdfCreator
{
    public class Function1
    {
        /// Hard Coded
        //private static string _storageAccount = "[YourStorageAccName]";
        //private static string _storageKey = "[YourStorageAccKey]";

        /// Get from KeyVault
        private static string _storageAccount = Environment.GetEnvironmentVariable("StorageAccount", EnvironmentVariableTarget.Process);
        private static string _storageKey = Environment.GetEnvironmentVariable("StorageKey", EnvironmentVariableTarget.Process);

        [FunctionName("PdfCreator")]
        public static async Task<Result> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                string id = req.Query["id"];

                string response;

                using (var sr = new StreamReader(req.Body))
                {
                    response = await sr.ReadToEndAsync();
                }

                var todoList = JsonConvert.DeserializeObject<List<Todo>>(response);

                var account = new CloudStorageAccount(new StorageCredentials(_storageAccount, _storageKey), true);
                var blobClient = account.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("temp-pdf");

                if (!container.Exists())
                {
                    await container.CreateAsync();
                }

                //Generating Pdf
                using (MemoryStream ms = new MemoryStream())
                {
                    using (var doc = new iTextSharp.text.Document())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                        doc.Open();
                        doc.Add(new Paragraph("TODOs", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                        doc.Add(new Paragraph("\n\n"));
                        
                        foreach (var todo in todoList)
                        {
                            doc.Add(new Paragraph(todo.Title));
                            doc.Add(new Paragraph(todo.Description));
                            doc.Add(new Paragraph(todo.Due.ToShortDateString()));
                            doc.Add(new Paragraph("\n"));
                        }
                    }

                    var byteArray = ms.ToArray();
                    var blobName = $"TODO-{DateTime.Now.ToString("yyyyMMddhhmmss")}-{Guid.NewGuid().ToString().Substring(0, 4)}.pdf";
                    var blob = container.GetBlockBlobReference(blobName);
                    blob.Properties.ContentType = "application/pdf";
                    await blob.UploadFromByteArrayAsync(byteArray, 0, byteArray.Length);
                    log.LogInformation($"Pdf created successfully. Name: {blobName}");

                    var outputBlob = container.GetBlockBlobReference(blobName);

                    var sasBlobToken = outputBlob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                    {
                        SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(5),
                        Permissions = SharedAccessBlobPermissions.Read
                    });

                    log.LogInformation($"Returning Pdf Download link: {outputBlob.Uri + sasBlobToken}");
                    return new Result(outputBlob.Uri + sasBlobToken);
                }
            }
            catch (Exception ex)
            {
                /// You can handle your exceptions here
                log.LogError(ex.Message);
                return new Result("Something went wrong");
            }
        }
    }
}
