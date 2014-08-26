using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTPViewer
{
    class FTPSrvClass
    {
        // Скрытые переменные
        private int timeout = 30000;
        private string _ftpServer;
        private int _ftpPort = 21;
        private string _ftpLogin;
        private string _ftpPassword;
        private string _ftpDir;
        private string _saveDir;
        private string _ftpFileName;
        // Доступ до свойств
        public int Timeout
        {
            get { return timeout; }
        }
        public string FtpServer {
            get { return _ftpServer; }
            set { _ftpServer = value; }
        }
        public int FtpPort 
        { 
            get { return _ftpPort; }
            set { _ftpPort = value; }
        }
        public string FtpLogin
        {
            get { return _ftpLogin; }
            set { _ftpLogin = value; }
        }
        public string FtpPassword
        {
            get { return _ftpPassword; }
            set { _ftpPassword = value; }
        }
        public string FtpDir
        {
            get { return _ftpDir; }
            set { _ftpDir = value; }
        }
        public string FtpSaveDir
        {
            get { return _saveDir; }
            set { _saveDir = value; }
        }
        public string FtpFileName
        {
            get { return _ftpFileName; }
            set { _ftpFileName = value; }
        }
    }
}
