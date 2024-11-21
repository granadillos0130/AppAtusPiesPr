<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ActualizarProducto.aspx.cs" Inherits="AppAtusPiesPr.Vista.ActualizarProducto" %>


<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">

    <link href="css/FormRegistro.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>


</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <div class="container mt-4">
        <form runat="server" class="needs-validation" novalidate>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <div class="card">
                <div class="card-header bg-primary text-white">
                    <h3 class="mb-0">Actualizar Producto</h3>
                </div>

                <div class="card-body">
                    <div class="mb-3">
                        <label for="txtProducto" class="form-label">ID Producto</label>
                        <div class="input-group">
                            <span class="input-group-text">🆔</span>
                            <asp:TextBox ID="txtProducto" runat="server" CssClass="form-control"
                                placeholder="Ingrese ID del producto"></asp:TextBox>
                            <div class="invalid-feedback">
                                Por favor ingrese el ID del producto
                       
                            </div>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre del Producto</label>
                        <div class="input-group">
                            <span class="input-group-text">🏷️ </span>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"
                                placeholder="Ingrese nombre del producto"></asp:TextBox>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label for="txtStock" class="form-label">Cantidad en Stock</label>
                        <div class="input-group">
                            <span class="input-group-text">📦</span>
                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"
                                TextMode="Number" placeholder="Ingrese cantidad"></asp:TextBox>
                        </div>
                    </div>

                
                    <div class="mb-3">
                        <label for="txtPrecio" class="form-label">Precio</label>
                        <div class="input-group">
                            <span class="input-group-text">💲</span>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                        <br />
                
                    <div class="mb-3">
                        <label for="txtDescripcionProduc" class="form-label">Descripción del Producto</label>
                        <div class="input-group">
                            <span class="input-group-text">✍️</span>
                            <asp:TextBox ID="txtDescripcionProduc" runat="server" CssClass="form-control" TextMode="MultiLine" />
                        </div>
                    </div>


                    <div class="mb-3">
                        <label for="txtReferencia" class="form-label">Referencia</label>
                        <div class="input-group">
                            <span class="input-group-text">🔢</span>
                            <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <br />



                    <div class="mb-3">
                        <label for="txtEstado" class="form-label">Estado</label>
                        <asp:DropDownList ID="txtEstado" runat="server" CssClass="form-select">

                            <asp:ListItem Value="disponible">Disponible</asp:ListItem>
                            <asp:ListItem Value="no disponible">No Disponible</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="mb-3">
                        <label for="txtMarca" class="form-label">Marca</label>
                        <asp:DropDownList ID="txtMarca" runat="server" CssClass="form-select">
                            <asp:ListItem Value="0">Seleccione una Marca</asp:ListItem>
                            <asp:ListItem Value="Nike">Nike</asp:ListItem>
                            <asp:ListItem Value="Adidas">Adidas</asp:ListItem>
                            <asp:ListItem Value="Reebok">Reebok</asp:ListItem>
                            <asp:ListItem Value="Puma">Puma</asp:ListItem>
                            <asp:ListItem Value="New Balance">New Balance</asp:ListItem>
                            <asp:ListItem Value="Under Armour">Under Armour</asp:ListItem>
                            <asp:ListItem Value="Asics">Asics</asp:ListItem>
                            <asp:ListItem Value="Saucony">Saucony</asp:ListItem>
                            <asp:ListItem Value="Hoka">Hoka</asp:ListItem>
                            <asp:ListItem Value="Mizuno">Mizuno</asp:ListItem>
                        </asp:DropDownList>
                    </div>


                    <div class="mb-3">
                        <label for="txtDescuento" class="form-label">Descuento</label>
                        <div class="input-group">
                            <span class="input-group-text">🔖</span>
                            <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="mb-3">
     <label for="txtCategoria" class="form-label">Categoría</label>
     <asp:DropDownList ID="txtCategoria" runat="server" CssClass="form-select">
         <asp:ListItem Value="">Seleccione un Tipo de calzado</asp:ListItem>
         <asp:ListItem Value="deportivo">Calzado deportivo</asp:ListItem>
         <asp:ListItem Value="casual">Calzado casual</asp:ListItem>
         <asp:ListItem Value="tecnico">Calzado técnico</asp:ListItem>
         <asp:ListItem Value="correr">Calzado para correr</asp:ListItem>
         <asp:ListItem Value="crossfit">Calzado para crossfit</asp:ListItem>
     </asp:DropDownList>
 </div>


                    <div class="mb-3">
                        <label for="inRuta" class="form-label">Añade una imagen</label><br />
                        <asp:FileUpload ID="inRuta" runat="server" />

                    </div>
                </div>

                <div class="card-footer text-center">
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Producto"
                        CssClass="btn btn-primary" OnClick="btnActualizar_Click" />
                </div>
            </div>
        </form>
    </div>
</asp:Content>
