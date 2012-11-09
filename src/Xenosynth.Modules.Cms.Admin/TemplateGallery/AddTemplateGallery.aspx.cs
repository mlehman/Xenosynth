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

namespace Xenosynth.Admin.Content.TemplateGallery {

    public partial class AddTemplateGallery : System.Web.UI.Page {

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
            CmsTemplateGallery g = new CmsTemplateGallery();
            DataBindingManagerGallery.DataSource = g;
            DataBindingManagerGallery.PullData();

            g.ParentID = DirectoryID;

            g.SortOrder = ParentDirectory.Files.Count;

            g.Insert();

            Response.Redirect(g.FileType.BrowseUrl + "?FileID=" + g.ID);
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect(ParentDirectory.FileType.BrowseUrl + "?FileID=" + ParentDirectory.ID);
        }
    }
}
