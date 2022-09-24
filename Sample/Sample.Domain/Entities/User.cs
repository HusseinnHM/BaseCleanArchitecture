using BaseCleanArchitecture.Domain.Primitives.Entity.Base;

namespace Sample.Domain.Entities;

public sealed class User : BaseIdentityUser<Guid>
{
    public User(string name, string userName, string email)
    {
        Name = name;
        UserName = userName;
        Email = email;
    }

    private User()
    {
        
    }

    public string Name { get; private set; }
    private readonly List<Task> _assignedTasks = new();
    public ICollection<Task> AssignedTasks => _assignedTasks.AsReadOnly();
    private readonly List<Notification> _notifications = new();
    public ICollection<Notification> Notifications => _notifications.AsReadOnly();
}