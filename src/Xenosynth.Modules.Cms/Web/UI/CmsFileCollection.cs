using System;
using System.Collections;

namespace Xenosynth.Web.UI {

	/// <summary>
	/// A collection of CmsFiles.
	/// </summary>
	public class CmsFileCollection : ReadOnlyCollectionBase {

		internal CmsFileCollection(IList files){
			this.InnerList.AddRange(files);
		}
		
		/// <summary>
		/// Gets the CmsFile by index. 
		/// </summary>
		/// <param name="index">
		/// A <see cref="System.Int32"/>
		/// </param>
        public CmsFile this[int index]{
            get {
                return (CmsFile)this.InnerList[index];
            }
        }

	}
}
