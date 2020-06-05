<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaLocacoes.aspx.cs" Inherits="LocadoraVeiculos.ListaLocacoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>
    <h3> Listagem de Locações</h3>

    Nome Cliente:&nbsp; <asp:TextBox ID="txtNomeCliente" runat="server" Width="284px"></asp:TextBox> 
    &nbsp;Placa:&nbsp; <asp:TextBox ID="txtPlaca" runat="server" Width="284px"></asp:TextBox> 

    <br />
    <br />
    <asp:Button ID="cmdPesquisa" runat="server" Text="Pesquisar placa e ou nome" OnClick="cmdPesquisa_Click" />
    <asp:Button ID="cmdListar" runat="server" Text="Listar" OnClick="cmdListar_Click" />
    <asp:Button ID="cmdNovo" runat="server" Text="Nova locação ..." OnClick="cmdNovo_Click" />
        <br />
    <br />

    <asp:GridView ID="gdvLocacoes" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="True" OnPageIndexChanging="gdvLocacoes_PageIndexChanging" OnRowDataBound="gdvLocacoes_RowDataBound" OnRowEditing="gdvLocacoes_RowEditing" OnRowCommand="gdvLocacoes_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="IdLocacao" HeaderText="Código" SortExpression="IdLocacao"/>
            <asp:BoundField DataField="PlacaVeiculo" HeaderText="Placa" SortExpression="PlacaVeiculo"/>
            <asp:BoundField DataField="CorVeiculo" HeaderText="Cor" SortExpression="CorVeiculo"/>
            <asp:BoundField DataField="NomeModeloVeiculo" HeaderText="Modelo" SortExpression="ModeloVeiculo"/>
            <asp:BoundField DataField="NomeMarcaVeiculo" HeaderText="Marca" SortExpression="MarcaVeiculo"/>
            <asp:BoundField DataField="NomeCliente" HeaderText="Nome" SortExpression="NomeCliente"/>
            <asp:BoundField DataField="DataLocacao" HeaderText="Data Locação" SortExpression="DataLocacao"/>
            <asp:BoundField DataField="DataDevolucao" HeaderText="Data Devolução" SortExpression="DataDevolucao"/>
            <asp:BoundField DataField="Observacoes" HeaderText="Observações" SortExpression="Observacoes"/>
            <asp:ButtonField ButtonType="Button" CommandName="devolver" Text="Devolução" />
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
