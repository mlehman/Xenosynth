<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditGallery.aspx.cs" Inherits="Xenosynth.Admin.Content.ImageGallery.EditGallery"  %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="ImageGalleryTabControl" Src="ImageGalleryTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="ImageGalleryTasks" Src="ImageGalleryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="../SearchFiles.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser runat="server" CurrentFile="<%# CurrentDirectory %>" ID="PathBrowser1" />
    
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentDirectory %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:ImageGalleryTabControl ID="ImageGalleryTabControl1" runat="server" FileID="<%# CurrentDirectory.ID %>"  Selected="Properties" />
    <div class="formPanel">
        <fieldset>
            <legend>Directory Properties</legend>
            
            <label for="TextBoxTitle">
                Title:</label>
            <asp:TextBox CssClass="input" runat="server" ID="TextBoxTitle" />
            <br />
            
            <label for="TextBoxFileName">
                File Name:</label>
            <asp:TextBox CssClass="input" runat="server" ID="TextBoxFileName" />
            <br />
            <span class="toolTip">This is the name as it will appear in the URL ( http://www.yoursite.com/filename
                ).</span>
            
            <label for="TextBoxDescription">Description:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxDescription" />
		    <br />
		    <span class="toolTip">A description of the page for search engines.<asp:TextBox Runat="server" ID="TextBoxDescriptionCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		    <mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxDescription" MaxLength="250" OutputControl="TextBoxDescriptionCount" ErrorMessage="Description can be no longer than 250 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator1"/>
		    <br />
            
            <asp:CheckBox CssClass="checkbox" runat="server" ID="CheckBoxIsHidden" Text="Is hidden?" />
            <span class="toolTip">Whether the directory is hidden in the navigation.</span>
            
            <span class="toolTip">This is the name that will be displayed.</span>
            <label for="TextBoxFullPath">
                Full Path:</label>
            <asp:TextBox ReadOnly="True" CssClass="input" runat="server" Width="300" ID="TextBoxFullPath" />
            <br />

            <asp:Button runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdateDirectory_OnClick"
                ID="Button1" />
            <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False" />
        </fieldset>
    </div>
    <databinding:DataBindingManager ID="DataBindingManagerImageGallery" runat="server">
        <databinding:TextBoxBinding ControlToBind="TextBoxFileName" DataMember="FileName" />
        <databinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
        <databinding:TextBoxBinding ControlToBind="TextBoxDescription" DataMember="Description" />
        <databinding:TextBoxBinding ControlToBind="TextBoxFullPath" DataMember="FullPath" />
        <databinding:CheckBoxBinding ControlToBind="CheckBoxIsHidden" DataMember="IsHidden" />
    </databinding:DataBindingManager>
    
	</MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:ImageGalleryTasks ID="ImageGalleryTasks1" runat="server" CurrentGallery="<%# CurrentDirectory %>" />
    <xs:SearchFiles ID="SearchFiles1" runat="server" />
</asp:Content>
