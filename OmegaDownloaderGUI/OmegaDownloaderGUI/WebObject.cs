using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace OmegaDownloaderGUI
{
    public class WebObject
    {
        private string m_webUrl;

        private string m_strFileName;
        public string FileName
        {
            get { return m_strFileName; }
            set { m_strFileName = value; }
        }

        private string m_strDownloadDirectory;
        public string DownloadDirectory
        {
            get { return m_strDownloadDirectory; }
            set { m_strDownloadDirectory = value; }
        }

        private string m_strFileExtension;
        public string FileExtension
        {
            get { return m_strFileExtension; }
            set { m_strFileExtension = value; }
        }

        private string m_strDownloadToDirectory = @"I:\TeahourFm";


        public WebObject(string strUrl)
        {
            m_webUrl = strUrl;
        }

        public List<string> GetHrefAddress()
        {
            HttpWebRequest request = WebRequest.Create(m_webUrl) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            StringBuilder sb = new StringBuilder();
            string rl;
            while ((rl = sr.ReadLine()) != null)
            {
                sb.Append(rl);
            }

            Regex rx = new Regex("href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(sb.ToString());

            List<string> hrefList = new List<string>();

            foreach (var mc in matches)
            {
                Console.WriteLine(mc.ToString());
                hrefList.Add(mc.ToString());
            }
            return hrefList;
        }

        public List<string> GetFileAddress()
        {
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData(m_webUrl)); 
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.UTF8);
            List<string> hrefList = new List<string>();
            List<string> retList = new List<string>();

            string strReturn = string.Empty;

            HtmlNodeCollection hnc2 = doc.DocumentNode.SelectNodes("//ul[@aria-labelledby='Episodes']//li//a");
            if (hnc2 != null)
            {
                foreach (HtmlNode link in hnc2)
                {
                    HtmlAttribute att = link.Attributes["href"];
                    hrefList.Add("http://teahour.fm" + att.Value);
                }
            }


            foreach (var item in hrefList)
            {
                WebClient subClient = new WebClient();
                HtmlDocument subDoc = new HtmlDocument();

                MemoryStream subMs = new MemoryStream(subClient.DownloadData(item.ToString()));
                subDoc.Load(subMs, Encoding.UTF8);
                HtmlNodeCollection hnc = subDoc.DocumentNode.SelectNodes("//audio/source");
                if (hnc != null && hnc.Count == 1)
                {
                    foreach (HtmlNode subItem in hnc)
                    {
                        HtmlAttribute att = subItem.Attributes["src"];
                        //m_strFileName = Path.GetFileNameWithoutExtension(att.Value);
                        //m_strDownloadDirectory = Path.GetDirectoryName(att.Value);
                        //m_strFileExtension = Path.GetExtension(att.Value);
                        retList.Add(att.Value);
                    }
                }
            }
            //HtmlNodeCollection hnc = doc.DocumentNode.SelectNodes("//audio/source");
            //if (hnc != null && hnc.Count == 1)
            //{
            //    foreach (HtmlNode item in hnc)
            //    {
            //        HtmlAttribute att = item.Attributes["src"];
            //        m_strFileName = Path.GetFileNameWithoutExtension(att.Value);
            //        m_strDownloadDirectory = Path.GetDirectoryName(att.Value);
            //        m_strFileExtension = Path.GetExtension(att.Value);
            //        strReturn = att.Value;
            //    }
            //}
            return retList;
        }

    }

}
