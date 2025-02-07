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