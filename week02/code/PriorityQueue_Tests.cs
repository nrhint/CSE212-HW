using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Test adding items to the queue then getting them back
    // Expected Result: The results should be in the priority order.
    // Defect(s) Found: The Dequeue was not actually removing them from the list
    public void TestPriorityQueue_1()
    {
        //Reuse the person:
        var high_priority = new Person("hp", 10);
        var low_priority = new Person("lp", 4);
        var med_priority = new Person("mp", 7);
        var med_priority2 = new Person("mp", 7);

        Person[] expectedResult = [high_priority, med_priority2, med_priority, low_priority];
        
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(med_priority2.Name, 7);
        priorityQueue.Enqueue(high_priority.Name, 10);
        priorityQueue.Enqueue(low_priority.Name, 4);
        priorityQueue.Enqueue(med_priority.Name, 7);

        int i = 0;
        while (priorityQueue.Length > 0) {
            if (i >= expectedResult.Length) {
                Assert.Fail("Queue should have run out of items at this point");
            }
            var person = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResult[i].Name, person);
            i++;
        }
    }

    [TestMethod]
    // Scenario: Test what happens if the list is empty
    // Expected Result: Exception with good message
    // Defect(s) Found: 
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        try {
            priorityQueue.Dequeue();
            Assert.Fail("Error should have been raised");
        } catch (InvalidOperationException e) {
            Assert.AreEqual(e.Message, "The queue is empty.");
        } catch (Exception e) {
            Assert.Fail("Unexpected error caught. It should have been an InvalidOperationException");
        }
    }

    // Add more test cases as needed below.
}
