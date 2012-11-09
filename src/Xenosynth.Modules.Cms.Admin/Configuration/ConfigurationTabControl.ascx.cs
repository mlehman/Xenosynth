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

namespace Xenosynth.Modules.Cms.Admin.Configuration {
    
    public partial class ConfigurationTabControl : System.Web.UI.UserControl {

        protected TabControl TabControlConfiguration;
        public string Selected;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "File Types";
                t.NavigateUrl = "Default.aspx";
                TabControlConfiguration.Tabs.Add(t);

                TabControlConfiguration.Tabs.FindByText(Selected).Selected = true;

            }
        }

    }
}