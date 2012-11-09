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
    public partial class ConfigurationSettingEditor : System.Web.UI.UserControl {

        public ConfigurationSetting Setting;

        private string Key {
            get {
                return (string)ViewState["Key"];
            }
            set { ViewState["Key"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected override void CreateChildControls() {
            this.Controls.Clear();

            CreateControlHierarchy();
        }

        private void CreateControlHierarchy() {

            TextBox textBox = new TextBox();
            Controls.Add(textBox);
            textBox.ID = Key;
            textBox.CssClass = "textBoxAttribute";

        }


        public override void DataBind() {
            base.DataBind();

            Controls.Clear();

            if (HasChildViewState) {
                ClearChildViewState();
            }

            Key = Setting.Key;
         

            CreateControlHierarchy();

            SetValue(Setting);

            if (!IsTrackingViewState) {
                TrackViewState();
            }
        }

        private void SetValue(ConfigurationSetting setting) {
            if (setting.Value != null) {
                TextBox textBox = (TextBox)FindControl(setting.Key);
                textBox.Text = setting.Value.ToString();
            }
        }

        public void GetValue(ConfigurationSetting setting) {
            //if (setting.ValueType == typeof(System.Boolean)) {
            //    CheckBox CheckBoxValue = (CheckBox)FindControl(setting.Key);
            //    setting.Value = CheckBoxValue.Checked;
            //} else if (setting.ValueType.IsEnum) {
            //    DropDownList DropDownListValue = (DropDownList)FindControl(setting.Key);
            //    setting.Value = Enum.Parse(setting.ValueType, DropDownListValue.SelectedValue);
            //} else {
            if (setting.ValueType.IsEnum) {
                TextBox TextBoxValue = (TextBox)FindControl(setting.Key);
                setting.Value = Enum.Parse(setting.ValueType, TextBoxValue.Text);
            } else {
                TextBox TextBoxValue = (TextBox)FindControl(setting.Key);
                setting.Value = Convert.ChangeType(TextBoxValue.Text, setting.ValueType);
            }
            //}
        }


        internal void Save() {
            ConfigurationSetting setting = XenosynthContext.Current.Configuration[this.Key];
            GetValue(setting);
        }
    }
}