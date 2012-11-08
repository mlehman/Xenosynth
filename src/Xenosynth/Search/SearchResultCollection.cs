using System;
using System.Collections.Generic;
using System.Text;

namespace Xenosynth.Search {
	
	/// <summary>
	/// The paged results from a SearchService search. 
	/// </summary>
    public class SearchResultCollection : IEnumerable<SearchResult> {

        private int startIndex;
        private int totalResults;
        private List<SearchResult> results = new List<SearchResult>();
		
		/// <summary>
		/// The starting index of the results.
		/// </summary>
        public int StartIndex {
            get { return startIndex; }
        }
		
		/// <summary>
		/// The stop index of the results. 
		/// </summary>
        public int StopIndex {
            get { return startIndex + results.Count; }
        }
		
		// The total results for the search.
        public int TotalResults {
            get { return totalResults; }
        }

        internal SearchResultCollection(int startIndex, int totalResults) {
            this.startIndex = startIndex;
            this.totalResults = totalResults;
        }

        public IEnumerator<SearchResult> GetEnumerator() {
            return results.GetEnumerator();
        }


        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return results.GetEnumerator();
        }

        #endregion

        internal void AddResult(SearchResult result) {
            this.results.Add(result);
        }
    }
}
