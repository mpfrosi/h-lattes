using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;
using System.Net;
using CurriculoParser;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace HLattes
{
    public partial class FrmNavegator : Form
    {
        public FrmNavegator()
        {
            InitializeComponent();
        }

        public void SetURL(string _strUrl)
        {
            //button1_Click(this, EventArgs.Empty);
            this.webBrowser1.Navigate(_strUrl);            
            //this.webBrowser1.Navigate("http://sorry.google.com/sorry/?continue=http://scholar.google.com/schhp%3Fhl%3Den%26tab%3Dws");    
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            bool blnPassou;
            using (StreamReader stmReader = new StreamReader(this.webBrowser1.DocumentStream))
            {
                this.webBrowser1.DocumentStream.Seek(0, SeekOrigin.Begin);
                string strContent = stmReader.ReadToEnd();
                blnPassou = !ScholarParser.IsSorryPage(strContent);
            }

            Parser.CoockieHeader = this.webBrowser1.Document.Cookie;

            if (blnPassou)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            ProcessStartInfo udtProcessInfo = new ProcessStartInfo("RunDll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 2");
            udtProcessInfo.CreateNoWindow = true;

            using (Process procClearCookies = Process.Start(udtProcessInfo))
            {
                procClearCookies.WaitForExit();
            }
            
            //StringBuilder coockie = new StringBuilder(256);
            //int tt = 0;
            //DeleteCache.InternetGetCookie(this.webBrowser1.Url.ToString(), "as", coockie, ref tt);
            //if (tt > 0)
            //{
            //    coockie = new StringBuilder(tt);
            //    DeleteCache.InternetGetCookie(this.webBrowser1.Url.ToString(), "sorry", coockie, ref tt);
            //DeleteCache.InternetSetCookie(".google.com", "S", "1; expires = Wed, 30-May-2007 09:21:15");
            //DeleteCache.InternetSetCookie(".google.com", "SID", "1; expires = Wed, 30-May-2007 09:21:15");
            //DeleteCache.InternetSetCookie(".google.com", "NID", "1; expires = Wed, 30-May-2007 09:21:15");
            //DeleteCache.InternetSetCookie(".google.com", "PREF", "1; expires = Wed, 30-May-2007 09:21:15");
            //DeleteCache.InternetSetCookie(".google.com", "GSP", "1; expires = Wed, 30-May-2007 09:21:15");
            //}GSP
            //DeleteCache.Deleta();

            //if (webBrowser1.Document != null)
            //{
            //    object result = webBrowser1.Document.InvokeScript("document.cookie=\"GDSESS=; expires = Wed, 30-May-2007 09:21:15\";");
            //    webBrowser1.Document.Cookie = "GDSESS=1;" + "; expires = Wed, 30-May-2007 09:21:15";
            //    webBrowser1.Document.Cookie = "PREF=1;" + "; expires = Wed, 30-May-2007 09:21:15";
            //}

            udtProcessInfo = new ProcessStartInfo("iexplore", "http://www.google.com/ncr");
            udtProcessInfo.CreateNoWindow = true;
            udtProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;

            using (Process procSetCookie = Process.Start(udtProcessInfo))
            {
                procSetCookie.WaitForExit(3000);
                procSetCookie.Kill();
            }

            m_blnPassou = false;

            WebBrowser wb = new WebBrowser();
            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
            wb.Navigate("http://www.google.com/ncr");

            DateTime dtmInicio = DateTime.Now;

            while (!m_blnPassou)
            {
                if (((TimeSpan)(DateTime.Now - dtmInicio)).TotalMilliseconds > 10000)
                {
                    break;
                }

                Application.DoEvents();
                Thread.Sleep(100);
            }

            //wb.Dispose();

            webBrowser1.Refresh(WebBrowserRefreshOption.Completely);

            this.Cursor = Cursors.Default;
        }
        
        bool m_blnPassou = false;

        void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            m_blnPassou = true;
        }             
    }

    // Class for deleting the cache.
    //class DeleteCache
    //{
    //    // For PInvoke: Contains information about an entry in the Internet cache
    //    [StructLayout(LayoutKind.Explicit, Size = 80)]
    //    public struct INTERNET_CACHE_ENTRY_INFOA
    //    {
    //        [FieldOffset(0)]
    //        public uint dwStructSize;
    //        [FieldOffset(4)]
    //        public IntPtr lpszSourceUrlName;
    //        [FieldOffset(8)]
    //        public IntPtr lpszLocalFileName;
    //        [FieldOffset(12)]
    //        public uint CacheEntryType;
    //        [FieldOffset(16)]
    //        public uint dwUseCount;
    //        [FieldOffset(20)]
    //        public uint dwHitRate;
    //        [FieldOffset(24)]
    //        public uint dwSizeLow;
    //        [FieldOffset(28)]
    //        public uint dwSizeHigh;
    //        [FieldOffset(32)]
    //        public FILETIME LastModifiedTime;
    //        [FieldOffset(40)]
    //        public FILETIME ExpireTime;
    //        [FieldOffset(48)]
    //        public FILETIME LastAccessTime;
    //        [FieldOffset(56)]
    //        public FILETIME LastSyncTime;
    //        [FieldOffset(64)]
    //        public IntPtr lpHeaderInfo;
    //        [FieldOffset(68)]
    //        public uint dwHeaderInfoSize;
    //        [FieldOffset(72)]
    //        public IntPtr lpszFileExtension;
    //        [FieldOffset(76)]
    //        public uint dwReserved;
    //        [FieldOffset(76)]
    //        public uint dwExemptDelta;
    //    }

    //    // For PInvoke: Initiates the enumeration of the cache groups in the Internet cache
    //    [DllImport(@"wininet",
    //        SetLastError = true,
    //        CharSet = CharSet.Auto,
    //        EntryPoint = "FindFirstUrlCacheGroup",
    //        CallingConvention = CallingConvention.StdCall)]
    //    public static extern IntPtr FindFirstUrlCacheGroup(
    //        int dwFlags,
    //        int dwFilter,
    //        IntPtr lpSearchCondition,
    //        int dwSearchCondition,
    //        ref long lpGroupId,
    //        IntPtr lpReserved);

    //    // For PInvoke: Retrieves the next cache group in a cache group enumeration
    //    [DllImport(@"wininet",
    //        SetLastError = true,
    //        CharSet = CharSet.Auto,
    //        EntryPoint = "FindNextUrlCacheGroup",
    //        CallingConvention = CallingConvention.StdCall)]
    //    public static extern bool FindNextUrlCacheGroup(
    //        IntPtr hFind,
    //        ref long lpGroupId,
    //        IntPtr lpReserved);

    //    // For PInvoke: Releases the specified GROUPID and any associated state in the cache index file
    //    [DllImport(@"wininet",
    //        SetLastError = true,
    //        CharSet = CharSet.Auto,
    //        EntryPoint = "DeleteUrlCacheGroup",
    //        CallingConvention = CallingConvention.StdCall)]
    //    public static extern bool DeleteUrlCacheGroup(
    //        long GroupId,
    //        int dwFlags,
    //        IntPtr lpReserved);

    //    // For PInvoke: Begins the enumeration of the Internet cache
    //    [DllImport(@"wininet",
    //        SetLastError = true,
    //        CharSet = CharSet.Auto,
    //        EntryPoint = "FindFirstUrlCacheEntryA",
    //        CallingConvention = CallingConvention.StdCall)]
    //    public static extern IntPtr FindFirstUrlCacheEntry(
    //        [MarshalAs(UnmanagedType.LPTStr)] string lpszUrlSearchPattern,
    //        IntPtr lpFirstCacheEntryInfo,
    //        ref int lpdwFirstCacheEntryInfoBufferSize);

    //    // For PInvoke: Retrieves the next entry in the Internet cache
    //    [DllImport(@"wininet",
    //        SetLastError = true,
    //        CharSet = CharSet.Auto,
    //        EntryPoint = "FindNextUrlCacheEntryA",
    //        CallingConvention = CallingConvention.StdCall)]
    //    public static extern bool FindNextUrlCacheEntry(
    //        IntPtr hFind,
    //        IntPtr lpNextCacheEntryInfo,
    //        ref int lpdwNextCacheEntryInfoBufferSize);

    //    // For PInvoke: Removes the file that is associated with the source name from the cache, if the file exists
    //    [DllImport(@"wininet",
    //        SetLastError = true,
    //        CharSet = CharSet.Auto,
    //        EntryPoint = "DeleteUrlCacheEntryA",
    //        CallingConvention = CallingConvention.StdCall)]
    //    public static extern bool DeleteUrlCacheEntry(
    //        IntPtr lpszUrlName);

    //    [DllImport("wininet.dll", SetLastError = true)]
    //    public static extern bool InternetGetCookie(
    //      string url, string cookieName, StringBuilder cookieData, ref int size);

    //    [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
    //    public static extern bool InternetSetCookie(string lpszUrl, string lpszCookieName, string lpszCookieData);

    //    public static void Deleta()
    //    {
    //        // Indicates that all of the cache groups in the user's system should be enumerated
    //        const int CACHEGROUP_SEARCH_ALL = 0x0;
    //        // Indicates that all the cache entries that are associated with the cache group
    //        // should be deleted, unless the entry belongs to another cache group.
    //        const int CACHEGROUP_FLAG_FLUSHURL_ONDELETE = 0x2;
    //        // File not found.
    //        const int ERROR_FILE_NOT_FOUND = 0x2;
    //        // No more items have been found.
    //        const int ERROR_NO_MORE_ITEMS = 259;
    //        // Pointer to a GROUPID variable
    //        long groupId = 0;

    //        // Local variables
    //        int cacheEntryInfoBufferSizeInitial = 0;
    //        int cacheEntryInfoBufferSize = 0;
    //        IntPtr cacheEntryInfoBuffer = IntPtr.Zero;
    //        INTERNET_CACHE_ENTRY_INFOA internetCacheEntry;
    //        IntPtr enumHandle = IntPtr.Zero;
    //        bool returnValue = false;

    //        // Delete the groups first.
    //        // Groups may not always exist on the system.
    //        // For more information, visit the following Microsoft Web site:
    //        // http://msdn.microsoft.com/library/?url=/workshop/networking/wininet/overview/cache.asp			
    //        // By default, a URL does not belong to any group. Therefore, that cache may become
    //        // empty even when the CacheGroup APIs are not used because the existing URL does not belong to any group.			
    //        enumHandle = FindFirstUrlCacheGroup(0, CACHEGROUP_SEARCH_ALL, IntPtr.Zero, 0, ref groupId, IntPtr.Zero);
    //        // If there are no items in the Cache, you are finished.
    //        if (enumHandle != IntPtr.Zero && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
    //            return;

    //        // Loop through Cache Group, and then delete entries.
    //        while (true)
    //        {
    //            // Delete a particular Cache Group.
    //            returnValue = DeleteUrlCacheGroup(groupId, CACHEGROUP_FLAG_FLUSHURL_ONDELETE, IntPtr.Zero);
    //            if (!returnValue && ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error())
    //            {
    //                returnValue = FindNextUrlCacheGroup(enumHandle, ref groupId, IntPtr.Zero);
    //            }

    //            if (!returnValue && (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error()))
    //                break;
    //        }

    //        // Start to delete URLs that do not belong to any group.
    //        enumHandle = FindFirstUrlCacheEntry(null, IntPtr.Zero, ref cacheEntryInfoBufferSizeInitial);
    //        if (enumHandle == IntPtr.Zero && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
    //            return;

    //        cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
    //        cacheEntryInfoBuffer = Marshal.AllocHGlobal(cacheEntryInfoBufferSize);
    //        enumHandle = FindFirstUrlCacheEntry(null, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);

    //        while (true)
    //        {
    //            internetCacheEntry = (INTERNET_CACHE_ENTRY_INFOA)Marshal.PtrToStructure(cacheEntryInfoBuffer, typeof(INTERNET_CACHE_ENTRY_INFOA));

    //            cacheEntryInfoBufferSizeInitial = cacheEntryInfoBufferSize;
    //            returnValue = DeleteUrlCacheEntry(internetCacheEntry.lpszSourceUrlName);
    //            if (!returnValue)
    //            {
    //                returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
    //            }
    //            if (!returnValue && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
    //            {
    //                break;
    //            }
    //            if (!returnValue && cacheEntryInfoBufferSizeInitial > cacheEntryInfoBufferSize)
    //            {
    //                cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
    //                cacheEntryInfoBuffer = Marshal.ReAllocHGlobal(cacheEntryInfoBuffer, (IntPtr)cacheEntryInfoBufferSize);
    //                returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
    //            }
    //        }
    //        Marshal.FreeHGlobal(cacheEntryInfoBuffer);
    //    }
    //}

}