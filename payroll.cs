namespace PayrollSystem;


/// <summary>
/// Base Class Employee
/// </summary>
public abstract class Employees
{
    #region Properties
    public int Id { get; }
    public string Name { get; }
    public string Designation { get; set; }
    public string EmploymentType { get; set; }
    public string Address { get; set; }
    public decimal GrossSalary { get; private set; }    

    #endregion

    #region Constructor
    public Employees(int Id, string Name, string Designation, string Address, decimal baseSalary) 
    {
        this.Id = Id; this.Name = Name; this.Designation = Designation; this.Address = Address; this.GrossSalary = baseSalary;
    }
    #endregion
    #region Methods
    public abstract decimal CalculateSalary();
    #endregion

}

/// <summary>
/// Full Time Employee Class inheriting Employee Base Class
/// </summary>
public class FullTimeEmployee : Employees
{
    #region Constructor
    public FullTimeEmployee(int Id, string Name, string Designation, string Address, decimal baseSalary) : base(Id, Name, Designation, Address, baseSalary)
    {
        EmploymentType = "Full Time";
    }
    #endregion

    #region Methods
    public override decimal CalculateSalary()
    {
        return GrossSalary - 50000;
    }
    #endregion
}

/// <summary>
/// Contract Employee Class inheriting Employee Base Class
/// </summary>
public class ContractEmployee : Employees
{
    #region Constructor
    public ContractEmployee(int Id, string Name, string Designation, string Address, decimal baseSalary) : base(Id, Name, Designation, Address, baseSalary)
    {
        EmploymentType = "On Contract";
    }
    #endregion

    #region Methods
    public override decimal CalculateSalary()
    {
        return GrossSalary - 10000;
    }
    #endregion
}

/// <summary>
/// PaySlip Class to generate PaySlip for Employees
/// </summary>
public class PaySlip
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public string Designation { get; set; }
    public string EmploymentType { get; set; }

    public decimal Salary { get; set; }
    #endregion

    #region Constructor
    public PaySlip(int Id, string Name, string Designation, string EmploymentType)
    {
        this.Id = Id; this.Name = Name; this.Designation = Designation; this.EmploymentType = EmploymentType;
    }
    #endregion  

}


/// <summary>
/// Notidy class enclosing delegate signature and delegate methods
/// </summary>
public class Notify
{
    // Delegate Signature
    public delegate void SalaryProcessed(Employees e, decimal salary);

    // Delegate Methods
    public static void HRNotification(Employees e, decimal salary)
    { 
        Console.WriteLine($"Salary Processed by HR : {salary}");
    }
    public static void FinanceNotification(Employees e,decimal salary)
    {
        Console.WriteLine($"Salary Processed by Finance Department: {salary}");
    }
}

/// <summary>
/// Static storage of Employee Data 
/// </summary>
//public class EmpData
//{
//    private static List<Employees> EmpList = new();

//    public void AddEmp()
//    {
//        EmpList.Add(new Employees());
//    }

//    public static List<Employees> GetEmp()
//    {
//        return EmpList;
//    }
//}