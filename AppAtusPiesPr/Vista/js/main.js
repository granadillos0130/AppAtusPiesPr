// Función para aplicar la animación antes de redirigir
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



