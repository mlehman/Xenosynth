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
using Fluent;
using Xenosynth.Web.UI;
using Xenosynth.Modules.Cms.User;
using Xenosynth.Admin.Controls;

namespace Xenosynth.Modules.Cms.Admin {
    public partial class DeletedFiles : System.Web.UI.Page {
        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                BindDataGridFiles();
            }
        }

        private void BindDataGridFiles() {
            DataGridAdapterFiles.BindDataGrid();
        }

        public void DataGridAdapterFiles_DataGridBinding(DataGridAdapter sender, DataGridBindingEventArgs e) {

            CmsWebDirectory d = CmsContext.Current.RootDirectory;
            DataGridAdapterFiles.DataSource = d.FindDeletedFiles();
        }


        protected void DataGridFiles_OnItemCommand(object sender, DataGridCommandEventArgs e) {
            try {
                if (e.CommandName == "Restore") {
                    CmsFile f = CmsFile.FindByID((Guid)DataGridFiles.DataKeys[e.Item.ItemIndex]);
                    f.Restore();
                    RecentFiles.AddFile(f);
                    BindDataGridFiles();
                } else if (e.CommandName == "Delete") {
                    //TODO: CmsFile
                    CmsFile f = CmsFile.FindByID((Guid)DataGridFiles.DataKeys[e.Item.ItemIndex]);
                    f.PermanentlyDelete();
                    BindDataGridFiles();
                }
            } catch (ApplicationException ae) {
                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Error, ae.Message);
            }
        }

        protected void ButtonRestoreSelected_OnClick(object sender, EventArgs e) {
            //TODO: Better handle this error
            try {
                foreach (DataGridItem di in DataGridFiles.Items) {
                    CheckBox CheckBoxSelect = (CheckBox)di.FindControl("CheckBoxSelect");
                    if (CheckBoxSelect.Checked) {
                        CmsFile f = CmsFile.FindByID((Guid)DataGridFiles.DataKeys[di.ItemIndex]);
                        if (f.State == CmsState.Unpublished) {
                            f.Restore();
                        }
                    }
                }
            } catch (ApplicationException ae) {
                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Error, ae.Message);
            }
            BindDataGridFiles();
        }

        protected void ButtonDeleteSelected_OnClick(object sender, EventArgs e) {
            //TODO: Better handle this error
            try {
                foreach (DataGridItem di in DataGridFiles.Items) {
                    CheckBox CheckBoxSelect = (CheckBox)di.FindControl("CheckBoxSelect");
                    if (CheckBoxSelect.Checked) {
                        CmsFile f = CmsFile.FindByID((Guid)DataGridFiles.DataKeys[di.ItemIndex]);
                        f.PermanentlyDelete();
                    }
                }
            } catch (ApplicationException ae) {
                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Error, ae.Message);
            }
            BindDataGridFiles();
        }
    }
}
