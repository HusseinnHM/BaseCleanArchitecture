namespace BaseCleanArchitecture.Domain.Primitives.Entity.Interface;

public interface ICreatable<TIndex>  where TIndex : struct
{
    public DateTime DateCreated { get; set; }
    public TIndex? CreatedBy { get; set; }
}