<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AddLibrary.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.DocumentLibrary.AddLibrary" %>
<%@ Register TagPrefix="xs" TagName="DocumentLibraryTabControl" Src="DocumentLibraryTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="../SearchFiles.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
     <xs:PathBrowser  runat="server" CurrentFile="<%# ParentDirectory %>"   ID="PathBrowser1" />
	
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# ParentDirectory %>" />
	    </SidePanelTemplate>
	    <MainPanelTemplate>

	        <xs:DocumentLibraryTabControl ID="DocumentLibraryTabControl1" runat="server" Selected="Properties" />
	        <div class="formPanel">
	            <fieldset>
		            <legend>Document Library Properties</legend>
            		
		            <label for="TextBoxDisplayName" class="required">Title:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxTitle" /> <br />
		            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Runat="server" ControlToValidate="TextBoxTitle" Display="Dynamic" ErrorMessage="Required." />				
		            <span class="toolTip">This is the name that will be displayed.</span>
		            
		            <label for="TextBoxFileName" class="required">File Name:</label> <asp:TextBox CssClass="input" Runat="server" ID="TextBoxFileName" /><br />
		            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" ErrorMessage="Required." />
		            <span class="toolTip">This is the name as it will appear in the URL ( http://www.yoursite.com/filename ).</span>
        		    
		            <label for="TextBoxDescription">Description:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxDescription" />
		            <br />
		            <span class="toolTip">A description of the page for search engines.<asp:TextBox Runat="server" ID="TextBoxDescriptionCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		            <mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxDescription" MaxLength="250" OutputControl="TextBoxDescriptionCount" ErrorMessage="Description can be no longer than 250 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator1"/>
		            <br />
            		
		            <asp:CheckBox CssClass="checkbox" Runat="server" ID="CheckBoxIsHidden" Text="Is hidden?" />
		            <span class="toolTip">Whether the gallery is hidden in the navigation.</span>
            		
		            <asp:Button Runat="server" CssClass="submit" Text="Create &raquo;" OnClick="ButtonAdd_OnClick" ID="Button1"/>
		            <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	            </fieldset>
	        </div>
        	
        	
	        <databinding:DataBindingManager ID="DataBindingManagerDocumentLibrary" runat="server">
		        <databinding:TextBoxBinding ControlToBind="TextBoxFileName"  DataMember="FileName" />
		        <databinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
		        <databinding:TextBoxBinding ControlToBind="TextBoxDescription" DataMember="Description" />
		        <databinding:CheckBoxBinding ControlToBind="CheckBoxIsHidden" DataMember="IsHidden" />
	        </databinding:DataBindingManager>
	
	    </MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
