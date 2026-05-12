/// <summary>
/// This queue is circular.  When people are added via AddPerson, then they are added to the 
/// back of the queue (per FIFO rules).  When GetNextPerson is called, the next person
/// in the queue is saved to be returned and then they are placed back into the back of the queue.  Thus,
/// each person stays in the queue and is given turns.  When a person is added to the queue, 
/// a turns parameter is provided to identify how many turns they will be given.  If the turns is 0 or
/// less than they will stay in the queue forever.  If a person is out of turns then they will 
/// not be added back into the queue.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add new people to the queue with a name and number of turns
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        
        // Since PersonQueue is LIFO (Enqueue adds to front), we need to
        // rebuild the queue each time to maintain FIFO order.
        var tempList = new List<Person>();
        
        // Save all existing items
        while (!_people.IsEmpty())
        {
            tempList.Add(_people.Dequeue());
        }
        
        // Add the new person to the END of the list
        tempList.Add(person);
        
        // Rebuild in reverse order (because Enqueue adds to front)
        for (int i = tempList.Count - 1; i >= 0; i--)
        {
            _people.Enqueue(tempList[i]);
        }
    }

    /// <summary>
    /// Get the next person in the queue and return them. The person should
    /// go to the back of the queue again unless the turns variable shows that they 
    /// have no more turns left.  Note that a turns value of 0 or less means the 
    /// person has an infinite number of turns.  An error exception is thrown 
    /// if the queue is empty.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }
        
        Person person = _people.Dequeue();
        
        // Check for infinite turns (turns <= 0)
        if (person.Turns <= 0)
        {
            // Re-add infinite turns person to the queue
            // Need to rebuild to maintain order
            var tempList = new List<Person>();
            while (!_people.IsEmpty())
            {
                tempList.Add(_people.Dequeue());
            }
            tempList.Add(person);
            for (int i = tempList.Count - 1; i >= 0; i--)
            {
                _people.Enqueue(tempList[i]);
            }
        }
        // Check if person has more than 1 turn left
        else if (person.Turns > 1)
        {
            person.Turns -= 1;
            // Re-add with updated turns
            var tempList = new List<Person>();
            while (!_people.IsEmpty())
            {
                tempList.Add(_people.Dequeue());
            }
            tempList.Add(person);
            for (int i = tempList.Count - 1; i >= 0; i--)
            {
                _people.Enqueue(tempList[i]);
            }
        }
        // else Turns == 1: do NOT add back
        
        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}