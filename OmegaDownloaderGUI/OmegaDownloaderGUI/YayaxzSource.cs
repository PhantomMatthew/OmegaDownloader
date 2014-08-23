using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaDownloaderGUI
{
    public class YayaxzSource : YYetsSource
    {
        public YayaxzSource(DownloadSettings ds) : base(ds)
        {
            _mDsConfiguration = ds;
            _mStrDownloadList = new List<string>();            
        }
    }
}
