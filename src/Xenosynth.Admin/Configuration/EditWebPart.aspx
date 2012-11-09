<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditWebPart.aspx.cs" Inherits="Xenosynth.Admin.Configuration.EditWebPart" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="WebPartTasks" Src="~/Configuration/WebPartTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Controls/PathBrowser.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:WebPartTasks ID="SiteTasks1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser ID="PathBrowser1" runat="server" 
	    RootPage="WebParts.aspx" 
	    RootPageName="Web Parts" 
	    SubPage='<%# "EditWebPart.aspx?WebPartID=" + WebPartID %>' 
	    SubPageName='<%# CurrentRegisteredWebPart.Title %>'
	    />
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
   
        
	<fieldset>
		<legend>Properties</legend>
		
		<label for="TextBoxTitle">Title:</label>  <asp:TextBox CssClass="input" style="width: 250px;" Runat="server" ID="TextBoxTitle"  MaxLength="250"/>
		<asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxTitle" Display="Dynamic" ErrorMessage="Required." ID="Requiredfieldvalidator1"/>
		<span class="toolTip">The title of the web part.</span>
					
		<label for="TextBoxDescription">Description:</label>  <asp:TextBox CssClass="input" Runat="server" Width="400" Rows="4" TextMode="MultiLine" ID="TextBoxDescription" />
		<br />
		<span class="toolTip">A description of the web part.<asp:TextBox Runat="server" ID="TextBoxDescriptionCount" CssClass="charCount" Columns="4" />&nbsp; characters remaining</span>
		<mtv:MultiLineTextBoxValidator runat="server" ControlToValidate="TextBoxDescription" MaxLength="250" OutputControl="TextBoxDescriptionCount" ErrorMessage="Description can be no longer than 250 characters" ShowJavascriptAlert="True" EnableClientSideRestriction="True" ShowCharacterCount="True" ID="Multilinetextboxvalidator1"/>
		<br />
		
		<label for="TextBoxImageUrl">Image URL:</label>  <asp:TextBox CssClass="input" style="width: 250px;" Runat="server" ID="TextBoxImageUrl"  MaxLength="250"/>
		<span class="toolTip">The URL for image.</span>
		
		<label for="TextBoxClassName">Class Name:</label>  <asp:TextBox CssClass="input" style="width: 250px;" Runat="server" ID="TextBoxClassName"  MaxLength="250"/>
		<span class="toolTip">The class name if web control.</span>
		
		<label for="TextBoxUrl">URL:</label>  <asp:TextBox CssClass="input" style="width: 250px;" Runat="server" ID="TextBoxUrl"  MaxLength="250"/>
		<span class="toolTip">The URL if user control.</span>
		
		<asp:Button Runat="server" CssClass="submit" Text="Update &raquo;" OnClick="ButtonUpdate_OnClick" ID="Button1"/>
		<asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	</fieldset>
	
	<DataBinding:DataBindingManager ID="DataBindingManagerWebPart" runat="server">
		<DataBinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxDescription" DataMember="Description" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxClassName" DataMember="ClassName" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxUrl" DataMember="Url" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxImageUrl" DataMember="ImageUrl" />
	</DataBinding:DataBindingManager>
    
    
</asp:Content>

