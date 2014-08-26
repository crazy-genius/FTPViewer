using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// Добавленые в ручную...
using System.Text.RegularExpressions; // Необходимо для использования РЕГЕХП
using System.IO; // Необходимо для доступа к файлам


namespace FTPViewer
{
    public partial class SettingsEditor : Form
    {
        public  BindingSource dataBS = new BindingSource(); // Биндинг соурсе для таблици принимает значения данных либо прокси либо фтп
        public UserResult userReturn = UserResult.Abbort; // Свойство формы по умолчанию ОТмена
        public EditConfigType _edit; //  принимаем тип формы
        public SettingsEditor() // Конструктор класса 
        {
            InitializeComponent(); // Иниициализация
            dataGridView1.DataSource = dataBS; // Присвоим наш источник данных таблице
        }

        public enum EditConfigType // Список для определения типа формы )
        {
            Proxy,
            Ftp
        }

        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++) // Пройдемся циклом по выбраным строкам
            {
                try
                {

                    this.dataGridView1.Rows.Remove(this.dataGridView1.SelectedRows[i]); // Попытаемся удалить
                }
                catch (Exception ex) // Если поймали ошибку
                {
                    MessageBox.Show(ex.Message,@"ERROR"); // Пожалуемся пользоавтелю
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // Кнопка добавить
        {
            // Пока ничего не умеет :) .
            //this.dataGridView1.Rows.Add("five", "six", "seven", "eight"); this.dataGridView1.Rows.Insert(0, "one", "two", "three", "four");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            userReturn = UserResult.Save; // Присвоим свойство Сейв
            this.Close(); // Закроем форму
        }

        public enum UserResult // Наш список свойств для формы
        {
            Save, 
            Abbort
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); // Выполняем закрытие окна
        }

        private void загрузитьИзФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true; // Автоматическая проверка файла на существование 
            openFileDialog1.DefaultExt = @"*.txt"; 
            openFileDialog1.ShowDialog(); // Покажем окно выбора файла

            System.IO.FileInfo file = new System.IO.FileInfo(openFileDialog1.FileName); // Созадим экземпляр класса и присвоем ему ссылку на наш файл
            if (file.Exists) // Если файл существует
            {
                string text = System.IO.File.ReadAllText(file.FullName, Encoding.Default); // считаем файл в экземпляр класса String

                using (StringReader reader = new StringReader(text)) // Запустим чтение файла по строчно
                {
                    string line; // Данный эл-т будет принимать строки нашего файла
                    while ((line = reader.ReadLine()) != null) // пока есть строки
                    {
                        RegexOptions option = RegexOptions.IgnoreCase; // Создадим экземпляр класса и задействуем игнорирование заглавных букв
                        option = RegexOptions.Multiline; //  Задействуем мультилайн в регекспе
                        string pattern = string.Empty; // Сюда запишем паттерн для обработки строки
                        if (_edit == EditConfigType.Proxy) pattern = @"^(.*);(\d*);(.*)?;?(.*)?;?"; // Если настраиваем прокси 
                        if (_edit == EditConfigType.Ftp) pattern = @"^(.*?);(\d+);(.*?);(.*?);(.*?);(.*?);(.*?);"; // Если настриваем ФТП то ...
                        Regex newReg = new Regex(pattern, option); // Пременим регейп к строке
                        addRow(newReg.Matches(line)); // Добавим найденое в табицу
                    }
                } 
            }
        }

        private void addRow(MatchCollection matches) // добавление данных в таблицу
        {           
            if (matches.Count != 0) // Если есть что добовлять
            {
                if (_edit == EditConfigType.Proxy) // Если добовляем прокси
                {
                    ProxyClass proxy = new ProxyClass(); // Создадим экземпляр класса прокси
                    // Наполним экземпляр элементами 
                    proxy.ProxyMode = 0; 
                    proxy.AutentRequired = @"NO";
                    proxy.ProxyServer = matches[0].Groups[1].Value; // Значение из 1ой скобки 
                    proxy.ProxyPort = Convert.ToInt32(matches[0].Groups[2].Value); // Значение из 2ой скобки 
                    proxy.ProxyLogin = matches[0].Groups[3].Value; // Значение из 3ей скобки 
                    proxy.ProxyPassword = matches[0].Groups[4].Value; // Значение из 4ой скобки 
                    dataBS.Add(proxy); // Добавим объект в БиндингСоурсе
                    proxy = null; // Обнулим ссылку на объект 
                }
                else
                {
                    FTPSrvClass ftp = new FTPSrvClass();// Создадим экземпляр класса прокси
                    // Наполним экземпляр элементами 
                    ftp.FtpServer = matches[0].Groups[1].Value; // Значение из 1ой скобки 
                    ftp.FtpPort = Convert.ToInt32(matches[0].Groups[2].Value); // Значение из 2й скобки 
                    ftp.FtpLogin = matches[0].Groups[3].Value; // Значение из 3ей скобки 
                    ftp.FtpPassword = matches[0].Groups[4].Value; // Значение из 4ой скобки 
                    ftp.FtpDir = matches[0].Groups[5].Value; // Значение из 5ой скобки 
                    ftp.FtpFileName = matches[0].Groups[6].Value; // Значение из 6ой скобки 
                    ftp.FtpSaveDir = matches[0].Groups[7].Value; // Значение из 7ой скобки 
                    dataBS.Add(ftp); // Добавим объект в БиндингСоурсе
                    ftp = null; // Обнулим ссылку на объект 
                }
            }
        }

    }
}
