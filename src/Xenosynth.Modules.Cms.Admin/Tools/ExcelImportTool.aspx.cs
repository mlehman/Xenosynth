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
using System.IO;
using System.Data.OleDb;
using Xenosynth.Data;
using Xenosynth.Admin.Controls;

namespace Xenosynth.Modules.Cms.Admin.Tools {
    public partial class ExcelImportTool : System.Web.UI.Page {

        protected MessageBox MessageBox1;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DropDownListTemplates.DataSource = CmsTemplate.FindAll();
				DropDownListTemplates.DataTextField = "Title";
				DropDownListTemplates.DataValueField = "ID";
				DropDownListTemplates.DataBind();

                //TODO: Better method to get, may need to restrict?
                CmsFileCollection files = CmsWebDirectory.FindAll();
                DropDownListDirectories.DataSource = files;
                DropDownListDirectories.DataTextField = "FullPath";
                DropDownListDirectories.DataValueField = "ID";
                DropDownListDirectories.DataBind();
            }
        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {


                Guid parentID = new Guid(DropDownListDirectories.SelectedValue);
                Guid templateID = new Guid(DropDownListTemplates.SelectedValue);

                string filePath = Path.GetTempFileName();
                InputFileUpload.PostedFile.SaveAs(filePath);
                try {
                    DataTable data = getDataFromXLS(filePath);
                    int count = 0;
                    foreach (DataRow dr in data.Rows) {
                        string filename = Clean(dr["Filename"]);
                        if (filename != null && filename.Length > 0) {

                            CmsPage page = new CmsPage();
                            page.FileName = filename;
                            page.Title = Clean(dr["Title"]);
                            page.ParentID = parentID;
                            page.TemplateID = templateID;
                            page.Insert();
                            count++;

                            
                            AddAttribute(page, "website", Clean(dr["Website"]));
                            AddAttribute(page, "phone", Clean(dr["Phone"]));
                            AddAttribute(page, "street", Clean(dr["Street"]));
                            AddAttribute(page, "addressHint", Clean(dr["AddressHint"]));
                            AddAttribute(page, "city", Clean(dr["City"]));
                            AddAttribute(page, "subcategory", Clean(dr["Subcategory"]));

                             //string description = Clean(dr["Description"]);
                             //if (description != null && description.Length > 0) {
                             //    LiteralContent content = (LiteralContent)page.ContentBlocks["Body"];
                             //    content.Text = description;
                             //    page
                             //}

                            //Website	Phone	Street	AddressHint	Subcategory	City	Featured	City	State	Description
                        
                        }
                    }

                    MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, count + " pages have been created.");

                } finally {
                    try {
                        File.Delete(filePath);
                    } catch (Exception ex) {
                    }
                }
            }
        }

        public void AddAttribute(CmsPage page, string name, string value) {
            if (value != null && value.Length > 0) {
                page.Attributes.Add(name, value);
            }
        }


        public string Clean(object o) {
            if (o == null) {
                return null;
            }

            return o.ToString().Trim();
          
        }


        protected void ButtonCancel_OnClick(object sender, EventArgs e) {

        }

        private DataTable getDataFromXLS(string filePath) {
            
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + @";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
            OleDbConnection xlsConnection = new OleDbConnection(connectionString);
            xlsConnection.Open();

            OleDbCommand sql = new OleDbCommand(@"SELECT * FROM [Sheet1$]", xlsConnection);
            OleDbDataAdapter xlsDataAdapter = new OleDbDataAdapter();
            xlsDataAdapter.SelectCommand = sql;

            DataTable xlsDataTable = new DataTable();
            xlsDataAdapter.Fill(xlsDataTable);
            xlsConnection.Close();
            xlsDataAdapter = null;
            return xlsDataTable;
        
        }
    }
}
