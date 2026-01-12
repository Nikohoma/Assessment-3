namespace OrderProcessing;


public enum OrderStatus
{
    Paid,
    Placed,
    Received,
    Shipped,
    Delivered,
    Cancelled
}


public class Product
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    #endregion

    #region Constructor
    public Product(int Id, string Name, decimal Price)
    {
        this.Id = Id; this.Name = Name; this.Price = Price;
    }
    #endregion
}

public class Customer
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    #endregion

    #region Constructor
    public Customer(int Id, string Name)
    {
        this.Id = Id; this.Name = Name;
    }
    #endregion

}

public class OrderItem
{
    #region Properties
    public Product product { get; }
    public int Quantity { get;  }
    public decimal Total => product.Price * Quantity;
    #endregion

    #region Constructor
    public OrderItem(Product product, int Quantity)
    {
        this.product = product;
        this.Quantity = Quantity;
    }
    #endregion

}

/// <summary>
/// Keeps track of Status
/// </summary>
public class StatusHistory
{
    public OrderStatus status { get; set; }

    public StatusHistory(OrderStatus status)
    {
        this.status = status;
    }
}

/// <summary>
/// Notification Class executing Delegate
/// </summary>
public class NotifyStatus
{
    // Delegate Signature
    public delegate void StatusNotification(Customer customer, OrderStatus status);

    // Delegate Methods
    public static void CustomerNotify(Customer customer, OrderStatus status)
    {
        Console.WriteLine($"Order Status for Customer: {status}");
    }

    public static void LogisticsNotify(Customer customer, OrderStatus status) { Console.WriteLine($"Order status for Logistics : {status}"); }

}

public class Order
{
    #region Properties
    public int Id { get; set; }
    public Customer customer { get;  }

    public List<OrderItem> Items { get; } = new();
    public List<string> statusHistory { get; } = new();
    public OrderStatus Status { get; set; }
    #endregion

    #region Constructor
    public Order(int Id, Customer customer)
    {
        this.Id = Id; this.customer = customer; Status = OrderStatus.Placed; statusHistory.Add("Placed");
    }
        #endregion

    public NotifyStatus.StatusNotification NotifyStatus;

    #region Methods

    /// <summary>
    /// Safe Checking the new order status without Assigning it to the current order status
    /// </summary>
    /// <param name="newStatus"></param>
    public void ChangeStatus(OrderStatus newStatus)
    {
        if (Status == OrderStatus.Cancelled) { Console.WriteLine("Order is Cancelled."); return; }
        if (newStatus == OrderStatus.Shipped && Status != OrderStatus.Paid) { Console.WriteLine("Cannot Ship before payment. Please Pay to Proceed."); return; }
        if (newStatus == OrderStatus.Delivered && Status != OrderStatus.Shipped) { Console.WriteLine("Cannot Deliver before Order is Shipped."); return; }

        Status = newStatus;
        statusHistory.Add($"{newStatus}");
        NotifyStatus?.Invoke(customer, newStatus);
    }
    
    /// <summary>
    /// To calculate the total Amount of the order.
    /// </summary>
    /// <returns></returns>
    public decimal TotaLAmount()
        {
            decimal total = 0;

            foreach(OrderItem i in Items)
            {
                total += i.Total;
            }
            return total;
        }
        
    #endregion



}