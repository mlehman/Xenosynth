using System;
using Inform;

namespace Xenosynth.Data {
	
	/// <summary>
	/// Literal Content is a text blob stored in the primary database. 
	/// </summary>
	public class LiteralContent : IContent, IContentPersister {

		private bool isAvialable;

		private string text;
		
		/// <summary>
		/// If content is available for the loaded content ID and page ID. 
		/// </summary>
		public bool IsAvailable {
			get { return isAvialable; }
		}
		
		/// <summary>
		/// Returns the Content as content item. 
		/// </summary>
		public IContent Content {
			get { return this; }
		}
		
		/// <summary>
		/// Returns the Content as a <see cref="String"/>. 
		/// </summary>
		public string Text {
			get { return text; }
			set { text = value; }
		}
		
		/// <summary>
		/// Load the content for the content ID and page ID. 
		/// </summary>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		public void LoadContent(string controlID, Guid pageID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(LiteralDataItem), "WHERE ControlID = @ControlID AND PageID = @PageID");
			cmd.CreateInputParameter("@ControlID", controlID);
			cmd.CreateInputParameter("@PageID", pageID);
			LiteralDataItem literalDataItem = (LiteralDataItem)cmd.Execute();
			
			if(literalDataItem != null){
				isAvialable = true;
				text = literalDataItem.Text;
			}
		}
		
		/// <summary>
		/// Creates a new content item for the content ID and page ID.   
		/// </summary>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		public void CreateInitialContent(string controlID, Guid pageID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			
			LiteralDataItem li = new LiteralDataItem();
			li.LiteralItemID = Guid.NewGuid();
			li.ControlID = controlID;
			li.PageID = pageID;
			li.Text = string.Empty;

			ds.Insert(li);
		}
		
		/// <summary>
		/// Saves the content for the content ID and page ID. 
		/// </summary>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		public void SaveContent(string controlID, Guid pageID){

			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(LiteralDataItem), "WHERE ControlID = @ControlID AND PageID = @PageID");
			cmd.CreateInputParameter("@ControlID", controlID);
			cmd.CreateInputParameter("@PageID", pageID);
			LiteralDataItem literalDataItem = (LiteralDataItem)cmd.Execute();

			literalDataItem.Text = text;

			ds.Update(literalDataItem);
		}
		
		/// <summary>
		/// Copies the content item to a new content item for new the content ID and page ID. 
		/// </summary>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="sourcePageID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <param name="destinationPageID">
		/// A <see cref="Guid"/>
		/// </param>
		public void CopyContent(string controlID, Guid sourcePageID, Guid destinationPageID){

			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(LiteralDataItem), "WHERE ControlID = @ControlID AND PageID = @PageID");
			cmd.CreateInputParameter("@ControlID", controlID);
			cmd.CreateInputParameter("@PageID", sourcePageID);
			LiteralDataItem literalDataItem = (LiteralDataItem)cmd.Execute();

			literalDataItem.LiteralItemID = Guid.NewGuid();
			literalDataItem.PageID = destinationPageID;

			ds.Insert(literalDataItem);
		}
		
		/// <summary>
		/// Deletes the content item for the content ID and page ID. 
		/// </summary>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		public void DeleteContent(string controlID, Guid pageID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(LiteralDataItem), "WHERE ControlID = @ControlID AND PageID = @PageID");
			cmd.CreateInputParameter("@ControlID", controlID);
			cmd.CreateInputParameter("@PageID", pageID);
			LiteralDataItem literalDataItem = (LiteralDataItem)cmd.Execute();
			if(literalDataItem != null){
				ds.Delete(literalDataItem);
			}
		}
		
	}
}
