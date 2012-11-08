using System;

namespace Xenosynth.Data {

	/// <summary>
	/// The interface for a ContentPersister. The content persister is an interface the enables adding new types of content to the system, stored in the primary database or in an external system.
	/// </summary>
	public interface IContentPersister {
		
		/// <summary>
		/// Loads the content item for the content ID and page ID.
		/// </summary>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		void LoadContent(string controlID, Guid pageID);
		
		/// <summary>
		/// Creates a new, empty content item for the content ID and page ID.
		/// </summary>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		void CreateInitialContent(string controlID, Guid pageID);
		
		/// <summary>
		/// Saves the content for the content ID and page ID.
		/// </summary>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		void SaveContent(string controlID, Guid pageID);
		
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
		void CopyContent(string controlID, Guid sourcePageID, Guid destinationPageID);
		
		/// <summary>
		/// Deletes the content item for the content ID and page ID. 
		/// </summary>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		void DeleteContent(string controlID, Guid pageID);
		
		/// <summary>
		/// Returns the content. 
		/// </summary>
		IContent Content {
			get;
		}
	}
}
