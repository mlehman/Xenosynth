using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

using Xenosynth.Data;

namespace Xenosynth.Web.UI.WebControls {
	
	/// <summary>
	/// A control for creating a image upload for an image content block.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:ImagePlaceHolder runat=server></{0}:ImagePlaceHolder>"),
	ContentType(typeof(LiteralContent))]
	public class ImagePlaceHolder : ContentPlaceHolder { 

		private Image image;
		private TextBox textBox;
		private StaticDust.Web.UI.Controls.UploadDialogButton b;


		protected override void CreateAuthoringChildControls() {

			image = new Image();
			image.ID = this.ID + "Image1";
			this.Controls.Add(image);

			this.Controls.Add(new LiteralControl("<br />"));

			textBox = new TextBox();
			textBox.ID = this.ID + "TextBox1";
			this.Controls.Add(textBox);


			b = new StaticDust.Web.UI.Controls.UploadDialogButton();
			b.ControlToFill = this.ID+"TextBox1";

			//image galleries
			if(((CmsWebDirectory)CmsPage.Current.ParentDirectory).HasImageGallery){
                CmsImageGallery mg = CmsImageGallery.FindByID(((CmsWebDirectory)CmsPage.Current.ParentDirectory).ImageGalleryID);
				//b.UploadDirectory = mg.Path; //TODO: Image Upload.
				b.FileTypes="gif,jpg,png";
				b.AllowCreateDirectory=true;
				b.AllowUpload=true;
				b.AllowRename=true;
				b.AllowDelete=true;
				b.AllowEditTextBox=false;
			} else {
				Literal l = new Literal();
				this.Controls.Add(l);
				l.Text = "[No Media Gallery.]";
				b.Visible = false;
			}
			
			this.Controls.Add(b);

			

			ChildControlsCreated = true;
		}

		protected override void CreatePresentationChildControls() {

			image = new Image();
			image.ID = this.ID + "Image1";
			this.Controls.Add(image);

			textBox = new TextBox();
			textBox.ID = this.ID + "TextBox1";
			this.Controls.Add(textBox);
			textBox.Visible = false;

			ChildControlsCreated = true;
		}


		protected override void LoadAuthoringPlaceHolderContent() {
			EnsureChildControls();
			if(Content.IsAvailable){
				this.image.ImageUrl = ((LiteralContent)Content).Text;
				this.textBox.Text = ResolveUrl(((LiteralContent)Content).Text);
			} else {
				this.b.Visible = false;
				this.textBox.Visible = false;
				Literal l = new Literal();
				this.Controls.Add(l);
				l.Text = string.Format("[Content '{0}' is not registered.]", this.ID);
			}
			
		}

		protected override void LoadPresentationPlaceHolderContent() {
			EnsureChildControls();
			if(Content.IsAvailable){
                string url = ((LiteralContent)Content).Text;
                if (url != null && url.Length > 0) {
                    this.image.ImageUrl = url;
                    this.image.Visible = true;
                } else {
                    this.image.Visible = false;
                }
			}
			
		}

		protected override void SavePlaceHolderContent() {
			EnsureChildControls();
			((LiteralContent)Content).Text = ReplaceAppName(this.textBox.Text);
		}

		protected string ReplaceAppName(string path){
			//TODO: Need better
			string appPath = ResolveUrl("~/");
			if(path.ToLower().StartsWith(appPath.ToLower())){
				return "~/" + path.Substring(appPath.Length);
			} else return path;
		}
	}
}
