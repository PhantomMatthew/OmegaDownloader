﻿using System;
using System.Windows;

namespace OmegaDownloaderGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindow()
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(System.IO.Path.GetFullPath(@"..\config\logger.config.xml"));
            log4net.Config.XmlConfigurator.Configure(fi);
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DownloadSettings thds = new DownloadSettings("http://teahour.fm", 59);
            TeahourFmSource thSource = new TeahourFmSource(thds);
            WaitingWindows waitWnd = new WaitingWindows();
            waitWnd.Show();
            thSource.DoDownload();
            waitWnd.Close();
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            OptionPanel op = new OptionPanel();
            op.Show();
        }

        private void Pingshu8Download_Click(object sender, RoutedEventArgs e)
        {
            String strBaseDownloadAddress = String.Empty;
            if (Pingshu8TextBox.Text != "")
            {
                strBaseDownloadAddress = Pingshu8TextBox.Text;
            }
            int nDownloadCount = 0;
            if (TBDownloadCount.Text != "")
            {
                nDownloadCount = Convert.ToInt32(TBDownloadCount.Text);
            }
            DownloadSettings ps8Ds = new DownloadSettings(strBaseDownloadAddress, nDownloadCount);
            Pingshu8Source ps8Source = new Pingshu8Source(ps8Ds);
            WaitingWindows waitWnd = new WaitingWindows();
            waitWnd.Show();
            ps8Source.DoDownload();
            waitWnd.Close();
        }

        private void BtnYYetsDownload_OnClickDownload_Click(object sender, RoutedEventArgs e)
        {
            String baseAddress = String.Empty;
            if (TbyyetsSeriesName.Text != String.Empty)
            {
                baseAddress = TbyyetsSeriesName.Text;
            }

            DownloadSettings yyetsDs = new DownloadSettings(baseAddress, 0);
            YYetsSource yyetsSource = new YYetsSource(yyetsDs);
            WaitingWindows waitWnd = new WaitingWindows();
            waitWnd.Show();
            yyetsSource.DoDownload();
            waitWnd.Close();
        }

        private void BtnYayaxzDownload_OnClickDownload_Click(object sender, RoutedEventArgs e)
        {
            String baseAddress = String.Empty;
            if (TbYayaxzSeriesName.Text != String.Empty)
            {
                baseAddress = TbYayaxzSeriesName.Text;
            }

            DownloadSettings yyetsDs = new DownloadSettings(baseAddress, 0);
            YYetsSource yyetsSource = new YYetsSource(yyetsDs);
            WaitingWindows waitWnd = new WaitingWindows();
            waitWnd.Show();
            yyetsSource.DoDownload();
            waitWnd.Close();
        }
    }
}
