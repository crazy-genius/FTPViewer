private void fillLocalTree()
        {
            string[] drives = Environment.GetLogicalDrives();
            for (int i = 0; i < drives.Length; i++)
            {
                treeViewLocal.Nodes.Add(drives[i]);
                treeViewLocal.Nodes[i].ImageIndex = 2;
                treeViewLocal.Nodes[i].SelectedImageIndex = 2;
            }
        }
 private void fillNode(FtpItem[] entries, TreeNode node)
        {
            List<string> folders = new List<string>();
            List<string> files = new List<string>();
            foreach (FtpItem item in entries)
            {
                if (item.ItemType == FtpItemType.Directory)
                {
                    folders.Add(item.Name);
                }
                if (item.ItemType == FtpItemType.File)
                {
                    files.Add(item.Name);
                }
            }
            for (int i = 0; i < folders.Count; i++)
            {
                node.Nodes.Add(folders[i]);
                node.Nodes[i].ImageIndex = 1;
                node.Nodes[i].SelectedImageIndex = 1;
            }
            for (int i = 0; i < files.Count; i++)
            {
                node.Nodes.Add(files[i]);
                string extension = files[i].Split('.').Last();
                if (!imageListFromTree.Images.ContainsKey(extension.ToUpper()))
                {
                    imageListFromTree.Images.Add(extension.ToUpper(), FileIconLoader.GetFileIcon(files[i]));
                }
                node.Nodes[i + folders.Count].ImageKey = extension.ToUpper();
                node.Nodes[i + folders.Count].SelectedImageKey = extension.ToUpper();
            }
            node.Expand();
        }

        private void fillNode(string path, TreeNode node)
        {
            try
            {
                string[] folders = Directory.GetDirectories(path);
                for (int i = 0; i < folders.Length; i++)
                {
                    folders[i] = folders[i].Split('\\').Last();
                }
                string[] files = Directory.GetFiles(path);
                for (int i = 0; i < files.Length; i++)
                {
                    files[i] = files[i].Split('\\').Last();
                }
                for (int i = 0; i < folders.Length; i++)
                {
                    node.Nodes.Add(folders[i]);
                    node.Nodes[i].ImageIndex = 1;
                    node.Nodes[i].SelectedImageIndex = 1;
                }
                for (int i = 0; i < files.Length; i++)
                {
                    node.Nodes.Add(files[i]);
                    string extension = files[i].Split('.').Last();
                    if (!imageListFromTree.Images.ContainsKey(extension.ToUpper()))
                    {
                        imageListFromTree.Images.Add(extension.ToUpper(), FileIconLoader.GetFileIcon(files[i]));
                    }
                    node.Nodes[i + folders.Length].ImageKey = extension.ToUpper();
                    node.Nodes[i + folders.Length].SelectedImageKey = extension.ToUpper();
                }
                node.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

   private void treeViewLocal_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (clickNode.ImageIndex >= 1 && clickNode.Nodes.Count == 0)
            {
                try
                {
                    //fillNode(clickNode.FullPath, clickNode);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "fillNode");
                }
            }
        }
        //private void treeViewFTP_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        //{
        //    if (clickNode.ImageIndex == 1 && clickNode.Nodes.Count == 0)
        //    {
        //        try
        //        {
        //            fillNode(ftp.getFileList(FTP.toFtpString(clickNode.FullPath)), clickNode);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "fillNode");
        //        }
        //    }
        //    if (clickNode.ImageIndex == -1)
        //    {
        //        try
        //        {
        //            string path = FTP.toFtpString(treeViewFTP.SelectedNode.FullPath);
        //            string[] info = ftp.getFileInfo(path).Split('~');
        //            MessageBox.Show("Name: " + info[0].Replace("/", "") + "\nSize: " + info[1] + " bytes\nDate: " + info[2], "File info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "fillNode");
        //        }
        //    }
        //}		
        //private void btnUpload_Click(object sender, EventArgs e)
        //{
        //    if (treeViewLocal.SelectedNode == null)
        //    {
        //        MessageBox.Show("Select source path for file.", "Error");
        //        return;
        //    }
        //    if (!File.Exists(treeViewLocal.SelectedNode.FullPath))
        //    {
        //        MessageBox.Show("Folder upload/download functionality not supported yet.", "Error");
        //        return;
        //    }
        //    if (treeViewFTP.SelectedNode.ImageIndex == -1)
        //    {
        //        treeViewFTP.SelectedNode = treeViewFTP.SelectedNode.Parent;
        //    }
        //    try
        //    {
        //        ftp.uploadFile(FTP.toFtpString(treeViewLocal.SelectedNode.FullPath), FTP.toFtpString(treeViewFTP.SelectedNode.FullPath) + "//" + treeViewLocal.SelectedNode.Text);
        //        treeViewFTP.SelectedNode.Nodes.Clear();
        //        fillNode(ftp.getFileList(FTP.toFtpString(treeViewFTP.SelectedNode.FullPath)), treeViewFTP.SelectedNode);
        //        foreach (TreeNode node in treeViewFTP.SelectedNode.Nodes)
        //        {
        //            if (node.Text == treeViewLocal.SelectedNode.Text)
        //                treeViewFTP.SelectedNode = node;
        //        }
        //        clickNode = treeViewFTP.SelectedNode;
        //        treeViewFTP.Select();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
 
 
 
 //  #endregion

        //private void btn_Delete_Click(object sender, EventArgs e)
        //{
        //    if (clickNode != null && clickNode.TreeView == treeViewLocal)
        //    {
        //        if (!File.Exists(treeViewLocal.SelectedNode.FullPath))
        //            MessageBox.Show("No files selected.", "Error");
        //        else
        //        {
        //            if (MessageBox.Show("You sure you want delete this file?\n" + clickNode.FullPath, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != System.Windows.Forms.DialogResult.Yes)
        //                return;
        //            try
        //            {
        //                File.Delete(treeViewLocal.SelectedNode.FullPath);
        //                treeViewLocal.SelectedNode = treeViewLocal.SelectedNode.Parent;
        //                treeViewLocal.SelectedNode.Nodes.Clear();
        //                fillNode(treeViewLocal.SelectedNode.FullPath, treeViewLocal.SelectedNode);
        //                treeViewLocal.Select();
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message, "Error");
        //            }
        //        }
        //    }
        //    if (clickNode != null && clickNode.TreeView == treeViewFTP)
        //    {
        //        if (treeViewFTP.SelectedNode.ImageIndex != -1 || treeViewFTP.SelectedNode.Parent == null)
        //            MessageBox.Show("No files selected.", "Error");
        //        else
        //        {
        //            if (MessageBox.Show("You sure you want delete this file?\n" + clickNode.FullPath, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != System.Windows.Forms.DialogResult.Yes)
        //                return;
        //            try
        //            {
        //                ftp.DeleteFile(FTP.toFtpString(treeViewFTP.SelectedNode.FullPath));
        //                treeViewFTP.SelectedNode = treeViewFTP.SelectedNode.Parent;
        //                treeViewFTP.SelectedNode.Nodes.Clear();
        //                fillNode(ftp.getFileList(FTP.toFtpString(treeViewFTP.SelectedNode.FullPath)), treeViewFTP.SelectedNode);
        //                treeViewFTP.Select();
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message, "Error");
        //            }
        //        }
        //    }
        //}
		
		
		
		
		
		1///////////////////////
		client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);

                //
                //using (Stream responseStream = response.GetResponseStream())
                //{
                //    using (Stream fileStream = File.OpenWrite(String.Format(@"C:\#FTP\{0}",_ftp.FtpFileName)))
                //    {
                //        byte[] buffer = new byte[4096];
                //        int bytesRead = responseStream.Read(buffer, 0, 4096);
                //        while (bytesRead > 0)
                //        {
                //            fileStream.Write(buffer, 0, bytesRead);
                //            DateTime nowTime = DateTime.UtcNow;
                //            if ((nowTime - startTime).TotalMinutes > 5) {
                //                throw new ApplicationException("Download time out");
                //            }
                //            bytesRead = responseStream.Read(buffer, 0, 4096);
                //        }
                //    }
                //}
                //MessageBox.Show("Download Complete");
				
				
				
		      //StringBuilder result = new StringBuilder();
                //var reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(String.Format(@"ftp://{0}//{1}{2}",_ftp.FtpServer,_ftp.FtpDir, _ftp.FtpFileName)));
                //reqFTP.Credentials = new NetworkCredential(_ftp.FtpLogin, _ftp.FtpPassword);
                //reqFTP.Proxy = new WebProxy(_proxy.ProxyServer, _proxy.ProxyPort)
                //{
                //    Credentials = new NetworkCredential(_proxy.ProxyLogin, _proxy.ProxyPassword, "")
                //};
                //reqFTP.KeepAlive = false;
                //reqFTP.UsePassive = false;
                //reqFTP.UseBinary = false;
                ////  reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails; //also tried ListDirectory
                //reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

                //DateTime startTime = DateTime.UtcNow;
                //FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse(); // Error here

                //
				
				
				
				
			
            List<FTPSrvClass> allowedFTpList = new List<FTPSrvClass>();
            List<ProxyClass> allowedProxyList = new List<ProxyClass>();

            foreach (int indexChecked in checkedFTPList.CheckedIndices)
            {
                allowedFTpList.Add(FTPSrvList[indexChecked]);
            }
   
            if (allowedFTpList.Count != 0)
            {
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
                        Random random = new Random();
                        int randomNumber = 0;
                        if (allowedProxyList.Count == 1)
                        {
                            randomNumber = 0;
                        }else randomNumber = random.Next(0, allowedProxyList.Count);

                        connectToFTP(ftpParam, allowedProxyList[randomNumber]);
                        try
                        {
                            if (ftp.IsServerConnect)
                            {
                                ftp.downloadFile(FTP.toFtpString(ftpParam.FtpDir), ftpParam.FtpFileName, @"C:\#FTP\20120723_1636_DIR_620_1.4.0_sdk-0.7.bin");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
                else
                {
                    foreach (FTPSrvClass ftpParam in allowedFTpList)
                    {
                        connectToFTP(ftpParam, null);
                        try
                        {
                            if (ftp.IsServerConnect)
                            {
                                ftp.downloadFile(FTP.toFtpString(ftpParam.FtpDir), ftpParam.FtpFileName, string.Format(@"C:\#FTP\{0}", ftpParam.FtpFileName) );
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
		
		
		
		
		
		
		
		
		
		
		