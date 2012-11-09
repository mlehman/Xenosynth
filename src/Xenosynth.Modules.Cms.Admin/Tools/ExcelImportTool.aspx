<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ExcelImportTool.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.Tools.ExcelImportTool" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
   <fieldset>
		    <legend>Create Pages From Excel File</legend>
		    
		    <label for="TextBoxID">Select File:</label><input runat="server" type="file" id="InputFileUpload" class="file" /><br />
		    <span class="toolTip">Excel file (*.xls)</span>
		    
		    <label for="DropDownListTemplates"> Template:</label>  <asp:DropDownList CssClass="input" Runat="server" ID="DropDownListTemplates" /> 
            <asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="DropDownListTemplates" Display="Dynamic" ErrorMessage="Required." ID="Requiredfieldvalidator2"/>
            <br />
            <span class="toolTip">The template that defines the look and feel of the page.</span>
    	
    	    <label for="TextBoxFullPath">Directory:</label>  <asp:DropDownList CssClass="input" ID="DropDownListDirectories" Runat="server" /> <br />
    	    
		    <asp:Button Runat="server" CssClass="submit" Text="Create &raquo;" OnClick="ButtonAdd_OnClick" ID="Button1"/>
		    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	    </fieldset>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
</asp:Content>
