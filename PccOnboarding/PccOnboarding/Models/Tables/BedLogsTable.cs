using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PccOnboarding.Models.Tables;


[Table("PccBedLogs")]
public class BedLogsTable
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [Column("lastChangedDate")]
    public string? LastChangedDate { get; set; }
    [Column("state")]
    public string? State { get; set; }
    [Column("ClientId")]
    public int? ClientId { get; set; }
    [Column("pccId")]
    public int? PccId { get; set; }
    [Column("bedId")]
    public int? BedId { get; set; }
    [Column("additionalBedId")]
    public int? AdditionalBedId { get; set; }
    [Column("bedDescription")]
    public string? BedDescription { get; set; }
    [Column("additionalBedDescription")]
    public string? AdditionalBedDescription { get; set; }
    [Column("unitId")]
    public int? UnitId { get; set; }
    [Column("additionalUnitId")]
    public int? AdditionalUnitId { get; set; }
    [Column("unitDescription")]
    public string? UnitDescription { get; set; }
    [Column("additionalUnitDescription")]
    public string? AdditionalUnitDescription { get; set; }
    [Column("roomId")]
    public int? RoomId { get; set; }
    [Column("additionalRoomId")]
    public int? AdditionalRoomId { get; set; }
    [Column("roomDescription")]
    public string? RoomDescription { get; set; }
    [Column("additionalRoomDescription")]
    public string? AdditionalRoomDescription { get; set; }
    [Column("floorId")]
    public int? FloorId { get; set; }
    [Column("additionalFloorId")]
    public int? AdditionalFloorId { get; set; }
    [Column("floorDescription")]
    public string? FloorDescription { get; set; }
    [Column("additionalFloorDescription")]
    public string? AdditionalFloorDescription { get; set; }
}

