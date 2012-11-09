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

namespace Xenosynth.Admin.Content.Page {
    public partial class ViewPageRevisions : System.Web.UI.Page {

        CmsPage pageCache;

        public Guid PageID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsPage CurrentPage {
            get {
                if (pageCache == null) {
                    pageCache = CmsPage.FindByID(PageID);
                }
                return pageCache;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            BindRevisionHistory();
        }

        protected void BindRevisionHistory() {
            DataGridRevisionHistory.DataSource = CurrentPage.RetrieveRevisionHistory();
            DataGridRevisionHistory.DataBind();
        }

        protected void DataGridRevisionHistory_OnDeleteCommand(object sender, DataGridCommandEventArgs e) {
            CmsPage p = CmsPage.FindByID((Guid)DataGridRevisionHistory.DataKeys[e.Item.ItemIndex]);
            p.Delete();
            BindRevisionHistory();
        }
    }
}
