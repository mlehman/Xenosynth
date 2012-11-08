using System;
using System.Collections.Generic;
using System.Text;

namespace Xenosynth.Search {
	
	/// <summary>
	/// A result from a SearchService search. 
	/// </summary>
    public class SearchResult {

        private int index;
        private string key;
        private string type;
        private float rank;
        private string name;
        private string description;
		
		/// <summary>
		/// The index of this search result. 
		/// </summary>
        public int Index {
            get { return index; }
        }
		
		/// <summary>
		/// The key of the search result. 
		/// </summary>
        public string Key {
            get{ return key; }
        }
		
		/// <summary>
		/// The type of the result. 
		/// </summary>
        public string Type {
            get{ return type; }
        }
		
		/// <summary>
		/// The rank of the result. 
		/// </summary>
        public float Rank {
            get { return rank; }
        }
		
		/// <summary>
		/// The name of the result. 
		/// </summary>
        public string Name {
            get { return name; }
        }
		
		/// <summary>
		/// The description of the result. 
		/// </summary>
        public string Description {
            get { return description; }
        }

        internal SearchResult(int index, string key, string type, float rank, string name, string description) {
            this.index = index;
            this.key = key;
            this.type = type;
            this.rank = rank;
            this.name = name;
            this.description = description;
        }
    }
}
