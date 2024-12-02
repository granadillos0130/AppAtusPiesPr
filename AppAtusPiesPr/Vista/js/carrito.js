// Función para agregar un producto al carrito
function agregarAlCarrito(idProducto) {
    let carrito = JSON.parse(localStorage.getItem('carrito')) || [];
    let producto = carrito.find(p => p.id === idProducto);

    if (producto) {
        producto.cantidad += 1;
    } else {
        carrito.push({ id: idProducto, cantidad: 1 });
    }

    localStorage.setItem('carrito', JSON.stringify(carrito));
    alert('Producto agregado al carrito');
    mostrarCarrito(); // Actualizar la vista del carrito
}

// Función para obtener los productos del carrito
function obtenerCarrito() {
    return JSON.parse(localStorage.getItem('carrito')) || [];
}

// Función para mostrar el carrito en la página
function mostrarCarrito() {
    let carrito = obtenerCarrito();
    let carritoContainer = document.getElementById('carritoContainer');
    carritoContainer.innerHTML = '';

    if (carrito.length === 0) {
        carritoContainer.innerHTML = '<p>Carrito vacío</p>';
        return;
    }

    carrito.forEach(producto => {
        obtenerInformacionProducto(producto.id).then(productoInfo => {
            let productoElement = document.createElement('div');
            productoElement.classList.add('card');
            productoElement.innerHTML = `
                <img src="${productoInfo.imagen}" alt="Producto" class="card-image" />
                <h4 class="card-title">${productoInfo.nombreProducto}</h4>
                <div class="card-info">
                    <div class="card-details">
                        <a class="cardseller" href="perfilInfoVendedor.aspx?id=${productoInfo.idVendedor}">
                            ${productoInfo.NombreVendedor} ${productoInfo.apellidos}
                        </a>
                        <div class="cardprice">
                            <p>$${productoInfo.precio}</p><br>
                            <p>Cantidad: ${producto.cantidad}</p>
                        </div>
                        <div class="cardButtons">
                            <a class="buy-button" href="moduloCompra.aspx?id=${productoInfo.idProdctoEmpresa}">Comprar</a>
                            <button class="delete-carrito" data-id="${producto.id}">Eliminar</button>
                        </div>
                    </div>
                </div>
            `;
            carritoContainer.appendChild(productoElement);
        }).catch(error => {
            console.error("Error al obtener la información del producto:", error);
        });
    });
}

// Función para obtener la información completa del producto
function obtenerInformacionProducto(idProducto) {
    return fetch(`https://tu-api.com/productos/${idProducto}`)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Error al obtener el producto (HTTP ${response.status})`);
            }
            return response.json();
        })
        .catch(error => {
            console.error("Error al obtener la información del producto:", error);
            // Devuelve datos predeterminados si ocurre un error
            return {
                idProdctoEmpresa: idProducto,
                imagen: 'https://via.placeholder.com/150', // Imagen predeterminada
                nombreProducto: `Producto ${idProducto}`,
                NombreVendedor: 'Vendedor',
                apellidos: 'Apellido',
                precio: '0', // Precio predeterminado
                idVendedor: '0' // ID predeterminado
            };
        });
}

// Función para eliminar un producto del carrito
function eliminarDelCarrito(idProducto) {
    let carrito = obtenerCarrito(); // Obtenemos el carrito actual
    carrito = carrito.filter(producto => producto.id !== idProducto); // Filtramos el producto que queremos eliminar

    localStorage.setItem('carrito', JSON.stringify(carrito)); // Guardamos el nuevo carrito en localStorage
    alert('Producto eliminado del carrito');
    mostrarCarrito(); // Actualizamos la interfaz
}


// Función para vaciar el carrito completamente
function vaciarCarrito() {
    localStorage.removeItem('carrito'); // Eliminamos el carrito del localStorage
    alert('Carrito vaciado');
    mostrarCarrito(); // Actualizamos la interfaz
}

// Evento para agregar productos al carrito
document.addEventListener('DOMContentLoaded', () => {
    let botonesGuardar = document.querySelectorAll('.save-button, .btn-agregar-carrito');
    botonesGuardar.forEach(boton => {
        boton.addEventListener('click', (e) => {
            e.preventDefault();
            let idProducto = boton.getAttribute('data-id');
            agregarAlCarrito(idProducto);
        });
    });

    // Evento para eliminar productos del carrito
    let carritoContainer = document.getElementById('carritoContainer');
    carritoContainer.addEventListener('click', (e) => {
        if (e.target.classList.contains('delete-carrito')) {
            let idProducto = e.target.getAttribute('data-id');
            eliminarDelCarrito(idProducto);
        }
    });

    // Evento para vaciar el carrito
    let vaciarCarritoButton = document.getElementById('vaciarCarritoButton');
    vaciarCarritoButton.addEventListener('click', () => {
        if (confirm('¿Estás seguro de que deseas vaciar el carrito?')) {
            vaciarCarrito();
        }
    });

    // Mostrar el carrito al cargar la página
    mostrarCarrito();
});
