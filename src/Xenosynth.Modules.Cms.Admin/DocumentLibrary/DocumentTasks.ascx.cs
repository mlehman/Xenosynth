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

namespace Xenosynth.Modules.Cms.Admin.DocumentLibrary {
    public partial class DocumentTasks : System.Web.UI.UserControl {
        private CmsFile fileCache;

        public CmsFile CurrentFile {
            get {
                if (fileCache == null) {
                    if (ViewState["ID"] != null) {
                        fileCache = CmsFile.FindByID((Guid)ViewState["ID"]);
                    }
                }
                return fileCache;
            }
            set {
                fileCache = value;
                ViewState["ID"] = fileCache.ID;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                if (this.Visible) {
                    DataBind();
                }
            }
        }

        protected void Publish_OnClick(object sender, System.EventArgs e) {
            CurrentFile.Publish();
            Response.Redirect(CurrentFile.FileType.EditUrl + "?FileID=" + CurrentFile.ID);
        }

        protected void Unpublish_OnClick(object sender, System.EventArgs e) {
            CurrentFile.Unpublish();
            Response.Redirect(CurrentFile.FileType.EditUrl + "?FileID=" + CurrentFile.ID);
        }

        protected void Archive_OnClick(object sender, System.EventArgs e) {
            CurrentFile.Archive();
            Response.Redirect(CurrentFile.FileType.EditUrl + "?FileID=" + CurrentFile.ID);
        }

        protected void Delete_OnClick(object sender, System.EventArgs e) {
            CurrentFile.Delete();
            Response.Redirect(CurrentFile.ParentDirectory.FileType.BrowseUrl + "?FileID=" + CurrentFile.ParentDirectory.ID);
        }

        protected void LinkButtonDownload_OnClick(object sender, System.EventArgs e) {
            CmsDocument document = (CmsDocument)CurrentFile;
            document.BeginDownload();
        }
    }
}