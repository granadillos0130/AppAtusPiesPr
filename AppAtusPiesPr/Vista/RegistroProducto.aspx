<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="RegistroProducto.aspx.cs" Inherits="AppAtusPiesPr.Vista.RegistroProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/FormRegistro.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <asp:Panel ID="pnlRegistrarVendedor" runat="server" CssClass="card shadow">
                    <div class="card-header text-center bg-primary text-white">
                        <h2>Registrar Nuevo Producto</h2>
                    </div>
                    <div class="card-body">
                        <form runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            
                            <div class="mb-3">
                                <label for="txtNombre" class="form-label">Nombre del Producto</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"  />
                            </div>

                            <div class="mb-3">
                                <label for="txtCodigo" class="form-label">Código del Producto</label>
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
                            </div>

                            <div class="mb-3">
                                <label for="txtStock" class="form-label">Cantidad Stock</label>
                                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" />
                            </div>

                            <div class="mb-3">
                                <label for="txtPrecio" class="form-label">Precio</label>
                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"  />
                            </div>

                            <div class="mb-3">
                                <label for="txtTalla" class="form-label">Talla</label>
                                <asp:TextBox ID="txtTalla" runat="server" CssClass="form-control"  />
                            </div>

                            <div class="mb-3">
                                <label for="txtVendedor" class="form-label">ID Vendedor</label>
                                <asp:TextBox ID="txtVendedor" runat="server" CssClass="form-control"  />
                            </div>

                            <div class="mb-3">
                                <label for="inRuta" class="form-label">Añade una Imagen</label>
                                <br />
                                <asp:FileUpload ID="inRuta" runat="server"  />
                               
                            </div>

                            <div class="d-grid">
                                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary btn-block" OnClick="btnRegistrar_Click" />
                            </div>
                        </form>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>