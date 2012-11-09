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
using Fluent;

namespace Xenosynth.Modules.Cms.Admin.Controls {
    public partial class ViewFileHistory : System.Web.UI.UserControl {

        CmsFile cachedFile;
        public string MessageBoxControl;
        private MessageBox messageBox1;
        protected DataGrid DataGridAuditLog;
        protected DataGridAdapter DataGridAdapterAuditLog;

        public MessageBox MessageBox1 {
            get {
                if (messageBox1 == null) {
                    messageBox1 = (MessageBox)this.Parent.FindControl(MessageBoxControl);
                    if (messageBox1 == null) {
                        throw new ApplicationException("MessageBoxControl '" + MessageBoxControl + "' not found.");
                    }
                }
                return messageBox1;
            }
        }

        public Guid FileID {
            get {
                if (ViewState["FileID"] != null) {
                    return (Guid)ViewState["FileID"];
                } else {
                    return Guid.Empty;
                }
            }
            set {
                ViewState["FileID"] = value;
            }
        }

        public CmsFile FileToBind {
            get {
                if (cachedFile == null) {
                    if (FileID != Guid.Empty) {
                        cachedFile = CmsFile.FindByID(FileID);
                    }
                }
                return cachedFile;
            }
            set {
                FileID = value.ID;
                cachedFile = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e) {

        }

        //protected void BindHistory() {
        //    DataGridAuditLog.DataSource = FileToBind.AuditLog;
        //    DataGridAuditLog.DataBind();
        //}

        public override void DataBind() {
            //base.DataBind();
            DataGridAdapterAuditLog.BindDataGrid();
        }

        protected void DataGridAdapterAuditLog_DataGridBinding(object source, DataGridBindingEventArgs e) {
            DataGridAdapterAuditLog.DataSource = FileToBind.AuditLog;
        }
    }
}