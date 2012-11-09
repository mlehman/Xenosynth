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
using Fluent.Navigation;

namespace Xenosynth.Modules.Cms.Admin.DocumentLibrary {
    public partial class DocumentTabControl : System.Web.UI.UserControl {

        protected TabControl TabControlDocument;

        public string Selected;
        public Guid FileID = Guid.Empty;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "Properties";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditDocument.aspx?FileID=" + FileID;
                TabControlDocument.Tabs.Add(t);

                t = new Tab();
                t.Text = "Attributes";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditDocumentAttributes.aspx?FileID=" + FileID;
                TabControlDocument.Tabs.Add(t);

                t = new Tab();
                t.Text = "Upload";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "UploadDocument.aspx?FileID=" + FileID;
                TabControlDocument.Tabs.Add(t);

                t = new Tab();
                t.Text = "Revisions";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "ViewDocumentRevisions.aspx?FileID=" + FileID;
                TabControlDocument.Tabs.Add(t);

                t = new Tab();
                t.Text = "History";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "ViewDocumentHistory.aspx?FileID=" + FileID;
                TabControlDocument.Tabs.Add(t);


                TabControlDocument.Tabs.FindByText(Selected).Selected = true;

            }
        }

    }
}