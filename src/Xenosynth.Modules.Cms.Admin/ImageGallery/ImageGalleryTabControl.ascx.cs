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

namespace Xenosynth.Admin.Content.ImageGallery {

    public partial class ImageGalleryTabControl : System.Web.UI.UserControl {

        public string Selected;
        public Guid FileID = Guid.Empty;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "Properties";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditGallery.aspx?FileID=" + FileID;
                TabControlImageGallery.Tabs.Add(t);

                t = new Tab();
                t.Text = "Attributes";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditGalleryAttributes.aspx?FileID=" + FileID;
                TabControlImageGallery.Tabs.Add(t);

                t = new Tab();
                t.Text = "History";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "ViewGalleryHistory.aspx?FileID=" + FileID;
                TabControlImageGallery.Tabs.Add(t);

                TabControlImageGallery.Tabs.FindByText(Selected).Selected = true;

            }
        }

    }
}