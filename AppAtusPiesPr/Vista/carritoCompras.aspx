﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="carritoCompras.aspx.cs" Inherits="AppAtusPiesPr.Vista.carritoCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP</title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="recursos/ATP.png" />
    <link rel="stylesheet" type="text/css" href="css/main.css" />
    <link rel='stylesheet' type='text/css' media='screen' href='css/carrito.css' />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css">
    <!-- Incluye SweetAlert2 CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <!-- Incluye SweetAlert2 JS -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://www.paypal.com/sdk/js?client-id=AXy1H95uxd5X2CWmEyZntFr2U3Jbu4LNRuizXbae7MSlnwF5LTqGaHTHE8LqTsJUGpBqGcgkwHmwbSOw&currency=USD" data-sdk-integration-source="button-factory"></script>

    <style>
        .order-button {
            position: relative;
            float: right;
            margin-top: 15px;
            margin-right: 10%;
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
    <center>
        <div class="navbarFiltros">
            <nav>

                <ul class="menuFiltros">
                    <asp:Repeater ID="RepeaterMarca" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href='<%# "moduloMarcaFiltrada.aspx?id=" + Eval("idMarca") %>'>
                                    <%# Eval("nombreMarca") %>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </nav>
        </div>
    </center>

    <div class="container">
        <!-- Botón para vaciar el carrito -->
<button id="vaciarCarritoButton" class="delete-carrito">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="16" height="16" fill="currentColor">
        <path d="M3 6v18h18v-18h-18zm5 14c0 .552-.448 1-1 1s-1-.448-1-1v-10c0-.552.448-1 1-1s1 .448 1 1v10zm5 0c0 .552-.448 1-1 1s-1-.448-1-1v-10c0-.552.448-1 1-1s1 .448 1 1v10zm5 0c0 .552-.448 1-1 1s-1-.448-1-1v-10c0-.552.448-1 1-1s1 .448 1 1v10zm4-18v2h-20v-2h5.711c.9 0 1.631-1.099 1.631-2h5.315c0 .901.73 2 1.631 2h5.712z"/>
    </svg>
</button>
        
        <br>
        <br>

        <!-- Contenedor del carrito -->
        <div id="carritoContainer"></div>

        <!-- Botón para abrir la modal de realizar el pedido -->

        <% if (Session["idUsuario"] != null)
            { %>
        <button class="btn btn-success hacer-pedido" data-bs-toggle="modal" data-bs-target="#modalPedido"> <i class="fas fa-shopping-cart"></i>
            Hacer Pedido
        </button>
        <% } %>
    </div>

    <!-- Modal Bootstrap para completar el pedido -->
    <div class="modal fade" id="modalPedido" tabindex="-1" aria-labelledby="modalPedidoLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalPedidoLabel">Realizar Pedido</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="carrera" class="form-label">Carrera:</label>
                        <input type="text" class="form-control" id="carrera" name="carrera" required>
                    </div>

                    <div class="mb-3">
                        <label for="ciudad" class="form-label">Ciudad:</label>
                        <select class="form-select" id="ciudad" name="ciudad" required>
                            <option value="Tunja">Tunja</option>
                            <option value="Sogamoso">Sogamoso</option>
                            <option value="Duitama">Duitama</option>
                        </select>
                    </div>

                    <div class="mb-3">
                        <label for="direccionPrincipal" class="form-label">Dirección Principal:</label>
                        <select class="form-select" id="direccionPrincipal" name="direccionPrincipal" required>
                            <option value="Sí">Sí</option>
                            <option value="No">No</option>
                        </select>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-success" onclick="realizarPedido()">Hacer Pedido</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Mensaje para inicio de sesión -->
    <div id="mensajeInicioSesion" style="display: none;" class="alert alert-danger text-center mt-3">
        Debes iniciar sesión para proceder con la compra.
    </div>



    <!-- Scripts -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>
    <script src="js/carrito.js"></script>
    <script src="https://www.paypal.com/sdk/js?client-id=AcJ4hvEnBv9MHxg3EggwPHF7XO7mdTT_G3N0wmqj0vieh-UOmSYC02lUOH_gAVONfGFwLvuWiGmIK1LZ&currency=USD"></script>

    <script>
        let pedidosGuardados = [];
let montosPedidos = [];
let vendedoresPedidos = [];

        // Obtener idCliente desde la sesión
        var idCliente = '<%= Session["idUsuario"] %>';
        console.log("ID Cliente desde la sesión:", idCliente);

        if (!idCliente || idCliente === "null" || idCliente === "undefined") {
            console.error("No se pudo obtener el idCliente.");
        }

        // Función para obtener la dirección del formulario
        function obtenerDireccion() {
            const direccion = document.getElementById("carrera").value;
            console.log("Dirección obtenida desde el formulario:", direccion);
            return direccion;
        }

        // Función para obtener la ciudad del formulario
        function obtenerCiudad() {
            const ciudad = document.getElementById("ciudad").value;
            console.log("Ciudad obtenida desde el formulario:", ciudad);
            return ciudad;
        }

        // Función para obtener si la dirección es principal desde el formulario
        function obtenerDireccionPrincipal() {
            const direccionPrincipal = document.getElementById("direccionPrincipal").value;
            return direccionPrincipal === "Sí";
        }

        function realizarPedido() {
            console.log("Intentando realizar pedido...");

            if (!idCliente || idCliente === "null" || idCliente === "undefined") {
                console.error("No se pudo obtener el idCliente.");
                alert("No se pudo obtener el idCliente. Asegúrate de haber iniciado sesión.");
                return;
            }

            const carrito = JSON.parse(localStorage.getItem("carrito")) || [];
            console.log("Carrito recuperado:", carrito);

            if (carrito.length === 0) {
                console.warn("El carrito está vacío. No se puede procesar el pedido.");
                alert("Tu carrito está vacío. Agrega productos antes de hacer el pedido.");
                return;
            }

            const vendedores = [...new Set(carrito.map(producto => producto.idVendedor))];
            console.log("Vendedores únicos en el carrito:", vendedores);

            vendedores.forEach(idVendedor => {
                console.log(`Procesando pedido para el vendedor: ${idVendedor}`);

                const productosPorVendedor = carrito.filter(producto => producto.idVendedor === idVendedor);
                console.log("Productos del vendedor:", productosPorVendedor);
                2
                const totalVendedor = productosPorVendedor.reduce((acc, producto) => {
                    const subtotal = (parseFloat(producto.precio) || 0) * (producto.cantidad || 1);
                    console.log(`Subtotal calculado para producto ${producto.idProducto}:`, subtotal);
                    return acc + subtotal;
                }, 0);
                console.log(`Total calculado para vendedor ${idVendedor}:`, totalVendedor);

                const pedido = {
                    IdCliente: idCliente,
                    FechaPedido: new Date().toISOString(),
                    Estado: "Pendiente",
                    TotalPedido: totalVendedor,
                    IdVendedor: idVendedor,
                    Detalles: productosPorVendedor.map(producto => ({
                        IdProducto: producto.idProducto,
                        Cantidad: producto.cantidad,
                        Precio: producto.precio,
                        Direccion: obtenerDireccion(),
                        Ciudad: obtenerCiudad(),
                        DireccionPrincipal: obtenerDireccionPrincipal()
                    }))
                };

                console.log("Pedido enviado:", JSON.stringify(pedido));

                fetch("carritoCompras.aspx/GuardarPedido", {
                    method: "POST",
                    body: JSON.stringify({ pedido }),
                    headers: { "Content-Type": "application/json" }
                })
                    .then(response => {
                        console.log("Respuesta recibida del servidor, esperando JSON...");
                        return response.json();
                    })
                    .then(data => {
                        console.log("Respuesta procesada del servidor:", data);
                        if (data.success) {
                            alert("Compra realizada con éxito!");
                            // Cerrar el modal después de una compra exitosa
                            const modal = bootstrap.Modal.getInstance(document.getElementById('modalPedido'));
                            modal.hide();
                            console.log("Pedido guardado correctamente con ID:", data.message);
                        } else {
                            Swal.fire({
                                title: '¡Pedido agregado!',
                                text: 'Completa el pedido con la transacción',
                                i1con: 'success',
                                confirmButtonText: 'Continuar'
                            });
                        }
                    })
                    .catch(error => {
                        console.error("Error en la petición fetch:", error);
                        alert("Hubo un error al procesar tu compra. Intenta nuevamente.");
                    });
               fetch("carritoCompras.aspx/GuardarPedido", {
    method: "POST",
    body: JSON.stringify({ pedido }),
    headers: { "Content-Type": "application/json" }
})
.then(response => response.json())
.then(data => {
    console.log("🔎 Respuesta completa del servidor:", data);

    if (data.d && data.d.success) {
        console.log("✅ Pedido guardado con ID:", data.d.message);

        const idPedido = data.d.message.match(/\d+/)[0]; // Extraer el ID del pedido
        console.log("📦 ID del pedido extraído:", idPedido);

        // Obtener el monto y el ID del vendedor del pedido actual
        const monto = pedido.TotalPedido;
        const idVendedor = pedido.IdVendedor;

        // Guardar en las variables globales
        pedidosGuardados.push(idPedido);
        montosPedidos.push(monto);
        vendedoresPedidos.push(idVendedor);

        console.log("📋 Lista de pedidos actualizada:", pedidosGuardados);
        console.log("💲 Lista de montos actualizada:", montosPedidos);
        console.log("👨‍💼 Lista de vendedores actualizada:", vendedoresPedidos);

        alert("Compra realizada con éxito!");

        // Cerrar la modal
        const modal = bootstrap.Modal.getInstance(document.getElementById('modalPedido'));
        modal.hide();
    } else {
        console.error("❌ Error en la respuesta del servidor:", data.d ? data.d.message : "Respuesta inesperada.");
        alert("Hubo un problema al realizar la compra: " + (data.d ? data.d.message : "Respuesta inesperada."));
    }
})
.catch(error => {
    console.error("⚠ Error en la petición fetch:", error);
    alert("Hubo un error al procesar tu compra. Intenta nuevamente.");
});


            });
        }
    </script>
    <style>
        .login-message {
            color: red;
        }

        .hacer-pedido {
            display: block;
            margin-left: auto;
            padding: 10px 20px;
            font-size: 16px;
            border-radius: 5px;
            background-color: #198754 !important;
            border: none;
            color: white;
            cursor: pointer;
            transition: background-color 0.3s ease;
            margin-right: 2%;
            margin-top: 15px;
        }

            .hacer-pedido:hover {
                background-color: #146c43 !important;
            }
    </style>
    <div class="pie-pagina">
        <br />
        <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
        <p>Diseñado con amor para brindar estilo y comodidad.</p>
        <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
        <br />
    </div>
</asp:Content>
