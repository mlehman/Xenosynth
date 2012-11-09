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

namespace Xenosynth.Modules.Cms.Admin.TemplateGallery {
    public partial class TemplateTasks : System.Web.UI.UserControl {

        private CmsTemplate templateCache;

        public CmsTemplate CurrentTemplate {
            get {
                if (templateCache == null) {
                    if (ViewState["ID"] != null) {
                        templateCache = (CmsTemplate)CmsFile.FindByID((Guid)ViewState["ID"]);
                    }
                }
                return templateCache;
            }
            set {
                templateCache = value;
                ViewState["ID"] = templateCache.ID;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                if (this.Visible) {
                    DataBind();
                }
            }
        }

    }
}