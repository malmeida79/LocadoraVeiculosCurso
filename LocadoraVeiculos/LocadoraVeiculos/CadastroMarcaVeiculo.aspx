<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroMarcaVeiculo.aspx.cs" Inherits="LocadoraVeiculos.CadastroMarcaVeiculo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <center>

    <h3>Cadastro de Marcas de Veiculo</h3>

        <asp:HiddenField ID="hdnIdMarca" runat="server"></asp:HiddenField>
    Marca: <asp:TextBox ID="txtMarca" runat="server"></asp:TextBox> <br />
        <br />
        <asp:Button ID="cmdSalvar" runat="server" Text="Salvar" OnClick="cmdSalvar_Click"></asp:Button>
        <asp:Button ID="cmdExcluir" runat="server" Text="Excluir" OnClick="cmdExcluir_Click"></asp:Button>
        <asp:Button ID="cmdVoltar" runat="server" Text="Voltar" OnClick="cmdVoltar_Click"></asp:Button>

    </center>

</asp:Content>
