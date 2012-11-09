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
using Fluent.DataBinding;
using Xenosynth.Admin.Controls;

namespace Xenosynth.Admin.Content {
	/// <summary>
	/// Summary description for EditPageAttributes.
	/// </summary>
	public partial class EditPageAttributes : System.Web.UI.Page {

		protected MessageBox MessageBox1;

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
			if(!IsPostBack){
				BindAttributes();
			}
		}

		protected void BindAttributes(){
			DataGridAttributes.DataSource = CmsFileAttribute.FindByFileID(CurrentPage.ID);
			DataGridAttributes.DataBind();
		}


        public void ButtonSave_OnClick(object sender, EventArgs e) {
            foreach (DataGridItem item in DataGridAttributes.Items) {
                CmsFileAttribute attr = CmsFileAttribute.FindByID((Guid)DataGridAttributes.DataKeys[item.ItemIndex]);
                TextBox txtBox = (TextBox)item.FindControl("TextBoxValue");
                if(txtBox.Text != attr.Value){
                    attr.Value = txtBox.Text;
                    attr.Update();
                }
            }

            CmsFile.LogAuditEvent(CurrentPage, "Updated", "Attributes Updated");

            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Attributes have been updated.");
        }

		protected void DataGridAttributes_OnItemCommand(object sender, DataGridCommandEventArgs e){
			CmsFileAttribute attr = CmsFileAttribute.FindByID((Guid)DataGridAttributes.DataKeys[e.Item.ItemIndex]);
			switch(e.CommandName){
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

		protected void ButtonAddAttribute_OnClick(object sender, EventArgs e){
			CmsFileAttribute attr = new CmsFileAttribute();
			DataBindingManagerAttribute.DataSource = attr;
			DataBindingManagerAttribute.PullData();
			attr.FileID = CurrentPage.ID;
			attr.SortOrder = DataGridAttributes.Items.Count + 1;
			attr.Save();

			MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Attribute has been added.");
			BindAttributes();
			DataBindingManagerAttribute.Reset();
		}

		protected void ButtonCancel_OnClick(object sender, EventArgs e){
			DataBindingManagerAttribute.Reset();
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
