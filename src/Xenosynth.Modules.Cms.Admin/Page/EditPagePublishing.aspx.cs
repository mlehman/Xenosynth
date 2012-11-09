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

using Fluent.DataBinding;
using Xenosynth.Web;
using Xenosynth.Web.UI;
using Xenosynth.Admin.Controls;

namespace Xenosynth.Admin.Content.Page {
    public partial class EditPagePublishing : System.Web.UI.Page {

        CmsPage pageCache;

        public Guid PageID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsPage CurrentPage {
            get {
                if (pageCache == null) {
                    pageCache = CmsPage.FindByID(PageID);
                }
                return pageCache;
            }
        }


        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                DataBindingManagerPage.DataSource = CurrentPage;
                DataBindingManagerPage.PushData();
            }
        }



        protected void ButtonUpdatePage_OnClick(object sender, EventArgs e) {
            CmsPage p = CurrentPage;
            DataBindingManagerPage.DataSource = p;
            DataBindingManagerPage.PullData();

            p.DateModified = DateTime.Now;
            p.Update();

            p.UpdatePublishingSchedule();
            
            CmsFile.LogAuditEvent(CurrentPage, "Updated", "Publishing Schedule Updated");

            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Page has been updated.");
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            CmsPage p = CurrentPage;
            DataBindingManagerPage.DataSource = CurrentPage;
            DataBindingManagerPage.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }

    }
}
