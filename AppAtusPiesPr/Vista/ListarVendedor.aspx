<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ListarVendedor.aspx.cs" Inherits="AppAtusPiesPr.Vista.ListarVendedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="formVendedores" runat="server">
        <div class="container" style="margin-top:0">
            <h2 class="text-center mb-6">Listado de Vendedores</h2>
            <div class="table-responsive">
                <asp:GridView ID="gvVendedores" runat="server" CssClass="table table-striped table-bordered"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="idVendedor" HeaderText="idVendedor" SortExpression="idVendedor" />
                        <asp:BoundField DataField="nombres" HeaderText="Nombres" SortExpression="nombres" />
                        <asp:BoundField DataField="apellidos" HeaderText="Apellidos" SortExpression="apellidos" />
                        <asp:BoundField DataField="documento" HeaderText="Documento" SortExpression="documento" />
                        <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" SortExpression="telefono" />
                        <asp:BoundField DataField="direccion" HeaderText="Dirección" SortExpression="direccion" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="estado" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>
