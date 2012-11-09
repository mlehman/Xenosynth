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


namespace Xenosynth.Modules.Cms.Admin.Shortcut {
    public partial class AddShortcut : System.Web.UI.Page {

        protected RegularExpressionValidator RegularExpressionValidatorFileName;
        protected DataBindingManager DataBindingManagerShortcut;

        private CmsWebDirectory parentCache;

        public CmsWebDirectory ParentDirectory {
            get {
                if (parentCache == null) {
                    parentCache = CmsWebDirectory.FindByID(DirectoryID);
                }
                return parentCache;
            }
        }

        public Guid DirectoryID {
            get { return new Guid(Request["FileID"]); }
        }

        protected string AllowedPageExtensionsMessage {
            get {
                string extensions = string.Join(",", CmsConfiguration.Current.AllowedPageExtensions);
                return string.Format("Check file name. Be sure to use a valid page extension ( {0} )", extensions);
            }
        }

        protected string AllowedPageExtensionsRegex {
            get {
                return CmsConfiguration.Current.AllowedPageNameRegex;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                RegularExpressionValidatorFileName.ValidationExpression = AllowedPageExtensionsRegex;
                RegularExpressionValidatorFileName.ErrorMessage = AllowedPageExtensionsMessage;

            }
        }

        protected void ButtonAddShortcut_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {
                CmsShortcut s = new CmsShortcut();
                DataBindingManagerShortcut.DataSource = s;
                DataBindingManagerShortcut.PullData();

                s.ParentID = DirectoryID;

                CmsWebDirectory d = CmsWebDirectory.FindByID(DirectoryID);
                s.SortOrder = d.Files.Count;  //TODO: Pull this out into page!
                s.Insert();

                Response.Redirect(s.FileType.EditUrl + "?FileID=" + s.ID);
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect(ParentDirectory.FileType.BrowseUrl + "?FileID=" + ParentDirectory.ID);
        }

    }
}
