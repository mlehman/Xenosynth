<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditBlogPost.aspx.cs" Inherits="Xenosynth.Modules.Blog.Admin.EditBlogPost" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTabControl" Src="BlogPostTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogTasks" Src="BlogTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTasks" Src="BlogPostTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Modules/Cms/PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="~/Modules/Cms/SearchFiles.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="~/Modules/Cms/FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="~/Modules/Cms/SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="radE" Namespace="Telerik.WebControls" Assembly="RadEditor.Net2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:BlogPostTasks ID="BlogPostTasks1" runat="server" CurrentFile="<%# CurrentBlogPost %>" />
    <xs:BlogTasks ID="BlogTasks1" runat="server" CurrentBlog="<%# CurrentBlogPost.ParentDirectory %>" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
     <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentBlogPost %>"   ID="PathBrowser1" />
	
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentBlogPost %>" />
	    </SidePanelTemplate>
	    <MainPanelTemplate>

            <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	        <xs:BlogPostTabControl ID="BlogPostTabControl" FileID="<%# CurrentBlogPost.ID %>" runat="server" Selected="Properties" />
	        <div class="formPanel">
	            <fieldset>
		            <legend>Blog Post Properties</legend>
		            
		            <label for="TextBoxDisplayName" class="required">Title:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxTitle" /> 
		            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Runat="server" ControlToValidate="TextBoxTitle" Display="Dynamic" ErrorMessage="Required." />				
		            <span class="toolTip">This is the name that will be displayed.</span>
		            
		            <label for="TextBoxFileName" class="required">File Name:</label> <asp:TextBox CssClass="input" Runat="server" ID="TextBoxFileName" />
		            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" ErrorMessage="Required." />
		            <span class="toolTip">This is the name as it will appear in the URL ( http://www.yoursite.com/filename ).</span>
		            
		            <label for="TextBoxAuthor" class="required">Author:</label> <asp:TextBox CssClass="input" Runat="server" ID="TextBoxAuthor" />
		            <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxAuthor" Display="Dynamic" ErrorMessage="Required." />
		            <span class="toolTip">The name of the author for this post.</span>
        		    
		            <label for="TextBoxText">Text:</label>
		            <br />
		            <radE:RadEditor ID="TextBoxText" Runat="server"
					    Width="400px"
					    Height="400px"
					    Editable="True"
					    ShowSubmitCancelButtons="False"
					    ToolsFile="~/Modules/Blog/ToolsFile.xml" >
					</radE:RadEditor>
				    <br/>
            		
		            <asp:CheckBox CssClass="checkbox" Runat="server" ID="CheckBoxIsHidden" Text="Is hidden?" />
		            <span class="toolTip">Whether the blog is hidden in the navigation.</span>
            		
		            <asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdate_OnClick" ID="Button1"/>
		            <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	            </fieldset>
	        </div>
        	
        	
	        <databinding:DataBindingManager ID="DataBindingManagerBlogPost" runat="server">
		        <databinding:TextBoxBinding ControlToBind="TextBoxFileName"  DataMember="FileName" />
		        <databinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
		        <databinding:TextBoxBinding ControlToBind="TextBoxAuthor" DataMember="Author" />
		        <DataBinding:PropertyBinding ControlToBind="TextBoxText" DataMember="Text"  Property="Html" />
		        <databinding:CheckBoxBinding ControlToBind="CheckBoxIsHidden" DataMember="IsHidden" />
	        </databinding:DataBindingManager>
	
	    </MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
