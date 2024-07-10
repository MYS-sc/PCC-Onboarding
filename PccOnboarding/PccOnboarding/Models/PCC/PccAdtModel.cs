namespace PccOnboarding.Models.PCC;

public class PccAdtModel
{
    //[Column("accessingEntityId")]
    public string? AccessingEntityId { get; set; }
    //[Column("actionCode")]
    public string? ActionCode { get; set; }
    public string? ActionType { get; set; }
    public string? AdditionalBedDesc { get; set; }
    public int? AdditionalBedId { get; set; }
    public string? AdditionalFloorDesc { get; set; }
    public int? AdditionalFloorId { get; set; }
    public string? AdditionalRoomDesc { get; set; }
    public int? AdditionalRommId { get; set; }
    public string? AdditionalUnitDesc { get; set; }
    public int? AdditionalUnitId { get; set; }
    public string? AdmissionSource { get; set; }
    public string? AdmissionSourceCode { get; set; }
    public string? AddmissionType { get; set; }
    public string? AddmissionTypeCode { get; set; }
    public int? AdtRecordId { get; set; }
    public string? BedDesc { get; set; }
    public int? BedId { get; set; }
    public string? Destination { get; set; }
    public string? DestinationType { get; set; }
    public string? EffectiveDateTime { get; set; }
    public string? EnteredBy { get; set; }
    public int? EnteredByPositionId { get; set; }
    public string? EnteredDate { get; set; }
    public string? FloorDesc { get; set; }
    public int? FloorId { get; set; }
    public bool? IsCancelledRecord { get; set; }
    public string? TransferReason { get; set; }
    public string? ModifiedDateTime { get; set; }
    public string? Origin { get; set; }
    public string? OriginType { get; set; }
    public bool? Outpatient { get; set; }
    public int? PatientId { get; set; }
    public string? PayerCode { get; set; }
    public string? PayerName { get; set; }
    public string? PayerType { get; set; }
    public int? RoomId { get; set; }
    public bool? SkilledCare { get; set; }
    public string? SkilledEffectiveFromDate { get; set; }
    public string? SkilledEffectiveToDate { get; set; }
    public string? StandardActionType { get; set; }
    public string? StopBillingDate { get; set; }
    public int? UnitId { get; set; }
}
