using System;
using System.Collections;
using System.Collections.Specialized;

namespace Xenosynth.Web.UI {
	
	/// <summary>
	/// A collection of CmsFileAttributes for a CmsFile..
	/// </summary>
	public class CmsFileAttributeCollection : IEnumerable {

		internal Guid fileID; //TODO: fix, this is version ID
		internal NameValueCollection nvc = new NameValueCollection();
        internal IList attributes;
		
		internal CmsFileAttributeCollection(Guid fileID, IList attributes) {
			this.fileID = fileID;

			foreach(CmsFileAttribute a in attributes){
				this.nvc.Add(a.Name,a.Value);
			}
		}
		
		/// <summary>
		/// Returns the value for an attribute name. 
		/// </summary>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		public string this[string name] {
			get {
				return (string)nvc[name];
			}
            set {
                CmsFileAttribute attr = CmsFileAttribute.FindSingleAttributeByFileID(this.fileID, name);
                if (attr != null) {
                    attr.Value = value;
                    attr.Update();
                } else {
                    this.Add(name, value);
                }
            }
		}
		
		/// <summary>
		/// Returns an array of all values for a a multi-valued attribute by name. 
		/// </summary>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String[]"/>
		/// </returns>
		public string[] GetValues(string name){
			string[] values = nvc.GetValues(name);
            if (values != null) {
                return values;
            } else {
                return new string[]{};
            }
		}
		
		/// <summary>
		/// Whether this collection contains an attribute with this name. 
		/// </summary>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public bool Contains(string name){
			return nvc[name] != null;
		}
		
		/// <summary>
		/// Whether this collection contains an attribute with this name and value. 
		/// </summary>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="value">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public bool Contains(string name, string value){
			string[] vals = nvc.GetValues(name);
			if(vals != null){
				foreach(string v in vals){
					if(v == value){
						return true;
					}
				}
			}
			return false;
		}
		
		/// <summary>
		/// Adds a new attributes to the collection and saves it the CmsFile for this collection in the database. 
		/// </summary>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="value">
		/// A <see cref="System.String"/>
		/// </param>
		public void Add(string name, string value){
			CmsFileAttribute attr = new CmsFileAttribute();
			attr.FileID = fileID;
			attr.Name = name;
			attr.Value = value;
			attr.SortOrder = this.nvc.Count;
			attr.Save();

			nvc.Add(name,value);
		}
		
		/// <summary>
		/// Removes the specified attributes by name and value from the database for this CmsFile. 
		/// </summary>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="value">
		/// A <see cref="System.String"/>
		/// </param>
		public void Remove(string name, string value){
			CmsFileAttribute.Delete(fileID, name, value);
		}


		#region IEnumerable Members

		public IEnumerator GetEnumerator() {
			return nvc.GetEnumerator();
		}

		#endregion
	}
}
