<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditDocumentAttributes.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.DocumentLibrary.EditDocumentAttributes" Title="Untitled Page" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="DocumentTasks" Src="DocumentTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="DocumentTabControl" Src="DocumentTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="DocumentLibraryTasks" Src="DocumentLibraryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="EditFileAttributes" Src="../Controls/EditFileAttributes.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>

<%@ Import namespace="Xenosynth.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Side" runat="server">
		<xs:DocumentTasks runat="server" CurrentFile="<%# CurrentDocument %>" />
		<xs:DocumentLibraryTasks runat="server" CurrentLibrary="<%# CurrentDocument.ParentDirectory %>" />	
</asp:Content>
			
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentDocument %>"   ID="PathBrowser1" />
    
    <xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentDocument %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
    	
            <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
            <xs:DocumentTabControl ID="DocumentTabControl1" runat="server" FileID="<%# CurrentDocument.ID %>"  Selected="Attributes" />
            <div class="formPanel">
            <fieldset>
                <xs:EditFileAttributes ID="EditFileAttributes1" runat="server" MessageBoxControl="MessageBox1" />
            </fieldset>
            </div>
        			
		</MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
