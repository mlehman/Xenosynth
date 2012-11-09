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

using Xenosynth.Web;

namespace Xenosynth.Admin.Configuration {
    public partial class EditSite : System.Web.UI.Page {

        private WebSite cachedSite;

        public WebSite CurrentSite {
            get {
                if (cachedSite == null) {
                    cachedSite = WebSite.FindByID(SiteID);
                }
                return cachedSite; 
            }
        }

        public Guid SiteID {
            get { return new Guid(Request["SiteID"]); }
        }


        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {

                DataBindingManagerSite.DataSource = CurrentSite;
                DataBindingManagerSite.PushData();

            }   
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {
                DataBindingManagerSite.DataSource = CurrentSite;
                DataBindingManagerSite.PullData();
                CurrentSite.Update();

                MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "Site updated.");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerSite.DataSource = CurrentSite;
            DataBindingManagerSite.PushData();
            MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }
    }
}
