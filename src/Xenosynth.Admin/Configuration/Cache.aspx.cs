using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

namespace Xenosynth.Admin.Configuration {
    public partial class Cache : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DirectoryInfo di = new DirectoryInfo(XenosynthContext.Current.FileCache.FileCachePath);
                if (di.Exists) {
                    DataGridFileCache.DataSource = di.GetDirectories();
                    DataGridFileCache.DataBind();
                }
            }
        }

        protected int GetFileCount(DirectoryInfo d) {
            int count = d.GetFiles().Length;
            
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis) {
                count += GetFileCount(di);
            }
            return count;
        }

        protected long GetTotalSize(DirectoryInfo d) {
            long size = 0;

            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis) {
                size += fi.Length;
            }

            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis) {
                size += GetTotalSize(di);
            }
            return size;
        }
    }
}
