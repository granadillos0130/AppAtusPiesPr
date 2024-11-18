<%@ Page Title="Registrar Vendedor" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="RegistrarVendedor.aspx.cs" Inherits="AppAtusPiesPr.Vista.RegistrarVendedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Registrar Vendedor</title>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <asp:Panel ID="pnlRegistrarVendedor" runat="server" CssClass="card shadow">
                    <div class="card-header text-center bg-primary text-white">
                        <h2>Registrar Vendedor</h2>
                    </div>
                    <div class="card-body">
                        <form id="formRegistrar" runat="server">
                            <div class="mb-3">
                                <label for="txtNombres" class="form-label">Nombres</label>
                                <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" />
                            </div>
                            <div class="mb-3">
                                <label for="txtApellidos" class="form-label">Apellidos</label>
                                <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" />
                            </div>
                            <div class="mb-3">
                                <label for="txtDocumento" class="form-label">Documento</label>
                                <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
                            </div>
                            <div class="mb-3">
                                <label for="txtCorreo" class="form-label">Correo Electrónico</label>
                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" />
                            </div>
                            <div class="mb-3">
                                <label for="txtPassword" class="form-label">Contraseña</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                            </div>
                            <div class="mb-3">
                                <label for="txtTelefono" class="form-label">Teléfono</label>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                            </div>
                            <div class="mb-3">
                                <label for="txtDireccion" class="form-label">Dirección</label>
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
                            </div>
                            <div class="mb-3">
                                <label for="txtDescripcion" class="form-label">Descripción</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                            </div>
                            <div class="d-grid">
                                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Vendedor" CssClass="btn btn-primary btn-block" OnClick="btnRegistrar_Click" />
                            </div>
                        </form>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
