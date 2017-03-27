using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxController
{
    enum BoxState
    { 
        none = 0,
        box
    }

    Map map;
    int[,] boxes;

    public int[,] Boxes
    {
        get { return boxes; }
    }

    public BoxController(int[,] _boxes, Map _map)
    {
        boxes = _boxes;
        map = _map;
    }

    public bool HaveBox(int[] index)
    {
        int state = boxes[index[0], index[1]];
        if (state == (int)BoxState.none)
            return false;
        else
            return true;
    }

    public bool MoveBoxToDestination(int[] boxIndex, int[] offset)
    {
        if (HaveBox(boxIndex) == false)
            return false;
        int[] desIndex = new int[] { boxIndex[0] + offset[0], boxIndex[1] + offset[1] };
        if (CheckBoundary(desIndex) == false)
            return false;
        if (HaveBox(desIndex))
            return false;
        int desTileState = map.GetTileState(desIndex);
        switch ((Map.TileState)desTileState)
        {
            case Map.TileState.floor:
            case Map.TileState.hole:
                boxes[boxIndex[0], boxIndex[1]] = (int)BoxState.none;
                boxes[desIndex[0], desIndex[1]] = (int)BoxState.box;
                return true;
            case Map.TileState.obstacle:
                return false;
            default:
                return false;
        }
    }

    public List<int[]> GetBoxesIndex()
    {
        List<int[]> boxesIndex = new List<int[]>();
        for (int r = 0; r < boxes.GetLength(0); r++)
        {
            for (int c = 0; c < boxes.GetLength(1); c++)
            {
                if (boxes[r, c] == (int)BoxState.box)
                {
                    boxesIndex.Add(new int[]{r, c});
                }
            }
        }
        return boxesIndex;
    }

    bool CheckBoundary(int[] index)
    {
        if(index[0] < 0 || index[0] >= boxes.GetLength(0))
            return false;
        if(index[1] < 0 || index[1] >= boxes.GetLength(1))
            return false;
        return true;
    }

    public void ToString()
    {
        for (int r = 0; r < boxes.GetLength(0); r++)
        {
            string str = r + " --- ";
            for (int c = 0; c < boxes.GetLength(1); c++)
            {
                str = str + boxes[r, c] + ", ";
            }
            Debug.Log(str);
        }
    }
}
