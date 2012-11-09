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
using Xenosynth.Admin.Controls;
using Xenosynth.Search;

namespace Xenosynth.Admin.Configuration {
    public partial class Search : System.Web.UI.Page {

     

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void ButtonRebuild_OnClick(object sender, EventArgs e) {

            SearchService.RebuildIndex();

            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Rebuilding Index");
        }
    }
}
