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

using Xenosynth.Web;
using Xenosynth.Web.UI;
using Fluent.DataBinding;
using Xenosynth.Admin.Controls;

namespace Xenosynth.Modules.Cms.Admin.Shortcut {
    public partial class EditShortcut : System.Web.UI.Page {

        protected DataBindingManager DataBindingManagerShortcut;
        protected MessageBox MessageBox1;

        public Guid FileID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsShortcut CurrentShortcut {
            get { return (CmsShortcut)CmsFile.FindByID(FileID); }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
              
                RegularExpressionValidatorFileName.ValidationExpression = AllowedPageExtensionsRegex;
                RegularExpressionValidatorFileName.ErrorMessage = AllowedPageExtensionsMessage;

                DataBindingManagerShortcut.DataSource = CurrentShortcut;
                DataBindingManagerShortcut.PushData();
            }
        }

        protected string AllowedPageExtensionsMessage {
            get {
                string extensions = string.Join(",", CmsConfiguration.Current.AllowedPageExtensions);
                return string.Format("Please use a page extension ( {0} )", extensions);
            }
        }

        protected string AllowedPageExtensionsRegex {
            get {
                return CmsConfiguration.Current.AllowedPageNameRegex;
            }
        }


        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {

            if (IsValid) {
                CmsShortcut sc = CurrentShortcut;
                DataBindingManagerShortcut.DataSource = sc;
                DataBindingManagerShortcut.PullData();

                sc.Update();

                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Shortcut has been updated.");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            CmsShortcut sc = CurrentShortcut;
            DataBindingManagerShortcut.DataSource = sc;
            DataBindingManagerShortcut.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }

    }
}
