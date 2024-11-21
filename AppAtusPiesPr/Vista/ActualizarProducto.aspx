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
                            placeholder="Ingrese ID del producto" ></asp:TextBox>
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
                            placeholder="Ingrese nombre del producto" ></asp:TextBox>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label for="txtCodigo" class="form-label">Código</label>
                        <div class="input-group">
                         <span class="input-group-text">🔢</span>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" 
                            placeholder="Ingrese código del producto" ></asp:TextBox>
                            </div>
                    </div>

                    <div class="mb-3">
                        <label for="txtStock" class="form-label">Cantidad en Stock</label>
                        <div class="input-group">
                        <span class="input-group-text">📦</span>
                        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" 
                            TextMode="Number" placeholder="Ingrese cantidad" ></asp:TextBox>
                          </div>
                    </div>

                    <div class="mb-3">
                        <label for="txtPrecio" class="form-label">Precio</label>
                        <div class="input-group">
                            <span class="input-group-text">💵</span>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" 
                                TextMode="Number" placeholder="Ingrese precio" ></asp:TextBox>
                        </div>
                    </div><br />
    


                    <div class="mb-3">
                        <label for="txtEstado" class="form-label">Estado</label>
                        <asp:DropDownList ID="txtEstado" runat="server" CssClass="form-select" >
                            <asp:ListItem Value="">Seleccione un estado</asp:ListItem>
                            <asp:ListItem Value="disponible">Disponible</asp:ListItem>
                            <asp:ListItem Value="no disponible">No Disponible</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    


                    <div class="mb-3">
                        <label for="txtTalla" class="form-label">Talla</label>
                        <div class="input-group">
                          <span class="input-group-text">🥾</span>
                        <asp:TextBox ID="txtTalla" runat="server" CssClass="form-control" 
                            placeholder="Ingrese  la talla" ></asp:TextBox>
                            </div>
                    </div>


                    <div class="mb-3">
                        <label for="inRuta" class="form-label">Añade una imagen</label><br />
                        <asp:FileUpload ID="inRuta" runat="server"  />
                        
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
