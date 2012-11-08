using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI.HtmlControls;
using Xenosynth.Modules.Cms;


namespace Xenosynth.Web.UI.WebControls.ConsoleControls {
	
	/// <summary>
	/// The console control is used to create a navigation bar in authoring mode on the edited site. 
	/// </summary>
	public class Console : Control {

		const string EditModeCommandName = "EditMode";
		const string UnpublishedModeCommandName = "UnpublishedMode";
		const string PublishedModeCommandName = "PublishedMode";
        const string SwitchModeCommandName = "SwitchMode";
		const string SaveCommandName = "Save";
		const string ViewPropertiesCommandName = "ViewProperties";
		const string BrowseDirectoryCommandName = "BrowseDirectory";
		const string PublishCommandName = "Publish";
		const string UnpublishCommandName = "Unpublish";


		public string CmsPath {
			get { 
				return Page.ResolveUrl(CmsConfiguration.Current.AdminPath);
			}
		}

        public CmsMode SwitchMode {
            get {
                if (CmsHttpContext.Current.Mode == CmsMode.Published) {
                    return CmsMode.Unpublished;
                } else {
                    return CmsMode.Published;
                }
            }
        }

		protected override bool OnBubbleEvent(object source, EventArgs args) {
			if (args is CommandEventArgs) {
				CommandEventArgs  command = (CommandEventArgs)args;
				switch(command.CommandName){
					case EditModeCommandName:
						CmsHttpContext.Current.Mode = CmsMode.Edit;
						Redirect(Page.Request.RawUrl);
						return true;
					case UnpublishedModeCommandName:
						CmsHttpContext.Current.Mode = CmsMode.Unpublished;
						Redirect(Page.Request.RawUrl);
						return true;
					case PublishedModeCommandName:
						CmsHttpContext.Current.Mode = CmsMode.Published;
						Redirect(Page.Request.RawUrl);
						return true;
                    case SwitchModeCommandName:              
                        CmsHttpContext.Current.Mode = SwitchMode;
                        Redirect(Page.Request.RawUrl);
                        return true;
					case SaveCommandName:
                        CmsHttpContext.Current.CmsFile.UpdateOrVersion();
						//preview
						CmsHttpContext.Current.Mode = CmsMode.Unpublished;
						Redirect(Page.Request.RawUrl);
						return true;
					case ViewPropertiesCommandName:
						Redirect(CmsPath + Resolve(CmsHttpContext.Current.CmsFile.FileType.EditUrl) + "?FileID=" + CmsHttpContext.Current.CmsFile.ID);
						return true;
					case BrowseDirectoryCommandName:
                        Redirect(CmsPath + Resolve(CmsHttpContext.Current.CmsFile.ParentDirectory.FileType.BrowseUrl) + "?FileID=" + CmsHttpContext.Current.CmsFile.ParentDirectory.ID);
						return true;
					case UnpublishCommandName:
						CmsHttpContext.Current.CmsFile.Unpublish();
						Redirect(Page.Request.RawUrl);
						return true;
					case PublishCommandName:
                        CmsHttpContext.Current.CmsFile.Publish();
						Redirect(Page.Request.RawUrl);
						return true;
				}
				return false;
			}
			return false;
		}

        private string Resolve(string path) {
           if(path.StartsWith("~/")){
                return path.Substring(2);
           } else {
               return path;
           }
        }

		public void Redirect(string path){
			Page.Response.Redirect(path);
		}
	
	
		protected override void OnLoad(EventArgs e) {
			if(Page.User.Identity.IsAuthenticated && CmsContext.Current.IsInAuthoringRole(Page.User)){
				this.Visible = true;
                if (Page.Header != null) {
                    HtmlLink link = new HtmlLink();
                    link.Attributes.Add("href", "~/cms/css/console.css"); //TODO: Set to correct theme
                    link.Attributes.Add("rel", "stylesheet");
                    link.Attributes.Add("type", "text/css");
                    Page.Header.Controls.Add(link);
                }
			} else {
				this.Visible = false;
			}
			base.OnLoad (e);
		}
	}
}
