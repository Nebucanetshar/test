using Grpc.Core;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace grpc;

/// <summary>
/// Orm to DbContext
/// </summary>
//public class Items
//{
//    [Key] public int Id { get; set; }
//    public int CurrentCount { get; set; }
//    public DateTime Timestamp { get; set; }
//}

/// <summary>
/// Orm to LinqToDb 
/// </summary>
[Table(Name = "Items")]
public class Items
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, NotNull]
    public int CurrentCount { get; set; }

    [Column, NotNull]
    public DateTime Timestamp { get; set; }
}
