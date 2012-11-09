<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ViewDocumentHistory.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.DocumentLibrary.ViewDocumentHistory" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="DocumentLibraryTasks" Src="DocumentLibraryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="DocumentTasks" Src="DocumentTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="DocumentTabControl" Src="DocumentTabControl.ascx" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="ViewFileHistory" Src="../Controls/ViewFileHistory.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentFile %>"   ID="PathBrowser1" />
    
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentFile %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
	        <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	        <xs:DocumentTabControl ID="DocumentTabControl1" runat="server" FileID="<%# CurrentFile.ID %>" Selected="History" />
            <div class="formPanel">
                <xs:ViewFileHistory ID="ViewFileHistory1" runat="server" MessageBoxControl="MessageBox1" />
	        </div>
    	   
	    </MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
     <xs:DocumentTasks ID="DocumentTasks1" runat="server" CurrentFile="<%# CurrentFile %>" />
    <xs:DocumentLibraryTasks ID="DocumentLibraryTasks1" runat="server" CurrentLibrary="<%# CurrentFile.ParentDirectory %>" />
</asp:Content>
