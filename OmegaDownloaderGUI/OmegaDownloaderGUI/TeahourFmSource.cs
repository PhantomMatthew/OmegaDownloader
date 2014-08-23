using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Windows;

namespace OmegaDownloaderGUI
{
    public class TeahourFmSource : IWebDownloadSource
    {
        DownloadSettings m_dsConfiguration = null;
		List<String> m_strDownloadList;

        public TeahourFmSource(DownloadSettings ds)
        {
            m_dsConfiguration = ds;
            m_strDownloadList = new List<String>();
        }
        
        public List<String> CreateDownloadList()
		{
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData(m_dsConfiguration.InitialAddress)); 
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.UTF8);
            List<string> hrefList = new List<string>();
            List<string> retList = new List<string>();

            string strReturn = string.Empty;

            String _mStrXpath = "//ul[@aria-labelledby='Episodes']//li//a";
            //HtmlNodeCollection hnc2 = doc.DocumentNode.SelectNodes("//ul[@aria-labelledby='Episodes']//li//a");
            HtmlNodeCollection hnc2 = doc.DocumentNode.SelectNodes(_mStrXpath);
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
                        retList.Add(att.Value);
                    }
                }
            }

            return retList;		
		}

        public void DoDownload()
		{
            m_strDownloadList = CreateDownloadList();

            WebClient downloadClient = new WebClient();

            int i = 1;
            foreach (String item in m_strDownloadList)
            {
                char[] charSplitter = {'/', '.'};
                String[] strToSplit = item.Split(charSplitter);
                downloadClient.DownloadFile(item ,"e:\\" + strToSplit[strToSplit.Count() - 2] +".m4a");
            }
		}
    }
}
