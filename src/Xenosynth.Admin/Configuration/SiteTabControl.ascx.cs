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

namespace Xenosynth.Admin.Configuration {

    public partial class SiteTabControl : System.Web.UI.UserControl {

        protected TabControl TabControlSite;
        public string Selected;
        public Guid SiteID = Guid.Empty;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "Properties";
                t.Enabled = SiteID != Guid.Empty;
                t.NavigateUrl = "EditSite.aspx?SiteID=" + SiteID;
                TabControlSite.Tabs.Add(t);

                t = new Tab();
                t.Text = "Host Headers";
                t.Enabled = SiteID != Guid.Empty;
                t.NavigateUrl = "EditSiteHostHeaders.aspx?SiteID=" + SiteID;
                TabControlSite.Tabs.Add(t);


                TabControlSite.Tabs.FindByText(Selected).Selected = true;

            }
        }
    }
}