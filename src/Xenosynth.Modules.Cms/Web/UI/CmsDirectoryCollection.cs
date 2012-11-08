using System;
using System.Collections;

namespace Xenosynth.Web.UI {

	/// <summary>
	/// A collection of CmsDirectories.
	/// </summary>
	public class CmsDirectoryCollection : CmsFileCollection {

		internal CmsDirectoryCollection(IList directories):base(directories){
		}

		public CmsWebDirectory this[int index]{
			get { return (CmsWebDirectory)this.InnerList[index]; }
		}

	}
}
