using BaseCleanArchitecture.Domain.Primitives.Entity.Base;

namespace Sample.Domain.Entities;

public sealed class Notification : BaseEntity<Guid>
{

    private Notification()
    {
        
    }
    public Notification(string text, Guid userId)
    {
        Text = text;
        UserId = userId;
    }
    
    public string Text { get; private set;  }

    public Guid UserId { get; private set; }
    public User User { get; private set;  }
}