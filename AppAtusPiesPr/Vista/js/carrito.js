document.addEventListener('DOMContentLoaded', () => {
    function mostrarCarrito() {
        const carritoContainer = document.getElementById('carritoContainer');
        if (!carritoContainer) {
            console.error('No se encontró el contenedor del carrito');
            return;
        }

        const carrito = JSON.parse(localStorage.getItem('carrito')) || [];
        carritoContainer.innerHTML = '';

        if (carrito.length === 0) {
            carritoContainer.innerHTML = `
                <div class="carrito-vacio">
                    <h3>Tu carrito está vacío</h3>
                    <p>Agrega productos para comenzar a comprar</p>
                </div>`;
            return;
        }

        let totalCarrito = 0;

        // Contenedor para todos los productos
        const productosContainer = document.createElement('div');
        productosContainer.className = 'productos-container';

        carrito.forEach(producto => {
            const precioNumerico = parseFloat(producto.precio) || 0;
            const subtotal = precioNumerico * (producto.cantidad || 1);
            totalCarrito += subtotal;

            const productoElement = document.createElement('div');
            productoElement.className = 'producto-carrito';
            productoElement.innerHTML = `
                <div class="producto-imagen">
                    <img src="${producto.imagen}" alt="${producto.nombreProducto}" class="card-image">
                </div>
                <div class="producto-detalles">
                    <h4 class="producto-nombre">${producto.nombreProducto}</h4>
                    <a class="vendedor-link" href="perfilInfoVendedor.aspx?id=${producto.idVendedor}">
                        ${producto.NombreVendedor} ${producto.apellidos}
                    </a>
                    <div class="precio-controles">
                        <div class="precio">
                            <span>Precio unitario:</span>
                            <span class="monto">$${precioNumerico.toFixed(2)}</span>
                        </div>
                        <div class="control-cantidad">
                            <button class="btn-cantidad" onclick="actualizarCantidad('${producto.id}', -1)">-</button>
                            <span class="cantidad">${producto.cantidad || 1}</span>
                            <button class="btn-cantidad" onclick="actualizarCantidad('${producto.id}', 1)">+</button>
                        </div>
                        <div class="subtotal">
                            <span>Subtotal:</span>
                            <span class="monto">$${subtotal.toFixed(2)}</span>
                        </div>
                    </div>
                </div>
                <div class="acciones">
                    <button class="btn-eliminar" onclick="eliminarProducto('${producto.id}')">
                        <img src="recursos/eliminar.png" alt="Eliminar" class="icon-eliminar">
                    </button>
                </div>`;
            productosContainer.appendChild(productoElement);
        });

        // Agregar los productos al contenedor principal
        carritoContainer.appendChild(productosContainer);

        // Agregar el resumen del carrito
        const resumenCarrito = document.createElement('div');
        resumenCarrito.className = 'resumen-carrito';
        resumenCarrito.innerHTML = `
            <div class="resumen-contenido">
                <h3>Resumen del pedido</h3>
                <div class="resumen-detalle">
                    <span>Total productos:</span>
                    <span>${carrito.reduce((acc, curr) => acc + (curr.cantidad || 1), 0)}</span>
                </div>
                <div class="resumen-detalle total">
                    <span>Total a pagar:</span>
                    <span>$${totalCarrito.toFixed(2)}</span>
                </div>
                <div id="mensajeInicioSesion" style="display: none; color: red;">
                    Por favor, inicia sesión para completar tu compra.
                </div>
                <div id="paypal-button-container"></div>
            </div>`;
        carritoContainer.appendChild(resumenCarrito);

        // Llamar a la función para renderizar el botón de PayPal
        renderizarBotonPayPal(totalCarrito);
    }

    // Función para renderizar el botón de PayPal
    function renderizarBotonPayPal(totalCarrito) {
        fetch('carritoCompras.aspx/VerificarSesion', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({}),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Error en la validación de sesión: ${response.statusText}`);
                }
                return response.json();
            })
            .then(data => {
                const mensajeInicioSesion = document.getElementById('mensajeInicioSesion');
                const contenedorPayPal = document.getElementById('paypal-button-container');

                if (data.d) {
                    // Hay sesión activa
                    mensajeInicioSesion.style.display = 'none';
                    contenedorPayPal.style.display = 'block';

                    paypal.Buttons({
                        style: {
                            shape: 'rect',
                            layout: 'vertical',
                            color: 'gold',
                            label: 'paypal',
                        },
                        createOrder: (data, actions) => {
                            return actions.order.create({
                                purchase_units: [{
                                    amount: { value: totalCarrito.toFixed(2) },
                                }],
                            });
                        },
                        onApprove: (data, actions) => {
                            return actions.order.capture().then(details => {
                                alert(`Transacción completada por ${details.payer.name.given_name}`);
                                localStorage.removeItem('carrito');
                                mostrarCarrito();
                            });
                        },
                        onError: (err) => {
                            console.error('Error en PayPal:', err);
                            alert('Hubo un error al procesar tu pago.');
                        },
                    }).render('#paypal-button-container');
                } else {
                    // No hay sesión activa
                    mensajeInicioSesion.style.display = 'block';
                    contenedorPayPal.style.display = 'none';
                }
            })
            .catch(error => {
                console.error('Error al verificar la sesión:', error);
                alert('Hubo un error al verificar tu sesión. Intenta nuevamente.');
            });
    }

    // Función para actualizar la cantidad
    window.actualizarCantidad = function (idProducto, cambio) {
        let carrito = JSON.parse(localStorage.getItem('carrito')) || [];
        const producto = carrito.find(p => p.id === idProducto);
        if (producto) {
            producto.cantidad = Math.max((producto.cantidad || 1) + cambio, 1);
            localStorage.setItem('carrito', JSON.stringify(carrito));
            mostrarCarrito();
        }
    };

    // Función para eliminar producto
    window.eliminarProducto = function (idProducto) {
        let carrito = JSON.parse(localStorage.getItem('carrito')) || [];
        carrito = carrito.filter(p => p.id !== idProducto);
        localStorage.setItem('carrito', JSON.stringify(carrito));
        mostrarCarrito();
    };

    function obtenerIdCliente() {
        return new Promise((resolve, reject) => {
            fetch('carritoCompras.aspx/ObtenerIdCliente', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    if (data.startsWith("Error")) {
                        reject(data);
                    } else {
                        resolve(data); // Retorna el IdCliente
                    }
                })
                .catch(error => reject(error));
        });
    }

    function realizarPedido() {
        obtenerIdCliente()
            .then(idCliente => {
                const carrito = JSON.parse(localStorage.getItem('carrito')) || [];
                const vendedores = [...new Set(carrito.map(producto => producto.idVendedor))];

                // Asumir que un pedido puede tener productos de varios vendedores
                const pedidos = vendedores.map(idVendedor => {
                    const productosPorVendedor = carrito.filter(producto => producto.idVendedor === idVendedor);
                    const totalVendedor = productosPorVendedor.reduce((acc, producto) => acc + (parseFloat(producto.precio) || 0) * (producto.cantidad || 1), 0);
                    return {
                        IdCliente: idCliente,
                        FechaPedido: new Date().toISOString(),
                        Estado: "Pendiente",
                        TotalPedido: totalVendedor,
                        IdVendedor: idVendedor,
                    };
                });

                const detalles = obtenerDetallesPedido();

                if (!idCliente || detalles.length === 0) {
                    alert("Por favor, completa los campos del pedido.");
                    return;
                }

                const datos = { pedidos, detalles };

                fetch('carritoCompras.aspx/GuardarPedido', {
                    method: 'POST',
                    body: JSON.stringify(datos),
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.includes("Pedido guardado exitosamente")) {
                            alert("Compra realizada con éxito!");
                        } else {
                            alert("Hubo un problema al realizar la compra. Intenta nuevamente.");
                        }
                    })
                    .catch(error => {
                        console.error('Error al realizar la compra:', error);
                        alert("Hubo un error al procesar tu compra. Intenta nuevamente.");
                    });
            })
            .catch(error => {
                console.error('Error al obtener el IdCliente:', error);
                alert("Error al obtener tu información. Por favor, inicia sesión.");
            });
    }

    // Función para obtener los detalles del pedido
    function obtenerDetallesPedido() {
        return [
            {
                carrera: document.getElementById('carrera').value,
                ciudad: document.getElementById('ciudad').value,
                direccionPrincipal: document.getElementById('direccionPrincipal').value
            }
        ];
    }

    // Función para calcular el total del pedido (solo un ejemplo)
    function calcularTotalPedido() {
        // Lógica para calcular el total (sumar precios de productos y cantidades)
        let total = 0;
        const carrito = JSON.parse(localStorage.getItem('carrito')) || [];
        carrito.forEach(producto => {
            total += (parseFloat(producto.precio) || 0) * (producto.cantidad || 1);
        });
        return total;
    }

    // Mostrar el carrito al cargar
    mostrarCarrito();
});
