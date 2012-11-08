using System;

namespace Xenosynth.Web.UI {
	
	/// <summary>
	/// A CmsAttribute can be used to represent a name value pair for CmsFileAttributes.
	/// </summary>
	public class CmsAttribute {
		
		private string name;
		private string val;

		public CmsAttribute() {
		}

		public CmsAttribute(string name, string value) {
			this.name = name;
			this.val = value;
		}
		
		/// <summary>
		/// The attribute name. 
		/// </summary>
		public string Name {
			get {return name; }
			set { name = value; }
		}
		
		/// <summary>
		/// The attribute value. 
		/// </summary>
		public string Value {
			get {return val; }
			set { val = value; }
		}
 
	}
}
