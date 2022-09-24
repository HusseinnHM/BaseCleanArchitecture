using BaseCleanArchitecture.Domain.Primitives;
using Sample.Domain.Events;

namespace Sample.Domain.Entities;

public sealed class Task : AggregateRoot<Guid>
{

    private Task()
    {
        
    }
    public Task(string text, int priority, Guid? assignedUserId)
    {
        Text = text;
        Priority = priority;
        AssignedUserId = assignedUserId;
    }

    public string Text { get; private set; }
    public int Priority { get; private set; }

    public Guid? AssignedUserId { get; private set; }
    public User AssignedUser { get; private set; }

    private readonly List<Todo> _todos = new();
    public IReadOnlyCollection<Todo> Todos => _todos.AsReadOnly();

    public void AddToDo(string text)
    {
        var todo = Todo.Create(text, Id, _todos.Count + 1);
        _todos.Add(todo);
        if (AssignedUserId.HasValue)
        {
            AddDomainEvent(new SendAddTodoNotificationEvent(new Notification($"New ToDo Add To Task {text}",AssignedUserId.Value)));
        }
    }
}