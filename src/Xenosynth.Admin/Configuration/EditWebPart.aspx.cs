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
    public partial class EditWebPart : System.Web.UI.Page {

        private RegisteredWebPart cachedRegisteredWebPart;

        public RegisteredWebPart CurrentRegisteredWebPart {
            get {
                if (cachedRegisteredWebPart == null) {
                    cachedRegisteredWebPart = RegisteredWebPart.FindByID(WebPartID);
                }
                return cachedRegisteredWebPart;
            }
        }

        public Guid WebPartID {
            get { return new Guid(Request["WebPartID"]); }
        }


        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {

                DataBindingManagerWebPart.DataSource = CurrentRegisteredWebPart;
                DataBindingManagerWebPart.PushData();

            }
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {
                DataBindingManagerWebPart.DataSource = CurrentRegisteredWebPart;
                DataBindingManagerWebPart.PullData();
                CurrentRegisteredWebPart.Update();

                MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "Web Part updated.");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerWebPart.DataSource = CurrentRegisteredWebPart;
            DataBindingManagerWebPart.PushData();
            MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }
    }
}
