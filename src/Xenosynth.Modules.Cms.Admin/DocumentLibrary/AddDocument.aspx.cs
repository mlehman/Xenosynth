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
using Xenosynth.Web.UI;
using Fluent.DataBinding;

namespace Xenosynth.Modules.Cms.Admin.DocumentLibrary {
    public partial class AddDocument : System.Web.UI.Page {

        protected HtmlInputFile HtmlInputFileAttach;
        protected DataBindingManager DataBindingManagerDocument;

        private CmsDocumentLibrary cachedLibrary;


        public Guid LibraryID {
            get {
                string id = Request["FileID"];
                if (id == null) {
                    return Guid.Empty;
                } else {
                    return new Guid(id);
                }
            }
        }

        public CmsDocumentLibrary CurrentLibrary {
            get {
                if (cachedLibrary == null && LibraryID != Guid.Empty) {
                    cachedLibrary = CmsDocumentLibrary.FindByID(LibraryID);
                }
                return cachedLibrary;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {
                CmsDocument d = new CmsDocument(HtmlInputFileAttach.PostedFile);
                DataBindingManagerDocument.DataSource = d;
                DataBindingManagerDocument.PullData();

                d.ParentID = CurrentLibrary.ID;
                d.SortOrder = CurrentLibrary.Files.Count;
                d.Insert();

                Response.Redirect(d.FileType.EditUrl + "?FileID=" + d.ID);
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {

        }
    }
}
