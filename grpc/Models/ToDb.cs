using LinqToDB.Mapping;

namespace grpc;

[Table(Name = "ToDb")]
public class ToDb
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, NotNull]
    public int CurrentCount { get; set; }

    [Column, NotNull]
    public DateTime Timestamp { get; set; }
}
