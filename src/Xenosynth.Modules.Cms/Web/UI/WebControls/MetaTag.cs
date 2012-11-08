using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Xenosynth.Web.UI.WebControls {
	/// <summary>
	/// A control for rendering the CmsPage's meta tags.
	/// </summary>
	[ToolboxData("<{0}:MetaTag runat=server></{0}:MetaTag>")]
	public class MetaTag :  Control {
		public MetaTag() {
		}

		protected override void Render(HtmlTextWriter writer) {
			CmsPage p = CmsPage.Current;
			if(p!=null){
				if(p.Description != null && p.Description.Length > 0){
					RenderMetaTag(writer, "description", p.Description);
				}
				if(p.Keywords != null && p.Keywords.Length > 0){
					RenderMetaTag(writer, "keywords", p.Keywords);
				}
			}
		}

		private void RenderMetaTag(HtmlTextWriter writer, string name, string content){
			writer.WriteBeginTag("meta");
			writer.WriteAttribute("name",name,true);
			writer.WriteAttribute("content",content,true);
			writer.WriteLine(HtmlTextWriter.SelfClosingTagEnd);
		}

	}
}
