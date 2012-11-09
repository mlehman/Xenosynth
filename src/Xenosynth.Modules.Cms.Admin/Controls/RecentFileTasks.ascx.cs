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

namespace Xenosynth.Modules.Cms.Admin.Controls {
    public partial class RecentFileTasks : System.Web.UI.UserControl {

        private CmsFile fileCache;

        public CmsFile CurrentFile {
            get {
                return fileCache;
            }
            set {
                fileCache = value;
            }
        }


        public ArrayList Files {
            get {
                return RecentFiles.Files;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

            this.DataBind();
           
        }
    }
}