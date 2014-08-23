using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaDownloaderGUI
{
    public interface IWebDownloadSource
    {
        List<String> CreateDownloadList();

        void DoDownload();
    }

    public class DownloadSettings
    {
        private String _mStrInitAddress = String.Empty;

        public String InitialAddress
        {
            get { return _mStrInitAddress; }
            set { _mStrInitAddress = value; }
        }

        private int _mIntNumber;

        public int IterationNumber
        {
            get { return _mIntNumber; }
            set { _mIntNumber = value; }
        }

        private String _mStrDestinationFolder;

        public String DestinationFolder
        {
            get { return _mStrDestinationFolder; }
            set { _mStrDestinationFolder = value; }
        }

        public DownloadSettings(String strInitialAddress, int intDownloadNumbers)
        {
            _mStrInitAddress = strInitialAddress;
            _mIntNumber = intDownloadNumbers;
        }
    }

    public abstract class DownloadRule
    {
        
    }

    enum DOWNLOAD_METHOD
    {
        DIRECT_DOWNLOAD = 0,
        THUNDER_DOWNLOAD = 1
    }

    public class DownloadMethod
    {
        
    }
}
