<%@ Page language="c#" MasterPageFile="~/Default.Master" Codebehind="EditDirectory.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Content.EditDirectory" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTabControl" Src="DirectoryTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTasks" Src="DirectoryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="../SearchFiles.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>

<asp:Content ContentPlaceHolderID="Side" runat="server">
	<xs:DirectoryTasks runat="server" CurrentDirectory="<%# CurrentDirectory %>" />
	<xs:SearchFiles runat="server" />
</asp:Content>
			
			
<asp:Content ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser runat="server" CurrentFile="<%# CurrentDirectory %>" ID="PathBrowser1" />
    
    <xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentDirectory %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:DirectoryTabControl ID="DirectoryTabControl1" runat="server" DirectoryID="<%# CurrentDirectory.ID %>"  Selected="Properties" />
    <div class="formPanel">
        <fieldset>
            <legend>Directory Properties</legend>
            
            <label for="TextBoxTitle">
                Title:</label>
            <asp:TextBox CssClass="input" runat="server" ID="TextBoxTitle" />
            <br />
            <span class="toolTip">This is the name that will be displayed.</span>
            
            <label for="TextBoxFileName">
                File Name:</label>
            <asp:TextBox CssClass="input" runat="server" ID="TextBoxFileName" />
            <br />
            <span class="toolTip">This is the name as it will appear in the URL ( http://www.yoursite.com/filename
                ).</span>
                
            <label for="TextBoxFullPath">
                Full Path:</label>
            <asp:TextBox ReadOnly="True" CssClass="input" runat="server" Width="300" ID="TextBoxFullPath" />
            <br />
            <label for="DropDownListTemplateGalleries">
                Template Gallery:</label>
            <asp:DropDownList CssClass="input" runat="server" ID="DropDownListTemplateGalleries" />
            <br />
            <span class="toolTip">The gallery of templates for the look and feel of the pages.</span>
            <label for="DropDownListMediaGalleries">
                Media Gallery:</label>
            <asp:DropDownList CssClass="input" runat="server" ID="DropDownListMediaGalleries" />
            <br />
            <span class="toolTip">The media gallery for the pages.</span>
            <asp:CheckBox CssClass="checkbox" runat="server" ID="CheckBoxIsHidden" Text="Is hidden?" />
            <span class="toolTip">Whether the directory is hidden in the navigation.</span>
            <asp:Button runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdateDirectory_OnClick"
                ID="Button1" />
            <asp:Button runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False" />
        </fieldset>
    </div>
    <databinding:DataBindingManager ID="DataBindingManagerDirectory" runat="server">
        <databinding:TextBoxBinding ControlToBind="TextBoxFileName" DataMember="FileName" />
        <databinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
        <databinding:TextBoxBinding ControlToBind="TextBoxFullPath" DataMember="FullPath" />
        <databinding:ListControlBinding ControlToBind="DropDownListTemplateGalleries" DataMember="TemplateGalleryID" />
        <databinding:ListControlBinding ControlToBind="DropDownListMediaGalleries" DataMember="ImageGalleryID" />
        <databinding:CheckBoxBinding ControlToBind="CheckBoxIsHidden" DataMember="IsHidden" />
    </databinding:DataBindingManager>
	</MainPanelTemplate>
    </xs:SlidePanel>
</asp:Content >