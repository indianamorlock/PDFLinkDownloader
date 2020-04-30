using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Linq;

namespace PDFLinkDownloader
{
    public static class Downloader
    {

        public static void DownloadFile(string url, string destinationFolder, string filename)
        {
            string fullFileName = NormalizeFileName(destinationFolder, filename);
            
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.OpenRead(url);
                    Int64 bytes_total = Convert.ToInt64(client.ResponseHeaders["Content-Length"]);
                    Console.WriteLine($" --> Downloading Total Bytes: {bytes_total}");
                    client.DownloadFile(url, fullFileName);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static Int64 GetContentLength(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.OpenRead(url);
                Int64 bytes_total = Convert.ToInt64(client.ResponseHeaders["Content-Length"]);
                //Console.Write($" --> File Bytes: {bytes_total}");
                return bytes_total;
            }
        }

        public static bool CheckFileAlreadyExists(string savelocation, string filename, string downloadLink)
        {
            try
            {
                FileInfo file;
                Int64 remoteFileSize = GetContentLength(downloadLink);
                string fullFilePath = savelocation + "\\" + filename;
                string normalizedName = NormalizeFileName(savelocation, filename);
                if (File.Exists(fullFilePath))
                {
                    file = new FileInfo(fullFilePath);
                    if (file.Length > 0 && (file.Length == remoteFileSize))
                    {
                        Console.WriteLine($"File: \"{file}\" already exists, will skip download... ");
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else if (File.Exists(normalizedName))
                {
                    file = new FileInfo(normalizedName);
                    if (file.Length > 0 && (file.Length == remoteFileSize))
                    {
                        Console.WriteLine($"File: \"{normalizedName}\" already exists, will skip download... ");
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public static string NormalizeFileName(string filepath, string filename)
        {
            string file = filename;
            string invalidFilename = new string(Path.GetInvalidFileNameChars());

            foreach (char c in invalidFilename)
            {
                file = file.Replace(c.ToString(), " - ");
            }
            return filepath + "\\" + file;
        }
    }
}
