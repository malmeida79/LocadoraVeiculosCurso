<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroClientes.aspx.cs" Inherits="LocadoraVeiculos.CadastroClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <center>

    <h3>Cadastro de Clientes</h3>

        <asp:HiddenField ID="hdnIdCliente" runat="server"></asp:HiddenField>
    Nome: <asp:TextBox ID="txtNome" runat="server"></asp:TextBox> <br />
    CPF / CNPJ: <asp:TextBox ID="txtCpf" runat="server"></asp:TextBox><br />
        <br />
        <asp:Button ID="cmdSalvar" runat="server" Text="Salvar" OnClick="cmdSalvar_Click"></asp:Button>
        <asp:Button ID="cmdExcluir" runat="server" Text="Excluir" OnClick="cmdExcluir_Click"></asp:Button>
        <asp:Button ID="cmdVoltar" runat="server" Text="Voltar" OnClick="cmdVoltar_Click"></asp:Button>

    </center>

</asp:Content>
