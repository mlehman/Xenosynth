namespace Xenosynth.Admin.Content {
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for SearchFiles.
	/// </summary>
	public partial class SearchFiles : System.Web.UI.UserControl {

		protected void Page_Load(object sender, System.EventArgs e) {
			// Put user code to initialize the page here
		}

		public void ButtonSearch_OnClick(object sender, System.EventArgs e){
			string url = string.Format("~/Modules/Cms/Search.aspx?Text={0}",
				HttpUtility.UrlEncode( TextBoxText.Text.Trim()));
			Response.Redirect(url);
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
