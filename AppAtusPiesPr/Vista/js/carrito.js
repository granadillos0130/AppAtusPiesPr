// carrito.js
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
                </div>
            `;
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
                <button class="btn-comprar">Proceder al pago</button>
            </div>
        `;
        carritoContainer.appendChild(resumenCarrito);
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

    // Función para agregar un producto al carrito
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
        alert('Producto agregado al carrito');
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

    // Evento para vaciar carrito
    const btnVaciarCarrito = document.getElementById('vaciarCarritoButton');
    if (btnVaciarCarrito) {
        btnVaciarCarrito.addEventListener('click', () => {
            if (confirm('¿Estás seguro que deseas vaciar el carrito?')) {
                localStorage.removeItem('carrito');
                mostrarCarrito();
            }
        });
    }
});