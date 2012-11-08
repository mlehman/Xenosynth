using System;
using System.Collections.Generic;
using System.Text;

using Xenosynth.Web;
using System.Web.UI;
using Inform;
using Xenosynth.Web.UI;
using System.Web;
using System.IO;

namespace Xenosynth.Modules.Blog {

    [TypeMapping(BaseType = typeof(CmsFile), TableName = "blog_BlogPosts")]
    public class BlogPost : CmsFile {

        [MemberMapping(ColumnName="Text", DbType="TEXT", AllowNulls=false)]
		private string text;

        [MemberMapping(ColumnName = "Author", Length=50)]
        private string author;

        private static Guid typeID = new Guid("735F16E4-05F8-45d2-B08B-33487390F7A7");

        public BlogPost()
            : base(typeID) {
        }

        public string Text {
            get { return text; }
            set { text = value; }
        }

        public string Author {
            get { return author; }
            set { author = value; }
        }

        public int TotalComments {
            get {
                return BlogComment.GetTotal(this.FileID);
            }
        }

        public new static BlogPost Current {
            get { return (BlogPost)CmsHttpContext.Current.CmsFile; }
        }

        public List<BlogComment> FindRecentComments() {
            return null;
        }

        public override IHttpHandler GetHandler(string url) {
            Blog b = (Blog)this.ParentDirectory;
            string pathTranslated = HttpContext.Current.Server.MapPath(ResolveTemplate(b.BlogPostTemplate.Url));   
            return PageParser.GetCompiledPageInstance(url, pathTranslated, HttpContext.Current);
        }

        public string ResolveTemplate(string url) {
            if (url.StartsWith("/")) {
                url = url.Replace(CmsContext.Current.RootDirectory.FullPath, "~");
            }
            return url;
        }

    }

}