namespace BaseCleanArchitecture.Domain.Primitives.Entity.Interface;

public interface IDeletable<TIndex>  where TIndex : struct
{
    public TIndex? DeletedBy { get; set; }
    public DateTime? DateDeleted { get; set; }
}