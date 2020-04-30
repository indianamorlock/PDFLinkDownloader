using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PDFLinkDownloader
{
    public static class HTMLParser
    {
		public static string Parse(string URL, out string filename)
		{
			string outFilename = string.Empty;
			List<string> pageLinks = new List<string>();
			string desiredLink;
			// declaring & loading dom
			//HtmlWeb web = new HtmlWeb();
			//HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			//doc = web.Load(URL);
			var doc = new HtmlWeb().Load(URL);
			var linkTags = doc.DocumentNode.Descendants("link");
			desiredLink = doc.DocumentNode.Descendants("a")
											  .Select(a => a.GetAttributeValue("href", null))
											  .Where(u => !String.IsNullOrEmpty(u) && u.StartsWith("/content")).FirstOrDefault();

			var div = doc.DocumentNode.SelectSingleNode("//div[@class='page-title']");
			if (div != null)
			{
				var bookName = div.Descendants("h1")
							   .Select(a => a.InnerText)
							   .FirstOrDefault();

				var bookSubName = div.Descendants("h2")
			   .Select(a => a.InnerText)
			   .FirstOrDefault();

				outFilename = (bookSubName != null || bookSubName != string.Empty)  ? bookName : (bookName + "--" + bookSubName);
			}
			if (outFilename != string.Empty)
				filename = outFilename;
			else
			{
				Random rnd = new Random();
				filename = "randomname_" + Convert.ToString(rnd.Next());
			}
			return desiredLink;
		}
		/*
		public static string Parse(string URL)
		{
			List<string> pageLinks = new List<string>();
			string desiredLink;
			// declaring & loading dom
			//HtmlWeb web = new HtmlWeb();
			//HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			//doc = web.Load(URL);
			var doc = new HtmlWeb().Load(URL);
			var linkTags = doc.DocumentNode.Descendants("link");
			desiredLink = doc.DocumentNode.Descendants("a")
											  .Select(a => a.GetAttributeValue("href", null))
											  .Where(u => !String.IsNullOrEmpty(u) && u.StartsWith("/content")).FirstOrDefault();

			return desiredLink;
		}*/

		//data-test
	}
}
