using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using Xenosynth.Data;

namespace Xenosynth.Web.UI.WebControls {
	
	/// <summary>
	/// This class supports the Xenosynth CMS Module and is not intended to be used directly from your code.  
	/// </summary>
	public abstract class ContentPlaceHolder : Control {

		private IContentPersister contentPersister;
		private string linked;

		abstract protected void CreateAuthoringChildControls();
		abstract protected void CreatePresentationChildControls();
		
		abstract protected void SavePlaceHolderContent();
		abstract protected void LoadAuthoringPlaceHolderContent();
		abstract protected void LoadPresentationPlaceHolderContent();

		public ContentPlaceHolder() {

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
		/// The content for this placeholder. 
		/// </summary>
		public IContent Content {
			get {
				if(contentPersister == null){
					
					contentPersister = LoadContentPersister();
				}
				return contentPersister.Content;
			}
		}

		protected IContentPersister LoadContentPersister() {
			object[] attrs =  this.GetType().GetCustomAttributes(typeof(ContentTypeAttribute), true);
			if(attrs.Length == 0){
				throw new ApplicationException("ContentTypeAttribute not defined.");
			}
			ContentTypeAttribute cta = (ContentTypeAttribute)attrs[0];
			IContentPersister c = cta.CreateInstanceOfContent();
			
			if (IsLinked){
				CmsPage linkedPage = (CmsPage)CmsFile.FindByFullPath(Linked);
				if(linkedPage == null){
					throw new ApplicationException("Linked page '" + Linked + "' could not be found");
				}
				c.LoadContent(this.ID, linkedPage.ID);

			} else if(CmsHttpContext.Current.CmsPage != null){
				c.LoadContent(this.ID, CmsHttpContext.Current.CmsPage.ID);
			} else {
				//TODO: throw error instead?
			}
			return c;
		}

		protected override void CreateChildControls() {
			if(IsAuthoringMode && !IsLinked){
				CreateAuthoringChildControls();
			} else {
				CreatePresentationChildControls();
			}
		}

		protected void OnSaveCmsPageEvent(object sender, PageEventArgs e){
			//TODO: Ignore unregistered Content?
			if(Content.IsAvailable && !IsLinked){
				SavePlaceHolderContent();
				contentPersister.SaveContent(this.ID, e.PageID);
			}
		}


		protected bool IsAuthoringMode {
			get {
				return CmsHttpContext.Current.Mode == CmsMode.Edit;
			}
		}
	
		protected override void OnLoad(EventArgs e) {
			base.OnLoad (e);
			if(IsAuthoringMode && !IsLinked){
				LoadAuthoringPlaceHolderContent(); 
			} else {
				LoadPresentationPlaceHolderContent();
			}
		}
	
		protected override void OnInit(EventArgs e) {
			base.OnInit (e);
			if(CmsHttpContext.Current.CmsPage != null  && !IsLinked){
				CmsHttpContext.Current.CmsPage.SaveCmsPageEvent += new PageEventHandler(OnSaveCmsPageEvent);
			}
		}
	}
}
