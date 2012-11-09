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
using Xenosynth.Configuration;

namespace Xenosynth.Admin.Configuration {
    public partial class AddSetting : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void ButtonAddSetting_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {
                ConfigurationSetting setting = new ConfigurationSetting(
                    TextBoxKey.Text,
                    TextBoxCategory.Text,
                    TextBoxName.Text,
                    TextBoxDescription.Text,
                    Type.GetType(TextBoxType.Text, true, true),
                    TextBoxValue.Text
                    );


                XenosynthContext.Current.Configuration.Add(setting);

                Response.Redirect("Settings.aspx?Category=" + setting.Category);
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect("Settings.aspx");
        }
        
    }
}
