<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AddRole.aspx.cs" Inherits="Xenosynth.Admin.Security.AddRole" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="RoleTasks" Src="~/Security/RoleTasks.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Side" runat="server">
 <xs:RoleTasks ID="RoleTasks1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <fieldset>
	    <legend>Role Properties</legend>
	    
	    <label for="TextBoxRole">Role:</label> <asp:TextBox CssClass="input" Runat="server" ID="TextBoxRole" />
	    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="TextBoxRole" Display="Dynamic" ErrorMessage="Required." />
	    <asp:CustomValidator CssClass="error" Runat="server" ControlToValidate="TextBoxRole" Display="Dynamic" ErrorMessage="Role already exists." OnServerValidate="ValidatorDuplicate_OnServerValidate" />
	    <br />
	    <asp:Button Runat="server" CssClass="submit" Text="Create &raquo;" OnClick="ButtonAdd_OnClick" ID="Button1"/>
	    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
    </fieldset>
</asp:Content>
