<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditSite.aspx.cs" Inherits="Xenosynth.Admin.Configuration.EditSite" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="SiteTasks" Src="~/Configuration/SiteTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SiteTabControl" Src="~/Configuration/SiteTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Controls/PathBrowser.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:SiteTasks runat="server" CurrentSite='<%# CurrentSite %>' />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser runat="server" 
	    RootPage="Sites.aspx" 
	    RootPageName="Sites" 
	    SubPage='<%# "EditSite.aspx?SiteID=" + SiteID %>' 
	    SubPageName='<%# CurrentSite.Name %>'
	    />
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:SiteTabControl ID="SiteTabControl1" runat="server" Selected="Properties" SiteID="<%# SiteID %>" />
    <div class="formPanel">
        
				<fieldset>
					<legend>Properties</legend>
					<label for="TextBoxName">Name:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxName" /> <br />
					
					<label for="TextBoxDescription">Description:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxDescription" />
					<br />
					<span class="toolTip">A description of the site.<asp:TextBox Runat="server" ID="TextBoxDescriptionCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
					<mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxDescription" MaxLength="250" OutputControl="TextBoxDescriptionCount" ErrorMessage="Description can be no longer than 250 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator1"/>
					<br />
					
					<label for="TextBoxApplicationPath">Application Path:</label>  <asp:TextBox CssClass="input" style="width: 250px;" Runat="server" ID="TextBoxApplicationPath"  MaxLength="250"/>
		            <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxApplicationPath" Display="Dynamic" ErrorMessage="Required." ID="Requiredfieldvalidator1"/>
		            <span class="toolTip">The web site's virtual application path on the web server.</span>
            		
		            <label for="TextBoxDirectory">Directory:</label>  <asp:TextBox CssClass="input" style="width: 500px;" Runat="server" ID="TextBoxDirectory"  MaxLength="500"/>
		            <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxDirectory" Display="Dynamic" ErrorMessage="Required." ID="Requiredfieldvalidator2"/>
		            <span class="toolTip">The path to the web site's directory on the file system.</span>
					
					<asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdate_OnClick" ID="Button1"/>
					<asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
				</fieldset>
				
				<DataBinding:DataBindingManager ID="DataBindingManagerSite" runat="server">
					<DataBinding:TextBoxBinding ControlToBind="TextBoxName" DataMember="Name" />
					<DataBinding:TextBoxBinding ControlToBind="TextBoxDescription" DataMember="Description" />
					<DataBinding:TextBoxBinding ControlToBind="TextBoxApplicationPath" DataMember="ApplicationPath" />
					<DataBinding:TextBoxBinding ControlToBind="TextBoxDirectory" DataMember="Directory" />
				</DataBinding:DataBindingManager>
    </div>
    
</asp:Content>
