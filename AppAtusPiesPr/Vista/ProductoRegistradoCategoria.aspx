﻿<%@ Page Title="Productos por Categoría" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ProductoRegistradoCategoria.aspx.cs" Inherits="AppAtusPiesPr.Vista.ProductoRegistradoCategoria" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

     <script type="text/javascript">
         function confirmDelete(button) {

             //Obtiene el idProducto por medio del CommandArgument
            var productId = button.getAttribute("CommandArgument");
            event.preventDefault();
            Swal.fire({
                title: '¿Estás seguro?',
                text: "¡No podrás revertir esta acción!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Encuentra el botón y activa su evento de clic
                    __doPostBack(button.name, '');
                }
            });
            return false;
        }
    </script>


    <div class="container" style="margin-top:0"><br />

    
    <h2 class="text-left mb-6">Productos Registrados por Categoría</h2>
    
    
    <div class="row mb-4">
        <div class="col-md-4">
    
     <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged" CssClass="form-control" class="form-label">
        <asp:ListItem Text="Todas las categorías" Value="0" />
 
    </asp:DropDownList>
            </div>
        </div>

        <div class="table-responsive">

    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped mt-3"  OnRowCommand="gvProductos_RowCommand">
        <Columns>
            <asp:BoundField DataField="idProducto" HeaderText="ID Producto" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="CantidadStock" HeaderText="Stock" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" />
            <asp:BoundField DataField="NombreVendedor" HeaderText="Vendedor" />

             <asp:TemplateField HeaderText="Eliminar Producto">
                <ItemTemplate>
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                        CommandName="EliminarProducto" 
                        CommandArgument='<%# Eval("idProducto") %>' 
                        CssClass="btn btn-danger btn-sm" 
                      OnClientClick="return confirmDelete(this);" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
            
        </div>
        </div>
</asp:Content>
