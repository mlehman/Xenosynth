<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AddBlogPost.aspx.cs" Inherits="Xenosynth.Modules.Blog.Admin.AddBlogPost" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTabControl" Src="BlogPostTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Modules/Cms/PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="~/Modules/Cms/SearchFiles.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="~/Modules/Cms/FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="~/Modules/Cms/SlidePanel.ascx" %>
<%@ Register TagPrefix="radE" Namespace="Telerik.WebControls" Assembly="RadEditor.Net2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
     <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentBlog %>"   ID="PathBrowser1" />
	
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentBlog %>" />
	    </SidePanelTemplate>
	    <MainPanelTemplate>

	        <xs:BlogPostTabControl ID="BlogPostTabControl" runat="server" Selected="Properties" />
	        <div class="formPanel">
	            <fieldset>
		            <legend>Blog Post Properties</legend>
		            
		            <label for="TextBoxDisplayName" class="required">Title:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxTitle" /> 
		            <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxTitle" Display="Dynamic" ErrorMessage="Required." />				
		            <span class="toolTip">This is the name that will be displayed.</span>
		            
		            <label for="TextBoxFileName" class="required">File Name:</label> <asp:TextBox CssClass="input" Runat="server" ID="TextBoxFileName" />
		            <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" ErrorMessage="Required." />
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
            		
		            <asp:Button Runat="server" CssClass="submit" Text="Create &raquo;" OnClick="ButtonAdd_OnClick" ID="Button1"/>
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
