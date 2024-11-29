<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="perfilUsuario.aspx.cs" Inherits="AppAtusPiesPr.Vista.perfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP</title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />

    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="recursos/ATP.png" />
    <link rel="stylesheet" type="text/css" href="css/main.css" />

    <style>
        /* Estilos para la modal */
.modalActualizar #myModal {
    display: none;
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 50%;
    max-width: 500px;
    background-color: #fff;
    border-radius: 5px;
    padding: 20px;
    box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
    z-index: 1000;
}

/* Estilos para los campos de texto */
.textField {
    display: block;
    width: 100%;
    margin-bottom: 15px;
    padding: 12px;
    font-size: 14px;
    border: 1px solid #ccc;
    border-radius: 5px;
    background-color: #f9f9f9;
    box-sizing: border-box;
    transition: all 0.3s ease;
}

.textField:focus {
    border-color: #007bff;
    background-color: #fff;
    outline: none;
    box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
}

/* Botón cerrar */
button[onclick*="myModal"] {
    background-color: #dc3545;
    color: #fff;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 14px;
    transition: background-color 0.3s ease;
}

button[onclick*="myModal"]:hover {
    background-color: #c82333;
}


    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <center>

        <div class="navbarFiltros">
            <nav>
                <ul class="menuFiltros">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href='<%# "moduloCatalogoFiltrado.aspx?id=" + Eval("idCategoria") %>'>
                                    <%# Eval("descripcion") %>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </nav>
        </div>

    </center>

    <hr />
    <br />

    <center>
        <h2>Tu Perfil</h2>
        <br>
    </center>

    <div class="alert alert-danger" role="alert" runat="server" visible="false" id="lblMensaje"></div>
    <!-- Foto del Vendedor -->
    <div class="producto-detalle">
        <div class="producto-imagen">
            <asp:Image alt="Imagen 1" ID="ImgVendedor" runat="server" />
        </div>

        <!-- Información del Cliente -->

        <div class="producto-info">
            <ul>
                <li><strong>Documento:</strong>
                    <asp:Label ID="documentoCliente" runat="server"></asp:Label></li>
                <li><strong>Nombres:</strong>
                    <asp:Label ID="nombreCliente" runat="server"></asp:Label>
                    <asp:Label ID="apellidoCliente" runat="server"></asp:Label></li>
                <li><strong>Email:</strong>
                    <asp:Label ID="emailCliente" runat="server"></asp:Label></li>
                <li><strong>Password:</strong>
                    <asp:Label ID="passCliente" runat="server"></asp:Label></li>
                <li><strong>Telefono:</strong>
                    <asp:Label ID="telCliente" runat="server"></asp:Label></li>

                <li><strong>Direccion: </strong>
                    <asp:Label ID="direcCliente" runat="server"></asp:Label></li>
                <li><strong>Estado: </strong>
                    <asp:Label ID="estadoCliente" runat="server"></asp:Label></li>


            </ul>

            <div class="modalActualizar">

                <div style="position: relative;">
                    <!-- Botón para abrir el modal -->
                    <button type="button" class="buy-button" onclick="document.getElementById('myModal').style.display = 'block'">
                        Editar Información
                    </button>

                    <div id="myModal" style="display: none; border-radius: 5px; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); width: 50%; background-color: white; border: 1px solid #ccc; padding: 20px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); z-index: 1000;">
                        <h5>Edita tus Datos Personales</h5>
                        <br>
                        <asp:TextBox CssClass="textField" ID="txtDocumento" runat="server" placeholder="Documento: "></asp:TextBox>

                        <asp:TextBox CssClass="textField" ID="txtNombres" runat="server" placeholder="Nombres: "></asp:TextBox>

                        <asp:TextBox CssClass="textField" ID="txtApellidos" runat="server" placeholder="Apellidos: "></asp:TextBox>

                        <asp:TextBox CssClass="textField" ID="txtEmail" runat="server" placeholder="Email: "></asp:TextBox>

                        <asp:TextBox CssClass="textField" ID="txtPass" runat="server" placeholder="Contraseña: "></asp:TextBox>

                        <asp:TextBox CssClass="textField" ID="txtTelefono" runat="server" placeholder="Telefono: "></asp:TextBox>

                        <asp:TextBox CssClass="textField" ID="txtDireccion" runat="server" placeholder="Dirección: "></asp:TextBox>

                        <button type="button" style="background-color: #dc3545; color: white; padding: 10px 20px; border: none; border-radius: 5px; font-size: 16px; cursor: pointer; transition: all 0.3s ease; display: inline-block; text-align: center; margin-right: 10px;" onclick="document.getElementById('myModal').style.display = 'none'">
                            Cerrar
                        </button>
                        <asp:Button ID="btnGuardar" CssClass="btn-guardar" runat="server" Text="Guardar" style="background-color: #28a745; color: white; padding: 10px 20px; border: none; border-radius: 5px; font-size: 16px; cursor: pointer; transition: all 0.3s ease; display: inline-block; text-align: center;" OnClick="btnGuardar_Click" />
                    </div>
                </div>

            </div>


        </div>




    </div>
    <hr>
    <br />

    <!-- PIE DE PAGINA -->
    <div class="pie-pagina">
        <br />
        <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
        <p>Diseñado con amor para brindar estilo y comodidad.</p>
        <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
        <br />
    </div>
</asp:Content>
