namespace FTPViewer
{
    partial class FTPViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTPViewer));
            this.imageListFromTree = new System.Windows.Forms.ImageList(this.components);
            this.checkedFTPList = new System.Windows.Forms.CheckedListBox();
            this.LogBox = new System.Windows.Forms.ListBox();
            this.logClearButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkedProxyList = new System.Windows.Forms.CheckedListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.restAllProxy = new System.Windows.Forms.Button();
            this.checkAllProxy = new System.Windows.Forms.Button();
            this.checkAllFTP = new System.Windows.Forms.Button();
            this.restAllFTP = new System.Windows.Forms.Button();
            this.ftpEditButton = new System.Windows.Forms.Button();
            this.proxyEditButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.inCompleteTimer = new System.Windows.Forms.Timer(this.components);
            this.AutoKillerTimer = new System.Windows.Forms.Timer(this.components);
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // imageListFromTree
            // 
            this.imageListFromTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFromTree.ImageStream")));
            this.imageListFromTree.TransparentColor = System.Drawing.Color.White;
            this.imageListFromTree.Images.SetKeyName(0, "Root.png");
            this.imageListFromTree.Images.SetKeyName(1, "folder.png");
            this.imageListFromTree.Images.SetKeyName(2, "harddrive.png");
            // 
            // checkedFTPList
            // 
            this.checkedFTPList.FormattingEnabled = true;
            this.checkedFTPList.Items.AddRange(new object[] {
            "Сервер 1",
            "Сервер 2",
            "Сервер 3"});
            this.checkedFTPList.Location = new System.Drawing.Point(9, 21);
            this.checkedFTPList.Name = "checkedFTPList";
            this.checkedFTPList.Size = new System.Drawing.Size(336, 109);
            this.checkedFTPList.TabIndex = 3;
            // 
            // LogBox
            // 
            this.LogBox.FormattingEnabled = true;
            this.LogBox.HorizontalScrollbar = true;
            this.LogBox.Location = new System.Drawing.Point(8, 232);
            this.LogBox.Name = "LogBox";
            this.LogBox.Size = new System.Drawing.Size(638, 95);
            this.LogBox.TabIndex = 4;
            // 
            // logClearButton
            // 
            this.logClearButton.Location = new System.Drawing.Point(551, 203);
            this.logClearButton.Name = "logClearButton";
            this.logClearButton.Size = new System.Drawing.Size(95, 23);
            this.logClearButton.TabIndex = 5;
            this.logClearButton.Text = "Очистить лог";
            this.logClearButton.UseVisualStyleBackColor = true;
            this.logClearButton.Click += new System.EventHandler(this.logClearButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Запустить загрузку";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Список FTP серверов";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(358, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Список прокси серверов";
            // 
            // checkedProxyList
            // 
            this.checkedProxyList.FormattingEnabled = true;
            this.checkedProxyList.Items.AddRange(new object[] {
            "Proxy Server 1",
            "Proxy Server 2",
            "Proxy Server 3"});
            this.checkedProxyList.Location = new System.Drawing.Point(361, 21);
            this.checkedProxyList.Name = "checkedProxyList";
            this.checkedProxyList.Size = new System.Drawing.Size(285, 109);
            this.checkedProxyList.TabIndex = 9;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(506, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(140, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Использовать прокси";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // restAllProxy
            // 
            this.restAllProxy.Location = new System.Drawing.Point(582, 136);
            this.restAllProxy.Name = "restAllProxy";
            this.restAllProxy.Size = new System.Drawing.Size(63, 23);
            this.restAllProxy.TabIndex = 14;
            this.restAllProxy.Text = "Сбросить";
            this.restAllProxy.UseVisualStyleBackColor = true;
            this.restAllProxy.Click += new System.EventHandler(this.restAllProxy_Click);
            // 
            // checkAllProxy
            // 
            this.checkAllProxy.Location = new System.Drawing.Point(361, 136);
            this.checkAllProxy.Name = "checkAllProxy";
            this.checkAllProxy.Size = new System.Drawing.Size(113, 23);
            this.checkAllProxy.TabIndex = 15;
            this.checkAllProxy.Text = "Отметить все";
            this.checkAllProxy.UseVisualStyleBackColor = true;
            this.checkAllProxy.Click += new System.EventHandler(this.checkAllProxy_Click);
            // 
            // checkAllFTP
            // 
            this.checkAllFTP.Location = new System.Drawing.Point(8, 136);
            this.checkAllFTP.Name = "checkAllFTP";
            this.checkAllFTP.Size = new System.Drawing.Size(113, 23);
            this.checkAllFTP.TabIndex = 17;
            this.checkAllFTP.Text = "Отметить все";
            this.checkAllFTP.UseVisualStyleBackColor = true;
            this.checkAllFTP.Click += new System.EventHandler(this.checkAllFTP_Click);
            // 
            // restAllFTP
            // 
            this.restAllFTP.Location = new System.Drawing.Point(282, 136);
            this.restAllFTP.Name = "restAllFTP";
            this.restAllFTP.Size = new System.Drawing.Size(63, 23);
            this.restAllFTP.TabIndex = 16;
            this.restAllFTP.Text = "Сбросить";
            this.restAllFTP.UseVisualStyleBackColor = true;
            this.restAllFTP.Click += new System.EventHandler(this.restAllFTP_Click);
            // 
            // ftpEditButton
            // 
            this.ftpEditButton.Location = new System.Drawing.Point(127, 136);
            this.ftpEditButton.Name = "ftpEditButton";
            this.ftpEditButton.Size = new System.Drawing.Size(149, 23);
            this.ftpEditButton.TabIndex = 18;
            this.ftpEditButton.Text = "Редактировать список";
            this.ftpEditButton.UseVisualStyleBackColor = true;
            this.ftpEditButton.Click += new System.EventHandler(this.ftpEditButton_Click);
            // 
            // proxyEditButton
            // 
            this.proxyEditButton.Location = new System.Drawing.Point(480, 136);
            this.proxyEditButton.Name = "proxyEditButton";
            this.proxyEditButton.Size = new System.Drawing.Size(98, 23);
            this.proxyEditButton.TabIndex = 19;
            this.proxyEditButton.Text = "Редактировать";
            this.proxyEditButton.UseVisualStyleBackColor = true;
            this.proxyEditButton.Click += new System.EventHandler(this.proxyEditButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(399, 165);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // inCompleteTimer
            // 
            this.inCompleteTimer.Interval = 5000;
            this.inCompleteTimer.Tick += new System.EventHandler(this.inCompleteTimer_Tick);
            // 
            // AutoKillerTimer
            // 
            this.AutoKillerTimer.Interval = 5000;
            this.AutoKillerTimer.Tick += new System.EventHandler(this.AutoKillerTimer_Tick);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(131, 170);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(263, 17);
            this.checkBox2.TabIndex = 21;
            this.checkBox2.Text = "Использовать общую диррексторию загрузки";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckStateChanged += new System.EventHandler(this.checkBox2_CheckStateChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(131, 193);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "Указать";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FTPViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(657, 339);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.proxyEditButton);
            this.Controls.Add(this.ftpEditButton);
            this.Controls.Add(this.checkAllFTP);
            this.Controls.Add(this.restAllFTP);
            this.Controls.Add(this.checkAllProxy);
            this.Controls.Add(this.restAllProxy);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.checkedProxyList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.logClearButton);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.checkedFTPList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FTPViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FTPViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageListFromTree;
        private System.Windows.Forms.CheckedListBox checkedFTPList;
        private System.Windows.Forms.ListBox LogBox;
        private System.Windows.Forms.Button logClearButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox checkedProxyList;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button restAllProxy;
        private System.Windows.Forms.Button checkAllProxy;
        private System.Windows.Forms.Button checkAllFTP;
        private System.Windows.Forms.Button restAllFTP;
        private System.Windows.Forms.Button ftpEditButton;
        private System.Windows.Forms.Button proxyEditButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer inCompleteTimer;
        private System.Windows.Forms.Timer AutoKillerTimer;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

