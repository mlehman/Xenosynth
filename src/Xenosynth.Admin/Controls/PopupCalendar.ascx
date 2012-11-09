<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PopupCalendar.ascx.cs" Inherits="Xenosynth.Admin.Controls.PopupCalendar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>

<span style="position: relative;">
<asp:ImageButton ID="LinkButtonToggle" runat="server" OnClick="LinkButtonToggle_OnClick" 
ImageUrl="~/images/icon_calendar.gif" border="0" />
<asp:UpdatePanel ID="UpdatePanelCalendar" runat="server" UpdateMode="Conditional" RenderMode="Inline">
    <Triggers>
        <asp:AsyncPostbackTrigger ControlID="LinkButtonToggle" EventName="Click" />
    </Triggers>
    <ContentTemplate>
            <asp:Panel ID="PanelCalendar" runat="server" Visible="false" style="position:absolute;z-index:100;top:0;left:20;" BackColor="#ffffff" BorderColor="#999999" BorderWidth="1"  >
                <asp:Calendar 
	                ID="Calendar1"
	                Runat="server"
	                OnSelectionChanged="Calendar1_OnSelectionChanged"
                >
                </asp:Calendar>
                <br />
                
                <asp:Button runat="server" ID="ButtonClose"  OnClick="ButtonClose_OnClick" Text="Cancel" />
            </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>
</span>