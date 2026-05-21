public class Maze
{
    private readonly Dictionary<(int, int), bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<(int, int), bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    private bool[] GetCurrentDirections()
    {
        if (!_mazeMap.TryGetValue((_currX, _currY), out bool[]? directions))
        {
            throw new InvalidOperationException($"Invalid position! ({_currX}, {_currY}) not found in maze");
        }
        return directions;
    }

    public void MoveLeft()
    {
        var directions = GetCurrentDirections();
        if (!directions[0])
            throw new InvalidOperationException("Can't go that way!");
        _currX--;
    }

    public void MoveRight()
    {
        var directions = GetCurrentDirections();
        if (!directions[1])
            throw new InvalidOperationException("Can't go that way!");
        _currX++;
    }

    public void MoveUp()
    {
        var directions = GetCurrentDirections();
        if (!directions[2])
            throw new InvalidOperationException("Can't go that way!");
        _currY--;  // Up = decrease Y
    }

    public void MoveDown()
    {
        var directions = GetCurrentDirections();
        if (!directions[3])
            throw new InvalidOperationException("Can't go that way!");
        _currY++;  // Down = increase Y
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}