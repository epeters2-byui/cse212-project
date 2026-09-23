using System;
using System.Collections.Generic;

/// <summary>
/// Problem 4: Maze navigation using a dictionary-based maze map.
/// The maze is represented as a dictionary where:
//- Key: (x, y) coordinate tuple
// Value: bool array [left, right, up, down] indicating valid directions
// Coordinate system:
// Moving Left: decreases X coordinate
// Moving Right: increases X coordinate
// Moving Up: decreases Y coordinate
// Moving Down: increases Y coordinate
/// If a move is attempted in a direction with a wall (false), an exception is thrown.
/// </summary>
public class Maze
{
    private readonly Dictionary<(int, int), bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<(int, int), bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    // Move left if possible.
    // Index 0 in directions array = Left
    // Decreases X coordinate by 1
    
    public void MoveLeft()
    {
        var position = (_currX, _currY);
        
        if (!_mazeMap.ContainsKey(position))
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        
        bool[] directions = _mazeMap[position];
        
        // Index 0 = Left
        if (!directions[0])
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        
        _currX--;
    }

    // Move right if possible.
    // Index 1 in directions array = Right
    // Increases X coordinate by 1
    
    public void MoveRight()
    {
        var position = (_currX, _currY);
        
        if (!_mazeMap.ContainsKey(position))
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        
        bool[] directions = _mazeMap[position];
        
        // Index 1 = Right
        if (!directions[1])
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        
        _currX++;
    }

    
    // Move up if possible.
    // Index 2 in directions array = Up
    // Decreases Y coordinate by 1
    public void MoveUp()
    {
        var position = (_currX, _currY);
        
        if (!_mazeMap.ContainsKey(position))
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        
        bool[] directions = _mazeMap[position];
        
        // Index 2 = Up
        if (!directions[2])
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        
        _currY--;
    }

    // <summary>
    // Move down if possible.
    // Index 3 in directions array = Down
    // Increases Y coordinate by 1
    public void MoveDown()
    {
        var position = (_currX, _currY);
        
        if (!_mazeMap.ContainsKey(position))
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        
        bool[] directions = _mazeMap[position];
        
        // Index 3 = Down
        if (!directions[3])
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        
        _currY++;
    }

    // Returns the current position as a formatted string.
    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}