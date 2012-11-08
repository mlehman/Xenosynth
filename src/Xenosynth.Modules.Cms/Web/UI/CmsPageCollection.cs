using System;
using System.Collections;

namespace Xenosynth.Web.UI {

	/// <summary>
	/// A collection of CmsPages.
	/// </summary>
	public class CmsPageCollection : CmsFileCollection {

		internal CmsPageCollection(IList pages): base(pages){
		}
		
		/// <summary>
		/// Gets a CmsPage by index. 
		/// </summary>
		/// <param name="index">
		/// A <see cref="System.Int32"/>
		/// </param>
		public CmsPage this[int index]{
			get { return (CmsPage)this.InnerList[index]; }
		}

	}
}
