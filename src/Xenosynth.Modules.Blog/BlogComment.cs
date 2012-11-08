using System;
using System.Collections;
using System.Text;
using Inform;
using System.Collections.Generic;

namespace Xenosynth.Modules.Blog {

    [TypeMapping(TableName = "blog_BlogComments")]
    public class BlogComment {

        [MemberMapping(PrimaryKey = true, ColumnName = "ID")]
        protected Guid id;

        [MemberMapping(ColumnName = "BlogPostID")]
        private Guid blogPostID;

        [MemberMapping(ColumnName = "DateCreated")]
        private DateTime dateCreated;

        [MemberMapping(ColumnName = "Name", Length = 50)]
        private string name;

        [MemberMapping(ColumnName = "Url", Length = 500)]
        private string url;

        [MemberMapping(ColumnName = "Text", Length = 4000)]
        private string text;

        [MemberMapping(ColumnName = "IP", Length = 50)]
        private string ip;

        public Guid ID {
            get { return id; }
        }

        public Guid BlogPostID {
            set { blogPostID = value; }
            get { return blogPostID; }
        }

        public DateTime DateCreated {
            set { dateCreated = value; }
            get { return dateCreated; }
        }

        public string Name {
            set { name = value; }
            get { return name; }
        }

        public string Url {
            set { url = value; }
            get { return url; }
        }

        public string Text {
            set { text = value; }
            get { return text; }
        }

        public string IP {
            set { ip = value; }
            get { return ip; }
        }


        public static IList FindAll() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(BlogComment), "ORDER BY DateCreated DESC", true);
            return cmd.Execute();
        }

        public static IList FindAllByPost(Guid blogPostID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(BlogComment), "WHERE BlogPostID = @BlogPostID ORDER BY DateCreated", true);
            cmd.CreateInputParameter("@BlogPostID", blogPostID);
            return cmd.Execute();
        }

        internal static IList FindRecentByPost(Guid blogPostID, int count) {
   
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth"); 
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(BlogComment), "SELECT TOP " + count +" * FROM blog_BlogComments WHERE BlogPostID = @BlogPostID ORDER BY DateCreated DESC");
            cmd.CreateInputParameter("@BlogPostID", blogPostID);
            return cmd.Execute();
        }

        public static BlogComment FindByID(Guid blogID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (BlogComment)ds.FindByPrimaryKey(typeof(BlogComment), blogID);
        }

        public void Insert() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            this.id = Guid.NewGuid();
            ds.Insert(this);
        }

        public virtual void Update() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Update(this);
        }

        public virtual void Delete() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Delete(this);
        }

        internal static int GetTotal(Guid blogPostID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IDataAccessCommand cmd = ds.CreateDataAccessCommand("SELECT COUNT(*) FROM blog_BlogComments WHERE BlogPostID = @BlogPostID ");
            cmd.CreateInputParameter("@BlogPostID", blogPostID);
            return (int)cmd.ExecuteScalar();
        }

        
    }
}
