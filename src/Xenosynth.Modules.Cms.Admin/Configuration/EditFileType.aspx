<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditFileType.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.Configuration.EditFileType" Title="Untitled Page" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileTypeTabControl" Src="FileTypeTabControl.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="fs" TagName="PathBrowser" Src="../../../Controls/PathBrowser.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <fs:PathBrowser runat="server" 
	    RootPage="Default.aspx" 
	    RootPageName="File Types" 
	    SubPage='<%# "EditFileType.aspx?FileTypeID=" + CurrentFileType.ID %>' 
	    SubPageName='<%# CurrentFileType.Name %>'
	    />
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:FileTypeTabControl ID="FileTypeTabControl1" runat="server"  Selected="Properties" />
    <div class="formPanel">
        <fieldset>
            <legend>File Type Properties</legend>

            <label for="TextBoxID">ID:</label>
            <asp:TextBox CssClass="input" runat="server" ID="TextBoxID" style="width: 500px;"/>
            <br />
            
            <label for="TextBoxName">Name:</label>
            <asp:TextBox CssClass="input" runat="server" ID="TextBoxName" style="width: 500px;"/>
            <br />
            
             <label for="TextBoxDescription">Description:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxDescription" />
		    <br />
		    <span class="toolTip">A description of the page for search engines.<asp:TextBox Runat="server" ID="TextBoxDescriptionCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		    <mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxDescription" MaxLength="250" OutputControl="TextBoxDescriptionCount" ErrorMessage="Description can be no longer than 250 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator1"/>
		    <br />
		    
		    <asp:CheckBox CssClass="checkbox" Runat="server" ID="CheckBoxIsDirectory" Text="Is directory?" />
		    <span class="toolTip">Whether the file is a directory and can contain other files.</span>
		    
		    <asp:CheckBox CssClass="checkbox" Runat="server" ID="CheckBoxIsVersioned" Text="Is versioned?" />
		    <span class="toolTip">Whether the file is versioned.</span>
		    
		    <label for="TextBoxCreateUrl">
                Create URL:</label>
            <asp:TextBox  CssClass="input" runat="server" Width="250" ID="TextBoxCreateUrl" style="width: 500px;"/>
            <br />
            
            <label for="TextBoxEditUrl">
                Edit URL:</label>
            <asp:TextBox  CssClass="input" runat="server" Width="250" ID="TextBoxEditUrl" style="width: 500px;"/>
            <br />
            
            <label for="TextBoxBrowseUrl">
                Browse URL:</label>
            <asp:TextBox CssClass="input" runat="server" Width="250" ID="TextBoxBrowseUrl" style="width: 500px;"/>
            <br />
            
            <label for="DropDownDefaultAction">
                Default Action:</label>
            <asp:DropDownList CssClass="input" runat="server" ID="DropDownDefaultAction" >
                <asp:ListItem Value="1" Text="Create"/>
                <asp:ListItem Value="2" Text="Edit"/>
                <asp:ListItem Value="3" Text="Browse"/>
                <asp:ListItem Value="4" Text="View"/>
            </asp:DropDownList>
            <br />
            <asp:Button runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdate_OnClick"
                ID="Button1" />
            <asp:Button runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False" />
        </fieldset>
    </div>
    
    <databinding:DataBindingManager ID="DataBindingManagerFileType" runat="server">
        <databinding:TextBoxBinding ControlToBind="TextBoxID" DataMember="ID" />
        <databinding:TextBoxBinding ControlToBind="TextBoxName" DataMember="Name" />
        <databinding:TextBoxBinding ControlToBind="TextBoxDescription" DataMember="Description" />
        <databinding:CheckBoxBinding ControlToBind="CheckBoxIsDirectory" DataMember="IsDirectory" />
        <databinding:CheckBoxBinding ControlToBind="CheckBoxIsVersioned" DataMember="IsVersioned" />
        <databinding:TextBoxBinding ControlToBind="TextBoxCreateUrl" DataMember="CreateUrl" />
        <databinding:TextBoxBinding ControlToBind="TextBoxEditUrl" DataMember="EditUrl" />
        <databinding:TextBoxBinding ControlToBind="TextBoxBrowseUrl" DataMember="BrowseUrl" />
        <databinding:ListControlBinding ControlToBind="DropDownDefaultAction" DataMember="DefaultAction" />
        
    </databinding:DataBindingManager>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
</asp:Content>
