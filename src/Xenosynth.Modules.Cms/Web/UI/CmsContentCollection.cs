using System;
using System.Collections;

using Xenosynth.Data;

namespace Xenosynth.Web.UI {
	
	/// <summary>
	/// The collection of content blocks for a CmsFile.
	/// </summary>
	public class CmsContentBlockCollection : IEnumerable  {

		private CmsPage page;
		private Hashtable contentCache = new Hashtable();
		bool preloaded;
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="page">
		/// A <see cref="CmsPage"/>
		/// </param>
        public CmsContentBlockCollection(CmsPage page) {
			this.page = page;
		}
		
		/// <summary>
		/// Gets a content block by name. 
		/// </summary>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		public IContent this[string controlID] {
			get {
                IContent content = (IContent)contentCache[controlID];
				if(content == null){
					content = LoadContent(controlID);
					contentCache[controlID] = content;
				}
				return content;
			} 
		}
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code.  
		/// </summary>
		/// <param name="controlID">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="IContent"/>
		/// </returns>
		protected IContent LoadContent(string controlID) {
			
			CmsRegisteredContent registeredContent = CmsRegisteredContent.FindByTemplateID(page.TemplateID, controlID);
			if(registeredContent == null){
				throw new ApplicationException("Could not find registered content '" + controlID + "'.");
			}
			IContentPersister c = registeredContent.GetContentTypePersister();
			c.LoadContent(controlID, page.ID);

			return c.Content;
		}
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		protected void PreloadContent(){
            contentCache.Clear();
			IList l = CmsRegisteredContent.FindByTemplateID(page.TemplateID);
			foreach(CmsRegisteredContent rc in l){
				IContentPersister c = rc.GetContentTypePersister();
				c.LoadContent(rc.ControlID, page.ID);
				contentCache[rc.ControlID] = c.Content;
			}
		}

		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <returns>
		/// A <see cref="IEnumerator"/>
		/// </returns>
		public IEnumerator GetEnumerator(){
            if (!preloaded) {
                PreloadContent();
            }
            return contentCache.GetEnumerator();
		}


    }
}
