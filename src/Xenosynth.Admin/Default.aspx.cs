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
using System.Web.UI.WebControls.WebParts;

namespace Xenosynth.Admin {
    /// <summary>
    /// Summary description for _Default.
    /// </summary>
    public partial class _Default : System.Web.UI.Page {

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                
            }
        }

        protected void LinkButtonEdit_OnClick(object sender, System.EventArgs e) {
            WebPartManager1.DisplayMode = WebPartManager.EditDisplayMode;
        }

        protected void LinkButtonCatalog_OnClick(object sender, System.EventArgs e) {
            WebPartManager1.DisplayMode = WebPartManager.CatalogDisplayMode;
        }

        protected void LinkButtonBrowse_OnClick(object sender, System.EventArgs e) {
            WebPartManager1.DisplayMode = WebPartManager.BrowseDisplayMode;
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
