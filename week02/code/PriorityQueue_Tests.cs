using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    /// Scenario: Enqueue items with same priority, then dequeue all
    /// Expected Result: Items should be returned in FIFO order (First In, First Out)
    /// Defect(s) Found: Original Dequeue method had three bugs:
    ///  1. Loop condition "i < _queue.Count - 1" excluded the last item from priority comparison
    ///   2. Using ">=" found the LAST highest priority instead of the FIRST, violating FIFO for equal priorities
    ///   3. The item was never removed from the queue (missing _queue.RemoveAt call)
    ///After fixing all three issues, this test passes.
    public void TestPriorityQueue_SamePriority_FIFO()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 1);
        priorityQueue.Enqueue("Third", 1);
        
        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items with different priorities
    // Expected Result: Highest priority returned first, then next highest
    // Defect(s) Found: Same three bugs as above. The loop condition excluded the last item,
    // the comparison operator was incorrect, and RemoveAt was missing. After fixes, this test passes.
    public void TestPriorityQueue_DifferentPriorities_HighestFirst()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);
        
        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Multiple items share the same highest priority
    // Expected Result: The earliest added item with highest priority should be removed first (FIFO among highest priority)
    // Defect(s) Found: Using ">=" instead of ">" caused the LAST occurrence of the highest priority
    // to be selected instead of the FIRST. For items with equal priority (both 5), the second one
    // (index 2) was being removed before the first one (index 0), violating FIFO requirement.
    // Changing to ">" ensures the first highest priority is kept. This test now passes.
    public void TestPriorityQueue_SameHighestPriority_FIFOOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First Highest", 5);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("Second Highest", 5);
        priorityQueue.Enqueue("Low", 1);
        
        Assert.AreEqual("First Highest", priorityQueue.Dequeue());
        Assert.AreEqual("Second Highest", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    /// Scenario: Dequeue from an empty queue
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: No defects found. The exception handling in Dequeue correctly throws 
    // InvalidOperationException with the exact message "The queue is empty." This test passes.
    public void TestPriorityQueue_EmptyQueue_ThrowsException()
    {
        var priorityQueue = new PriorityQueue();
        
        var exception = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", exception.Message);
    }

    [TestMethod]
    /// Scenario: Enqueue and dequeue multiple times with mixed priorities
    // Expected Result: Queue handles repeated operations correctly
    // Defect(s) Found: The missing RemoveAt call caused the queue to never shrink, so items were never
    // actually removed from the list. After adding RemoveAt, items are properly removed and subsequent
    // operations work correctly. This test now passes.
    public void TestPriorityQueue_MixedOperations()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);
        
        Assert.AreEqual("B", priorityQueue.Dequeue());
        
        priorityQueue.Enqueue("D", 4);
        priorityQueue.Enqueue("E", 5);
        
        Assert.AreEqual("E", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    /// Scenario: Negative priority values (lower priority)
    // Expected Result: Higher numbers (even if negative) have higher priority
    // Defect(s) Found: Priority comparison works correctly with negative numbers, but the other bugs
    // (loop range excluded last item, FIFO violation with >=, missing RemoveAt) affected this test.
    // After fixing all three issues, this test passes.
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Lowest", -10);
        priorityQueue.Enqueue("Medium", 0);
        priorityQueue.Enqueue("Highest", 5);
        priorityQueue.Enqueue("Low", -5);
        
        Assert.AreEqual("Highest", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
        Assert.AreEqual("Lowest", priorityQueue.Dequeue());
    }

    [TestMethod]
    /// Scenario: Single item in queue
    // Expected Result: The single item will be returned and queue becomes empty
    // Defect(s) Found: The missing RemoveAt meant the single item was never removed from the queue.
    // After Dequeue returned the item, the queue still contained it, causing subsequent Dequeue calls
    // to return the same item again instead of throwing an exception. After adding RemoveAt,
    // this test passes.
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Only One", 10);
        
        Assert.AreEqual("Only One", priorityQueue.Dequeue());
        
        /// After dequeue, queue should be emptyand then next dequeue should throw
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }
}