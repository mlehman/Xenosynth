using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;

namespace Xenosynth.Web.UI.WebControls {

	/// <summary>
	/// This class supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
	/// </summary>
	public abstract class AttributePlaceHolder : Control {

		abstract protected void CreateAuthoringChildControls();
		abstract protected void CreatePresentationChildControls();
		
		abstract protected void SavePlaceHolderContent();
		abstract protected void LoadAuthoringPlaceHolderContent();
		abstract protected void LoadPresentationPlaceHolderContent();

		private CmsFileAttribute attribute;
		private string attributeName;
		private string linked;
		
		/// <summary>
		/// The CmsFileAttribute name for the placeholder. 
		/// </summary>
		public string AttributeName {
			get { return attributeName; }
			set { attributeName = value; }
		}
		
		/// <summary>
		/// Allows using the path to a different CmsFile for loading the content, instead of the current page, to allow sharing content between templates. 
		/// </summary>
		public string Linked {
			get { return linked; }
			set { linked = value; }
		}
		
		/// <summary>
		/// Whether this control is using linked content. 
		/// </summary>
		public bool IsLinked { 
			get { 
				return linked != null 
					&& linked.Length > 0 
					&& (
							CmsHttpContext.Current.CmsPage == null
							|| !Linked.ToLower().Equals(CmsHttpContext.Current.CmsPage.FullPath.ToLower())
					); 
			}
		}
		
		/// <summary>
		/// Gets an instance of the CmsFileAttribute for this place holder. 
		/// </summary>
		public CmsFileAttribute Attribute {
			get {
				if(attribute == null){
					if(!IsLinked){
						Guid pageID = CmsHttpContext.Current.CmsPage.ID;
						IList attrs = CmsFileAttribute.FindByFileID(pageID, AttributeName);
						if(attrs.Count > 0 ){
							attribute = (CmsFileAttribute)attrs[0];
						}
						if(attribute == null){
							attribute = new CmsFileAttribute();
							attribute.FileID = pageID;
							attribute.Name = AttributeName;
							attribute.Save();
						}
					} else {
						CmsPage linkedPage = (CmsPage)CmsFile.FindByFullPath(Linked);
						if(linkedPage == null){
							throw new ApplicationException("Linked page '" + Linked + "' could not be found");
						}
						
						IList attrs = CmsFileAttribute.FindByFileID(linkedPage.ID, AttributeName);
						if(attrs.Count > 0 ){
							attribute = (CmsFileAttribute)attrs[0];
						}
					}
				}
				return attribute;
			}
		}

		protected override void CreateChildControls() {
			if(IsAuthoringMode && !IsLinked){
				CreateAuthoringChildControls();
			} else {
				CreatePresentationChildControls();
			}
		}

		protected void OnSaveCmsPageEvent(object sender, PageEventArgs e){
			if(!IsLinked){
				attribute = null;
				SavePlaceHolderContent();
				Attribute.Update();
			}
		}


		protected bool IsAuthoringMode {
			get {
				return CmsHttpContext.Current.Mode == CmsMode.Edit;
			}
		}
	
		protected override void OnLoad(EventArgs e) {
			base.OnLoad (e);
			if(IsAuthoringMode){
				LoadAuthoringPlaceHolderContent(); 
			} else {
				LoadPresentationPlaceHolderContent();
			}
		}
	
		protected override void OnInit(EventArgs e) {
			base.OnInit (e);
			if(CmsHttpContext.Current.CmsPage != null){
				CmsHttpContext.Current.CmsPage.SaveCmsPageEvent += new PageEventHandler(OnSaveCmsPageEvent);
			}
		}
	}
}
