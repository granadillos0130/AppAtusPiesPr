<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="AdministrarProveedores.aspx.cs" Inherits="AppAtusPiesPr.Vista.AdministrarProveedores" %>


<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <title>Administrar Proveedores</title>
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        .modal-header {
            padding: 15px;
            color: #fff;
        }

        .modal-content {
            border-radius: 15px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }

        .form-group {
            margin-bottom: 20px;
        }

        .btn-primary {
            background-color: #4e73df;
            border-color: #4e73df;
        }

        .btn-secondary {
            background-color: #f6c23e;
            border-color: #f6c23e;
        }





        
        .table {
            border: none;
            border-collapse: collapse;
            width: 100%;
            background-color: #ffffff;
            margin: 0 auto;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

            .table thead th {
                background-color: #333333; 
                color: #ffffff;
                font-weight: bold;
                text-align: left;
                padding: 15px;
                border-bottom: 2px solid #e7e7e7;
                text-transform: uppercase;
            }

            .table tbody tr {
                border-bottom: 1px solid #e7e7e7;
                transition: all 0.3s ease;
            }

                .table tbody tr:hover {
                    background-color: #f9f9f9; 
                }

            .table tbody td {
                padding: 15px;
                color: #555555;
                font-size: 14px;
                text-align: left;
            }

        
        .btn {
            border-radius: 5px;
            padding: 8px 16px;
            font-size: 14px;
            text-transform: uppercase;
            transition: all 0.3s ease;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
        }

       
        .btn-success {
            background-color: #5cb85c;
            color: #ffffff;
            border: none;
        }

            .btn-success:hover {
                background-color: #4cae4c;
                transform: scale(1.05); 
            }

        
        .btn-primary {
            background-color: #007bff;
            color: #ffffff;
            border: none;
        }

            .btn-primary:hover {
                background-color: #0056b3;
                transform: scale(1.05);
            }

      
        .btn-secondary {
            background-color: #6c757d;
            color: #ffffff;
            border: none;
        }

            .btn-secondary:hover {
                background-color: #5a6268;
                transform: scale(1.05);
            }



        .form-control.is-invalid {
            border-color: #dc3545 !important;
            background-color: #f8d7da !important;
        }

        .form-control.is-valid {
            border-color: #28a745 !important;
            background-color: #d4edda !important;
        }

        .text-danger {
            font-size: 0.9rem;
            color: #dc3545;
        }
    </style>
</asp:Content>


<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container mt-5">
        <h2 class="text-center font-weight-bold mb-4" style="color: #333333;">Administrar Proveedores</h2>

        
        <div class="text-right mb-3">
            <button type="button" class="btn btn-success" data-toggle="modal" data-target="#modalRegistrarProveedor">
                Registrar Proveedor
       
            </button>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Documento</th>
                            <th>Nombre</th>
                            <th>Email</th>
                            <th>Teléfono</th>
                            <th>Editar</th>
                            <th>Eliminar</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptProveedores" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("Documento") %></td>
                                    <td><%# Eval("Nombres") %></td>
                                    <td><%# Eval("Email") %></td>
                                    <td><%# Eval("Telefono") %></td>
                                    <td>
                                       
                                        <button type="button" class="btn btn-primary btn-sm"
                                            onclick="AbrirModalEditar('<%# Eval("idProveedor") %>')">
                                            Editar
                                   
                                        </button>
                                    </td>
                                    <td>
                                        <button type="button" class="btn btn-danger btn-sm" onclick="EliminarProveedor('<%# Eval("idProveedor") %>')">
                                            Eliminar
   
                                        </button>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>




    <!-- Modal para Registrar Proveedor -->
    <div class="modal fade" id="modalRegistrarProveedor" tabindex="-1" role="dialog" aria-labelledby="modalRegistrarProveedorLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
            <div class="modal-content" style="border-radius: 15px; overflow: hidden;">
                <div class="modal-header" style="background: linear-gradient(90deg, #4e73df, #1cc88a); color: white;">
                    <h5 class="modal-title" id="modalRegistrarProveedorLabel" style="font-weight: bold;">Registrar Proveedor</h5>
                    <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>



                <div class="modal-body">
                    <div class="form-group">
                        <label for="txtDocumento">Documento</label>
                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Ingrese el documento"></asp:TextBox>
                        <asp:Label ID="lblDocumentoError" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="txtNombres">Nombre</label>
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" placeholder="Ingrese el nombre"></asp:TextBox>
                        <asp:Label ID="lblNombresError" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="txtEmail">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingrese el email"></asp:TextBox>
                        <asp:Label ID="lblEmailError" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="txtTelefono">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Ingrese el teléfono"></asp:TextBox>
                        <asp:Label ID="lblTelefonoError" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                </div>

                <div class="modal-footer" style="background-color: #f8f9fc;">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnGuardarProveedor" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarProveedor_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal para Editar Proveedor -->
    <div class="modal fade" id="modalEditarProveedor" tabindex="-1" role="dialog" aria-labelledby="modalEditarProveedorLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
            <div class="modal-content" style="border-radius: 15px; overflow: hidden;">
                <div class="modal-header" style="background: linear-gradient(90deg, #4e73df, #1cc88a); color: white;">
                    <h5 class="modal-title" id="modalEditarProveedorLabel" style="font-weight: bold;">Editar Proveedor</h5>
                    <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="background-color: #f8f9fc; padding: 20px;">
                   
                    <asp:HiddenField ID="hfIdProveedor" runat="server" />

                    
                    <div class="form-group">
                        <label for="txtDocumentoEditar" class="font-weight-bold">Documento</label>
                        <asp:TextBox ID="txtDocumentoEditar" runat="server" CssClass="form-control" placeholder="Documento del Proveedor"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtNombresEditar" class="font-weight-bold">Nombre</label>
                        <asp:TextBox ID="txtNombresEditar" runat="server" CssClass="form-control" placeholder="Nombre del Proveedor"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtEmailEditar" class="font-weight-bold">Email</label>
                        <asp:TextBox ID="txtEmailEditar" runat="server" CssClass="form-control" placeholder="Email del Proveedor"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtTelefonoEditar" class="font-weight-bold">Teléfono</label>
                        <asp:TextBox ID="txtTelefonoEditar" runat="server" CssClass="form-control" placeholder="Teléfono del Proveedor"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer" style="background-color: #f8f9fc;">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnActualizarProveedor" runat="server" CssClass="btn btn-primary" Text="Actualizar" OnClick="btnActualizarProveedor_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function EliminarProveedor(idProveedor) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: "Esta acción eliminará el proveedor de manera permanente.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {

                    $.ajax({
                        type: "POST",
                        url: "AdministrarProveedores.aspx/EliminarProveedor",
                        data: JSON.stringify({ idProveedor: idProveedor }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            Swal.fire('Eliminado', response.d, 'success');


                            __doPostBack('<%= UpdatePanel1.ClientID %>', '');
                        },
                        error: function () {
                            Swal.fire('Error', 'No se pudo eliminar el proveedor.', 'error');
                        }
                    });
                }
            });
        }

        $(document).ready(function () {

            $("#<%= txtDocumento.ClientID %>").on("input", function () {
                if ($(this).val().trim() === "") {
                    $(this).addClass("is-invalid").removeClass("is-valid");
                    $("#<%= lblDocumentoError.ClientID %>").text("Por favor, ingresa el documento.");
               } else if (!/^\d+$/.test($(this).val())) {
                   $(this).addClass("is-invalid").removeClass("is-valid");
                   $("#<%= lblDocumentoError.ClientID %>").text("El documento debe contener solo números.");
               } else {
                   $(this).removeClass("is-invalid").addClass("is-valid");
                   $("#<%= lblDocumentoError.ClientID %>").text("");
                }
            });


            $("#<%= txtNombres.ClientID %>").on("input", function () {
                if ($(this).val().trim() === "") {
                    $(this).addClass("is-invalid").removeClass("is-valid");
                    $("#<%= lblNombresError.ClientID %>").text("Por favor, ingresa el nombre.");
               } else {
                   $(this).removeClass("is-invalid").addClass("is-valid");
                   $("#<%= lblNombresError.ClientID %>").text("");
               }
           });


            $("#<%= txtEmail.ClientID %>").on("input", function () {
                if ($(this).val().trim() === "") {
                    $(this).addClass("is-invalid").removeClass("is-valid");
                    $("#<%= lblEmailError.ClientID %>").text("Por favor, ingresa el email.");
        } else if (!$(this).val().includes("@")) {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#<%= lblEmailError.ClientID %>").text("El correo debe contener el carácter '@'.");
        } else {
            $(this).removeClass("is-invalid").addClass("is-valid");
            $("#<%= lblEmailError.ClientID %>").text("");
        }
    });

    
    $("#<%= txtTelefono.ClientID %>").on("input", function () {
        if ($(this).val().trim() === "") {
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#<%= lblTelefonoError.ClientID %>").text("Por favor, ingresa el teléfono.");
        } else if (!/^\d+$/.test($(this).val())) { 
            $(this).addClass("is-invalid").removeClass("is-valid");
            $("#<%= lblTelefonoError.ClientID %>").text("El teléfono debe contener solo números.");
        } else {
            $(this).removeClass("is-invalid").addClass("is-valid");
            $("#<%= lblTelefonoError.ClientID %>").text("");
        }
    });
       });


        function LimpiarCamposModal() {
            
            $("#<%= txtDocumento.ClientID %>").val("").removeClass("is-valid is-invalid");
            $("#<%= txtNombres.ClientID %>").val("").removeClass("is-valid is-invalid");
            $("#<%= txtEmail.ClientID %>").val("").removeClass("is-valid is-invalid");
            $("#<%= txtTelefono.ClientID %>").val("").removeClass("is-valid is-invalid");

           
            $("#<%= lblDocumentoError.ClientID %>").text("");
            $("#<%= lblNombresError.ClientID %>").text("");
            $("#<%= lblEmailError.ClientID %>").text("");
            $("#<%= lblTelefonoError.ClientID %>").text("");

            
            $('#modalRegistrarProveedor').modal('hide');
        }

        function AbrirModalEditar(idProveedor) {
            $.ajax({
                type: "POST",
                url: "AdministrarProveedores.aspx/CargarProveedor",
                data: JSON.stringify({ idProveedor: idProveedor }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var proveedor = response.d;

                    $("#<%= hfIdProveedor.ClientID %>").val(proveedor.idProveedor);
                    $("#<%= txtDocumentoEditar.ClientID %>").val(proveedor.Documento);
                    $("#<%= txtNombresEditar.ClientID %>").val(proveedor.Nombres);
                    $("#<%= txtEmailEditar.ClientID %>").val(proveedor.Email);
                    $("#<%= txtTelefonoEditar.ClientID %>").val(proveedor.Telefono);

                    $("#modalEditarProveedor").modal("show");
                },
                error: function () {
                    Swal.fire("Error", "No se pudo cargar la información del proveedor.", "error");
                }
            });
        }
    </script>
</asp:Content>
