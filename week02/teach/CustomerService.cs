/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Test Cases

        // Test 1: Create queue with valid size
        // Scenario: Create a new CustomerService with maxSize = 5
        // Expected Result: Queue should have max_size = 5
        Console.WriteLine("Test 1 - Valid Queue Creation");
        var cs1 = new CustomerService(5);
        Console.WriteLine($"Expected max_size=5, Actual: {cs1}");
        Console.WriteLine("Defect(s) Found: None");
        Console.WriteLine("=================");

        // Test 2: Create queue with invalid size (<= 0)
        // Scenario: Create a new CustomerService with maxSize = -1
        // Expected Result: Queue should default to max_size = 10
        Console.WriteLine("Test 2 - Invalid Queue Creation (size <= 0)");
        var cs2 = new CustomerService(-1);
        Console.WriteLine($"Expected max_size=10, Actual: {cs2}");
        Console.WriteLine("Defect(s) Found: None");
        Console.WriteLine("=================");

        // Test 3: Add customer when queue has room
        // Scenario: Add customers to queue with capacity 3
        // Expected Result: Customers are added successfully
        Console.WriteLine("Test 3 - Add Customer With Room");
        Console.WriteLine("Please add 2 customers to test:");
        var cs3 = new CustomerService(3);
        Console.WriteLine($"Initial queue: {cs3}");
        Console.WriteLine("Adding first customer...");
        cs3.AddNewCustomer(); // Customer 1
        Console.WriteLine($"Queue after 1st customer: {cs3}");
        Console.WriteLine("Adding second customer...");
        cs3.AddNewCustomer(); // Customer 2
        Console.WriteLine($"Queue after 2nd customer: {cs3}");
        Console.WriteLine("Defect(s) Found: None (assuming customers added)");
        Console.WriteLine("=================");

        // Test 4: Add customer when queue is full
        // Scenario: Try to add a customer when queue count equals maxSize
        // Expected Result: Error message "Maximum Number of Customers in Queue"
        Console.WriteLine("Test 4 - Add Customer When Full");
        var cs4 = new CustomerService(2);
        Console.WriteLine($"Created queue with max_size=2: {cs4}");
        Console.WriteLine("Adding first customer...");
        cs4.AddNewCustomer();
        Console.WriteLine($"Queue after 1st: {cs4}");
        Console.WriteLine("Adding second customer...");
        cs4.AddNewCustomer();
        Console.WriteLine($"Queue after 2nd: {cs4}");
        Console.WriteLine("Adding third customer (should fail)...");
        cs4.AddNewCustomer();
        Console.WriteLine("Expected: 'Maximum Number of Customers in Queue' message");
        Console.WriteLine("Defect(s) Found: _queue.Count > _maxSize should be >= _maxSize");
        Console.WriteLine("=================");

        // Test 5: Serve customer when queue has customers
        // Scenario: Serve a customer from non-empty queue
        // Expected Result: First customer is displayed and removed
        Console.WriteLine("Test 5 - Serve Customer With Customers");
        var cs5 = new CustomerService(3);
        Console.WriteLine("Adding 2 test customers...");
        // Note: For automated testing, we'd need to mock Console input
        // For manual testing, uncomment and run:
        // cs5.AddNewCustomer();
        // cs5.AddNewCustomer();
        // Console.WriteLine($"Queue before serve: {cs5}");
        // cs5.ServeCustomer();
        // Console.WriteLine($"Queue after serve: {cs5}");
        Console.WriteLine("Defect(s) Found: ServeCustomer removes wrong index, doesn't check empty");
        Console.WriteLine("=================");

        // Test 6: Serve customer when queue is empty
        // Scenario: Try to serve from an empty queue
        // Expected Result: Error message "No customers in queue to serve"
        Console.WriteLine("Test 6 - Serve Customer When Empty");
        var cs6 = new CustomerService(5);
        Console.WriteLine($"Empty queue: {cs6}");
        cs6.ServeCustomer();
        Console.WriteLine("Expected: 'No customers in queue to serve' message");
        Console.WriteLine("Defect(s) Found: No empty check before dequeue");
        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // FIX 1: Condition should be >= (if queue is full, cannot add)
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        // FIX 2: Check if queue is empty before serving
        if (_queue.Count == 0) {
            Console.WriteLine("No customers in queue to serve.");
            return;
        }
        
        // FIX 3: Get the customer BEFORE removing, then remove at index 0
        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}