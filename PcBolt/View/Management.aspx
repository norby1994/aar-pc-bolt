<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Management.aspx.cs" Inherits="PcBolt.View.Management" 
         MasterPageFile="~/View/MasterPage.Master" Title="PcBolt termék menedzsment"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <br />
    <p>Termék menedzsment</p>
    <table>
    <tr>    
        <td>
            <asp:Panel runat="server" GroupingText="Új típusok felvétele:" Width="180">
                <br />
                <asp:ListBox ID="lb_type_add" runat="server" Rows="1" Width="142px">
                    <asp:ListItem Value="CPU">Processzor foglalat</asp:ListItem>
                    <asp:ListItem Value="RAM">Memória foglalat</asp:ListItem>
                    <asp:ListItem Value="HDD">HDD csatoló</asp:ListItem>
                    <asp:ListItem Value="GFX">Videókártya csatoló</asp:ListItem>
                </asp:ListBox>
                <br /><br />
                <asp:TextBox ID="tb_type_add" TextMode="MultiLine" Rows="5" runat="server" Width="136px" />
                <br /><br />
                <asp:Button runat="server" ID="btn_add" Text="Felvesz" OnClick="btn_add_Click" />
            </asp:Panel>
        </td>
        <td valign="top">
            <asp:Panel runat="server" GroupingText="Típusok törlése" Width="180">
                <br />
                 <asp:ListBox ID="lb_type_del" runat="server" Rows="1" Width="142px" AutoPostBack="true">
                    <asp:ListItem Selected="True" Value="CPU">Processzor foglalat</asp:ListItem>
                    <asp:ListItem Value="RAM">Memória foglalat</asp:ListItem>
                    <asp:ListItem Value="HDD">HDD csatoló</asp:ListItem>
                    <asp:ListItem Value="GFX">Videókártya csatoló</asp:ListItem>
                </asp:ListBox>
                <br /><br />
                <asp:ListBox ID="lb_type_del_items" SelectionMode="Multiple" runat="server" Rows="5"  Width="142px">
                </asp:ListBox>
                <br /><br />
                <asp:Button runat="server" ID="btn_del" Text="Töröl" OnClick="btn_del_Click" />
            </asp:Panel>
        </td>
    </tr>
    </table>
    <br /><br />
    <asp:Panel runat = "server" GroupingText="Gyártó felvétele" Width="250">
        <table>
        <tr>
            <td>
                Gyártó neve:
            </td>
            <td>
                <asp:TextBox ID="tb_uj_gyarto" runat="server" Width="136px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" ID="btn_add_gyarto" Text="OK" OnClick="addGyarto" />
            </td>
        </tr>
        </table>
    </asp:Panel>
    <br /><br />        
    <asp:Panel runat="server" GroupingText="Áruk felvétele" Width="900px">
    <table>
        <tr>
            <td rowspan="3" valign="top">
                <asp:Panel runat="server" GroupingText="Alapadatok" Width = "250">
                    <table>
                    <tr>
                        <td>
                            Áru neve:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_nev" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Gyártó:
                        </td>
                        <td>
                            <asp:Listbox ID="lb_gyarto" Rows="1" runat="server" Width="142px"></asp:Listbox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ár:
                        </td>
                        <td>
                            <asp:TextBox  ID="tb_ar" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Darabszám:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_darab" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>     
                    <tr>
                        <td>
                            Akció (%):
                        </td>
                        <td>
                            <asp:TextBox ID="tb_akcio" runat="server" Width="136px">0</asp:TextBox>
                        </td>
                    </tr>        
                    <tr>
                        <td>
                            Leírás:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_leiras" Rows="9" TextMode="MultiLine" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
            </td>
            <td valign="top">
                <asp:Panel runat="server" GroupingText="HDD">
                    <table>
                    <tr>
                        <td>
                            Kapacitás:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_hdd_meret" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Csatoló:
                        </td>
                        <td>
                            <asp:Listbox ID="lb_hdd_csatolo" Rows="1" runat="server" Width="142px"></asp:Listbox>
                        </td>
                         <td>
                            <asp:Button runat="server" ID="Button1" Text="OK" OnClick="btn_hdd_Click"/>
                         </td>
                    </tr>
                    </table>
                </asp:Panel>
            </td>
            <td valign="top" rowspan="2">
                <asp:Panel ID="Panel4" runat="server" GroupingText="Alaplap">
                    <table>
                    <tr>
                        <td>
                            Memóriák száma:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_alaplap_mem" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            IDE száma:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_alaplap_ide" Rows="1" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           Sata száma:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_alaplap_sata" Rows="1" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            CPU foglalat:
                        </td>
                        <td>
                            <asp:Listbox ID="lb_alaplap_cpu_fog" Rows="1" runat="server" Width="142px"></asp:Listbox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Memória foglalat:
                        </td>
                        <td>
                            <asp:Listbox ID="lb_alaplap_mem_fog" Rows="1" runat="server" Width="142px"></asp:Listbox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Videó foglalat:
                        </td>
                        <td>
                            <asp:Listbox ID="lb_alaplap_video_fog" Rows="1" runat="server" Width="142px"></asp:Listbox>
                        </td>
                         <td>
                            <asp:Button runat="server" ID="Button2" Text="OK" OnClick="btn_alaplap_Click"/>
                         </td>
                    </tr>
                    </table>
                </asp:Panel>
            </td>              
        </tr>
        <tr>
             <td valign="top">
                <asp:Panel ID="Panel3" runat="server" GroupingText="Videókártya">
                    <table>
                    <tr>
                        <td>
                            Memória méret:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_video_memoria" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Foglalat:
                        </td>
                        <td>
                            <asp:Listbox ID="lb_video_foglalat" Rows="1" runat="server" Width="142px"></asp:Listbox>
                        </td>
                         <td>
                            <asp:Button runat="server" ID="Button3" Text="OK" OnClick="btn_video_Click" />
                         </td>
                    </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
             <td valign="top">
                <asp:Panel ID="Panel1" runat="server" GroupingText="Processzor">
                    <table>
                    <tr>
                        <td>
                            Sebesség:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_proc_sebesseg" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Magok száma:
                        </td>
                        <td>
                            <asp:Textbox ID="tb_proc_magok" Rows="1" runat="server" Width="136px"></asp:Textbox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Dobozos:
                        </td>
                        <td>
                            <asp:CheckBox ID="chk_proc_dobozos" Checked="true" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Foglalat:
                        </td>
                        <td>
                            <asp:Listbox ID="lb_proc_foglalat" Rows="1" runat="server" Width="142px"></asp:Listbox>
                        </td>
                         <td>
                            <asp:Button runat="server" ID="Button4" Text="OK" OnClick="btn_proc_Click" />
                         </td>
                    </tr>
                    </table>
                </asp:Panel>
            </td>
            <td >
                <asp:Panel ID="Panel2" runat="server" GroupingText="Memória">
                    <table>
                    <tr>
                        <td>
                            Méret:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ram_meret" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sebesség:
                        </td>
                        <td>
                            <asp:TextBox ID="tb_ram_sebesseg" runat="server" Width="136px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Típus:
                        </td>
                        <td>
                            <asp:Listbox ID="lb_ram_tipus" Rows="1" runat="server" Width="142px"></asp:Listbox>
                        </td>
                         <td>
                            <asp:Button runat="server" ID="Button5" Text="OK" OnClick="btn_mem_Click"/>
                         </td>
                    </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>
