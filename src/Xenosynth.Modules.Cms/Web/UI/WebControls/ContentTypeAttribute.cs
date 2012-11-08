using System;

using Xenosynth.Data;

namespace Xenosynth.Web.UI.WebControls {

	/// <summary>
	/// This class supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
	/// </summary>
	public class ContentTypeAttribute : Attribute {

		private Type contentType;
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code.  
		/// </summary>
		/// <param name="contentType">
		/// A <see cref="Type"/>
		/// </param>
		public ContentTypeAttribute(Type contentType){
			this.contentType = contentType;
		}
		
		internal IContentPersister CreateInstanceOfContent(){
			return (IContentPersister)contentType.GetConstructor(System.Type.EmptyTypes).Invoke(null);
		}

	}
}
