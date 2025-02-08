﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="carritoCompras.aspx.cs" Inherits="AppAtusPiesPr.Vista.carritoCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP</title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="recursos/ATP.png" />
    <link rel="stylesheet" type="text/css" href="css/main.css" />
    <link rel='stylesheet' type='text/css' media='screen' href='css/carrito.css' />
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

    <div class="container">
        <!-- Botón para vaciar el carrito -->
        <button id="vaciarCarritoButton" class="delete-carrito">Vaciar Carrito</button>
        <br>
        <br>

        <!-- Contenedor del carrito -->
        <div id="carritoContainer"></div>

        <!-- Botón para abrir la modal de realizar el pedido -->
        <button onclick="mostrarModal()">Hacer Pedido</button>
    </div>

   <!-- Modal para completar el pedido -->
<div id="modalPedido" style="display:none;">
    <h2>Realizar Pedido</h2>
    <!-- Aquí no utilizamos un formulario, solo los campos -->
    <label for="carrera">Carrera:</label>
    <input type="text" id="carrera" name="carrera" required><br><br>

    <label for="ciudad">Ciudad:</label>
    <select id="ciudad" name="ciudad" required>
        <option value="Tunja">Tunja</option>
        <option value="Sogamoso">Sogamoso</option>
        <option value="Duitama">Duitama</option>
    </select><br><br>

    <label for="direccionPrincipal">Dirección Principal:</label>
    <select id="direccionPrincipal" name="direccionPrincipal" required>
        <option value="Sí">Sí</option>
        <option value="No">No</option>
    </select><br><br>

    <button type="button" onclick="realizarPedido()">Hacer Pedido</button>
    <button type="button" onclick="ocultarModal()">Cancelar</button>
</div>

<script>
    // Obtener idCliente desde la sesión
    var idCliente = '<%= Session["idUsuario"] %>';
    console.log("ID Cliente desde la sesión:", idCliente);

    if (!idCliente || idCliente === "null" || idCliente === "undefined") {
        console.error("No se pudo obtener el idCliente.");
        alert("No se pudo obtener el idCliente. Asegúrate de haber iniciado sesión.");
    }

    document.addEventListener("DOMContentLoaded", function () {
        console.log("Mostrando modal de pedido...");
        document.getElementById("modalPedido").style.display = "block";
    });

    // Función para ocultar el modal
    function ocultarModal() {
        console.log("Ocultando modal de pedido...");
        document.getElementById("modalPedido").style.display = "none";
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
        return direccionPrincipal === "Sí"; // Convertimos "Sí" a true y cualquier otro valor a false
    }


    function realizarPedido() {
        console.log("Intentando realizar pedido...");

        if (!idCliente || idCliente === "null" || idCliente === "undefined") {
            console.error("No se pudo obtener el idCliente.");
            alert("No se pudo obtener el idCliente. Asegúrate de haber iniciado sesión.");
            return;
        }

        // Obtener el carrito y los vendedores únicos
        const carrito = JSON.parse(localStorage.getItem("carrito")) || [];
        console.log("Carrito recuperado:", carrito);

        if (carrito.length === 0) {
            console.warn("El carrito está vacío. No se puede procesar el pedido.");
            alert("Tu carrito está vacío. Agrega productos antes de hacer el pedido.");
            return;
        }

        const vendedores = [...new Set(carrito.map(producto => producto.idVendedor))];
        console.log("Vendedores únicos en el carrito:", vendedores);

        // Crear los pedidos por cada vendedor
        vendedores.forEach(idVendedor => {
            console.log(`Procesando pedido para el vendedor: ${idVendedor}`);

            const productosPorVendedor = carrito.filter(producto => producto.idVendedor === idVendedor);
            console.log("Productos del vendedor:", productosPorVendedor);

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

            // Mostrar el pedido en la consola para depuración
            console.log("Pedido enviado:", JSON.stringify(pedido));

            // Enviar el pedido al backend
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
                        console.log("Pedido guardado correctamente con ID:", data.message);
                    } else {
                        console.error("Error al guardar el pedido:", data.message);
                        alert("Hubo un problema al realizar la compra: " + data.message);
                    }
                })
                .catch(error => {
                    console.error("Error en la petición fetch:", error);
                    alert("Hubo un error al procesar tu compra. Intenta nuevamente.");
                });
        });
    }
</script>

    <!-- Mensaje para inicio de sesión -->
    <div id="mensajeInicioSesion" style="display: none; text-align: center; color: red; margin-top: 20px;">
        Debes iniciar sesión para proceder con la compra.
    </div>

    <script src="js/carrito.js"></script>
    <script src="https://www.paypal.com/sdk/js?client-id=AcJ4hvEnBv9MHxg3EggwPHF7XO7mdTT_G3N0wmqj0vieh-UOmSYC02lUOH_gAVONfGFwLvuWiGmIK1LZ&currency=USD"></script>

    <div class="pie-pagina">
        <br />
        <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
        <p>Diseñado con amor para brindar estilo y comodidad.</p>
        <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
        <br />
    </div>
</asp:Content>
