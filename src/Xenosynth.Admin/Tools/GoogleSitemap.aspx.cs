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
using System.Xml;

using Xenosynth.Web;
using Xenosynth.Web.UI;

namespace Xenosynth.Admin.Tools {
	/// <summary>
	/// Summary description for GoogleSitemap.
	/// </summary>
	public partial class GoogleSitemap : System.Web.UI.Page {


		private CmsHostHeaderMapping host;
		private CmsWebDirectory rootDirectory;


		protected void Page_Load(object sender, System.EventArgs e) {
			if(!IsPostBack){
				DropDownListHostHeader.DataSource = CmsHostHeaderMapping.FindAll();
				DropDownListHostHeader.DataTextField = "HostHeaderName";
				DropDownListHostHeader.DataValueField = "ID";
				DropDownListHostHeader.DataBind();

				ListItem li = DropDownListHostHeader.Items.FindByValue(CmsHostHeaderMapping.Current.ID.ToString());
				li.Selected = true;
			}
		}


		public void ButtonGenerateSitemap_OnClick(object sender, System.EventArgs e){

			host = CmsHostHeaderMapping.FindByID(new Guid(DropDownListHostHeader.SelectedValue));
            CmsSite site = CmsSite.FindByID(host.CmsSiteID);
			rootDirectory = CmsWebDirectory.FindByID( site.RootWebDirectoryID );

			XmlDocument doc = new XmlDocument();
			XmlElement urlsetElement = doc.CreateElement("urlset", "http://www.google.com/schemas/sitemap/0.84");
			doc.AppendChild(urlsetElement);

			CrawlSite(rootDirectory, urlsetElement);

			Response.Clear();
			Response.ContentType = "text/xml";
			Response.AddHeader("Content-Disposition","attachment; filename=Sitemap1.xml");
			Response.ContentEncoding = System.Text.Encoding.UTF8;
			doc.Save(Response.Output);
			Response.End();
		}


		private void CrawlSite(CmsWebDirectory directory, XmlElement urlsetElement){
			CmsPageCollection pages = CmsPage.FindByDirectoryID(directory.ID, false);
			foreach(CmsPage p in pages){
				CreateUrlElement(p, urlsetElement);
			}

			CmsDirectoryCollection directories = directory.Subdirectories;
			foreach(CmsWebDirectory d in directories){
				CrawlSite(d, urlsetElement);
			}

		}

		private void CreateUrlElement(CmsPage p, XmlElement urlsetElement){
			XmlElement urlElement = urlsetElement.OwnerDocument.CreateElement("url");
			urlsetElement.AppendChild(urlElement);
			
			XmlElement locElement = urlsetElement.OwnerDocument.CreateElement("loc");
			urlElement.AppendChild(locElement);
			locElement.InnerText = "http://" + host.HostHeaderName + p.FullPath.Substring(rootDirectory.FullPath.Length);

			XmlElement lastModElement = urlsetElement.OwnerDocument.CreateElement("lastmod");
			urlElement.AppendChild(lastModElement);
			lastModElement.InnerText = p.DateModified.ToString("yyyy-MM-dd");
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
