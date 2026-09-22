var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () => "Bienvenido a mi Api");

app.MapGet("/juan-mercedes", () => new
{
    Nombre = "Juan Bautista",
    Apellido = "Mercedes Molina",
    Edad = 33,
    Matricula = "LR-2024-02270", 
    Carrera = "Ingeniería de Software"
});

app.Run();