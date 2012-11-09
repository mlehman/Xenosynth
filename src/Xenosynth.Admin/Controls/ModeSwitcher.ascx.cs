namespace Xenosynth.Admin.Controls {
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using Xenosynth.Web;

	/// <summary>
	///		Summary description for ModeSwitcher.
	/// </summary>
	public partial class ModeSwitcher : System.Web.UI.UserControl {


		protected void Page_Load(object sender, System.EventArgs e) {
			if(!IsPostBack){
				if(CmsHttpContext.Current.Mode == CmsMode.Published){
					DropDownListMode.SelectedIndex = 1;
				} else {
					DropDownListMode.SelectedIndex = 0;
				}
			}
		}

		protected void DropDownListMode_OnSelectedIndexChanged(object sender, EventArgs e){
			if(DropDownListMode.SelectedIndex == 1){
				CmsHttpContext.Current.Mode = CmsMode.Published;
			} else {
				CmsHttpContext.Current.Mode = CmsMode.Unpublished;
			}
			Page.Response.Redirect(Page.Request.RawUrl);
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
		}
		#endregion
	}
}
