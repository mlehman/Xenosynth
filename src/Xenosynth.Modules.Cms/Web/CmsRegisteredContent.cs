using System;
using System.Collections;
using Inform;

using Xenosynth.Data;
using Xenosynth.Web.UI;

namespace Xenosynth.Web {
	
	/// <summary>
	/// Registers a content block for a template.
	/// </summary>
	public class CmsRegisteredContent {

		[MemberMapping(PrimaryKey=true, ColumnName="ID")]
		private Guid registeredContentID;
		
		[MemberMapping(ColumnName="TemplateID")]
		private Guid templateID;
		
		[MemberMapping(ColumnName="ControlID", Length=250)]
		private string controlID;

		[MemberMapping(ColumnName="ContentTypeName", Length=500)]
		private string contentTypeName;

		public CmsRegisteredContent() {
		}
		
		/// <summary>
		/// The unique identifier of the registered content. 
		/// </summary>
		public Guid ID {
			get { return registeredContentID; }
		}
		
		/// <summary>
		/// The Cmstemplate's unique identifier. 
		/// </summary>
		public Guid TemplateID {
			get { return templateID; }
			set { templateID = value; }
		}
		
		/// <summary>
		/// The name of the content block. 
		/// </summary>
		public string ControlID {
			get { return controlID; }
			set { controlID = value; }
		}
		
		/// <summary>
		/// The class that implements the IContentPersister for this content block. 
		/// </summary>
		public string ContentTypeName {
			get { return contentTypeName; }
			set { contentTypeName = value; }
		}

		internal IContentPersister GetContentTypePersister(){
			Type t = Type.GetType(ContentTypeName, true);
			return (IContentPersister)t.GetConstructor(Type.EmptyTypes).Invoke(null);
		}
		
		/// <summary>
		/// Registers the content for the pages that use the template. 
		/// </summary>
		public void Register(){
			registeredContentID = Guid.NewGuid();
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			try{
				ds.BeginTransaction();
				ds.Insert(this);
			
				//for each page using template, add default content
				IList pages = CmsPage.FindByTemplateID(this.TemplateID);
				IContentPersister cp = GetContentTypePersister();
				foreach(CmsPage p in pages){
					cp.CreateInitialContent(this.ControlID, p.ID);
				}

				ds.CommitTransaction();
			} catch (Exception e) {
				ds.RollbackTransaction();
				throw e;
			}
		}
		
		/// <summary>
		/// Creates the intial content for the page. 
		/// </summary>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		public void CreateInitialContent(Guid pageID){
			IContentPersister cp = GetContentTypePersister();
			cp.CreateInitialContent(this.ControlID, pageID);
		}
		
		/// <summary>
		/// Copies the content to another page/ 
		/// </summary>
		/// <param name="sourcePageID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <param name="destinationPageID">
		/// A <see cref="Guid"/>
		/// </param>
		public void CopyContent(Guid sourcePageID, Guid destinationPageID){
			IContentPersister cp = GetContentTypePersister();
			cp.CopyContent(this.ControlID, sourcePageID, destinationPageID);
		}
		
		/// <summary>
		/// Deletes the content for a page. 
		/// </summary>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		public void DeleteContent(Guid pageID){
			//TODO: Call more directly?
			IContentPersister cp = GetContentTypePersister();
			cp.DeleteContent(this.ControlID, pageID);
		}
		
		/// <summary>
		/// Deletes the content for a page.
		/// </summary>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		public void Delete(Guid pageID){
			//TODO: Call more directly?
			IContentPersister cp = GetContentTypePersister();
			cp.DeleteContent(this.ControlID, pageID);
		}
		
		/// <summary>
		/// Finds the CmsRegisteredContent by ID. 
		/// </summary>
		/// <param name="id">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsRegisteredContent"/>
		/// </returns>
		public static CmsRegisteredContent FindByID(Guid id){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			return (CmsRegisteredContent)ds.FindByPrimaryKey(typeof(CmsRegisteredContent),id);
		}
		
		/// <summary>
		/// Finds all CmsRegisteredContent for a template. 
		/// </summary>
		/// <param name="templateID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
		public static IList FindByTemplateID(Guid templateID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsRegisteredContent),"WHERE TemplateID = @TemplateID");
			cmd.CreateInputParameter("@TemplateID", templateID);
			return cmd.Execute();
		}
		
		/// <summary>
		/// Finds all CmsRegisteredContent for a template using a specified name.
		/// </summary>
		/// <param name="templateID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsRegisteredContent"/>
		/// </returns>
		public static CmsRegisteredContent FindByTemplateID(Guid templateID, string controlID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(CmsRegisteredContent),"WHERE TemplateID = @TemplateID");
			cmd.CreateInputParameter("@TemplateID", templateID);
			cmd.CreateInputParameter("@ControlID", controlID);
			return (CmsRegisteredContent)cmd.Execute();
		}
		
		/// <summary>
		/// Removes a content bloc from all pages using the template. 
		/// </summary>
		public void Unregister(){
		
			//for each page using template, delete content
			IList pages = CmsPage.FindByTemplateID(this.TemplateID);
			IContentPersister cp = GetContentTypePersister();
			foreach(CmsPage p in pages){
				cp.DeleteContent(this.ControlID, p.ID);
			}

			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			ds.Delete(this);
		
		}

	}
}
