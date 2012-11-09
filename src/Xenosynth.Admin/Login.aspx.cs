using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;


namespace Xenosynth.Admin {

	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public partial class Login : System.Web.UI.Page {

		protected Fluent.ControlFocus.ControlFocus Controlfocus1;
		protected System.Web.UI.WebControls.Label LabelMessage;
		protected Fluent.AutoPostBack Autopostback1;

		protected void Page_Load(object sender, System.EventArgs e) {
			
		}

		
		protected void ButtonLogin_OnClick(object sender, System.EventArgs e) {

			string username = TextBoxUsername.Text;
			string password = TextBoxPassword.Text;

			if(Page.IsValid) {


                if (Membership.ValidateUser(username, password)) {

                    FormsAuthentication.RedirectFromLoginPage(username, CheckBoxPersist.Checked);

				} else {

                    //check if email?
                    username = Membership.GetUserNameByEmail(username);

                    if (Membership.ValidateUser(username, password)) {

                        FormsAuthentication.RedirectFromLoginPage(username, CheckBoxPersist.Checked);

                    }

					LabelError.Text = "Authentication Failed.";
				}
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
