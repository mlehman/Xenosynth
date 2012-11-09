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
using Xenosynth.Web;

namespace Xenosynth.Modules.Cms.Admin.Shortcut {
    public partial class ShortcutTabControl : System.Web.UI.UserControl {

        protected TabControl TabControl1;
        public string Selected;
        public Guid FileID = Guid.Empty;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "Properties";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditShortcut.aspx?FileID=" + FileID;
                TabControl1.Tabs.Add(t);

                t = new Tab();
                t.Text = "Attributes";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditShortcutAttributes.aspx?FileID=" + FileID;
                TabControl1.Tabs.Add(t);

                t = new Tab();
                t.Text = "History";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "ViewShortcutHistory.aspx?FileID=" + FileID;
                TabControl1.Tabs.Add(t);


                TabControl1.Tabs.FindByText(Selected).Selected = true;

            }
        }

    }
}