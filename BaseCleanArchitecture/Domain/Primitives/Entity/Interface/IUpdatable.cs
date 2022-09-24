namespace BaseCleanArchitecture.Domain.Primitives.Entity.Interface;

public interface IUpdatable<TIndex> where TIndex : struct
{
    public DateTime? DateUpdated { get; set; }

    public TIndex? UpdatedBy { get; set; }
}