using System;
using System.Collections.Generic;
using System.Text;

namespace Xenosynth.Search {
	
	/// <summary>
	/// The Indexer for the search an index. 
	/// </summary>
    public abstract class Indexer {
		
		/// <summary>
		/// Rebuilds the index. 
		/// </summary>
		/// <param name="writer">
		/// A <see cref="Lucene.Net.Index.IndexWriter"/>
		/// </param>
        abstract public void RebuildIndex(Lucene.Net.Index.IndexWriter writer);
    }
}
