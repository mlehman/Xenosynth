using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Xenosynth.Admin.Controls;

namespace Xenosynth.Admin.Configuration {

    /// <summary>
    /// Summary description for Settings.
    /// </summary>
    public partial class Settings : System.Web.UI.Page {

        public string Category {
            get {
                string category = Request["Category"];
                if (category == null) {
                    category = XenosynthContext.Current.Configuration.Categories[0];
                }
                return category;
            }
        }


        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                BindSettings();
            }
        }

        private void BindSettings() {

            DataGridSettings.DataSource = XenosynthContext.Current.Configuration.GetConfigurationSettingByCategory(Category);
            DataGridSettings.DataBind();

        }

        protected void ButtonSave_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {
                try {
                    foreach (DataGridItem item in DataGridSettings.Items) {
                        ConfigurationSettingEditor editor = (ConfigurationSettingEditor)item.FindControl("ConfigurationSettingEditor");
                        editor.Save();
                    }
                    MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Settings have been saved.");
                } catch (Exception ex) {
                    MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Error, ex.Message);
                }


                BindSettings();
            }

        }

        protected void DataGridSettings_OnDelete(object sender, DataGridCommandEventArgs e) {
            string key = (string)DataGridSettings.DataKeys[e.Item.ItemIndex];
            XenosynthContext.Current.Configuration.Remove(key);
            if (DataGridSettings.Items.Count > 1) {
                BindSettings();
            } else {
                Response.Redirect("Settings.aspx");
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
    }
}
