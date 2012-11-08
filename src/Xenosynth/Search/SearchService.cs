using System;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;

namespace Xenosynth.Search {
	
	/// <summary>
	/// Provides services for searching and indexing. 
	/// </summary>
    public class SearchService {

        private static List<Indexer> indexers = new List<Indexer>();
		
		/// <summary>
		/// Rebuilds all indexes. 
		/// </summary>
        public static void RebuildIndex() {

            string configuredSearchIndex = (string)XenosynthContext.Current.Configuration["xenosynth.installation.searchIndex"].Value;
            IndexWriter writer = new IndexWriter(configuredSearchIndex, new StandardAnalyzer(), true);

            foreach (Indexer i in indexers) {
                i.RebuildIndex(writer);
            }

            writer.Close();

        }
		
		/// <summary>
		/// Adds an Index to the service. 
		/// </summary>
		/// <param name="indexer">
		/// A <see cref="Indexer"/>
		/// </param>
        public static void AddIndexer(Indexer indexer) {
            indexers.Add(indexer);
        }
		
		/// <summary>
		/// Searches the configured index.  The default index is set using the key 'xenosynth.installation.searchIndex' in the <see ref="Xenosynth.Configuration.SystemConfiguration"/>.
		/// </summary>
		/// <param name="searchTerms">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="pageIndex">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <param name="pageSize">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <returns>
		/// A <see cref="SearchResultCollection"/>
		/// </returns>
        public static SearchResultCollection Search(string searchTerms, int pageIndex, int pageSize) {

            string configuredSearchIndex = (string)XenosynthContext.Current.Configuration["xenosynth.installation.searchIndex"].Value;
            IndexSearcher searcher = new IndexSearcher(configuredSearchIndex);

            MultiFieldQueryParser qp = new MultiFieldQueryParser(new string[] { "name", "content" }, new StandardAnalyzer());
            Query query = qp.Parse(searchTerms);

            Hits hits = searcher.Search(query);

            int startIndex = pageIndex * pageSize;
            int stopIndex = startIndex + pageSize;
            stopIndex = Math.Min(stopIndex, hits.Length());

            SearchResultCollection results = new SearchResultCollection(startIndex, hits.Length());

            if (startIndex < hits.Length()) {
                for (int i = startIndex; i < stopIndex; i++) {
                    Document d = hits.Doc(i);
                    SearchResult result = new SearchResult(
                        i,
                        GetField(d, "key"),
                        GetField(d, "type"),
                        hits.Score(i),
                        GetField(d, "name"),
                        GetField(d, "description")
                    );

                    results.AddResult(result);
                }
            }

            searcher.Close();

            return results;
        }

        private static string GetField(Document d, string name) {
            Field f = d.GetField(name);
            if (f != null) {
                return f.StringValue();
            } else {
                return null;
            }
        }
    }
}
