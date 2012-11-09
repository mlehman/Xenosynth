<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditPagePublishing.aspx.cs" Inherits="Xenosynth.Admin.Content.Page.EditPagePublishing" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="PageTasks" Src="PageTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PopupCalendar" Src="~/Controls/PopupCalendar.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>
<%@ Register TagPrefix="xs" TagName="PageTabControl" Src="PageTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTasks" Src="../Directory/DirectoryTasks.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentPage %>"   ID="PathBrowser1" />

    <xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentPage %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
	<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<xs:PageTabControl ID="PageTabControl1" runat="server" FileID="<%# CurrentPage.ID %>" Selected="Publishing" />
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
	    </fieldset>
	</div>
	
	<DataBinding:DataBindingManager ID="DataBindingManagerPage" runat="server">
		<DataBinding:TextBoxBinding ControlToBind="TextBoxPublishStart" DataMember="PublishStart" DisplayFormat="{0:d}" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxPublishEnd" DataMember="PublishEnd" DisplayFormat="{0:d}" />
	</DataBinding:DataBindingManager>
	
	 </MainPanelTemplate>
    </xs:SlidePanel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
		<xs:PageTasks Runat="server" CurrentPage="<%# CurrentPage %>" ID="Pagetasks1"/>	
		<xs:DirectoryTasks runat="server" CurrentDirectory="<%# CurrentPage.ParentDirectory %>" />	
</asp:Content>
