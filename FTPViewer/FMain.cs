using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// Добавленные в ручную
using System.IO; // Для работы с файлами 
using System.Net.Sockets; // Для работы с сокетами
using System.Net; // Для работы с функциями среды Нет
using System.Xml; // Для работы с XML
using System.Resources; // Для работы с системными ресурсами
using System.Net.NetworkInformation; // Для работы с сетью
//using System;

namespace FTPViewer
{
    public partial class FTPViewer : Form
    {
        private int _globalCount = 0; // Счетчик файлов на загрузку используется для авто закрытия при запуске из консоли
        private string _globalPath = string.Empty; // Путь 
        // Объявим переменные для хронения XML-ок
        public XmlDocument proxyXML = new XmlDocument();
        public XmlDocument FTPXML = new XmlDocument();
        // А теперь объявим переменные для хранения списков FTP и Proxy серверов 
        List<ProxyClass> proxySrvList = new List<ProxyClass>();       
        List<FTPSrvClass> FTPSrvList = new List<FTPSrvClass>();
        //
        public enum LoadConfigType // Список для определения типа 
        {
            Proxy,
            Ftp
        }
        // Объявим экземпляр класса FTP в катором мы описали алгоритм подключения.
        // Обратим внимания что переменая не инициализирована !!.
        private FTP ftp; // Для выделения памяти нужно написать следующее "= new FTP();"
        private bool _autoClose = false; // Говорим нужно ли запускаться автоматом 
        private string _startPasth; // Запомним откуда запустились )

        public FTPViewer() // Конструктор класса
        {
            InitializeComponent();
            //startup
            _startPasth =  Environment.CurrentDirectory;

            //Загрузим из ресурсов наши XML-ки
            resourceLoad();
            
            //Объявим экземпляр класса ToolTip для отображения подсказок пользователю
            ToolTip toolTip = new ToolTip();
            // Данная функция разбирает XML-ки и достает необходимые нам данные.
            loadPreConfigFromXml(LoadConfigType.Proxy, proxyXML); // Загрузим прокси
            loadPreConfigFromXml(LoadConfigType.Ftp, FTPXML); // Загрузим ФТП

            paramKickStarter();
            

        }


        private void resourceLoad()
        {
            if (System.IO.File.Exists(string.Format(@"{0}\\ProxyList.xml", _startPasth)))
            {
                proxyXML.Load(string.Format(@"{0}\\ProxyList.xml", _startPasth));
            }
            else
            {
                proxyXML.LoadXml(Properties.Resources.ProxyList);
                logAdd(string.Format(@"Внимание файл ProxyList.xml не найден !"));
            }
            if (System.IO.File.Exists(string.Format(@"{0}\\FTPServersList.xml", _startPasth)))
            {
                FTPXML.Load(string.Format(@"{0}\\FTPServersList.xml", _startPasth));
                
            }
            else
            {
                FTPXML.LoadXml(Properties.Resources.FTPServersList);
                logAdd(string.Format(@"Внимание файл FTPServersList.xml не найден !"));
            }

        }
        private void paramKickStarter()
        {
            String[] arguments = Environment.GetCommandLineArgs();
            foreach (string arg in arguments)
            {
                if (arg.ToLower().Equals(@"\all"))
                {
                    logAdd(string.Format(@"Получен параметр {0} выделены все доступные серверы",arg));
                    setAll(checkedFTPList);
                    setAll(checkedProxyList);
                }
                if (arg.ToLower().Equals(@"\start"))
                {
                    button1_Click(this, EventArgs.Empty);
                    logAdd(string.Format(@"Получен параметр {0} программа запустит загрузку автоматически", arg));
                }
                if (arg.ToLower().Equals(@"\exit"))
                {
                    inCompleteTimer.Enabled = true;
                    AutoKillerTimer.Interval = (1000*60)*5; 
                    AutoKillerTimer.Enabled = true;
                    _autoClose = true;
                    logAdd(string.Format(@"Получен параметр {0} программа будет завершена автоматически через {1} минут", arg, (AutoKillerTimer.Interval / 60)/1000));
                }
            }
            
            
        }

        private void logAdd(string message)
        {
            LogBox.Items.Insert(0,string.Format(@"{0} - {1}",DateTime.Now.ToLongTimeString(),message)); // Опубликуем лог
        }

        private void logClear()
        {
            LogBox.Items.Clear(); // Очистим лог.
        }

        private void reloadLists(LoadConfigType type)  // Событие обрабатывает изменения в списках после закрытия окна редоктирования. 
        {
            if (type == LoadConfigType.Proxy)
            {

                if (proxySrvList.Count != 0) // Собственно если список не пуст
                {
                    checkedProxyList.Items.Clear(); // Очистим наш ЛистБокс
                    foreach (ProxyClass proxy in proxySrvList) // Для каждого элемента в proxySRVList
                    {
                        checkedProxyList.Items.Add(string.Format(@"{0} : {1}", proxy.ProxyServer, proxy.ProxyPort)); // Добавим текущий эл-т в ЛистБокс
                    }
                }
            }
            else
            {
                if (FTPSrvList.Count != 0)// Собственно если список не пуст
                {
                    checkedFTPList.Items.Clear();// Очистим наш ЛистБокс
                    foreach (FTPSrvClass srv in FTPSrvList) // Для каждого элемента в FTPSrvList
                    {
                        checkedFTPList.Items.Add(string.Format(@"{0} : {1}", srv.FtpServer, srv.FtpFileName));// Добавим текущий эл-т в ЛистБокс
                    }
                }
            }
        }

        private void savePLToXml(List<ProxyClass> ProxList) // Сохранение данных обратно в XML
        {
            if (ProxList.Count != 0)// Собственно если список не пуст
            {
                XmlNode root = proxyXML.SelectSingleNode(@"ROOT"); // Зададим корневой элемент 
                root.RemoveAll(); // Удалим все под элементы нашего корневого эл-та
                int i = 0; // Просто счетчик для нумерации.
                foreach (ProxyClass proxy in ProxList) // Для каждого эл-та списка прокси
                {
                    ++i; // Увеличим счетчик
                    // Зададим имя под элемента для элемента <ROOT>
                    XmlNode newSRV = proxyXML.CreateNode(XmlNodeType.Element, "SERVER", null); 
                    XmlAttribute id = proxyXML.CreateAttribute("ID"); // Создадим атрибут ID={i} 
                    id.Value = string.Format(@"{0}", i);
                    newSRV.Attributes.Append(id); // Присвоим данный атрибут нашему эл-ту сервер <SERVER ID={i}>
                    // Дальше наполним наш эл-т <SERVER ID={i}> под элементами
                    // Эл-т Адрес
                    XmlNode srvSubNode = proxyXML.CreateNode(XmlNodeType.Element, @"ADDRESS", null);
                    srvSubNode.InnerText = proxy.ProxyServer;
                    newSRV.AppendChild(srvSubNode); // Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null; // Пока обнулим за ненедобностью
                    // Эл-т Порт
                    srvSubNode = proxyXML.CreateNode(XmlNodeType.Element, @"PORT", null);
                    srvSubNode.InnerText = proxy.ProxyPort.ToString();
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью
                    // Эл-т Тип
                    srvSubNode = proxyXML.CreateNode(XmlNodeType.Element, @"TYPE", null);
                    srvSubNode.InnerText = proxy.ProxyMode.ToString();
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью
                    // Эл-т Авторизация
                    srvSubNode = proxyXML.CreateNode(XmlNodeType.Element, @"AUTENT", null);
                    srvSubNode.InnerText = @"No";
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью
                    // Эл-т Логин
                    srvSubNode = proxyXML.CreateNode(XmlNodeType.Element, @"LOGIN", null);
                    srvSubNode.InnerText = proxy.ProxyLogin;
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью
                    // Эл-т Пароль
                    srvSubNode = proxyXML.CreateNode(XmlNodeType.Element, @"PASS", null);
                    srvSubNode.InnerText = proxy.ProxyPassword;
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью

                    root.AppendChild(newSRV); // Присвоим наш эл-т <SERVER> с его под элеиентами в <ROOT>
                    proxyXML.AppendChild(root);// Запишем то что вышло в наш XML
                }
                logAdd(String.Format(@"Новый список сохранен в {0}", _startPasth));
                proxyXML.Save(String.Format(@"{0}\\ProxyList.xml", _startPasth)); // Запишем в файл
            }

        }
        private void saveFTPLToXml(List<FTPSrvClass> FtpList)// Сохранение данных обратно в XML
        {
            if (FtpList.Count != 0)// Собственно если список не пуст
            {

                XmlNode root = FTPXML.SelectSingleNode(@"ROOT");// Зададим корневой элемент 
                root.RemoveAll();// Удалим все под элементы нашего корневого эл-та
                int i = 0;// Просто счетчик для нумерации.
                foreach (FTPSrvClass ftp in FtpList) // Для каждого эл-та списка прокси
                {
                    ++i; // Увеличим счетчик
                    XmlNode newSRV = FTPXML.CreateNode(XmlNodeType.Element, "SERVER", null);
                    XmlAttribute id = FTPXML.CreateAttribute("ID");// Создадим атрибут ID={i} 
                    id.Value = string.Format(@"{0}", i);
                    newSRV.Attributes.Append(id);// Присвоим данный атрибут нашему эл-ту сервер <SERVER ID={i}>
                    // Дальше наполним наш эл-т <SERVER ID={i}> под элементами
                    // Эл-т Адрес
                    XmlNode srvSubNode = FTPXML.CreateNode(XmlNodeType.Element, @"ADDRESS", null);
                    srvSubNode.InnerText = ftp.FtpServer;
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью
                    // Эл-т Порт
                    srvSubNode = FTPXML.CreateNode(XmlNodeType.Element, @"PORT", null);
                    srvSubNode.InnerText = ftp.FtpPort.ToString();
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью
                    // Эл-т Дирректория
                    srvSubNode = FTPXML.CreateNode(XmlNodeType.Element, @"SRVDIR", null);
                    srvSubNode.InnerText = ftp.FtpDir;
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью
                    srvSubNode = FTPXML.CreateNode(XmlNodeType.Element, @"SAVEDIR", null);
                    srvSubNode.InnerText = ftp.FtpSaveDir;
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью
                    // Эл-т Имя Файла
                    srvSubNode = FTPXML.CreateNode(XmlNodeType.Element, @"FILE", null);
                    srvSubNode.InnerText = ftp.FtpFileName;
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью
                    // Эл-т Логин   
                    srvSubNode = FTPXML.CreateNode(XmlNodeType.Element, @"LOGIN", null);
                    srvSubNode.InnerText = ftp.FtpLogin;
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью
                    // Эл-т Пароль
                    srvSubNode = FTPXML.CreateNode(XmlNodeType.Element, @"PASS", null);
                    srvSubNode.InnerText = ftp.FtpPassword;
                    newSRV.AppendChild(srvSubNode);// Присвоим наш эл-т Серверу и добавим в конец
                    srvSubNode = null;// Пока обнулим за ненедобностью

                    root.AppendChild(newSRV);// Присвоим наш эл-т <SERVER> с его под элеиентами в <ROOT>
                    FTPXML.AppendChild(root);// Запишем то что вышло в наш XML
                }
                logAdd(String.Format(@"Новый список сохранен в {0}", _startPasth));
                FTPXML.Save(String.Format(@"{0}\\FTPServersList.xml", _startPasth)); // Запишем в файл
            }
        }
        
        private void loadPreConfigFromXml(LoadConfigType type, XmlDocument xml)
        {
            foreach (XmlNode node in xml.DocumentElement) // Проходимся по всем узлам XML
            {
                // объявим переменные
                ProxyClass proxySRV = null; 
                FTPSrvClass ftpSRV = null;
                // Определим что хотим загрузить
                if (type == LoadConfigType.Proxy) 
                {
                    proxySRV = new ProxyClass();  // После данной операции будет выделена память для класса Прокси
                }
                else
                {
                    ftpSRV = new FTPSrvClass();  // После данной операции будет выделена память для класса ФТП
                }
                foreach (XmlNode child in node.ChildNodes) // Пройдемся по детям каждого узла
                {
                    // Пройдемся по всем вложеным эл-там
                    switch (child.LocalName.ToLower())
                    {
                        // Данные узлы есть только в proxy XML
                        case @"type":
                            proxySRV.ProxyMode = Convert.ToInt32(child.InnerText);
                            break;
                        case @"autent":
                            proxySRV.AutentRequired = child.InnerText;
                            break;
                        // Данные узлы есть как в прокси xml так и в FTP xml
                        // Так-как класс не инициализирован может произайти ошибка
                        // По этому пережде всего проверим что загружаем 
                        case @"address":
                            if(type == LoadConfigType.Proxy) // Если прокси то 
                            {
                                proxySRV.ProxyServer = child.InnerText;
                            }
                            else // Если фтп то 
                            {
                                ftpSRV.FtpServer = child.InnerText;
                            }
                            break;
                        case @"port":
                            if(type == LoadConfigType.Proxy)
                            {
                                proxySRV.ProxyPort = Convert.ToInt32(child.InnerText);
                            }
                            else
                            {
                                ftpSRV.FtpPort = Convert.ToInt32(child.InnerText);
                            }
                            break;
                        case @"login":
                            if (type == LoadConfigType.Proxy)
                            {
                                proxySRV.ProxyLogin = child.InnerText;
                            }
                            else
                            {
                                ftpSRV.FtpLogin = child.InnerText;
                            }
                            break;
                        case @"pass":
                            if (type == LoadConfigType.Proxy)
                            {
                                proxySRV.ProxyPassword = child.InnerText;
                            }
                            else
                            {
                                ftpSRV.FtpPassword = child.InnerText;
                            }
                            break;
                        // В наших XML данные узлы есть только у FTP
                        case @"srvdir":
                            ftpSRV.FtpDir = child.InnerText;
                            break;
                        case @"savedir":
                            ftpSRV.FtpSaveDir = child.InnerText;
                            break;
                        case @"file":
                            ftpSRV.FtpFileName = child.InnerText;
                            break;
                    }
                }
                if (type == LoadConfigType.Proxy)
                {
                    proxySrvList.Add(proxySRV); // Занесем наш прокси сервер в список
                    proxySRV = null; // Обнулим ссылку на прокси.
                }else
                {
                    FTPSrvList.Add(ftpSRV); // Занесем наш FTP сервер в список
                    ftpSRV = null; // Обноулим ссылку на FTP.
                }

            }
            reloadLists(type);
        }

        private  void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            //Собственно отобразим прогресс.
            double bytesIn =  System.Math.Round(Convert.ToDouble(e.BytesReceived) / 1024, 3);
            double totalBytes = System.Math.Round(Convert.ToDouble(e.TotalBytesToReceive) / 1024, 3);
            double percentage = e.ProgressPercentage;
                //bytesIn / totalBytes * 100;

            if (totalBytes < 0)
            {
                totalBytes = 0;
            }
             logAdd(string.Format(@"{0}    загружено {1} из {2} Kb. {3} % завершено...",
                (string)e.UserState,
                bytesIn,
                totalBytes,
                e.ProgressPercentage));
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (_autoClose)
            {
                _globalCount = _globalCount - 1;
            }
            logAdd(string.Format(@"{0} - загрузка окончена",(string)e.UserState));
        }

        private void button1_Click(object sender, EventArgs e) // Запустить загрузку
        {
            // Переменные где будем хранить списки отмеченых прокси и ФТП
            List<FTPSrvClass> allowedFTpList = new List<FTPSrvClass>(); 
            List<ProxyClass> allowedProxyList = new List<ProxyClass>();
            // Наполним нашу переменную выбранными ФТП 
            foreach (int indexChecked in checkedFTPList.CheckedIndices)
            {
                allowedFTpList.Add(FTPSrvList[indexChecked]);
            }

            if (allowedFTpList.Count != 0) // Если выбран хот 1 ФТП то начнем магию, иначе ...  
            {
                _globalCount = allowedFTpList.Count;
                if (checkBox1.Checked != false)
                {
                    foreach (int indexChecked in checkedProxyList.CheckedIndices)
                    {
                        allowedProxyList.Add(proxySrvList[indexChecked]);
                    }
                }
                if (allowedProxyList.Count != 0)
                {
                    foreach (FTPSrvClass ftpParam in allowedFTpList)
                    {
                        Random random = new Random(); // Наш рандомайзер 
                        int randomNumber = 0; // Объявим переменную каторая будет хранить результат рандома
                        if (allowedProxyList.Count == 1) // Если выбран всего 1 Прокси сервер
                        {
                            randomNumber = 0; // Смысла рандомайза нет и по этому берем первую проксю )
                        }
                        else randomNumber = random.Next(0, allowedProxyList.Count); // Иначе рандом в бой
                        try
                        {
                            logAdd(string.Format(@"Попытка загрузки через проки {0}", allowedProxyList[randomNumber].ProxyServer));
                            ftp = new FTP(1000, ftpParam.FtpServer, ftpParam.FtpPort, ftpParam.FtpLogin, ftpParam.FtpPassword, allowedProxyList[randomNumber].ProxyServer, allowedProxyList[randomNumber].ProxyPort, allowedProxyList[randomNumber].ProxyMode);
                            //if (ftp.connect().ToLower().Equals("connected"))
                            //{                                
                                ftp.client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
                                ftp.client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                                if (checkBox2.Checked && !_globalPath.Equals(string.Empty))
                                {
                                    if (!new System.IO.DirectoryInfo(_globalPath).Exists) System.IO.Directory.CreateDirectory(_globalPath);
                                    ftp.downloadFile(ftpParam.FtpDir, ftpParam.FtpFileName, string.Format(@"{0}\{1}", _globalPath, ftpParam.FtpFileName));
                                }
                                else
                                {

                                    if (!new System.IO.DirectoryInfo(ftpParam.FtpSaveDir).Exists) System.IO.Directory.CreateDirectory(ftpParam.FtpSaveDir);
                                    ftp.downloadFile(ftpParam.FtpDir, ftpParam.FtpFileName, string.Format(@"{0}\{1}",ftpParam.FtpSaveDir , ftpParam.FtpFileName));
                                }
                                
                            //}
                        }
                        catch (Exception ex) // Ловим событие (ошибку)
                        {
                            logAdd(ex.Message); // пишем событие (ошибку) в лог
                        }
                    }
                }
                else
                {
                    foreach (FTPSrvClass ftpParam in allowedFTpList)
                    {
                        try
                        {
                            logAdd(string.Format(@"Попытка загрузки минуя прокси"));
                            ftp = new FTP(1000, ftpParam.FtpServer, ftpParam.FtpPort, ftpParam.FtpLogin, ftpParam.FtpPassword);
                            //if (ftp.connect().ToLower().Equals("connected"))
                            //{
                                ftp.client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
                                ftp.downloadFile(ftpParam.FtpDir, ftpParam.FtpFileName, string.Format(@"C:\#FTP\{0}", ftpParam.FtpFileName));
                            //}
                        }
                        catch (Exception ex) // Ловим событие (ошибку)
                        {
                            logAdd(ex.Message);// пишем событие (ошибку) в лог
                        }
                    }
                }
            }
            else logAdd("Не выбрано ни одного ФТП сервера!"); // Пожалуемся пользователю
        }

        private void restAllProxy_Click(object sender, EventArgs e) // Сбросить выбор 
        {
            rest(checkedProxyList);
        }

        private void restAllFTP_Click(object sender, EventArgs e) // Сбросить выбор 
        {
            rest(checkedFTPList);
        }

        private void checkAllProxy_Click(object sender, EventArgs e) // Выбрать все
        {
            setAll(checkedProxyList);
        }

        private void checkAllFTP_Click(object sender, EventArgs e) // Выбрать все
        {
            setAll(checkedFTPList);
        }

        private void setAll(object sender) //Выбрать все
        {
            CheckedListBox chBox = sender as CheckedListBox;
            for (int i = 0; i < chBox.Items.Count; i++)
            {
                chBox.SetItemChecked(i, true);
            }
        }

        private void rest(object sender) // Сбросить выбор 
        {
            CheckedListBox chBox = sender as CheckedListBox;
            foreach (int indexChecked in chBox.CheckedIndices)
            {
                chBox.SetItemChecked(indexChecked, false);
            }
        }

        private void ftpEditButton_Click(object sender, EventArgs e) // Редактор списка FTP
        {
            SettingsEditor SettingForm = new SettingsEditor();
            SettingForm.Text = "Редактор FTP списка";
            SettingForm.dataBS.DataSource = FTPSrvList;
            SettingForm._edit = SettingsEditor.EditConfigType.Ftp;
            SettingForm.ShowDialog();
            if (SettingForm.userReturn == SettingsEditor.UserResult.Save)
            {
                reloadLists(LoadConfigType.Ftp);
                saveFTPLToXml(FTPSrvList);
            }            
        }

        private void proxyEditButton_Click(object sender, EventArgs e) // Редактор списка Proxy
        {
            SettingsEditor proxySeditor = new SettingsEditor();
            proxySeditor.Text = @"Прокси сервер лист";
            proxySeditor.dataBS.DataSource = proxySrvList;
            proxySeditor._edit = SettingsEditor.EditConfigType.Proxy;
            proxySeditor.ShowDialog();
            if (proxySeditor.userReturn == SettingsEditor.UserResult.Save)
            {
                reloadLists(LoadConfigType.Proxy);
                savePLToXml(proxySrvList);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) // Если отметили не использовать прокси
        {
            if (!checkedProxyList.Enabled)
            {
                checkedProxyList.Enabled = true;
            }
            else checkedProxyList.Enabled = false;
        }

        private void logClearButton_Click(object sender, EventArgs e)
        {
            logClear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ftp != null)
            {
                if (ftp.client.IsBusy) ftp.client.CancelAsync();
            }
            else logAdd(@"Запущеных процессов не найдено");
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void AutoKillerTimer_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void inCompleteTimer_Tick(object sender, EventArgs e)
        {
            if (_globalCount <= 0)
            {
                Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            
            folderBrowserDialog1.ShowDialog();
            if (System.IO.Directory.Exists(folderBrowserDialog1.SelectedPath))
            {
                _globalPath = folderBrowserDialog1.SelectedPath;
                logAdd(string.Format(@"Указана общая папка {0}", folderBrowserDialog1.SelectedPath));
            }
            else
            {
                checkBox2.Checked = false;
                logAdd(string.Format(@"Не указана папка"));
            }
           
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                button3.Visible = true;
            }
            else
            {
                _globalPath = string.Empty;
                button3.Visible = false;
            }
        }
    }
}