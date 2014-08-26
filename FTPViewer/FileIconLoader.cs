﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace FTPViewer
{
    public static class FileIconLoader
    {
        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_ICONATTRIBUTE = 0x0;
        private const uint SHGFI_USEFILEATTRIBUTES = 0x10;
        private const uint FILE_ATTRIBUTE_NORMAL = 0x80;

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath,
        uint dwFileAttributes,
        ref SHFILEINFO psfi,
        uint cbSizeFileInfo,
        uint uFlags);

        public static Icon GetFileIcon(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            fileName = "*" + extension;

            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hImg;
            hImg = SHGetFileInfo(fileName, FILE_ATTRIBUTE_NORMAL, ref shinfo,
            (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_ICONATTRIBUTE | SHGFI_USEFILEATTRIBUTES);
            try
            {
                return Icon.FromHandle(shinfo.hIcon);
            }
            catch
            {
                return null;
            }
            finally
            {
                Marshal.CleanupUnusedObjectsInCurrentContext();
                GC.Collect();
            }
        }
    }

    /*Danger zone?*/[StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };
}