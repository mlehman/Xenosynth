namespace Xenosynth.Admin.Controls {
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using Fluent;
    using System.Web.UI;

	/// <summary>
	///		Summary description for Calendar.
	/// </summary>
	public partial class PopupCalendar : System.Web.UI.UserControl {


		public string TextBoxToSet;
		public string DateFormat;

		private TextBox GetTextBox() {
			if(TextBoxToSet == null){
				throw new ApplicationException("TextBoxToSet must be set.");
			}
				TextBox tb = (TextBox)this.NamingContainer.FindControl(TextBoxToSet);
				if(tb == null){
					throw new ApplicationException("Could not find control '" + TextBoxToSet + "'");
				}
				return tb;
		}

		protected void Page_Load(object sender, System.EventArgs e) {
			if(!IsPostBack){
                TextBox tb = GetTextBox();
                try {
                    this.Calendar1.SelectedDate = DateTime.Parse(tb.Text);
                } catch { }
			}
		}

        protected void LinkButtonToggle_OnClick(object sender, ImageClickEventArgs e) {
            PanelCalendar.Visible = true;
        }

        protected void ButtonClose_OnClick(object sender, System.EventArgs e) {
            PanelCalendar.Visible = false;
        }

		protected void Calendar1_OnSelectionChanged(object sender, EventArgs e){
			TextBox tb = GetTextBox();
			tb.Text = Calendar1.SelectedDate.ToString(DateFormat);
            PanelCalendar.Visible = false;
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
