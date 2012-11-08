using System;
using System.Collections;

namespace Xenosynth.Web {
	
	/// <summary>
	/// A set of search results returned from a CmsDirectory search.
	/// </summary>
	public class SearchResultCollection : IEnumerable, ICollection {

		private IList results;

		protected internal SearchResultCollection(IList results) {
			this.results = results;
		}
		
		/// <summary>
		/// The result count. 
		/// </summary>
		public int Count {
			get{ return results.Count; }
		}

		public IEnumerator GetEnumerator() {
			return results.GetEnumerator();
		}
	
		#region ICollection Members

		public bool IsSynchronized {
			get {
				return results.IsSynchronized;
			}
		}

		public void CopyTo(Array array, int index) {
			throw new NotImplementedException();
		}

		public object SyncRoot {
			get {
				return results.SyncRoot;
			}
		}

		#endregion
	}
}
