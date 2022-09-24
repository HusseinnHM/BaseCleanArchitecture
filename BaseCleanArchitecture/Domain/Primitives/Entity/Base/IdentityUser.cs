using BaseCleanArchitecture.Domain.Primitives.Entity.Interface;
using Microsoft.AspNetCore.Identity;

namespace BaseCleanArchitecture.Domain.Primitives.Entity.Base;

public class BaseIdentityUser<TIndex> : IdentityUser<TIndex>, IBaseEntity<TIndex> where TIndex : struct, IEquatable<TIndex>
{
    public DateTime? DateDeleted { get; set; }
    public TIndex? DeletedBy { get; set; }
    
    public DateTime DateCreated { get; set; }
    public TIndex? CreatedBy { get; set; }
    
    public DateTime? DateUpdated { get; set; }
    public TIndex? UpdatedBy { get; set; }
    
    public DateTime? DateBlocked { get; set; }
    
}
