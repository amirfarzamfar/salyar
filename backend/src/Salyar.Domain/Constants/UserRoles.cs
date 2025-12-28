namespace Salyar.Domain.Constants;

public static class UserRoles
{
    public const string Admin = "Admin"; // مدیر
    public const string Supervisor = "Supervisor"; // سوپروایزر
    public const string Nurse = "Nurse"; // پرستار
    public const string AssistantNurse = "AssistantNurse"; // کمک پرستار
    public const string ElderlyCompanion = "ElderlyCompanion"; // سالمندیار
    public const string MotherHelper = "MotherHelper"; // مادریار
    public const string Patient = "Patient"; // بیمار
    public const string Elderly = "Elderly"; // سالمند
    public const string Family = "Family"; // خانواده سالمند

    public static readonly IReadOnlyList<string> All = new[]
    {
        Admin, Supervisor, Nurse, AssistantNurse, ElderlyCompanion, 
        MotherHelper, Patient, Elderly, Family
    };
}
