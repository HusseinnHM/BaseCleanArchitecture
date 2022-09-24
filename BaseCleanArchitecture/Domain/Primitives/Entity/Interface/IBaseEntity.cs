using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCleanArchitecture.Domain.Primitives.Entity.Interface;

public interface IIndex<TIndex> where TIndex : IEquatable<TIndex>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TIndex Id { get; set; }
}

public interface IBaseEntity
{
}

public interface IBaseEntity<TIndex> : IBaseEntity, IIndex<TIndex>, IDeletable<TIndex>, ICreatable<TIndex>,
    IUpdatable<TIndex> where TIndex : struct, IEquatable<TIndex>
{
}