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
    public partial class FileTypeTabControl : System.Web.UI.UserControl {
        protected TabControl TabControlFileType;
        public Guid FileTypeID;
        public string Selected;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "Properties";
                t.NavigateUrl = "EditFileType.aspx?FileTypeID=" + FileTypeID;
                TabControlFileType.Tabs.Add(t);

                TabControlFileType.Tabs.FindByText(Selected).Selected = true;

            }
        }

    }
}