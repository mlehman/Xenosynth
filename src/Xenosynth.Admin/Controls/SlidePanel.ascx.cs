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

namespace Xenosynth.Admin.Controls {
    public partial class SlidePanel : System.Web.UI.UserControl {
        private ITemplate sidePanelTemplate;
        private ITemplate mainPanelTemplate;

        [PersistenceMode(PersistenceMode.InnerProperty), 
        TemplateContainer(typeof(TemplateControl)), 
        TemplateInstance(TemplateInstance.Single)]
        public ITemplate SidePanelTemplate {
            get { return sidePanelTemplate; }
            set { sidePanelTemplate = value; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty), 
        TemplateContainer(typeof(TemplateControl)), 
        TemplateInstance(TemplateInstance.Single)]
        public ITemplate MainPanelTemplate {
            get { return mainPanelTemplate; }
            set { mainPanelTemplate = value; }
        }

        protected override void OnInit(EventArgs e) {

            base.OnInit(e);

            if (sidePanelTemplate != null) {
                sidePanelTemplate.InstantiateIn(SidePanelPlaceHolder);
            }

            if (mainPanelTemplate != null) {
                mainPanelTemplate.InstantiateIn(MainPanelPlaceHolder);
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                CollapsiblePanelExtenderSidePanel.Collapsed =
                   Request.Cookies["spb.clpsd"] == null || Request.Cookies["spb.clpsd"].Value != "false";
            }
        }
    }
}