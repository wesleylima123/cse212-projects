using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue a single item and then dequeue it.
    // Expected Result: The dequeued value should be the same as the enqueued value.
    // Defect(s) Found: 
    public void TestPriorityQueue_EnqueueOneDequeueOne()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("TestValue", 5);
        
        var result = priorityQueue.Dequeue();
        
        Assert.AreEqual("TestValue", result);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with different priorities. Dequeue should return highest priority.
    // Expected Result: Item with priority 10 (highest) should be dequeued first.
    // Defect(s) Found: 
    public void TestPriorityQueue_DifferentPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High", 10);
        
        var result = priorityQueue.Dequeue();
        
        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with same priority. Dequeue should return first in (FIFO).
    // Expected Result: The first item enqueued with highest priority should be dequeued.
    // Defect(s) Found: 
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 10);
        priorityQueue.Enqueue("Second", 10);
        priorityQueue.Enqueue("Third", 5);
        
        // Should return "First" (first among highest priority = 10)
        var result1 = priorityQueue.Dequeue();
        Assert.AreEqual("First", result1);
        
        // Should return "Second" (remaining highest priority = 10)
        var result2 = priorityQueue.Dequeue();
        Assert.AreEqual("Second", result2);
        
        // Should return "Third" (priority = 5)
        var result3 = priorityQueue.Dequeue();
        Assert.AreEqual("Third", result3);
    }

    [TestMethod]
    // Scenario: Dequeue from empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: 
    public void TestPriorityQueue_DequeueFromEmpty()
    {
        var priorityQueue = new PriorityQueue();
        
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }

    [TestMethod]
    // Scenario: Enqueue items with negative priorities.
    // Expected Result: Higher priority (less negative) should be dequeued first.
    // Defect(s) Found: 
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Lowest", -10);
        priorityQueue.Enqueue("Higher", -5);
        
        // -5 is higher priority than -10
        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Higher", result);
    }

    [TestMethod]
    // Scenario: Enqueue items and dequeue multiple times to verify order.
    // Expected Result: Items dequeued in correct priority order.
    // Defect(s) Found: 
    public void TestPriorityQueue_MultipleDequeues()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Value1", 3);
        priorityQueue.Enqueue("Value2", 1);
        priorityQueue.Enqueue("Value3", 2);
        
        var first = priorityQueue.Dequeue();
        Assert.AreEqual("Value1", first); // Priority 3
        
        var second = priorityQueue.Dequeue();
        Assert.AreEqual("Value3", second); // Priority 2
        
        var third = priorityQueue.Dequeue();
        Assert.AreEqual("Value2", third); // Priority 1
    }
}