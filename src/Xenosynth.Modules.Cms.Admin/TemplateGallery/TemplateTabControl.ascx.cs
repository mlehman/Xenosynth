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
    public partial class TemplateTabControl : System.Web.UI.UserControl {
        
        protected TabControl TabControlFile;
        public string Selected;
        public Guid FileID = Guid.Empty;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "Properties";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditTemplate.aspx?FileID=" + FileID;
                TabControlFile.Tabs.Add(t);

                t = new Tab();
                t.Text = "Content Blocks";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditTemplateContentBlocks.aspx?FileID=" + FileID;
                TabControlFile.Tabs.Add(t);

                t = new Tab();
                t.Text = "Attributes";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditTemplateAttributes.aspx?FileID=" + FileID;
                TabControlFile.Tabs.Add(t);


                TabControlFile.Tabs.FindByText(Selected).Selected = true;

            }
        }
    }
}