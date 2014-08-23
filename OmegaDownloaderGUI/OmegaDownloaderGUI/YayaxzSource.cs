using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using System.Windows;

namespace OmegaDownloaderGUI
{
    public class YayaxzSource : IWebDownloadSource
    {
        protected DownloadSettings _mDsConfiguration = null;
        protected List<String> _mStrDownloadList;
        protected String _mStrXpath = String.Empty;
        public string downloadStr;

        public YayaxzSource(DownloadSettings ds) 
        {
            _mDsConfiguration = ds;
            _mStrDownloadList = new List<String>();
        }

        public List<String> CreateDownloadList()
        {
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData(_mDsConfiguration.InitialAddress));
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.UTF8);
            List<string> hrefList = new List<string>();
            string strReturn = string.Empty;

            _mStrXpath = "//dd[@data-format='HR-HDTV']//a[@data-download-type='1']";

            HtmlNodeCollection hnc2 = doc.DocumentNode.SelectNodes(_mStrXpath);
            if (hnc2 != null)
            {
                foreach (HtmlNode link in hnc2)
                {
                    HtmlAttribute att = link.Attributes["href"];
                    hrefList.Add(att.Value);
                }
            }
            return hrefList;
        }

        public void DoDownload()
        {
            downloadStr = string.Empty;
            _mStrDownloadList = CreateDownloadList();
            foreach (var item in _mStrDownloadList)
            {
                downloadStr += item;
                downloadStr += "\n";
            }
            Clipboard.SetDataObject(downloadStr);

        }
    }
}
