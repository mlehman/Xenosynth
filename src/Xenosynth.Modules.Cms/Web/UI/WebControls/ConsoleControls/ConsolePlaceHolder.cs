using System;
using System.Web.UI.WebControls;

namespace Xenosynth.Web.UI.WebControls.ConsoleControls {
	
	/// <summary>
	/// The ConsolePlaceHolder can be used to create content only shown during the editing modes.
	/// </summary>
	public class ConsolePlaceHolder : PlaceHolder {

        public enum ConsoleMode {
            Editing,
            Unpublished,
            Published,
            Archived
        }

        public ConsoleMode Mode {
			get {
				object o = ViewState["Mode"];
				if(o == null){
                    return ConsoleMode.Editing;
				} else {
                    return (ConsoleMode)o;
				}
			}
			set { ViewState["Mode"] = value; }
		}

		public ConsolePlaceHolder() {
		}
	
		protected override void OnLoad(EventArgs e) {
			
			base.OnLoad (e);
			this.Visible = false;

			//TODO: Make this logic more consistent?
			if(CmsHttpContext.Current.CmsFile == null){
				return;	
			} else if(Mode == ConsoleMode.Editing && CmsHttpContext.Current.Mode == CmsMode.Edit ){
				this.Visible = true;
            } else if (CmsHttpContext.Current.Mode == CmsMode.Edit) {
                return;
            } else if (Mode == ConsoleMode.Unpublished && CmsHttpContext.Current.CmsFile.State == CmsState.Unpublished) {
				this.Visible = true;
            } else if (Mode == ConsoleMode.Published && CmsHttpContext.Current.CmsFile.State == CmsState.Published) {
				this.Visible = true;
            } else if (Mode == ConsoleMode.Archived && CmsHttpContext.Current.CmsFile.State == CmsState.Archived) {
                this.Visible = true;
            } 
		}
	}
}
