using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Inform;

namespace Xenosynth.Admin.Controls.WebParts {
    
    [TypeMapping(TableName="xs_RegisteredWebParts")]
    public class RegisteredWebPart {

        [MemberMapping(PrimaryKey = true, ColumnName = "ID")]
        protected Guid id;

        [MemberMapping(ColumnName = "Title", Length = 100)]
        protected string title;

        [MemberMapping(ColumnName = "Description", Length = 250)]
        protected string description;

        [MemberMapping(ColumnName = "ClassName", Length = 100)]
        protected string className;

        [MemberMapping(ColumnName = "Url", Length = 250)]
        protected string url;

        [MemberMapping(ColumnName = "ImageUrl", Length = 250)]
        protected string imageUrl;

        public RegisteredWebPart() {
        }

        public Guid ID {
            get { return id; }
            set { id = value; }
        }

        public string Title {
            get { return title; }
            set { title = value; }
        }

        public string Description {
            get { return description; }
            set { description = value; }
        }

        public string ClassName {
            get { return className; }
            set { className = value; }
        }

        public string Url {
            get { return url; }
            set { url = value; }
        }

        public string ImageUrl {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        public void Insert() {
            this.id = Guid.NewGuid();
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Insert(this);
        }
        

        public void Update() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Update(this);
        }

        public void Delete() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Delete(this);
        }

        public static IList FindAll() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(RegisteredWebPart), "ORDER BY Title");
            return cmd.Execute();
        }


        public static RegisteredWebPart FindByID(Guid id) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (RegisteredWebPart)ds.FindByPrimaryKey(typeof(RegisteredWebPart), id);
        }

    }


}
