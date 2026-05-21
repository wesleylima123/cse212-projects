/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary will contain the
/// following mapping:
///
/// (x,y) : [left, right, up, down]
///
/// 'x' and 'y' are integers and represents locations in the maze.
/// 'left', 'right', 'up', and 'down' are boolean are represent valid directions
///
/// If a direction is false, then we can assume there is a wall in that direction.
/// If a direction is true, then we can proceed.  
///
/// If there is a wall, then throw an InvalidOperationException with the message "Can't go that way!".  If there is no wall,
/// then the 'currX' and 'currY' values should be changed.
/// </summary>
public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    /// <summary>
    /// Check to see if you can move left.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveLeft()
    {
        // Get the valid directions for the current position
        if (!_mazeMap.ContainsKey((_currX, _currY)))
        {
            throw new InvalidOperationException("Invalid position!");
        }
        
        bool[] directions = _mazeMap[(_currX, _currY)];
        
        // Check if left is allowed (index 0)
        if (directions[0]) // left
        {
            _currX--; // Move left decreases x coordinate
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    /// <summary>
    /// Check to see if you can move right.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveRight()
    {
        if (!_mazeMap.ContainsKey((_currX, _currY)))
        {
            throw new InvalidOperationException("Invalid position!");
        }
        
        bool[] directions = _mazeMap[(_currX, _currY)];
        
        // Check if right is allowed (index 1)
        if (directions[1]) // right
        {
            _currX++; // Move right increases x coordinate
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    /// <summary>
    /// Check to see if you can move up.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveUp()
    {
        if (!_mazeMap.ContainsKey((_currX, _currY)))
        {
            throw new InvalidOperationException("Invalid position!");
        }
        
        bool[] directions = _mazeMap[(_currX, _currY)];
        
        // Check if up is allowed (index 2)
        if (directions[2]) // up
        {
            _currY++; // Move up increases y coordinate (depending on coordinate system)
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    /// <summary>
    /// Check to see if you can move down.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveDown()
    {
        if (!_mazeMap.ContainsKey((_currX, _currY)))
        {
            throw new InvalidOperationException("Invalid position!");
        }
        
        bool[] directions = _mazeMap[(_currX, _currY)];
        
        // Check if down is allowed (index 3)
        if (directions[3]) // down
        {
            _currY--; // Move down decreases y coordinate
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}