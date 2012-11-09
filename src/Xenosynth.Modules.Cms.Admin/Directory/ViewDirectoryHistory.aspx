<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ViewDirectoryHistory.aspx.cs" Inherits="Xenosynth.Admin.Content.Directory.ViewDirectoryHistory" Title="Untitled Page" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTabControl" Src="DirectoryTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTasks" Src="DirectoryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="../SearchFiles.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="xs" TagName="ViewFileHistory" Src="../Controls/ViewFileHistory.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
     <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentFile %>"   ID="PathBrowser1" />
     
      <xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentFile %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
	        <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	        <xs:DirectoryTabControl ID="DirectoryTabControl1" runat="server" DirectoryID="<%# CurrentFile.ID %>"  Selected="History" />
            <div class="formPanel">
                <xs:ViewFileHistory ID="ViewFileHistory1" runat="server" MessageBoxControl="MessageBox1" />
	        </div>
	    </MainPanelTemplate>
    </xs:SlidePanel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
	<xs:DirectoryTasks ID="DirectoryTasks1" runat="server" CurrentDirectory="<%# CurrentFile %>" />
	<xs:SearchFiles ID="SearchFiles1" runat="server" />
</asp:Content>
