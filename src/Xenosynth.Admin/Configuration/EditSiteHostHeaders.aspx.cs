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

using Xenosynth.Web;
using Xenosynth.Admin.Controls;

namespace Xenosynth.Admin.Configuration {
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
    public partial class EditSiteHostHeaders : System.Web.UI.Page {

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

		protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                BindHostHeaders();
            }
		}

		public void BindHostHeaders(){
            DataGridHostHeaders.DataSource = HostHeaderMapping.FindBySite(SiteID);
			DataGridHostHeaders.DataBind();
		}

		public void DataGridHostHeaders_OnDeleteCommand(object sender, DataGridCommandEventArgs e){
			
			HostHeaderMapping h = HostHeaderMapping.FindByID((Guid)DataGridHostHeaders.DataKeys[e.Item.ItemIndex]);
			if(h != null){
				h.Delete();
			}
			
			BindHostHeaders();

		}

        public void ButtonSave_OnClick(object sender, EventArgs e) {
            foreach (DataGridItem item in DataGridHostHeaders.Items) {
                HostHeaderMapping h = HostHeaderMapping.FindByID((Guid)DataGridHostHeaders.DataKeys[item.ItemIndex]);
                TextBox txtBox = (TextBox)item.FindControl("TextBoxName");
                CheckBox chkBox = (CheckBox)item.FindControl("CheckBoxIsDefault");
                if (txtBox.Text != h.HostHeaderName || chkBox.Checked != h.IsDefault) {
                    h.HostHeaderName = txtBox.Text;
                    h.IsDefault = chkBox.Checked;
                    h.Update();
                }
            }
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Host Headers have been updated.");
        }

        protected void ButtonAddHostHeader_OnClick(object sender, EventArgs e) {
            HostHeaderMapping h = new HostHeaderMapping();
            h.HostHeaderName = TextBoxHostHeader.Text;
            h.WebSiteID = SiteID;
            h.Insert();

            BindHostHeaders();

            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Host Header has been added.");
            TextBoxHostHeader.Text = "";
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            TextBoxHostHeader.Text = "";
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
