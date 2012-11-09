<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditBlogPostPublishing.aspx.cs" Inherits="Xenosynth.Modules.Blog.Admin.EditBlogPostPublishing" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTabControl" Src="BlogPostTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogTasks" Src="BlogTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTasks" Src="BlogPostTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Modules/Cms/PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="~/Modules/Cms/SearchFiles.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="~/Modules/Cms/FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="~/Modules/Cms/SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PopupCalendar" Src="~/Controls/PopupCalendar.ascx" %>

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
	        <xs:BlogPostTabControl ID="BlogPostTabControl" FileID="<%# CurrentBlogPost.ID %>" runat="server" Selected="Publishing" />
	        <div class="formPanel">
	            <fieldset>
		            <legend>Page Publishing</legend>
		   
    		<asp:UpdatePanel ID="UpdatePanelPublishStart" runat="server" UpdateMode="Always"  RenderMode="Inline" >
    		    <Triggers>
    		    </Triggers>
    		    <ContentTemplate>
		            <label for="TextBoxPublishStart">Publish Start:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxPublishStart" />
		            <xs:PopupCalendar ID="PopupCalendar1" Runat="server" TextBoxToSet="TextBoxPublishStart" DateFormat="d" /> <br clear="left" />
		            <span class="toolTip">The date the page will be available after publication.</span>
		        </ContentTemplate>
		    </asp:UpdatePanel>
		   
		    
    		
    		
    		<asp:UpdatePanel ID="UpdatePanelPublishEnd" runat="server" UpdateMode="Always" RenderMode="Inline">
    		     <Triggers>
    		    </Triggers>
    		    <ContentTemplate>
		            <label for="TextBoxPublishEnd">Publish End:</label>  <asp:TextBox  CssClass="input" Runat="server" ID="TextBoxPublishEnd" />
		            <xs:PopupCalendar ID="PopupCalendar2" Runat="server" TextBoxToSet="TextBoxPublishEnd" DateFormat="d" /><br />
		            <span class="toolTip">The date the page will no longer be available after publication.</span>
		        </ContentTemplate>
		    </asp:UpdatePanel>
		    
		   
    		
		   
    		
		    <asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdatePage_OnClick" ID="Button1"/>
		    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	        </div>
        	
        	
	        <databinding:DataBindingManager ID="DataBindingManagerBlogPost" runat="server">
		        <DataBinding:TextBoxBinding ControlToBind="TextBoxPublishStart" DataMember="PublishStart" DisplayFormat="{0:d}" />
		        <DataBinding:TextBoxBinding ControlToBind="TextBoxPublishEnd" DataMember="PublishEnd" DisplayFormat="{0:d}" />
	        </databinding:DataBindingManager>
	
	    </MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
