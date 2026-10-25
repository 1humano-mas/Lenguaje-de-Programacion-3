namespace gestor_tarea.Modules.Tasks.Dtos;

// DTO para Creación (POST)
public record CreateTaskDto(
    string Titulo, 
    string Descripcion, 
    string? Estado
);

// DTO para Actualización Parcial (PATCH)
public record PatchTaskDto(
    string? Titulo, 
    string? Descripcion, 
    string? Estado
);