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


namespace Xenosynth.Admin.Content {
    public partial class FileExplorer : System.Web.UI.UserControl {

        public CmsFile CurrentFile;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                BuildTree();
            }
        }

        public void BuildTree() {
            this.DataBind();
            TreeViewFiles.Nodes.Clear();
            CmsWebDirectory rootDirectory = (CmsWebDirectory)CmsFile.FindByID(CmsContext.Current.RootDirectory.ID); //TODO: Caching Bug!!!
            AddDirectory(TreeViewFiles.Nodes, rootDirectory);
        }

        public void TreeViewFiles_OnTreeNodePopulate(Object sender, TreeNodeEventArgs e) {

           CmsDirectory dir = (CmsDirectory)CmsFile.FindByFileID(new Guid(e.Node.Value));
           foreach (CmsDirectory sub in dir.Subdirectories) {
               if (sub.State != CmsState.Deleted) {
                   AddDirectory(e.Node.ChildNodes, sub);
               }
           }

        }

        public void AddDirectory(TreeNodeCollection nodes, CmsDirectory dir) {
            TreeNode node = new TreeNode();
            node.NavigateUrl = dir.FileType.BrowseUrl + "?FileID=" + dir.ID;
            node.Text = dir.Title;
            node.Value = dir.FileID.ToString();

             if (CurrentFile != null && CurrentFile.IsDescendantOf(dir)) {
                node.ImageUrl = dir.FileType.Module.ResourceFolder + "/images/icon_" + dir.FileType.CssClass + "_open.png";

                if (CurrentFile.ID == dir.ID || CurrentFile.ParentID == dir.ID) {
                    node.Selected = true;
                }
            } else {
                node.Expanded = false;
                node.ImageUrl = dir.FileType.Module.ResourceFolder + "/images/icon_" + dir.FileType.CssClass + ".png";
            }

            nodes.Add(node);

            if (dir.Subdirectories.Count > 0) {

                if (CurrentFile != null && CurrentFile.IsDescendantOf(dir) && CurrentFile.FileID != dir.FileID) {
                    node.Expanded = true;
                    foreach (CmsDirectory sub in dir.Subdirectories) {
                        if (sub.State != CmsState.Deleted) {
                            AddDirectory(node.ChildNodes, sub);
                        }
                    }
                } else {
                    node.PopulateOnDemand = true;
                }
            }
        }
    }
}