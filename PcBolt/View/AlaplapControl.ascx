<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlaplapControl.ascx.cs" Inherits="PcBolt.View.AlaplapControl" 
    ClassName="AlaplapControl"%>


<asp:Panel ID="Panel1" runat="server" Font-Size="Larger" GroupingText="Alaplap" Width="527px">
   <table>
        <tr>
            <td>
                    <table>
                   
                    <tr>
                        <td>
                            Gyártó:
                        </td>
                        <td>
                            <asp:Textbox ID="tb_gyarto" runat="server" Width="136px" ReadOnly="True"></asp:Textbox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ár:
                        </td>
                        <td>
                            <asp:TextBox  ID="tb_ar" runat="server" Width="136px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>                        
                    <tr>
                        <td>
                            Akció (%):
                        </td>
                        <td>
                            <asp:TextBox ID="tb_akcio" runat="server" Width="136px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>        
                    <tr>
                        <td>
                            Leírás:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_leiras" Rows="6" TextMode="MultiLine" runat="server" 
                                Width="136px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
              </td>
              <td>
                   <table>
                    <tr>
                        <td>
                            Memóriák száma:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_alaplap_mem" runat="server" Width="136px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            IDE száma:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_alaplap_ide"  runat="server" Width="136px" 
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           Sata száma:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_alaplap_sata" runat="server" Width="136px" 
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            CPU foglalat:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_alaplap_cpu_fog" runat="server" Width="142px" 
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Memória foglalat:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_alaplap_mem_fog" runat="server" Width="142px" 
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Videó foglalat:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_alaplap_video_fog"  runat="server" Width="142px" 
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btn_details" Text="Részletek" OnClick="btn_details_Click" />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btn_buy" Text="Kosárba" OnClick="btn_buy_Click" />
                        </td>
                    </tr>
                    </table>                
                </td>
                </tr>
                </table>
          </asp:Panel>