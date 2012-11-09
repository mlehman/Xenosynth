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

using Fluent.DataBinding;
using Xenosynth.Web;
using Xenosynth.Web.UI;

namespace Xenosynth.Admin.Content.ImageGallery {

    public partial class AddGallery : System.Web.UI.Page {

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
            CmsImageGallery g = new CmsImageGallery();
            DataBindingManagerGallery.DataSource = g;
            DataBindingManagerGallery.PullData();

            g.ParentID = DirectoryID;

            CmsDirectory pd = (CmsDirectory)CmsFile.FindByID(DirectoryID);
            g.SortOrder = pd.Files.Count;

            g.Insert();

            Response.Redirect(g.FileType.BrowseUrl + "?FileID=" + g.ID);
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect(ParentDirectory.FileType.BrowseUrl + "?FileID=" + ParentDirectory.ID);
        }
    }
}
