using gestor_tarea.Modules.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Habilitar Controladores y registrar el Módulo de Tareas
builder.Services.AddControllers();
builder.Services.AddTasksModule();

var app = builder.Build();

app.MapControllers();

app.Run();