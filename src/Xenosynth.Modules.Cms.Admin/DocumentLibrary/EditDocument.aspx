<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditDocument.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.DocumentLibrary.EditDocument" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="DocumentTabControl" Src="DocumentTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="DocumentLibraryTasks" Src="DocumentLibraryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="DocumentTasks" Src="DocumentTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
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
	<xs:DocumentTabControl ID="DocumentTabControl1" runat="server" FileID="<%# CurrentDocument.ID %>" Selected="Properties" />
	<div class="formPanel">
	    <fieldset>
		    <legend>Document Properties</legend>

		    <label for="TextBoxTitle">Title:</label>  <asp:TextBox CssClass="input" Runat="server" style="width: 250px;" ID="TextBoxTitle"  MaxLength="100"/> <br />
		    <span class="toolTip">This is the name that will be displayed.</span>

            <label for="TextBoxFileName">File Name:</label>  <asp:TextBox CssClass="input" Runat="server" style="width: 250px;" ID="TextBoxFileName"  MaxLength="100"/> <br />
		    <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" ErrorMessage="Required." />
	        <span class="toolTip">The name as it will appear in the URL ( http://www.yoursite.com/filename.aspx ).</span>
    	    		
		    <label for="TextBoxDescription">Description:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxDescription" />
		    <br />
		    <span class="toolTip">A description of the document for searching.<asp:TextBox Runat="server" ID="TextBoxDescriptionCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		    <mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxDescription" MaxLength="250" OutputControl="TextBoxDescriptionCount" ErrorMessage="Description can be no longer than 250 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator1"/>
		    <br />
    		
		    <label for="TextBoxKeywords"> Keywords:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxKeywords" />
		    <br />
		    <span class="toolTip">Keywords for searching. Separate with commas.<asp:TextBox Runat="server" ID="TextBoxKeywordsCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		    <mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxKeywords" MaxLength="250" OutputControl="TextBoxKeywordsCount" ErrorMessage="Description can be no longer than 250 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator2"/>
		    <br />
		    
		    
		    <label for="TextBoxDateCreated">Created:</label>  <asp:TextBox ReadOnly="True" Enabled="True" CssClass="input" Runat="server" ID="TextBoxDateCreated" /> <br />
		    <span class="toolTip">The date created.</span>
		    
		    <label for="TextBoxDateModified">Modified:</label>  <asp:TextBox ReadOnly="True" Enabled="True" CssClass="input" Runat="server" ID="TextBoxDateModified" /> <br />
		    <span class="toolTip">The date last modified.</span>
		    
		    <label for="TextBoxVersion">Version:</label>  <asp:TextBox ReadOnly="True" Enabled="True" CssClass="input" Runat="server" ID="TextBoxVersion" /> <br />
		    <span class="toolTip">The current version.</span>
    		
		    <label for="TextBoxStatus">Status:</label>  <asp:TextBox ReadOnly="True" Enabled="True" CssClass="input" Runat="server" ID="TextBoxStatus" /> <br />
		    <span class="toolTip">The current status.</span>
    		
		    <label for="TextBoxFullPath">Full Path:</label>  <asp:TextBox ReadOnly="True" Enabled="True" CssClass="input" Runat="server" Width="300" ID="TextBoxFullPath" /> <br />
    		
		    <label for="TextBoxVersionID">Version ID:</label>  <asp:TextBox ReadOnly="True" Enabled="True" CssClass="input" Runat="server" Width="300" ID="TextBoxVersionID" /> <br />
		    
		    <label for="TextBoxFileID">File ID:</label>  <asp:TextBox ReadOnly="True" Enabled="True" CssClass="input" Runat="server" Width="300" ID="TextBoxFileID" /> <br />
    		
    	
		    <asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdate_OnClick" ID="Button1"/>
		    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	    </fieldset>
	    
	</div>
	
	
	<databinding:DataBindingManager ID="DataBindingManagerDocument" runat="server">
		<DataBinding:TextBoxBinding ControlToBind="TextBoxDescription" DataMember="Description" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxKeywords" DataMember="Keywords" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxFileName" DataMember="FileName" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title"  />
		<DataBinding:TextBoxBinding ReadOnly="true" ControlToBind="TextBoxDateCreated" DataMember="DateCreated" DisplayFormat="{0:d}" />
		<DataBinding:TextBoxBinding ReadOnly="true" ControlToBind="TextBoxDateModified" DataMember="DateModified" DisplayFormat="{0:d}" />
		<DataBinding:TextBoxBinding ReadOnly="True" ControlToBind="TextBoxVersion" DataMember="Version" />
		<DataBinding:TextBoxBinding ReadOnly="True" ControlToBind="TextBoxStatus" DataMember="State" />
		<DataBinding:TextBoxBinding ReadOnly="True" ControlToBind="TextBoxFullPath" DataMember="FullPath" />
		<DataBinding:TextBoxBinding ReadOnly="True" ControlToBind="TextBoxVersionID" DataMember="ID" />
		<DataBinding:TextBoxBinding ReadOnly="True" ControlToBind="TextBoxFileID" DataMember="FileID" />
	</databinding:DataBindingManager>
	
	</MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
