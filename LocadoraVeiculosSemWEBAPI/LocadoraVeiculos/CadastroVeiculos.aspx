<%@ Page Title="Veiculos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroVeiculos.aspx.cs" Inherits="LocadoraVeiculos.cadastroVeiculos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>

    <h3>Cadastro de Veiculos</h3>

        <asp:HiddenField ID="hdnIdVeiculo" runat="server"></asp:HiddenField>
    Placa: <asp:TextBox ID="txtPlaca" runat="server"></asp:TextBox> <br />
    Marca: <asp:DropDownList ID="cboMarca" runat="server"></asp:DropDownList><br />
    Modelo:<asp:DropDownList ID="cboModelo" runat="server"></asp:DropDownList><br />
    Cor: <asp:TextBox ID="txtCor" runat="server"></asp:TextBox><br />
        <br />
        <asp:Button ID="cmdSalvar" runat="server" Text="Salvar" OnClick="cmdSalvar_Click"></asp:Button>
        <asp:Button ID="cmdExcluir" runat="server" Text="Excluir" OnClick="cmdExcluir_Click"></asp:Button>
        <asp:Button ID="cmdVoltar" runat="server" Text="Voltar" OnClick="cmdVoltar_Click"></asp:Button>

    </center>
</asp:Content>
