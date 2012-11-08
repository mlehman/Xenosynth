using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using Xenosynth.Data;
using FreeTextBoxControls;
using Xenosynth.Modules.Cms;

namespace Xenosynth.Web.UI.WebControls {

	/// <summary>
	/// A control for creating an html editor for a content block.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:HtmlContentPlaceHolder runat=server></{0}:HtmlContentPlaceHolder>"),
	ContentType(typeof(LiteralContent))]
	public class HtmlContentPlaceHolder : ContentPlaceHolder {

		Unit width;
		Unit height;
		//string toolbarLayout;

		public Unit Width {
			get { return width; }
			set { width = value; }
		}

		public Unit Height {
			get { return height; }
			set { height = value; }
		}

//		public string ToolbarLayout {
//			get { return toolbarLayout; }
//			set { toolbarLayout = value; }
//		}


		FreeTextBox textBox;
		Literal literal;

		protected override void CreateAuthoringChildControls() {

			literal = new Literal();
			literal.ID = this.ID + "Literal1";
			this.Controls.Add(literal);
			literal.Visible = false;

			textBox = new FreeTextBox();
			textBox.ID = this.ID + "TextBox1";
			this.Controls.Add(textBox);


			textBox.Width = Width;
			textBox.Height = Height;
			textBox.Visible = true;
			
			//image galleries
			if(((CmsWebDirectory)CmsPage.Current.ParentDirectory).HasImageGallery){				
				textBox.Toolbars[2].Items.Add( new InsertImageFromGallery());
				textBox.ImageGalleryUrl = ResolveUrl( CmsConfiguration.Current.AdminPath + "MediaGalleries/ftb.imagegallery.aspx?GalleryID=" + ((CmsWebDirectory)CmsPage.Current.ParentDirectory).ImageGalleryID );
			}

			ChildControlsCreated = true;

		}

		protected override void CreatePresentationChildControls() {
			literal = new Literal();
			literal.ID = this.ID + "Literal1";
			this.Controls.Add(literal);

			textBox = new FreeTextBox();
			textBox.ID = this.ID + "TextBox1";
			this.Controls.Add(textBox);
			textBox.Visible = false;

			ChildControlsCreated = true;
		}


		protected override void LoadAuthoringPlaceHolderContent() {
			EnsureChildControls();
			if(Content.IsAvailable){
				this.textBox.Text = ((LiteralContent)Content).Text;
			} else {
				this.textBox.Visible = false;
				Literal l = new Literal();
				this.Controls.Add(l);
				l.Text = string.Format("[Html Content Block '{0}' is not registered.]", this.ID);
			}
			
		}

		protected override void LoadPresentationPlaceHolderContent() {
			EnsureChildControls();
			if(Content.IsAvailable){
				this.literal.Text = ((LiteralContent)Content).Text;
			}
	
		}

		protected override void SavePlaceHolderContent() {
			EnsureChildControls();
			((LiteralContent)Content).Text = this.textBox.Text;
		}

	}
}
