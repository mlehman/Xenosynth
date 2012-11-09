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
using Xenosynth.Admin.Controls.WebParts;

namespace Xenosynth.Admin.Configuration {
    public partial class AddWebPart : System.Web.UI.Page {
        


        protected void Page_Load(object sender, EventArgs e) {
           
        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {
                RegisteredWebPart rwp = new RegisteredWebPart();

                DataBindingManagerWebPart.DataSource = rwp;
                DataBindingManagerWebPart.PullData();
                rwp.Insert();

                Response.Redirect("WebParts.aspx");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerWebPart.Reset();
            MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }
    }
}
