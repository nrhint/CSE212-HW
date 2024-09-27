/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: A queue of 10 shall be created with items being able to be added and removed.
        // Expected Result: It shall enqueue and deque properly.
        // Defect(s) Found: 
        Console.WriteLine("Test 1");
        CustomerService customerService = new(-5);
        if (customerService._maxSize != 10) { throw new Exception();}
        customerService.AddNewCustomer();
        customerService.ServeCustomer();
        try {
            customerService.ServeCustomer();
            throw new Exception("This shuold have raised an exception");
        } catch (InvalidOperationException) {
            // Do nothing. This is expected
        } catch (Exception e) {
            throw new Exception("Raised the wrong exception");
        }

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Create a queue with a length that is not 10 and not <= 0
        // Expected Result: Entering in people will add them to the queue until it becomes overfilled
        // Defect(s) Found: Failed to raise error
        Console.WriteLine("Test 2");
        CustomerService customerService3 = new(3);
        if (customerService3._maxSize != 3) {throw new Exception();}
        customerService3.AddNewCustomer();
        customerService3.AddNewCustomer();
        customerService3.AddNewCustomer();
        try{
            customerService3.AddNewCustomer();
            throw new Exception("Did not raise an exception when making the queue too long");
        } catch (InvalidOperationException) {
            //Do nothing this is expected
        } catch (Exception e) {
            throw new Exception("Raised wrong exception");
        }

        Console.WriteLine("All tests passed!");
        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below
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
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            throw new InvalidOperationException("The maximum number of Customers is already in the queue... What kind of a service do you have???");
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
        if (_queue.Count() > 0) {
            var customer = _queue[0];
            _queue.RemoveAt(0);
            Console.WriteLine(customer);
        } else {
            Console.WriteLine("Can not serve a customer. There are none left. Take a break?");
            throw new InvalidOperationException("Can not serve a customer. There are none left. Take a break?");
        }
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