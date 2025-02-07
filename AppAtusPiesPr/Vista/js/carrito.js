﻿

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

    function realizarCompra() {
        obtenerIdCliente()
            .then(idCliente => {
                const pedido = {
                    IdCliente: idCliente, // Ahora tienes el ID real del cliente
                    FechaPedido: new Date().toISOString(),
                    Estado: "Pendiente",
                    TotalPedido: calcularTotalPedido(),
                    IdVendedor: 2, // ID del vendedor, si aplica
                };

                const detalles = obtenerDetallesPedido();

                if (!pedido.IdCliente || !pedido.TotalPedido || detalles.length === 0) {
                    alert("Por favor, completa los campos del pedido.");
                    return;
                }

                const datos = { pedido, detalles };

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


    // Función para calcular el total del pedido (solo un ejemplo)
    function calcularTotalPedido() {
        // Lógica para calcular el total (sumar precios de productos y cantidades)
        let total = 0;
        const productos = obtenerDetallesPedido(); // Obtén los detalles del carrito
        productos.forEach(item => {
            total += item.Precio * item.Cantidad;
        });
        return total;
    }

    // Función para obtener los detalles del carrito (deberás adaptarla a tu implementación)
    function obtenerDetallesPedido() {
        const carrito = [
            {
                IdProducto: 1,
                Cantidad: 2,
                Precio: 50.00,
                Direccion: "Calle Ficticia 123",
                Ciudad: "Ciudad Ejemplo",
                DireccionPrincipal: true
            },
            // Aquí agregarías más productos del carrito
        ];

        return carrito;
    }


    function agregarProductoAlCarrito(producto) {
        let carrito = JSON.parse(localStorage.getItem('carrito')) || [];

        // Verifica si el producto ya existe en el carrito
        const productoExistente = carrito.find(p => p.id === producto.id);
        if (productoExistente) {
            productoExistente.cantidad += 1; // Incrementa la cantidad si ya existe
        } else {
            carrito.push(producto); // Agrega un nuevo producto
        }

        // Guarda el carrito actualizado en localStorage
        localStorage.setItem('carrito', JSON.stringify(carrito));

        // Opcional: muestra un mensaje de éxito
        Swal.fire({
            icon: 'success', // Ícono de éxito
            title: '¡Producto agregado!', // Título del alert
            showConfirmButton: false, // Oculta el botón de confirmación
            timer: 1500, // Cierra automáticamente después de 1.5 segundos
            timerProgressBar: true, // Muestra una barra de progreso
            position: 'bottom-end', // Posición del alert (esquina inferior derecha)
            toast: true, // Muestra el alert como un toast
            background: '#4CAF50', // Color de fondo
            color: '#fff', // Color del texto
            iconColor: '#fff', // Color del ícono
        });
    }

    // Captura el evento de clic en los botones "Guardar"
    document.querySelectorAll('.save-button').forEach(button => {
        button.addEventListener('click', (event) => {
            event.preventDefault();

            // Extrae los datos del producto desde los atributos del botón
            const producto = {
                id: button.getAttribute('data-id'),
                nombreProducto: button.getAttribute('data-nombre'),
                imagen: button.getAttribute('data-imagen'),
                precio: parseFloat(button.getAttribute('data-precio')),
                idVendedor: button.getAttribute('data-idvendedor'),
                NombreVendedor: button.getAttribute('data-vendedor'),
                apellidos: button.getAttribute('data-apellidos'),
                cantidad: 1 // Cantidad inicial
            };

            // Llama a la función para agregar el producto al carrito
            agregarProductoAlCarrito(producto);
        });
    });


    // Inicializar carrito
    mostrarCarrito();
});
