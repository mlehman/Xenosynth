<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="UploadDocument.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.DocumentLibrary.UploadDocument" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="DocumentTabControl" Src="DocumentTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="DocumentLibraryTasks" Src="DocumentLibraryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="DocumentTasks" Src="DocumentTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
     <xs:DocumentTasks ID="DocumentTasks1" runat="server" CurrentFile="<%# CurrentDocument %>" />
    <xs:DocumentLibraryTasks ID="DocumentLibraryTasks1" runat="server" CurrentLibrary="<%# CurrentDocument.ParentDirectory %>" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentDocument %>"   ID="PathBrowser1" />
    
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentDocument %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<xs:DocumentTabControl ID="DocumentTabControl1" runat="server" FileID="<%# CurrentDocument.ID %>" Selected="Upload" />
	<div class="formPanel">
	    <fieldset>
		    <legend>Upload Properties</legend>
		    
		    <label for="TextBoxID">Select File:</label><input runat="server" type="file" id="HtmlInputFileAttach" class="file"  /><br />
		  
		    <asp:Button Runat="server" CssClass="submit" Text="Upload &raquo;" OnClick="ButtonAdd_OnClick" ID="Button1"/>
		    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	    </fieldset>
	</div>
	
	
	<databinding:DataBindingManager ID="DataBindingManagerDocument" runat="server">
		<databinding:TextBoxBinding ControlToBind="TextBoxFileName"  DataMember="FileName" />
		<databinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
	</databinding:DataBindingManager>
	
	</MainPanelTemplate>
    </xs:SlidePanel>
    

</asp:Content>

