<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditTemplate.aspx.cs" Inherits="Xenosynth.Admin.Content.TemplateGallery.EditTemplate" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="TemplateTabControl" Src="TemplateTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="TemplateGalleryTasks" Src="TemplateGalleryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="TemplateTasks" Src="TemplateTasks.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentTemplate %>"   ID="PathBrowser1" />
    
    <xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentTemplate %>" />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
     <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<xs:TemplateTabControl ID="TemplateTabControl1" runat="server" Selected="Properties" FileID="<%# CurrentTemplate.ID %>" />
	<div class="formPanel">
	    <fieldset>
		    <legend>Template Properties</legend>
    		
		    <label for="TextBoxTitle" class="required">Title:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxTitle" /> 
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Runat="server" ControlToValidate="TextBoxTitle" Display="Dynamic" ErrorMessage="Required." /><br />				
		    <span class="toolTip">This is the name that will be displayed.</span> 
		    
		    <label for="TextBoxFileName" class="required">File Name:</label> <asp:TextBox CssClass="input" Runat="server" ID="TextBoxFileName" />
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" ErrorMessage="Required." /> <br />
		    <span class="toolTip">This is the name as it will appear in the URL ( http://www.yoursite.com/filename ).</span>
		    		    
		    <asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdate_OnClick" ID="Button1"/>
		    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	    </fieldset>
	</div>
	
	<databinding:DataBindingManager ID="DataBindingManagerTemplate" runat="server">
		<databinding:TextBoxBinding ControlToBind="TextBoxFileName"  DataMember="FileName" />
		<databinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
	</databinding:DataBindingManager>
	
	            </MainPanelTemplate>
        </xs:SlidePanel>
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:TemplateTasks ID="TemplateTasks1" runat="server" CurrentTemplate="<%# CurrentTemplate %>" />
    <xs:TemplateGalleryTasks ID="TemplateGalleryTasks1" runat="server" CurrentGallery="<%# CurrentTemplate.ParentDirectory %>" />
</asp:Content>
