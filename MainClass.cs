namespace MainClass;

using PayrollSystem;
using OrderProcessing;
public class MainClass
{
    public static void Main(string[] args)
    {
        #region PayRoll System

        /// Storing All Employee data in inline memory (static Collection)
        EmpData data = new EmpData();
        data.AddEmployee(new FullTimeEmployee(1, "Nikhil", "SDE", "Delhi", 1600000));
        data.AddEmployee(new ContractEmployee(2, "Punya", "HR", "Delhi", 900000));
        data.AddEmployee(new FullTimeEmployee(3, "Harsh", "Marketing", "Noida", 180000));
        data.AddEmployee(new ContractEmployee(4, "Yashika", "Analyst", "Gurugram", 850000));
        data.AddEmployee(new FullTimeEmployee(5, "Abhinav", "Designer", "Himachal", 950000));
        data.AddEmployee(new ContractEmployee(6, "Vartika", "SWE", "Rajasthan", 1200000));

        // Calling Static Method to return the list of All Employees
        EmpData.GetEmp();

        // List to store payslip of each employee
        List<PaySlip> payslips = new List<PaySlip>();

        // Loop to calculate Net Salary, Deductions, Adding Employees to payslip list and invoking delegate for each employee.
        foreach (Employee e in EmpData.GetEmp())
        {
            // Gross Salary
            decimal grossSalary = e.GrossSalary;
            // Calculating Net Salary based on Employment Type
            decimal salary = e.CalculateSalary();
            // Calculating Deductions (Gross - Net)
            decimal Deductions = e.GrossSalary - salary;

            // Adding the Employee to the PaySlip List
            payslips.Add(
                new PaySlip(e.Id, e.Name, e.Designation, e.EmploymentType) { Salary = salary }
                );

            // Printing Payslip of Each Employee
            Console.WriteLine($"\nId :{e.Id}, Name:{e.Name}, Designation : {e.Designation}, Gross Salary : {grossSalary}, Deductions: {Deductions}, Net Salary : {salary}");

            // Delegate
            Notify.SalaryProcessed notify = Notify.HRNotification;
            notify += Notify.FinanceNotification;

            // Invoking Delegate
            notify(e, salary);

        }

        #endregion

        #region Order Processing

        Console.WriteLine("\nOrder Processing Outputs: ");

        Dictionary<int, Product> products = new()
        {
            {1,new Product(100, "Product A", 1000) },
            {2,new Product(101, "Product B", 2000) },
            {3,new Product(102, "Product C", 3000) },
            {4,new Product(103, "Product D", 4000) },
            {5,new Product(104, "Product E", 5000) },
            {6,new Product(105, "Product F", 6000) },

        };
        List<Customer> customers = new () { new Customer (1,"Customer A"), new Customer(2,"Customer B"), new Customer(3, "Customer C"), new Customer(4, "Customer D") };

        List<Order> orders = new();

        Order o1 = new Order(101, customers[0]);
        o1.Items.Add(new OrderItem(products[1], 1));
        o1.Items.Add(new OrderItem(products[2], 2));

        o1.ChangeStatus(OrderStatus.Paid);
        o1.ChangeStatus(OrderStatus.Shipped);
        o1.ChangeStatus(OrderStatus.Delivered);


        Order o2 = new Order(102, customers[1]);
        o2.Items.Add(new OrderItem(products[3], 1));
        o2.ChangeStatus(OrderStatus.Paid);
        o2.ChangeStatus(OrderStatus.Cancelled);
        o2.ChangeStatus(OrderStatus.Shipped);

        Order o3 = new Order(103, customers[2]);
        o3.Items.Add(new OrderItem(products[4], 2));
        o3.ChangeStatus(OrderStatus.Shipped);
        o3.ChangeStatus(OrderStatus.Paid);

        Order o4 = new Order(104, customers[0]);
        o4.Items.Add(new OrderItem(products[5], 1));
        o4.ChangeStatus(OrderStatus.Paid);

        orders.AddRange(new[] { o1, o2, o3, o4 });


        foreach (Order order in orders)
        {
            order.NotifyStatus += NotifyStatus.CustomerNotify;
            order.NotifyStatus += NotifyStatus.LogisticsNotify;
        }


        Console.WriteLine("\nOrders Summary\n");

        foreach (Order order in orders)
        {
            Console.WriteLine($"Order ID: {order.Id}");
            Console.WriteLine($"Customer: {order.customer.Name}");
            Console.WriteLine($"Total Amount: {order.TotaLAmount()}");
            Console.WriteLine($"Current Status: {order.Status}");

            Console.WriteLine("Status History:");
            foreach (string status in order.statusHistory)
            {
                Console.WriteLine(status);
            }

            Console.WriteLine();
        }
        #endregion
    }
}