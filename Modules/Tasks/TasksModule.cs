using gestor_tarea.Modules.Tasks.Services;

namespace gestor_tarea.Modules.Tasks;

public static class TasksModule
{
    public static IServiceCollection AddTasksModule(this IServiceCollection services)
    {
        services.AddSingleton<TaskService>();
        return services;
    }
}