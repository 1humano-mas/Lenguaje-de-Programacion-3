using Microsoft.AspNetCore.Mvc;
using gestor_tarea.Modules.Tasks.Dtos;
using gestor_tarea.Modules.Tasks.Services;

namespace gestor_tarea.Modules.Tasks.Controllers;

[ApiController]
[Route("api/tareas")]
public class TasksController : ControllerBase
{
    private readonly TaskService _service;

    public TasksController(TaskService service)
    {
        _service = service;
    }

    // GET /api/tareas
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    // GET /api/tareas/{uuid}
    [HttpGet("{uuid:guid}")]
    public IActionResult GetByUuid(Guid uuid)
    {
        var task = _service.GetByUuid(uuid);
        if (task is null) return NotFound(new { mensaje = "Recurso no encontrado" });
        return Ok(task);
    }

    // POST /api/tareas
    [HttpPost]
    public IActionResult Create([FromBody] CreateTaskDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Titulo) || string.IsNullOrWhiteSpace(dto.Descripcion))
        {
            return BadRequest(new { mensaje = "El título y la descripción son obligatorios." });
        }

        var createdTask = _service.Create(dto);
        return Created($"/api/tareas/{createdTask.Uuid}", createdTask);
    }

    // PATCH /api/tareas/{uuid}
    [HttpPatch("{uuid:guid}")]
    public IActionResult UpdatePartial(Guid uuid, [FromBody] PatchTaskDto dto)
    {
        if (dto.Titulo == null && dto.Descripcion == null && dto.Estado == null)
        {
            return BadRequest(new { mensaje = "Debe proporcionar al menos un campo para actualizar." });
        }

        var updatedTask = _service.UpdatePartial(uuid, dto);
        if (updatedTask is null) return NotFound(new { mensaje = "Recurso no encontrado." });

        return Ok(updatedTask);
    }

    // DELETE /api/tareas/{uuid}
    [HttpDelete("{uuid:guid}")]
    public IActionResult Delete(Guid uuid)
    {
        var deleted = _service.Delete(uuid);
        if (!deleted) return NotFound(new { mensaje = "Recurso no encontrado para eliminar." });

        return Ok(new { mensaje = "Registro eliminado correctamente" });
    }
}