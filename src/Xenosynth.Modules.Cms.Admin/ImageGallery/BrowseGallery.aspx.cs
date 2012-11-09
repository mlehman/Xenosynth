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
using Xenosynth.Admin.Controls;
using Fluent;

namespace Xenosynth.Admin.Content.ImageGallery {

    public partial class BrowseGallery : System.Web.UI.Page {

        private CmsImageGallery cmsDirectoryCache;

        public CmsImageGallery CurrentDirectory {
            get {
                if (cmsDirectoryCache == null) {
                    Guid id = new Guid(Request["FileID"]);
                    cmsDirectoryCache = CmsImageGallery.FindByID(id);
                }
                return cmsDirectoryCache;
            }
        }


        protected void Page_Load(object sender, System.EventArgs e) {

            if (!IsPostBack) {

                PathBrowser1.AvaliableViews = new String[] { "Details", "Thumbnails"};

                string sortExpression = (string)XenosynthContext.Current.Configuration.GetValue("Xenosynth.Modules.Cms.ImageGallery.SortExpression", false);
                if (sortExpression == null) {
                    sortExpression = "SortOrder";
                }
                DataGridAdapterFiles.SortExpression = "SortOrder";
            }

            //Rebind rather than use viewstate
            BindDataGridFiles();

        }


        private void BindDataGridFiles() {
            if (PathBrowser1.SelectedView == "Details") {
                DataGridFiles.Visible = true;
                DataListThumbnails.Visible = false;
                DataGridAdapterFiles.BindDataGrid();
            } else {
                DataGridFiles.Visible = false;
                DataListThumbnails.Visible = true;
                DataListThumbnails.DataSource = CmsFile.FindByDirectoryID(CurrentDirectory.ID, true);
                DataListThumbnails.DataBind();
            }
        }

        public void DataGridAdapterFiles_DataGridBinding(DataGridAdapter sender, DataGridBindingEventArgs e) {
            CmsHttpContext.Current.Mode = CmsMode.Unpublished;
            DataGridAdapterFiles.DataSource = CmsFile.FindByDirectoryID(CurrentDirectory.ID, true);
        }


        protected void DataGridFiles_OnDeleteCommand(object sender, DataGridCommandEventArgs e) {
            //TODO: Better handle this error
            try {
                CmsFile f = CmsFile.FindByID((Guid)DataGridFiles.DataKeys[e.Item.ItemIndex]);
                f.Delete();
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
                        f.Delete();
                    }
                }
            } catch (ApplicationException ae) {
                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Error, ae.Message);
            }
            BindDataGridFiles();
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
            BindDataGridFiles();
        }

        protected void DataGridFiles_OnItemCommand(Object sender, DataGridCommandEventArgs e) {

            switch (e.CommandName) {
                case "SortOrderUpdated":
                    BindDataGridFiles();
                    break;
            }

        }

        protected string GetStateDescription(object o) {
            string state = "";

            if (o is CmsPage) {
                CmsPage p = (CmsPage)o;
                state += p.State;
            }

            return state;
        }
    }
}
