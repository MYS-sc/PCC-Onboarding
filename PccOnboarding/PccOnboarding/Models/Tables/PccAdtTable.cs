using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PccOnboarding.Models.Tables;
[Table("tbl_pcc_adt")]
public class PccAdtTable
{
    [Key]
    [Column("pcc_adt_id")]
    public int OurId { get; set;}
    [Column("accessingEntityId")]
    public string? AccessingEntityId { get; set; }
    [Column("actionCode")]
    public string? ActionCode { get; set; }
    [Column("actionType")]
    public string? ActionType { get; set; }
    [Column("additionalBedDesc")]
    public string? AdditionalBedDesc { get; set; }
    [Column("additionalBedId")]
    public int? AdditionalBedId { get; set; }
    [Column("additionalFloorDesc")]
    public string? AdditionalFloorDesc { get; set; }
    [Column("additionalFloorId")]
    public int? AdditionalFloorId { get; set; }
    [Column("additionalRoomDesc")]
    public string? AdditionalRoomDesc { get; set; }
    [Column("additionalRoomId")]
    public int? AdditionalRommId { get; set; }
    [Column("additionalUnitDesc")]
    public string? AdditionalUnitDesc { get; set; }
    [Column("additionalUnitId")]
    public int? AdditionalUnitId { get; set; }
    [Column("admissionSource")]
    public string? AdmissionSource { get; set; }
    [Column("admissionSourceCode")]
    public string? AdmissionSourceCode { get; set; }
    [Column("admissionType")]
    public string? AdmissionType { get; set; }
    [Column("admissionTypeCode")]
    public string? AdmissionTypeCode { get; set; }
    [Column("adtRecordId")]
    public int? AdtRecordId { get; set; }
    [Column("bedDesc")]
    public string? BedDesc { get; set; }
    [Column("bedId")]
    public int? BedId { get; set; }
    [Column("destination")]
    public string? Destination { get; set; }
    [Column("destinationType")]
    public string? DestinationType { get; set; }
    [Column("effectiveDateTime")]
    public string? EffectiveDateTime { get; set; }
    [Column("enteredBy")]
    public string? EnteredBy { get; set; }
    [Column("enteredByPositionId")]
    public int? EnteredByPositionId { get; set; }
    [Column("enteredDate")]
    public string? EnteredDate { get; set; }
    [Column("floorDesc")]
    public string? FloorDesc { get; set; }
    [Column("floorId")]
    public int? FloorId { get; set; }
    [Column("isCancelledRecord")]
    public bool? IsCancelledRecord { get; set; }
    [Column("transferReason")]
    public string? TransferReason { get; set; }
    [Column("modifiedDateTime")]
    public string? ModifiedDateTime { get; set; }
    [Column("origin")]
    public string? Origin { get; set; }
    [Column("originType")]
    public string? OriginType { get; set; }
    [Column("outpatient")]
    public bool? Outpatient { get; set; }
    [Column("patientId")]
    public int? PatientId { get; set; }
    [Column("payerCode")]
    public string? PayerCode { get; set; }
    [Column("payerName")]
    public string? PayerName { get; set; }
    [Column("payerType")]
    public string? PayerType { get; set; }
    [Column("roomId")]
    public int? RoomId { get; set; }
    [Column("skilledCare")]
    public bool? SkilledCare { get; set; }
    [Column("skilledEffectiveFromDate")]
    public string? SkilledEffectiveFromDate { get; set; }
    [Column("skilledEffectiveToDate")]
    public string? SkilledEffectiveToDate { get; set; }
    [Column("standardActionType")]
    public string? StandardActionType { get; set; }
    [Column("stopBillingDate")]
    public string? StopBillingDate { get; set; }
    [Column("unitId")]
    public int? UnitId { get; set; }
}
