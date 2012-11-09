using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Xenosynth.Web.UI;
using Fluent;
using Xenosynth.Admin.Controls;
using Xenosynth.Web;

namespace Xenosynth.Admin.Content {

    /// <summary>
    /// Summary description for Search.
    /// </summary>
    public partial class Search : System.Web.UI.Page {

        protected MessageBox MessageBox1;

        public bool IsValidSearch {
            get {
                return TextBoxText.Text.Length > 0;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                TextBoxText.Text = Request["Text"];

                DoSearch();

            }
        }

        private void DoSearch() {
            if (IsValidSearch) {
                DataGridAdapterFiles.BindDataGrid();
            }
        }

        public void DataGridAdapterFiles_DataGridBinding(DataGridAdapter sender, DataGridBindingEventArgs e){

            CmsWebDirectory d = CmsContext.Current.RootDirectory;
            DataGridAdapterFiles.DataSource = d.SearchFiles(TextBoxText.Text);
		}

        protected void DataGridFiles_OnDeleteCommand(object sender, DataGridCommandEventArgs e) {
            //TODO: CmsFile
            CmsFile f = CmsFile.FindByID((Guid)DataGridFiles.DataKeys[e.Item.ItemIndex]);
            f.Delete();
            DoSearch();
        }

        protected void ButtonSearch_OnClick(object sender, System.EventArgs e) {
            DoSearch();
        }

        protected void ButtonPublishSelected_OnClick(object sender, EventArgs e) {
            //TODO: Better handle this error
            try {
                foreach (DataGridItem di in DataGridFiles.Items) {
                    CheckBox CheckBoxSelect = (CheckBox)di.FindControl("CheckBoxSelect");
                    if (CheckBoxSelect.Checked) {
                        CmsFile f = CmsFile.FindByID((Guid)DataGridFiles.DataKeys[di.ItemIndex]);
                        if (f.State == CmsState.Unpublished) {
                            f.Publish();
                        }
                    }
                }
            } catch (ApplicationException ae) {
                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Error, ae.Message);
            }
            DoSearch();
        }

        protected void ButtonDeleteSelected_OnClick(object sender, EventArgs e) {
            //TODO: Better handle this error
            try {
                foreach (DataGridItem di in DataGridFiles.Items) {
                    CheckBox CheckBoxSelect = (CheckBox)di.FindControl("CheckBoxSelect");
                    if (CheckBoxSelect.Checked) {
                        CmsFile f = CmsFile.FindByID((Guid)DataGridFiles.DataKeys[di.ItemIndex]);
                        f.Delete();
                    }
                }
            } catch (ApplicationException ae) {
                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Error, ae.Message);
            }
            DoSearch();
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
    }
}
