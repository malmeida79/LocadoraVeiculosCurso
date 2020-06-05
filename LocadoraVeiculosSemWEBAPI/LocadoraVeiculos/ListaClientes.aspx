<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaClientes.aspx.cs" Inherits="LocadoraVeiculos.ListaClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>
    <h3> Listagem de clientes</h3>

    Nome Cliente:&nbsp; <asp:TextBox ID="txtNomeCliente" runat="server" Width="284px"></asp:TextBox> 

    <br />
    <br />
    <asp:Button ID="cmdPesquisa" runat="server" Text="Pesquisa Nome" OnClick="cmdPesquisa_Click" />
    <asp:Button ID="cmdListar" runat="server" Text="Listar" OnClick="cmdListar_Click" />
    <asp:Button ID="cmdNovo" runat="server" Text="Novo cliente ..." OnClick="cmdNovo_Click" />
        <br />
    <br />

    <asp:GridView ID="gdvClientes" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="True" OnPageIndexChanging="gdvClientes_PageIndexChanging" OnRowDataBound="gdvClientes_RowDataBound" OnRowEditing="gdvClientes_RowEditing">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="idcliente" HeaderText="Código" SortExpression="idcliente"/>
            <asp:BoundField DataField="nomeCliente" HeaderText="Nome" SortExpression="nomeCliente"/>
            <asp:BoundField DataField="cpfCnpjCliente" HeaderText="CPF / CNPJ" SortExpression="cpfCnpjCliente"/>
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
