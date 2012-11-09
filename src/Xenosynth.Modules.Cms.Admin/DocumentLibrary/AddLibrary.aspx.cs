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
using Fluent.DataBinding;

namespace Xenosynth.Modules.Cms.Admin.DocumentLibrary {
    public partial class AddLibrary : System.Web.UI.Page {

        protected DataBindingManager DataBindingManagerDocumentLibrary;

        private CmsDirectory parentCache;

        public CmsDirectory ParentDirectory {
            get {
                if (parentCache == null) {
                    parentCache = (CmsDirectory)CmsFile.FindByID(DirectoryID);
                }
                return parentCache;
            }
        }

        public Guid DirectoryID {
            get { return new Guid(Request["FileID"]); }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
            }
        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            CmsDocumentLibrary d = new CmsDocumentLibrary();
            DataBindingManagerDocumentLibrary.DataSource = d;
            DataBindingManagerDocumentLibrary.PullData();

            d.ParentID = DirectoryID;

            CmsDirectory pd = (CmsDirectory)CmsFile.FindByID(DirectoryID);
            d.SortOrder = pd.Files.Count;

            d.Insert();

            Response.Redirect(d.FileType.BrowseUrl + "?FileID=" + d.ID);
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect(ParentDirectory.FileType.BrowseUrl + "?FileID=" + ParentDirectory.ID);
        }
    }
}
