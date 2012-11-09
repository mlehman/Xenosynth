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
using Xenosynth.Admin.Controls;

namespace Xenosynth.Modules.Cms.Admin.Configuration {
    public partial class EditFileType : System.Web.UI.Page {

        protected MessageBox MessageBox1;

        public Guid FileTypeID {
            get { return new Guid(Request["FileTypeID"]); }
        }

        public CmsFileType CurrentFileType {
            get { return CmsFileType.FindByID(FileTypeID); }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                DataBindingManagerFileType.DataSource = CurrentFileType;
                DataBindingManagerFileType.PushData();
            }
        }


        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {
            CmsFileType ft = CurrentFileType;
            DataBindingManagerFileType.DataSource = ft;
            DataBindingManagerFileType.PullData();

            ft.Update();

            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "File type has been updated.");
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            CmsFileType ft = CurrentFileType;
            DataBindingManagerFileType.DataSource = ft;
            DataBindingManagerFileType.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }

    }
}
