<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaModelos.aspx.cs" Inherits="LocadoraVeiculos.ListaModelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>
    <h3> Listagem de Marcas de Veiculos</h3>

    <asp:Button ID="cmdListar" runat="server" Text="Listar" OnClick="cmdListar_Click" />
    <asp:Button ID="cmdNovo" runat="server" Text="Nova modelo ..." OnClick="cmdNovo_Click" />
        <br />
    <br />

    <asp:GridView ID="gdvModeloVeiculo" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="True" OnPageIndexChanging="gdvModeloVeiculo_PageIndexChanging" OnRowDataBound="gdvModeloVeiculo_RowDataBound" OnRowEditing="gdvModeloVeiculo_RowEditing">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="idModeloVeiculo" HeaderText="Código" SortExpression="idModeloVeiculo"/>
            <asp:BoundField DataField="ModeloVeiculo" HeaderText="Modelo" SortExpression="ModeloVeiculo"/>
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
