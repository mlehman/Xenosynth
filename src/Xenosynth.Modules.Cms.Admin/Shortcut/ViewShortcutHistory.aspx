<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ViewShortcutHistory.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.Shortcut.ViewShortcutHistory" Title="Untitled Page" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="ShortcutTasks" Src="ShortcutTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="ShortcutTabControl" Src="ShortcutTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="ViewFileHistory" Src="../Controls/ViewFileHistory.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTasks" Src="../Directory/DirectoryTasks.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Side" runat="server">
		<xs:ShortcutTasks runat="server" CurrentFile="<%# CurrentShortcut %>" />
		<xs:DirectoryTasks runat="server" CurrentDirectory="<%# CurrentShortcut.ParentDirectory %>" />	
</asp:Content>
			
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentShortcut %>"   ID="PathBrowser1" />
    
    <xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentShortcut %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
    	
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:ShortcutTabControl ID="ShortcutTabControl1" runat="server" FileID="<%# CurrentShortcut.ID %>"  Selected="History" />
    <div class="formPanel">
    <fieldset>
	    <xs:ViewFileHistory ID="ViewFileHistory1" runat="server" MessageBoxControl="MessageBox1" />
    </fieldset>
    </div>

			
			</MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>