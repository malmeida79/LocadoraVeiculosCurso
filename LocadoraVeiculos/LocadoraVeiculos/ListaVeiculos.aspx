<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaVeiculos.aspx.cs" Inherits="LocadoraVeiculos.ListaVeiculos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>
    <h3> Listagem de veículos</h3>

    Placa:&nbsp; <asp:TextBox ID="txtPlaca" runat="server" Width="284px"></asp:TextBox> 

    <br />
    <br />
    <asp:Button ID="cmdPesquisa" runat="server" Text="Pesquisa Placa" OnClick="cmdPesquisa_Click" />
    <asp:Button ID="cmdListar" runat="server" Text="Listar" OnClick="cmdListar_Click" />
    <asp:Button ID="cmdNovo" runat="server" Text="Novo veiculo ..." OnClick="cmdNovo_Click" />
        <br />
    <br />

    <asp:GridView ID="gdvVeiculos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="True" OnPageIndexChanging="gdvVeiculos_PageIndexChanging" OnRowDataBound="gdvVeiculos_RowDataBound" OnRowEditing="gdvVeiculos_RowEditing">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="idVeiculo" HeaderText="Código" SortExpression="codEndereco"/>
            <asp:BoundField DataField="placaVeiculo" HeaderText="Placa" SortExpression="codEndereco"/>
            <asp:BoundField DataField="NomeMarcaVeiculo" HeaderText="Marca" SortExpression="codEndereco"/>
            <asp:BoundField DataField="NomeModeloVeiculo" HeaderText="Modelo" SortExpression="codEndereco"/>
            <asp:BoundField DataField="corVeiculo" HeaderText="Cor" SortExpression="codEndereco"/>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />

    </asp:GridView>

    <br />
        </center>
</asp:Content>
