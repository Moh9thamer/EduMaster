namespace Application.Users;

public static class UserRoles
{
    public const string Admin = "Admin";
    public const string Manager = "Manager";
    public const string Teacher = "Teacher";
    public const string Student = "Student";

    public static readonly IReadOnlyList<string> All = [Admin, Manager, Teacher, Student];
    public static readonly IReadOnlyList<string> ManagerCanCreate = [Teacher, Student];
}
