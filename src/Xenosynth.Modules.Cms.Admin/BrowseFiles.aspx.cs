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

using Xenosynth.Web;
using Xenosynth.Web.UI;
using Xenosynth.Admin.Controls;
using Fluent;

namespace Xenosynth.Modules.Cms.Admin {
    public partial class BrowseFiles : System.Web.UI.Page {

        private CmsDirectory cmsDirectoryCache;

        public CmsDirectory CurrentDirectory {
            get {
                if (cmsDirectoryCache == null) {
                    Guid id = (Guid)ViewState["cmsDirectoryID"];
                    cmsDirectoryCache = (CmsDirectory)CmsFile.FindByID(id);
                }
                return cmsDirectoryCache;
            }
            set {
                cmsDirectoryCache = value;
                ViewState["cmsDirectoryID"] = value.ID;
            }
        }

        private void AddLink(string href) {
            HtmlLink link = new HtmlLink();
            link.Attributes["type"] = "text/css";
            link.Attributes["rel"] = "stylesheet";
            link.Href = ResolveUrl(href);
            Page.Header.Controls.AddAt(0, link);
        }


        protected void Page_Load(object sender, System.EventArgs e) {

            if (!IsPostBack) {
                if (Request["FileID"] != null && !Guid.Empty.Equals(Request["FileID"])) {
                    CurrentDirectory = (CmsDirectory)CmsFile.FindByID(new Guid(Request["FileID"]));
                } else {
                    CurrentDirectory = CmsContext.Current.RootDirectory;
                }
                DataGridAdapterFiles.SortExpression = "SortOrder";


            }
            //Rebind rather than use viewstate
            BindDataGridFiles();

            AddLink("~/Css/style.css");
            AddLink("~/Css/icons.css");
            foreach (RegisteredModule registeredModule in XenosynthContext.Current.Modules) {
                AddLink(registeredModule.ResourceFolder + "/Css/style.css");
            }
        }


        private void BindDataGridFiles() {
            DataGridAdapterFiles.BindDataGrid();
        }

        public void DataGridAdapterFiles_DataGridBinding(DataGridAdapter sender, DataGridBindingEventArgs e) {
            CmsHttpContext.Current.Mode = CmsMode.Unpublished;
            DataGridAdapterFiles.DataSource = CmsFile.FindByDirectoryID(CurrentDirectory.ID, true);
        }



        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion
    }
}
