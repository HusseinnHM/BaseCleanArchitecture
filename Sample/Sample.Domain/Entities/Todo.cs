using BaseCleanArchitecture.Domain.Primitives.Entity.Base;
using Sample.Domain.Enums;

namespace Sample.Domain.Entities;

public sealed class Todo : BaseEntity<Guid>
{
    private Todo()
    {
        
    }
    
    private Todo(string text, Guid taskId, int order)
    {
        Text = text;
        TaskId = taskId;
        Order = order;
    }

    internal static Todo Create(string text, Guid taskId, int order)
    {
        var todo = new Todo(text, taskId, order);
        todo._toDoStatuses.Add(new ToDoStatus(DateTime.Now, Enums.ToDoStatuses.BackLog));
        return todo;
    }

    public string Text { get; private set; }
    public int Order { get; private set; }
    public Guid TaskId { get; private set; }

    public Task Task { get; private set; }

    public ToDoStatuses CurrentStatus => _toDoStatuses.MaxBy(t => t.Date)?.ToDoStatuses ?? Enums.ToDoStatuses.BackLog;
    
    private readonly List<ToDoStatus> _toDoStatuses = new();
    public IReadOnlyCollection<ToDoStatus> ToDoStatuses => _toDoStatuses.AsReadOnly();

    public void UpdateStatus(ToDoStatuses toDoStatuses) =>
        _toDoStatuses.Add(new ToDoStatus(DateTime.Now, toDoStatuses));
}