<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroModeloVeiculo.aspx.cs" Inherits="LocadoraVeiculos.CadastroModeloVeiculo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <center>

    <h3>Cadastro de Modelos de Veiculo</h3>

        <asp:HiddenField ID="hdnIdModelo" runat="server"></asp:HiddenField>
    Modelo: <asp:TextBox ID="txtModelo" runat="server"></asp:TextBox> <br />
        <br />
        <asp:Button ID="cmdSalvar" runat="server" Text="Salvar" OnClick="cmdSalvar_Click"></asp:Button>
        <asp:Button ID="cmdExcluir" runat="server" Text="Excluir" OnClick="cmdExcluir_Click"></asp:Button>
        <asp:Button ID="cmdVoltar" runat="server" Text="Voltar" OnClick="cmdVoltar_Click"></asp:Button>

    </center>

</asp:Content>
