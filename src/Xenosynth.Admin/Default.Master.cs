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

using Fluent.Navigation;
using Xenosynth.Security;
using Xenosynth.Modules;


namespace Xenosynth.Admin {

    public partial class Default : System.Web.UI.MasterPage {


        protected void Page_Init(object sender, EventArgs e) {
            if (SiteMap.CurrentNode != null) {
                this.Page.Title = SiteMap.CurrentNode.Title;
            }
        }

        private void AddLink(string href) {
            HtmlLink link = new HtmlLink();
            link.Attributes["type"] = "text/css";
            link.Attributes["rel"] = "stylesheet";
            link.Href = ResolveUrl(href);
            Page.Header.Controls.AddAt(0, link);
        }

        protected void Page_Load(object sender, EventArgs e) {

            HtmlGenericControl si = new HtmlGenericControl();
            si.TagName = "script";
            si.Attributes.Add("type", "text/javascript");
            si.Attributes.Add("src", ResolveUrl("~/site.js"));
            this.Page.Header.Controls.Add(si);

            AddLink("~/Css/style.css");
            AddLink("~/Css/icons.css");
            foreach (RegisteredModule registeredModule in XenosynthContext.Current.Modules) {
                AddLink(registeredModule.ResourceFolder + "/Css/style.css");
            }

            if (!IsPostBack) {

                foreach (SiteMapNode topLevelNode in SiteMap.RootNode.ChildNodes) {
                    if (UserHasPermission(topLevelNode)) {
                        Tab t = AddMainTab(topLevelNode);
                        AddSubTabs(t, topLevelNode);
                    }
                }

                //t = new Tab();
                //t.Text = "Log Out";
                //t.CommandName = "Logout";
                //TabControlNavigationMain.Tabs.Add(t);

            }

        }

        private bool UserHasPermission(SiteMapNode node) {
            string permissions = node["permissions"];
            if (permissions == null || permissions.Trim().Length == 0) {
                return true;
            } else {
               return Permissions.UserHasPermission(permissions);
            }
        }

        private Tab AddMainTab(SiteMapNode topLevelNode) {
            Tab t = new Tab();
            if (topLevelNode.HasChildNodes) {
                if (topLevelNode.ChildNodes[0].HasChildNodes) {
                    t.NavigateUrl = topLevelNode.ChildNodes[0].ChildNodes[0].Url;
                } else {
                    t.NavigateUrl = topLevelNode.ChildNodes[0].Url;
                }
            }
            t.Text = topLevelNode.Title;
            TabControlNavigationMain.Tabs.Add(t);
            return t;
        }

        private void AddSubTabs(Tab t, SiteMapNode topLevelNode) {
            if (SiteMap.CurrentNode != null && SiteMap.CurrentNode.IsDescendantOf(topLevelNode)) {

                t.Selected = true;

                foreach (SiteMapNode secondLevelNode in topLevelNode.ChildNodes) {
                    t = new Tab();
                    if (secondLevelNode.HasChildNodes) {
                        t.NavigateUrl = secondLevelNode.ChildNodes[0].Url;
                    } else {
                        t.NavigateUrl = secondLevelNode.Url;
                    }
                    t.Text = secondLevelNode.Title;
                    TabControlNavigationSub.Tabs.Add(t);

                    if (SiteMap.CurrentNode == secondLevelNode || SiteMap.CurrentNode.IsDescendantOf(secondLevelNode)) {
                        t.Selected = true;
                    }
                }
            }
        }

        protected void LinkButtonLogOut_OnClick(object sender, EventArgs e) {
            //CmsHttpContext.Current.Mode = CmsMode.Published; //TODO: Still needed?

            //TODO: Should just link to a log out page?
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Default.aspx", true);

        }

        protected void TabControlNavigation_OnCommand(object sender, CommandEventArgs e) {
            switch (e.CommandName) {
                case "Logout":
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Default.aspx", true);
                    break;
            }
        }

    }
}
