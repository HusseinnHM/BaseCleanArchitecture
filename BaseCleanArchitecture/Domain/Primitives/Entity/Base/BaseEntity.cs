using System.ComponentModel.DataAnnotations;
using BaseCleanArchitecture.Domain.Primitives.Entity.Interface;

namespace BaseCleanArchitecture.Domain.Primitives.Entity.Base;

public abstract class BaseEntity<TIndex> : IBaseEntity<TIndex> where TIndex :struct, IEquatable<TIndex>
{
    [Key]
    public TIndex Id { get; set; }

    public DateTime? DateDeleted { get; set; }
    public TIndex? DeletedBy { get; set; }
    
    public DateTime DateCreated { get; set; }
    public TIndex? CreatedBy { get; set; }
    
    public DateTime? DateUpdated { get; set; }
    public TIndex? UpdatedBy { get; set; }
        

        
        
    public static bool operator ==(BaseEntity<TIndex> a, BaseEntity<TIndex> b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(BaseEntity<TIndex> a, BaseEntity<TIndex> b) => !(a == b);

    public bool Equals(BaseEntity<TIndex>? other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id.Equals(other.Id) ;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (!(obj is BaseEntity<TIndex> other))
        {
            return false;
        }
            

        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode() * 7;
}