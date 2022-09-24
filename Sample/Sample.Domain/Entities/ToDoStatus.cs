using BaseCleanArchitecture.Domain.Primitives.Entity.Base;
using Sample.Domain.Enums;

namespace Sample.Domain.Entities;

public sealed class ToDoStatus : BaseEntity<Guid>
{

    private ToDoStatus()
    {
        
    }
    public ToDoStatus(DateTime date, ToDoStatuses toDoStatuses)
    {
        Date = date;
        ToDoStatuses = toDoStatuses;
    }
    
    public DateTime Date { get; private set; }
    public ToDoStatuses ToDoStatuses { get; private set; }

    public Guid ToDoId { get; private set; }
    public Todo Todo { get; private set; }


}