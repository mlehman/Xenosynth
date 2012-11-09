<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditBlog.aspx.cs" Inherits="Xenosynth.Modules.Blog.Admin.EditBlog" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="BlogTabControl" Src="BlogTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogTasks" Src="BlogTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Modules/Cms/PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="~/Modules/Cms/SearchFiles.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="~/Modules/Cms/FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="~/Modules/Cms/SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:BlogTasks ID="BlogTasks1" runat="server" CurrentBlog="<%# CurrentBlog %>" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
     <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentBlog %>"   ID="PathBrowser1" />
	
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentBlog %>" />
	    </SidePanelTemplate>
	    <MainPanelTemplate>

            <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	        <xs:BlogTabControl ID="BlogTabControl1" CurrentFile="<%# CurrentBlog %>" runat="server" Selected="Properties" />
	        <div class="formPanel">
	            <fieldset>
		            <legend>Blog Properties</legend>
		            
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
		            <span class="toolTip">Whether the blog is hidden in the navigation.</span>
		            
		            <label for="DropDownListBlogTemplates"> Blog Template:</label>  <asp:DropDownList CssClass="input" Runat="server" ID="DropDownListBlogTemplates" /> 
		            <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="DropDownListBlogTemplates" Display="Dynamic" ErrorMessage="Required." />
		            <br />
		            <span class="toolTip">The template that defines the look and feel of the blog.</span>
    		        
		            <label for="DropDownListBlogPostTemplates"> Blog Post Template:</label>  <asp:DropDownList CssClass="input" Runat="server" ID="DropDownListBlogPostTemplates" /> 
		            <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="DropDownListBlogPostTemplates" Display="Dynamic" ErrorMessage="Required." />
		            <br />
		            <span class="toolTip">The template that defines the look and feel of the blog post.</span>
            		
		            <asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdate_OnClick" ID="Button1"/>
		            <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	            </fieldset>
	        </div>
        	
        	
	        <databinding:DataBindingManager ID="DataBindingManagerBlog" runat="server">
		        <databinding:TextBoxBinding ControlToBind="TextBoxFileName"  DataMember="FileName" />
		        <databinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
		        <databinding:TextBoxBinding ControlToBind="TextBoxDescription" DataMember="Description" />
		        <databinding:CheckBoxBinding ControlToBind="CheckBoxIsHidden" DataMember="IsHidden" />
		        <DataBinding:ListControlBinding ControlToBind="DropDownListBlogTemplates" DataMember="BlogTemplateID" />
		        <DataBinding:ListControlBinding ControlToBind="DropDownListBlogPostTemplates" DataMember="BlogPostTemplateID" />
	        </databinding:DataBindingManager>
	
	    </MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>

