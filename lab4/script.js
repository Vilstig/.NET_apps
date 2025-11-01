// Pobierz wszystkie elementy <canvas> z klasą drawingX
const canvases = document.querySelectorAll(".drawingX");

canvases.forEach((canvas) => {
  const ctx = canvas.getContext("2d");

  // 🔹 Reakcja na ruch myszki
  canvas.addEventListener("mousemove", (event) => {
    const rect = canvas.getBoundingClientRect();
    const x = event.clientX - rect.left;
    const y = event.clientY - rect.top;

    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.beginPath();

    ctx.moveTo(0, 0);
    ctx.lineTo(x, y);

    ctx.moveTo(canvas.width, 0);
    ctx.lineTo(x, y);

    ctx.moveTo(0, canvas.height);
    ctx.lineTo(x, y);

    ctx.moveTo(canvas.width, canvas.height);
    ctx.lineTo(x, y);

    ctx.stroke();
  });

  // 🔹 Wyczyść po wyjściu myszką
  canvas.addEventListener("mouseleave", () => {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
  });
});
