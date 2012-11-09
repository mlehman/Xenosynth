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

using Xenosynth.Web.UI;
using Xenosynth.Modules.Cms.User;

namespace Xenosynth.Admin.Content {
    public partial class PathBrowser : System.Web.UI.UserControl {

        public CmsFile CurrentFile;
        public string DefaultUrl;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (this.Visible) {
                    DataBind();
                    BindPath();
                    
                    RecentFiles.AddFile(CurrentFile);
                }
            }
        }

        public string GetDefaultUrl(object o) {
            CmsFile file = (CmsFile)o;
            if (DefaultUrl == null) {
                return file.DefaultActionUrl;
            } else {
                return DefaultUrl + "?FileID=" + file.ID;
            }
        }

        public string[] AvaliableViews {
            get {
                string[] views = new string[DropDownListView.Items.Count];
                for(int i=0; i < views.Length; i++) {
                    views[i] = DropDownListView.Items[i].Value;
                }
                return views;
            }
            set {
                DropDownListView.DataSource = value;
                DropDownListView.DataBind();
            }
        }

        public string SelectedView {
            get {
                return DropDownListView.SelectedValue;
            }
        }

        private void BindPath() {

            if (CurrentFile == null) {
                throw new NullReferenceException("CurrentFile is required");
            }

            ArrayList directories = new ArrayList();
            CmsFile c = CurrentFile;
            directories.Add(c);
            while (!c.IsRoot) {
                c = c.ParentDirectory;
                directories.Insert(0, c);
            }

            RepeaterDirectoryPath.DataSource = directories;
            RepeaterDirectoryPath.DataBind();
        }
    }
}