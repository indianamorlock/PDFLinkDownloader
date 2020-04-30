using System;
using System.Collections.Generic;
using System.Linq;

namespace PDFLinkDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lets get some links");
            List<string> links = new List<string>();
            List<string> distinctLinks;
            string fileToProcess = @"D:\Downloads\SpringerEbooks.pdf";
            string savePDFLocation = @"D:\Downloads\SpringerPDFs";
            string downloadLink;
            string fileUrl;
            string fileName;
            string fullFilePath;
            string basefileurl = @"https://link.springer.com";
            int index = 1;

            for (int i = 1; i <= PdfWorker.GetNumberOfPages(fileToProcess); i++)
            {
                links.AddRange(PdfWorker.GetPdfLinks(fileToProcess, i));
            }
            distinctLinks = links.Distinct().ToList();
           /* foreach (string uris in distinctLinks)
            {
                fileUrl = HTMLParser.Parse(uris, out fileName);
                Console.WriteLine($"\"{fileName}\" \t".PadRight(150,' ') + "|| " +$"\"{basefileurl+fileUrl}\"");
            }
            */
            foreach (string link in links)
            {
                
               fullFilePath = string.Empty;
               fileUrl = HTMLParser.Parse(link, out fileName);
               downloadLink = basefileurl + fileUrl;
               fileName = fileName + ".pdf";
                Console.WriteLine($"[[{index}]:\"{fileName}\" from URL: >>{downloadLink}<<");
               if (!Downloader.CheckFileAlreadyExists(savePDFLocation, fileName, downloadLink))
               {
                    Downloader.DownloadFile(downloadLink, savePDFLocation, fileName);
                    
               }
                index = index + 1;


            }
        }
    }
}

