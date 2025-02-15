﻿// Función para aplicar la animación antes de redirigir
function animateBeforeRedirect(event) {
    event.preventDefault(); // Evita que el enlace se ejecute inmediatamente
    document.body.classList.add('page-exit'); // Aplica la animación
    setTimeout(() => {
        window.location.href = event.target.href; // Redirige después de la animación
    }, 500); // Duración de la animación
}

// Asigna el evento a todos los botones "Ver más..." y enlaces del vendedor
document.querySelectorAll('.buy-button, .cardseller').forEach(link => {
    link.addEventListener('click', animateBeforeRedirect);
});

document.addEventListener("DOMContentLoaded", function () {
    const hearts = document.querySelectorAll(".heart-icon");
    const hdnValoracion = document.querySelector("[id$='hdnValoracion']"); // Buscar por sufijo

    if (!hdnValoracion) {
        console.error("No se encontró el HiddenField de valoración.");
        return;
    }

    hearts.forEach(heart => {
        heart.addEventListener("click", function () {
            const value = this.getAttribute("data-value");
            hdnValoracion.value = value;  // Asignar el valor

            console.log("Valoración seleccionada:", value); // Verificar en consola
            console.log("Nuevo valor en HiddenField:", hdnValoracion.value); // Confirmar actualización


            // Cambiar la apariencia de los corazones
            hearts.forEach((h, index) => {
                if (index < value) {
                    h.classList.add("active", "fas");
                    h.classList.remove("far");
                } else {
                    h.classList.remove("active", "fas");
                    h.classList.add("far");
                }
            });
        });
    });
});

function actualizarValoracion(promedio) {
    const validation = validatePromedio(promedio);

    if (!validation.isValid) {
        console.error(`Error en el promedio:`, {
            valorOriginal: promedio,
            mensaje: validation.message,
            tipo: typeof promedio
        });
        return;
    }

    const numHearts = Math.min(Math.max(validation.normalizedValue, 1), 5);
    const hearts = document.querySelectorAll(".heart");

    hearts.forEach((heart, index) => {
        const valor = parseFloat(heart.getAttribute("data-value"));
        if (valor <= numHearts) {
            heart.classList.add("filled");
        } else {
            heart.classList.remove("filled");
        }
    });
}

function validatePromedio(valor) {
    if (typeof valor !== 'string' && typeof valor !== 'number') {
        return {
            isValid: false,
            message: 'El valor debe ser un número o una cadena numérica',
            normalizedValue: 0
        };
    }

    const numberValue = Number(valor);
    if (isNaN(numberValue)) {
        return {
            isValid: false,
            message: 'El valor no puede convertirse a número',
            normalizedValue: 0
        };
    }

    const roundedValue = Math.round(numberValue);
    if (roundedValue < 1 || roundedValue > 5) {
        return {
            isValid: false,
            message: 'El valor debe estar entre 1 y 5',
            normalizedValue: Math.min(Math.max(roundedValue, 1), 5)
        };
    }

    return {
        isValid: true,
        message: 'Valor válido',
        normalizedValue: roundedValue
    };
}

document.addEventListener("DOMContentLoaded", function () {
    console.log('Estado inicial del DOM:');
    console.log(document.body.innerHTML);

    const promedioLabel = document.getElementById('lblPromedio');
    console.log('Elemento encontrado:', promedioLabel);

    if (promedioLabel) {
        console.log('Atributos del elemento:', Object.fromEntries(
            Array.from(promedioLabel.attributes).map(attr => [attr.name, attr.value])
        ));

        const promedio = parseFloat(promedioLabel.getAttribute("data-promedio"));
        console.log('Valor del promedio:', promedio);

        if (!isNaN(promedio)) {
            actualizarValoracion(promedio);
        } else {
            console.error("El valor del promedio no es un número válido");
        }
    } else {
        console.error("No se encontró el elemento lblPromedio en el DOM.");
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const imagen = document.querySelector(".imagen-zoom");

    // Crear el overlay
    const overlay = document.createElement("div");
    overlay.classList.add("overlay");
    document.body.appendChild(overlay);

    // Crear el botón de cerrar
    const cerrarZoom = document.createElement("button");
    cerrarZoom.id = "cerrar-zoom";
    cerrarZoom.innerHTML = '<i class="fas fa-times"></i>'; // Usar innerHTML para el icono de FontAwesome
    overlay.appendChild(cerrarZoom);

    // Al pasar el cursor sobre la imagen
    imagen.addEventListener("mouseenter", function () {
        imagen.classList.add("zoom-activo"); // Aplica la clase para el zoom
        overlay.classList.add("activo"); // Muestra el overlay
    });

    // Al mover el cursor fuera de la imagen (incluso en el overlay)
    document.addEventListener("mousemove", function (e) {
        if (imagen.classList.contains("zoom-activo")) {
            // Verifica si el cursor está fuera de la imagen
            const rectImagen = imagen.getBoundingClientRect();
            const rectBoton = cerrarZoom.getBoundingClientRect();

            // Verifica si el cursor está fuera de la imagen y no sobre el botón de cerrar
            if (
                (e.clientX < rectImagen.left ||
                    e.clientX > rectImagen.right ||
                    e.clientY < rectImagen.top ||
                    e.clientY > rectImagen.bottom) &&
                !(e.clientX >= rectBoton.left &&
                    e.clientX <= rectBoton.right &&
                    e.clientY >= rectBoton.top &&
                    e.clientY <= rectBoton.bottom)
            ) {
                imagen.classList.remove("zoom-activo"); // Quita la clase para el zoom
                overlay.classList.remove("activo"); // Oculta el overlay
            }
        }
    });

    // Cerrar el zoom al hacer clic en el botón de cerrar
    cerrarZoom.addEventListener("click", function () {
        imagen.classList.remove("zoom-activo"); // Quita la clase para el zoom
        overlay.classList.remove("activo"); // Oculta el overlay
    });
});

