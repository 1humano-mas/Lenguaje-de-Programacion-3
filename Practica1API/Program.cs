var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var api = app.MapGroup("/api");

// Lista en memoria con estados de texto
List<TaskItem> tasks = [
    new TaskItem(Guid.NewGuid(), "Estudiar ASP.NET Core", "Revisar Minimal APIs y conceptos básicos", "Pendiente"),
    new TaskItem(Guid.NewGuid(), "Comprar suministros", "Ir a la papelería por cuadernos y carpetas", "Completado")
];

// --- READ (Mensaje de bienvenida) ---
api.MapGet("/", () => {
    return new { Message = "Bienvenido a la API de Gestor de Tareas" };
});

// --- READ (Obtener todas las tareas) ---
api.MapGet("/tasks", () => {
    return Results.Ok(tasks);
});

// --- READ (Obtener tarea por UUID) ---
api.MapGet("/task/{uuid}", IResult (Guid uuid) => {
    var task = tasks.FirstOrDefault(t => t.Uuid == uuid);
    if (task is null) {
        return Results.NotFound(new { Message = "Tarea no encontrada" });
    }

    return Results.Ok(task);
});

// --- CREATE (Crear nueva tarea con UUID generado automáticamente) ---
api.MapPost("/tasks", (CreateTaskRequest request) => {
    // Genera el UUID en el servidor automáticamente
    var newTask = new TaskItem(
        Guid.NewGuid(),
        request.Titulo,
        request.Descripcion,
        string.IsNullOrWhiteSpace(request.Estado) ? "Pendiente" : request.Estado
    );

    tasks.Add(newTask);

    return Results.Created($"/api/task/{newTask.Uuid}", newTask);
});

// --- UPDATE (Actualizar tarea existente) ---
api.MapPut("/task/{uuid}", IResult (Guid uuid, UpdateTaskRequest request) => {
    var index = tasks.FindIndex(t => t.Uuid == uuid);
    if (index == -1) {
        return Results.NotFound(new { Message = "Tarea no encontrada" });
    }

    // Mantiene el UUID original y actualiza los campos
    var taskToUpdate = new TaskItem(
        uuid,
        request.Titulo,
        request.Descripcion,
        request.Estado
    );

    tasks[index] = taskToUpdate;

    return Results.Ok(taskToUpdate);
});

// --- DELETE (Eliminar tarea por UUID) ---
api.MapDelete("/task/{uuid}", IResult (Guid uuid) => {
    var task = tasks.FirstOrDefault(t => t.Uuid == uuid);
    if (task is null) {
        return Results.NotFound(new { Message = "Tarea no encontrada" });
    }

    tasks.Remove(task);
    return Results.NoContent();
});

app.Run();

// Modelo Principal de la Tarea
record TaskItem(
    Guid Uuid, 
    string Titulo, 
    string Descripcion, 
    string Estado
);

// DTO para la creación (Sin UUID)
record CreateTaskRequest(
    string Titulo, 
    string Descripcion, 
    string Estado
);

// DTO para la edición
record UpdateTaskRequest(
    string Titulo, 
    string Descripcion, 
    string Estado
);
