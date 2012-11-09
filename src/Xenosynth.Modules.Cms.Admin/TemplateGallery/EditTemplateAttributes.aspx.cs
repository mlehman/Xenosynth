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

namespace Xenosynth.Modules.Cms.Admin.TemplateGallery {
    public partial class EditTemplateAttributes : System.Web.UI.Page {
        protected MessageBox MessageBox1;

        private CmsFile fileCache;

        public Guid FileID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsFile CurrentFile {
            get {
                if (fileCache == null) {
                    fileCache = CmsFile.FindByID(FileID);
                }
                return fileCache;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                BindAttributes();
            }
        }

        protected void BindAttributes() {
            DataGridAttributes.DataSource = CmsFileAttribute.FindByFileID(CurrentFile.ID);
            DataGridAttributes.DataBind();
        }


        public void ButtonSave_OnClick(object sender, EventArgs e) {
            foreach (DataGridItem item in DataGridAttributes.Items) {
                CmsFileAttribute attr = CmsFileAttribute.FindByID((Guid)DataGridAttributes.DataKeys[item.ItemIndex]);
                TextBox txtBox = (TextBox)item.FindControl("TextBoxValue");
                if (txtBox.Text != attr.Value) {
                    attr.Value = txtBox.Text;
                    attr.Update();
                }
            }
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Attributes have been updated.");
        }

        protected void DataGridAttributes_OnItemCommand(object sender, DataGridCommandEventArgs e) {
            CmsFileAttribute attr = CmsFileAttribute.FindByID((Guid)DataGridAttributes.DataKeys[e.Item.ItemIndex]);
            switch (e.CommandName) {
                case "Update":
                    attr.Value = ((TextBox)e.Item.FindControl("TextBoxValue")).Text;
                    attr.Update();
                    MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Attribute has been updated.");
                    break;
                case "Delete":
                    attr.Delete();
                    MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Attribute has been deleted.");
                    break;
                case "MoveUp":
                    CmsFileAttribute.UpdateSortOrder(attr.FileID);
                    attr.MoveSortOrder(true);
                    break;
                case "MoveDown":
                    CmsFileAttribute.UpdateSortOrder(attr.FileID);
                    attr.MoveSortOrder(false);
                    break;
            }
            BindAttributes();
        }

        protected void ButtonAddAttribute_OnClick(object sender, EventArgs e) {
            CmsFileAttribute attr = new CmsFileAttribute();
            DataBindingManagerAttribute.DataSource = attr;
            DataBindingManagerAttribute.PullData();
            attr.FileID = CurrentFile.ID;
            attr.SortOrder = DataGridAttributes.Items.Count + 1;
            attr.Save();

            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Attribute has been added.");
            BindAttributes();
            DataBindingManagerAttribute.Reset();
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerAttribute.Reset();
        }
    }
}
