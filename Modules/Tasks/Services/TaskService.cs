using gestor_tarea.Modules.Tasks.Dtos;
using gestor_tarea.Modules.Tasks.Entities;

namespace gestor_tarea.Modules.Tasks.Services;

public class TaskService
{
    // Lista temporal en memoria
    private readonly List<TaskItem> _tasks = new()
    {
        new TaskItem { Uuid = Guid.NewGuid(), Titulo = "Estudiar ASP.NET Core", Descripcion = "Revisar arquitectura modular", Estado = "Pendiente" },
        new TaskItem { Uuid = Guid.NewGuid(), Titulo = "Comprar suministros", Descripcion = "Ir a la papelería por cuadernos", Estado = "Completado" }
    };

    public IEnumerable<TaskItem> GetAll() => _tasks;

    public TaskItem? GetByUuid(Guid uuid) => _tasks.FirstOrDefault(t => t.Uuid == uuid);

    public TaskItem Create(CreateTaskDto dto)
    {
        var newTask = new TaskItem
        {
            Uuid = Guid.NewGuid(),
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Estado = string.IsNullOrWhiteSpace(dto.Estado) ? "Pendiente" : dto.Estado
        };

        _tasks.Add(newTask);
        return newTask;
    }

    public TaskItem? UpdatePartial(Guid uuid, PatchTaskDto dto)
    {
        var task = GetByUuid(uuid);
        if (task is null) return null;

        if (!string.IsNullOrWhiteSpace(dto.Titulo))
            task.Titulo = dto.Titulo;

        if (!string.IsNullOrWhiteSpace(dto.Descripcion))
            task.Descripcion = dto.Descripcion;

        if (!string.IsNullOrWhiteSpace(dto.Estado))
            task.Estado = dto.Estado;

        return task;
    }

    public bool Delete(Guid uuid)
    {
        var task = GetByUuid(uuid);
        if (task is null) return false;

        _tasks.Remove(task);
        return true;
    }
}