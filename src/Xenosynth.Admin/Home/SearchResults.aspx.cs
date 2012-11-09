using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Xenosynth.Search;
using Lucene.Net.Documents;

namespace Xenosynth.Admin.Home {
    public partial class SearchResults : System.Web.UI.Page {

        public SearchResultCollection Results;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                string searchTerms = Request["terms"];

                if (searchTerms != null && searchTerms.Length > 0) {

                    Results = SearchService.Search(searchTerms, 0, 10);

                    RepeaterSearchResults.DataSource = Results;
                    RepeaterSearchResults.DataBind();

                }

            }
        }
    }
}
