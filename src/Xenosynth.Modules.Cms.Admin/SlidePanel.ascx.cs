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

namespace Xenosynth.Modules.Cms.Admin {

    [ParseChildren(true)]
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

        protected bool Collapsed {
            get {
                return Request.Cookies["spb.clpsd"] == null || Request.Cookies["spb.clpsd"].Value != "false";
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (Collapsed) {
                SidePanel.Width = 0;
            } else {
                SidePanel.Width = 160;
            }

            if (!IsPostBack) {
                CollapsiblePanelExtenderSidePanel.Collapsed = Collapsed;
            }
        }
    }
}