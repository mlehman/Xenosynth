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

using Xenosynth.Security;
using Fluent;

namespace Xenosynth.Admin.Security {
    public partial class ViewPermissions : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {

        }

        public void DataGridPermissions_OnDeleteCommand(object sender, DataGridCommandEventArgs e) {
            Guid permissionID = (Guid)DataGridPermissions.DataKeys[e.Item.ItemIndex];

            Permissions.DeletePermission(permissionID);

            MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "The permission has been deleted.");
             

            DataGridAdapterPermissions.BindDataGrid();
        }

        public void DataGridAdapterPermissions_DataGridBinding(DataGridAdapter sender, DataGridBindingEventArgs e) {
            DataGridAdapterPermissions.DataSource = Permissions.FindAllPermissions();
        }
    }
}
