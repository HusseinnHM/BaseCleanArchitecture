using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using BaseCleanArchitecture.Application.Core.Abstractions.Request;
using BaseCleanArchitecture.Application.OperationResponse;

namespace Sample.Application.Notification.Queries.GetAllNotification;

public sealed class GetAllNotificationQuery 
{
    public sealed class Request : IRequest<OperationResponse<IEnumerable<Response>>>
    {
        [Required]
        public Guid UserId { get; set; }
    }
    
    public sealed  class Response
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }

        public static readonly Expression<Func<Sample.Domain.Entities.Notification, Response>> Selector = n
            => new()
            {
                Id = n.Id,
                Text = n.Text,
                DateCreated = n.DateCreated
            };
        
    }
   
}