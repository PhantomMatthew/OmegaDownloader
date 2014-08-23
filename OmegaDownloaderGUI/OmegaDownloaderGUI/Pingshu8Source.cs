using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Windows;

namespace OmegaDownloaderGUI
{
    public class Pingshu8Source: IWebDownloadSource
    {
        private static String _pingshu8BaseAddress = "http://www.pingshu8.com";
        DownloadSettings m_dsConfiguration = null;
        List<String> m_strDownloadList;
        private String _mStrXpath = String.Empty;

        public Pingshu8Source(DownloadSettings ds)
        {
            m_dsConfiguration = ds;
            m_strDownloadList = new List<String>();
        }

        public void CreateDownloadObject(DownloadSettings ds)
        {

        }

        public void CreateDownloadRule()
        {

        }

        private List<String> GenerateTrueAddress()
        {
            List<String> lstDownloadAddressList = new List<string>();

            String strFullBaseAddress = m_dsConfiguration.InitialAddress;
            WebClient client = new WebClient();
            HtmlDocument doc = new HtmlDocument();
            lstDownloadAddressList.Add(strFullBaseAddress);
            _mStrXpath = "//div[@class='tab6']/div[@class='tab']//a[last()]";
            //_mStrXpath = "//div[@class='tab']//a[last()]";

            for (int i = 0; i < m_dsConfiguration.IterationNumber - 1; i++)
            {
                MemoryStream ms = new MemoryStream(client.DownloadData(strFullBaseAddress));
                doc.Load(ms, Encoding.GetEncoding("gb2312"));
                HtmlNodeCollection webAddressCollection =
                    doc.DocumentNode.SelectNodes(_mStrXpath);
                if (webAddressCollection != null)
                {
                    foreach (HtmlNode webNode in webAddressCollection)
                    {
                        HtmlAttribute fullAddressAttribute = webNode.Attributes["href"];
                        strFullBaseAddress = _pingshu8BaseAddress + fullAddressAttribute.Value;
                        lstDownloadAddressList.Add(strFullBaseAddress);
                    }
                }
            }
            return lstDownloadAddressList;
        }

        public List<String> CreateDownloadList()
        {
            List<String> trueList = GenerateTrueAddress();
            List<String> hrefList = new List<string>();

            foreach (String item in trueList)
            {
                WebClient client = new WebClient();
                MemoryStream ms = new MemoryStream(client.DownloadData(item));
                HtmlDocument doc = new HtmlDocument();

                doc.Load(ms, Encoding.GetEncoding("gb2312"));

                HtmlNodeCollection hnc2 = doc.DocumentNode.SelectNodes("//div/a[@thunderpid]");
                if (hnc2 != null)
                {
                    foreach (HtmlNode link in hnc2)
                    {
                        HtmlAttribute att = link.Attributes["thunderHref"];
                        hrefList.Add(att.Value);
                    }
                }
            }

            return hrefList;
        }

        public void DoDownload()
        {
            string downloadStr = string.Empty;
            m_strDownloadList = CreateDownloadList();
            foreach (var item in m_strDownloadList)
            {
                downloadStr += item;
                downloadStr += "\n";
            }
            Clipboard.SetDataObject(downloadStr);    
        }
    }
}
