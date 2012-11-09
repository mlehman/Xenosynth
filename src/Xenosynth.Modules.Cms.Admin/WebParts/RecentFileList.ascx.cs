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
using Xenosynth.Web.UI;
using Xenosynth.Modules.Cms.User;

namespace Xenosynth.Admin.Controls.WebParts {
    public partial class RecentFileList : System.Web.UI.UserControl, IWebPart {

      
        
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataGridRecentFiles.DataSource = RecentFiles.Files;
                DataGridRecentFiles.DataBind();
            }
        }

        #region IWebPart Members

        protected string title = "My Recent Files";
        public string Title {
            get { return title; }
            set { title = value; }
        }

        private string titleIconImageUrl = "";
        public string TitleIconImageUrl {
            get { return titleIconImageUrl; }
            set { titleIconImageUrl = value; }
        }

        private string catalogIconImageUrl = "";
        public string CatalogIconImageUrl {
            get { return catalogIconImageUrl; }
            set { catalogIconImageUrl = value; }
        }

        private string description = "";
        public string Description {
            get { return description; }
            set { description = value; }
        }

        private string subtitle = "";
        public string Subtitle {
            get { return subtitle; }
            set { subtitle = value; }
        }

        private string titleUrl = "";
        public string TitleUrl {
            get { return titleUrl; }
            set { titleUrl = value; }
        }
        #endregion

    }

    
}