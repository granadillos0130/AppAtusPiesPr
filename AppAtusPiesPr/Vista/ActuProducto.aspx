<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ActuProducto.aspx.cs" Inherits="AppAtusPiesPr.Vista.ActuProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <!-- Tarjeta de encabezado -->
                <div class="card shadow-sm mb-4">
                    <div class="card-header text-center bg-primary text-white py-3">
                        <h2 class="mb-0">Actualizar Producto</h2>
                    </div>
                    <div class="card-body">
                        
                        <!-- Selección del producto -->
                        <div class="mb-4">
                            <h3 class="h4 mb-3">Seleccione un Producto</h3>
                            <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control form-control-lg"></asp:DropDownList>
                        </div>
                        
                        <!-- Información básica -->
                        <div class="mb-4">
                            <label for="txtNombre" class="form-label fw-bold">Nombre del Producto</label>
                            <div class="input-group">
                                <span class="input-group-text">🏷️</span>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese nombre del producto"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="row mb-4">
                            <div class="col-md-6">
                                <label for="txtStock" class="form-label fw-bold">Cantidad en Stock</label>
                                <div class="input-group">
                                    <span class="input-group-text">📦</span>
                                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number" placeholder="Ingrese cantidad"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label for="txtPrecio" class="form-label fw-bold">Precio</label>
                                <div class="input-group">
                                    <span class="input-group-text">💲</span>
                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        
                        <!-- Descripción -->
                        <div class="mb-4">
                            <label for="txtDescripcionProduc" class="form-label fw-bold">Descripción del Producto</label>
                            <div class="input-group">
                                <span class="input-group-text">✍️</span>
                                <asp:TextBox ID="txtDescripcionProduc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                            </div>
                        </div>
                        
                        <!-- Referencia y Estado -->
                        <div class="row mb-4">
                            <div class="col-md-6">
                                <label for="txtReferencia" class="form-label fw-bold">Referencia</label>
                                <div class="input-group">
                                    <span class="input-group-text">🔢</span>
                                    <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label for="txtEstado" class="form-label fw-bold">Estado</label>
                                <asp:DropDownList ID="txtEstado" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="disponible">Disponible</asp:ListItem>
                                    <asp:ListItem Value="no disponible">No Disponible</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <!-- Marca y Categoría -->
                        <div class="row mb-4">
                            <div class="col-md-6">
                                <label for="txtMarca" class="form-label fw-bold">Marca</label>
                                <asp:DropDownList ID="txtMarca" runat="server" CssClass="form-control">
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
                            <div class="col-md-6">
                                <label for="ddlCategoria" class="form-label">Categoría</label>
                                <div class="input-group">
                                    <span class="input-group-text">👞</span>
                                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Descuento e Imagen -->
                        <div class="row mb-4">
                            <div class="col-md-6">
                                <label for="txtDescuento" class="form-label fw-bold">Descuento</label>
                                <div class="input-group">
                                    <span class="input-group-text">🔖</span>
                                    <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label for="inRuta" class="form-label fw-bold">Añade una imagen</label>
                                <asp:FileUpload ID="inRuta" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        
                        <!-- Botón de actualización -->
                        <div class="card-footer text-center bg-light py-3">
                            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Producto" CssClass="btn btn-primary btn-lg px-5" OnClick="btnActualizar_Click1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
