using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTPViewer
{
    class ProxyClass
    {
        // Скрытые переменные
        private string _proxyServer;
        private int _proxyPort = 21;
        private int _proxyMode;
        private string _proxyLogin;
        private string _proxyPassword;
        private string _autentRequired;
        // Доступ до свойств
        public int ProxyMode 
        {
            get { return _proxyMode; }
            set { _proxyMode = value; }
        }
        public string ProxyServer 
        {
            get { return _proxyServer; }
            set { _proxyServer = value; }
        }
        public int ProxyPort 
        { 
            get { return _proxyPort; }
            set { _proxyPort = value; }
        }
        public string ProxyLogin 
        { 
            get { return _proxyLogin; }
            set { _proxyLogin = value; }
        }
        public string ProxyPassword 
        {
            get { return _proxyPassword; }
            set { _proxyPassword = value; }
        }
        public string AutentRequired
        {
            get { return _autentRequired; }
            set { _autentRequired = value; }
        }
    }
}
