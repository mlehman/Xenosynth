using System;
using System.Collections;
using System.Text;
using Inform.Common;
using Xenosynth.Web.UI;
using System.Web;
using Inform;
using System.Web.UI;
using Xenosynth.Web;
using System.IO;

namespace Xenosynth.Modules.Blog {

    [TypeMapping(BaseType = typeof(CmsDirectory), TableName = "blog_Blogs")]
    public class Blog : CmsDirectory {

		[MemberMapping(ColumnName="Description", Length=500)]
		private string description;

        [MemberMapping(ColumnName = "CommentsEnabled", Length = 500)]
        private bool isCommentsEnabled;

        [MemberMapping(ColumnName = "BlogTemplateID", AllowNulls = true)]
        private Guid blogTemplateID;

        [MemberMapping(ColumnName = "BlogPostTemplateID", AllowNulls = true)]
        private Guid blogPostTemplateID;

        [RelationshipMapping(Name = "BlogTemplate", ChildMember = "blogTemplateID",
             ParentType = typeof(CmsTemplate), ParentMember = "id")]
        private ObjectCache cachedBlogTemplate = new ObjectCache();

        [RelationshipMapping(Name = "BlogPostTemplate", ChildMember = "blogPostTemplateID",
             ParentType = typeof(CmsTemplate), ParentMember = "id")]
        private ObjectCache cachedBlogPostingTemplate = new ObjectCache();

        private static Guid typeID = new Guid("4FB439C5-5E8C-41dd-AB4B-CC98194AB529");

        public string Description {
			set { description = value; }
			get { return description; }
		}

        public Guid BlogTemplateID {
            set { blogTemplateID = value; }
            get { return blogTemplateID; }
        }

        public bool IsCommentsEnabled {
            set { isCommentsEnabled = value; }
            get { return isCommentsEnabled; }
        }

        public CmsTemplate BlogTemplate {
            get { return (CmsTemplate)cachedBlogTemplate.CachedObject; }
        }

        public Guid BlogPostTemplateID {
            set { blogPostTemplateID = value; }
            get { return blogPostTemplateID; }
        }

        public CmsTemplate BlogPostTemplate {
            get { return (CmsTemplate)cachedBlogPostingTemplate.CachedObject; }
        }


        public Blog()
            : base(typeID) {
        }

		//TODO: Typed?
		public static IList FindAll(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(Blog), "ORDER BY FullPath", true);
			return cmd.Execute();
		}

        public static Blog FindByID(Guid blogID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (Blog)ds.FindByPrimaryKey(typeof(Blog), blogID);
        }

        public override string DefaultFileName {
            //TODO: Expand this logic!
            get {
                return "_Default.aspx";
            }

        }

        public override IHttpHandler GetHandler(string url) {
            string pathTranslated = HttpContext.Current.Server.MapPath(ResolveTemplate(this.BlogTemplate.Url));
            //HttpContext.Current.RewritePath(this.Template.Url, false);         
            return PageParser.GetCompiledPageInstance(Path.Combine(url, DefaultFileName), pathTranslated, HttpContext.Current);
        }

        public string ResolveTemplate(string url) {
            if (url.StartsWith("/")) {
                url = url.Replace(CmsContext.Current.RootDirectory.FullPath, "~");
            }
            return url;
        }
      
        
    }

}
