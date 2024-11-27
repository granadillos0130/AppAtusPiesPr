<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ListarVendedor.aspx.cs" Inherits="AppAtusPiesPr.Vista.ListarVendedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
        /* Estilo para la tabla */


        .table th {
            background-color: #007bff;
            color: white;
        }

        .table-striped tbody tr:nth-child(odd) {
            background-color: #f2f2f2;
        }

      

       
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <h2 class="text-center mb-6">Listado de Vendedores</h2>
        <div class="table-responsive">
    <asp:GridView ID="gvVendedores" runat="server" CssClass="table table-striped table-bordered table-sm" 
        AutoGenerateColumns="False" OnRowDataBound="gvVendedores_RowDataBound">
        <Columns>
            <asp:BoundField DataField="idVendedor" HeaderText="idVendedor" SortExpression="idVendedor" ItemStyle-Width="120px" />
            <asp:BoundField DataField="nombres" HeaderText="Nombres" SortExpression="nombres" ItemStyle-Width="180px" />
            <asp:BoundField DataField="apellidos" HeaderText="Apellidos" SortExpression="apellidos" ItemStyle-Width="180px" />
            <asp:BoundField DataField="documento" HeaderText="Documento" SortExpression="documento" ItemStyle-Width="160px" />
            <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" ItemStyle-Height="75" ItemStyle-Width="220px" />
            <asp:BoundField DataField="telefono" HeaderText="Teléfono" SortExpression="telefono" ItemStyle-Width="150px" />
            <asp:BoundField DataField="direccion" HeaderText="Dirección" SortExpression="direccion" ItemStyle-Width="250px" />
            <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="estado" ItemStyle-Width="120px" />
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnInactivar" runat="server" Text="Inactivar" CssClass="btn btn-primary btn-sm"
                        CommandArgument='<%# Eval("idVendedor") %>' OnCommand="btnInactivar_Command" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
    </div>
</asp:Content>
