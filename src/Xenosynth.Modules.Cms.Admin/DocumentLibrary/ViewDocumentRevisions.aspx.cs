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
    public partial class ViewDocumentRevisions : System.Web.UI.Page {

        protected DataGrid DataGridRevisionHistory;

        CmsFile fileCache;

        public Guid FileID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsFile CurrentFile {
            get {
                if (fileCache == null) {
                    fileCache = CmsFile.FindByID(FileID);
                }
                return fileCache;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            BindRevisionHistory();
        }

        protected void BindRevisionHistory() {
            DataGridRevisionHistory.DataSource = CurrentFile.RetrieveRevisionHistory();
            DataGridRevisionHistory.DataBind();
        }

        protected void DataGridRevisionHistory_OnDeleteCommand(object sender, DataGridCommandEventArgs e) {
            CmsFile f = CmsFile.FindByID((Guid)DataGridRevisionHistory.DataKeys[e.Item.ItemIndex]);
            f.Delete();
            BindRevisionHistory();
        }
    }

}