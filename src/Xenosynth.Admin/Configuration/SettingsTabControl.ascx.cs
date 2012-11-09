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
    public partial class SettingsTabControl : System.Web.UI.UserControl {

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                foreach (String category in XenosynthContext.Current.Configuration.Categories) {
                    Tab t = new Tab();
                    t.Text = category;
                    t.NavigateUrl = "Settings.aspx?Category=" + category;
                    TabControlSettings.Tabs.Add(t);
                }

                string selectedCategory = Request["Category"];
                if (selectedCategory != null) {
                    TabControlSettings.Tabs.FindByText(selectedCategory).Selected = true;
                } else {
                    TabControlSettings.Tabs[0].Selected = true;
                }

            }
        }
    }
}