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

namespace Xenosynth.Admin.Content.TemplateGallery {
    public partial class TemplateGalleryTabControl : System.Web.UI.UserControl {
        
        protected TabControl TabControlDirectory;
        public string Selected;
        public Guid DirectoryID = Guid.Empty;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "Properties";
                t.Enabled = DirectoryID != Guid.Empty;
                t.NavigateUrl = "EditTemplateGallery.aspx?FileID=" + DirectoryID;
                TabControlDirectory.Tabs.Add(t);

                t = new Tab();
                t.Text = "Attributes";
                t.Enabled = DirectoryID != Guid.Empty;
                t.NavigateUrl = "EditTemplateGalleryAttributes.aspx?FileID=" + DirectoryID;
                TabControlDirectory.Tabs.Add(t);

                //t = new Tab();
                //t.Text = "Configuration";
                //t.Enabled = DirectoryID != Guid.Empty;
                //t.NavigateUrl = "EditDirectoryConfiguration.aspx?FileID=" + DirectoryID;
                //TabControlDirectory.Tabs.Add(t);


                TabControlDirectory.Tabs.FindByText(Selected).Selected = true;

            }
        }
    }
}