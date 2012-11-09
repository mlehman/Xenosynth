namespace Xenosynth.Admin.Controls {
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for MessageBox.
	/// </summary>
	public partial class MessageBox : System.Web.UI.UserControl {


		public enum MessageBoxMode {
			Alert,
			Info,
			Error
		}

		
		string text;
		MessageBoxMode mode;
		bool show;

		public string Text {
			get {return text;}
		}

		public MessageBoxMode Mode {
			get {return mode;}
		}

		public string CssClass;

		protected string CompleteCssClass {
			get { return CssClass + " " + CssClass+ Mode.ToString(); }
		}

		public void ShowMessage(MessageBoxMode mode, string text){
			this.mode = mode;
			this.text = text;
			this.show = true;
		}

        public void ShowMessage(MessageBoxMode mode, string text, params object[] args) {
            ShowMessage(mode, string.Format(text, args));
        }

		protected override void Render(System.Web.UI.HtmlTextWriter writer) {
			if(show){
				base.Render (writer);
			}
		}


		protected void Page_Load(object sender, System.EventArgs e) {
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
