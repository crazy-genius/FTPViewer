using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace FTPViewer
{
    public class FTP
    {
        //Сам клиент ФТП
        public WebClient client = new WebClient();
        public FtpWebRequest ftpReq; 

        private int _timeoutFTP;
        private string _ftpServer;     			//ФТП сервер
        private int _ftpPort = 21;              //порт для передачи данных
        private string _ftpLogin;               //логин на ФТП
        private string _ftpPassword;            //пароль на ФТП
        private bool _useProxy = false;               // Прокси используем?

        public bool IsServerConnect { get; set; } // А подкючились ли мы ? 

        public FTP(int timeoutFTP, string ftpServer, int ftpPort, string ftpLogin, string ftpPassword)
        {
            _timeoutFTP = timeoutFTP;
            _ftpServer = ftpServer; 
            _ftpPort = ftpPort;
            _ftpLogin = ftpLogin;
            // Иногда при соеденении без авторизации при передаче парпметров null в пароле приводит к ошибке
            // По этому проверим и если Null заменим на пустую строку.
            if (ftpPassword == null)
            {
                _ftpPassword = string.Empty;
            }else _ftpPassword = ftpPassword;
            
            client.BaseAddress = new Uri(String.Format(@"ftp://{0}", ftpServer)).ToString();
            client.Credentials = new NetworkCredential(ftpLogin, ftpPassword);
            _useProxy = false;
        }

        public FTP(int timeoutFTP, string ftpServer, int ftpPort, string ftpLogin, string ftpPassword, string serverProxy, int portProxy, int proxyMode)
        {
            _timeoutFTP = timeoutFTP;
            _ftpServer = ftpServer; 
            _ftpPort = ftpPort;
            _ftpLogin = ftpLogin;
            // Иногда при соеденении без авторизации при передаче парпметров null в пароле приводит к ошибке
            // По этому проверим и если Null заменим на пустую строку.
            if (ftpPassword == null)
            {
                _ftpPassword = string.Empty;
            }
            else _ftpPassword = ftpPassword;
            client.BaseAddress = new Uri(String.Format(@"ftp://{0}", ftpServer)).ToString();
            client.Credentials = new NetworkCredential(ftpLogin, ftpPassword);
            // Созданм эекземпляр класса (Веб прокси)
            WebProxy pinfo = new WebProxy(serverProxy, portProxy);

            //Присваиваем параметры прокси клиенту.
            client.Proxy = pinfo;
            _useProxy = true;
            //Присвоим экземпляру класса Null чтоб не жрал (т.е не кушал) память.
            pinfo = null; 
        }

        public FTP(int timeoutFTP, string ftpServer, int ftpPort, string ftpLogin, string ftpPassword, string serverProxy, int portProxy, int proxyMode, string proxyLogin, string proxyPassword)
        {

            // Иногда при соеденении без авторизации при передаче парпметров null в пароле приводит к ошибке
            // По этому проверим и если Null заменим на пустую строку.
            _timeoutFTP = timeoutFTP;
            _ftpServer = ftpServer; 
            _ftpPort = ftpPort;
            _ftpLogin = ftpLogin;
            if (ftpPassword == null)
            {
                _ftpPassword = string.Empty;
            }
            else _ftpPassword = ftpPassword;
            client.BaseAddress = new Uri(String.Format(@"ftp://{0}//{1}", ftpServer)).ToString();
            client.Credentials = new NetworkCredential(ftpLogin, ftpPassword);
            // Созданм эекземпляр класса (Веб прокси)
            WebProxy pinfo = new WebProxy(serverProxy, portProxy);
            //Указываем параметры авторизации для прокси.
            pinfo.Credentials = new  NetworkCredential(proxyLogin, proxyPassword, "");
            //Присваиваем параметры прокси клиенту.
            client.Proxy = pinfo;
            _useProxy = true;
            //Присвоим экземпляру класса Null чтоб не жрал (т.е не кушал) память.            
            pinfo = null;
        }

        public string connect()
        {
            try
            {
                string res = client.DownloadString(new Uri(String.Format(@"ftp://{0}",_ftpServer)));
                IsServerConnect = true;
                return "connected";
            }
            catch (Exception ex)
            {
                IsServerConnect = false;
                return ex.Message;
            }
        }


        //public string disconnect()
        //{
        //    try
        //    {
        //        client.di
        //        client.Disconnect(_timeoutFTP);
        //        IsServerConnect = false;
        //        return "Successfylly disconnected";
        //    }
        //    catch (Exception ex)
        //    {
        //        IsServerConnect = true;
        //        return ex.Message;
        //    }
        //}

        //public FtpItem[] getFileList()
        //{
        //    try
        //    {
        //        return client.GetDirectoryList(_timeoutFTP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public FtpItem[] getFileList(string folder)
        //{
        //    try
        //    {
        //        return client.GetDirectoryList(_timeoutFTP, folder);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public string downloadFile(string srcPath, string fName, string savePath)
        {

            try
            {
                client.DownloadFileAsync(new Uri(String.Format(@"ftp://{0}//{1}{2}", _ftpServer, srcPath, fName)), string.Format(@"{0}",savePath));
                return ("Запущена асинхронная загрузка");
            }
            catch (Exception ex)
            {
                return ex.Message;  // Пожалуемся пользователю.
            }
        }

        //public string uploadFile(string srcPath, string savePath)
        //{
        //    try
        //    {
        //        client.PutFile(_timeoutFTP, savePath, srcPath);
        //        return "File successfully uploaded at location:\n" +savePath;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        //public static string toFtpString(string str)
        //{
        //    return str.Replace("FTP", "").Replace("\\\\", "\\").Replace("\\", "//");
        //}

        //public void DeleteFile(string path)
        //{
        //    client.DeleteFile(_timeoutFTP, path);
        //}

        //public string getFileInfo(string path)
        //{
        //    FtpItem item = client.GetDirectoryList(_timeoutFTP, path)[0];
        //    return item.Name + "~" + item.Size + "~" + item.Date.ToShortDateString();
        //}
    }
}
