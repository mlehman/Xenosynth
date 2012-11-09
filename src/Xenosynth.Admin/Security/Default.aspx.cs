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
using Fluent;

namespace Xenosynth.Admin.Users {
    public partial class Default : System.Web.UI.Page {

        protected DataGridAdapter DataGridAdapterUsers;

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void DataGridAdapterUsers_DataGridBinding(DataGridAdapter sender, DataGridBindingEventArgs e) {
            DataGridAdapterUsers.DataSource = Membership.GetAllUsers();
		}
        

        protected void DataGridUsers_OnDeleteCommand(object sender, DataGridCommandEventArgs e) {
            Guid g = (Guid)DataGridUsers.DataKeys[e.Item.ItemIndex];
            MembershipUser user = Membership.GetUser(g);
            if (user.UserName != Membership.GetUser().UserName) { //TODO: andy other no delete rules?
                Membership.DeleteUser(user.UserName, true);
            }

            DataGridAdapterUsers.BindDataGrid();
        
        }
    }
}
