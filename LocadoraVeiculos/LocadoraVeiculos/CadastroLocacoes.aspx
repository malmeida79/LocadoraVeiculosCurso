<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroLocacoes.aspx.cs" Inherits="LocadoraVeiculos.cadastroLocacoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>
    <h3>Cadastro de Locações </h3>

    <asp:HiddenField ID="hdnIdClienteSelecionado" runat="server" />
    <asp:HiddenField ID="hdnIdVeiculoSelecionado" runat="server" />
    Nome Cliente: <asp:Label ID="lblNome" runat="server" Text=""></asp:Label> <br />
    Placa Veiculo: <asp:Label ID="lblVeiculo" runat="server" Text=""></asp:Label><br /><br />

    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModalSelCliente">
        Selecionar Cliente
    </button>

    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModalSelVeiculo">
        Selecionar Veiculo
    </button>

        <asp:Button ID="btnLimpar"  CssClass="btn btn-primary" runat="server" Text="Limpar Seleções" OnClick="btnLimpar_Click" />
        <asp:Button ID="btnLocacao"  CssClass="btn btn-primary" runat="server" Text="Efetuar Locação" OnClick="btnLocacao_Click" />

        </center>

    <!-- Modal -->
    <div class="modal fade" id="myModalSelCliente" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel">:: SysTest - Selecionar Clientes ::</h4>
                </div>
                <div class="modal-body">
                    <center>
                    <asp:GridView ID="gdvClientes" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="True" OnPageIndexChanging="gdvClientes_PageIndexChanging" OnRowDataBound="gdvClientes_RowDataBound" OnRowEditing="gdvClientes_RowEditing">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="idcliente" HeaderText="Código" SortExpression="idcliente" />
                            <asp:BoundField DataField="nomeCliente" HeaderText="Nome" SortExpression="nomeCliente" />
                            <asp:BoundField DataField="cpfCnpjCliente" HeaderText="CPF / CNPJ" SortExpression="cpfCnpjCliente" />
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
                        </center>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="myModalSelVeiculo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel">:: SysTest - Selecionar Veiculos ::</h4>
                </div>
                <div class="modal-body">
                    <center>
                    <asp:GridView ID="gdvVeiculos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="True" OnPageIndexChanging="gdvVeiculos_PageIndexChanging" OnRowDataBound="gdvVeiculos_RowDataBound" OnRowEditing="gdvVeiculos_RowEditing">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="idVeiculo" HeaderText="Código" SortExpression="codEndereco" />
                            <asp:BoundField DataField="placaVeiculo" HeaderText="Placa" SortExpression="codEndereco" />
                            <asp:BoundField DataField="NomeMarcaVeiculo" HeaderText="Marca" SortExpression="codEndereco" />
                            <asp:BoundField DataField="NomeModeloVeiculo" HeaderText="Modelo" SortExpression="codEndereco" />
                            <asp:BoundField DataField="corVeiculo" HeaderText="Cor" SortExpression="codEndereco" />
                            <asp:BoundField DataField="StatusVeiculo" HeaderText="Status" SortExpression="StatusVeiculo" />                            
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
                        </center>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
