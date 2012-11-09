<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileExplorer.ascx.cs" Inherits="Xenosynth.Admin.Content.FileExplorer" %>


<div class="actionPanel" >
    <div class="title">Directories</div>
    <div class="body" style="overflow-x: auto;">
<asp:TreeView runat="server" 
    ID="TreeViewFiles"
    SelectedNodeStyle-Font-Underline="false"
    SelectedNodeStyle-Font-Bold="false"
    SelectedNodeStyle-BackColor="#d1d1d1"
    NodeStyle-HorizontalPadding="2px"
    EnableClientScript="true"
    PopulateNodesFromClient="true"  
    OnTreeNodePopulate="TreeViewFiles_OnTreeNodePopulate"
    >
</asp:TreeView>
    </div>
</div>
             

