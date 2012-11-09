<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SlidePanel.ascx.cs" Inherits="Xenosynth.Modules.Cms.Admin.SlidePanel" %>

<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td valign="top" style="overflow: scroll;">
            <asp:Panel ID="SidePanel" runat="server" Width="160">
                <div id="SidePanelContents" style="display: <%= Collapsed ? "none" : "block" %>";">
                <asp:PlaceHolder ID="SidePanelPlaceHolder" runat="server"></asp:PlaceHolder>
                </div>
            </asp:Panel>
        </td>
        <td class="slider" width="20">
            <asp:Image ID="ImageSlider" ImageUrl="~/images/slider.gif" Width="20" runat="server" />
            <div style="width:20px;">&nbsp;</div>
        </td>
        <td valign="top" width="100%">
            <asp:PlaceHolder ID="MainPanelPlaceHolder" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
</table>

 <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSidePanel" runat="Server" 
            TargetControlID="SidePanel"
            CollapsedSize="0"
            ExpandedSize="160"
            ImageControlID="ImageSlider"
            ExpandControlID="ImageSlider"
            CollapseControlID="ImageSlider"
            SuppressPostBack="true"
            TextLabelID="Label1"
            CollapsedText="Open Directory Explorer"
            ExpandedText="Close Directory Explorer"
            ExpandedImage="~/images/sliderLeft.gif"
            CollapsedImage="~/images/sliderRight.gif"
            ExpandDirection="Horizontal"
            ScrollContents="false"
            Collapsed="true"
            />
    
    <script language="javascript">
    //
        function pageLoad(){
     
           var _trackState = function(sender, e) { 
                var spb = $get('SidePanelContents');   
                if(sender.get_Collapsed()){
                    spb.style.display = "none";
                } else {
                    spb.style.display = "block";
                }
                document.cookie = 'spb.clpsd=' + sender.get_Collapsed() + ';path=/'; 
           }
           
           var b = $find('<%= CollapsiblePanelExtenderSidePanel.ClientID %>');
           b.add_collapseComplete(_trackState);
           b.add_expandComplete(_trackState);
        }
        
        
    </script>