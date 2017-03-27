using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map
{
    public enum TileState
    { 
        floor = 0,
        hole,
        obstacle,
    }

    int[,] list;
    BoxController boxCtl;

    public Map(int[,] mapList, int[,] boxList)
    {
        list = mapList;
        boxCtl = new BoxController(boxList, this);
    }

    public int GetTileState(int[] index)
    {
        return list[index[0], index[1]];
    }

    public void SetTileState(int[] index, int state)
    {
        list[index[0], index[1]] = state;
    }

    public bool MoveToDestination(int[] startIndex, int[] offset)
    {
        int[] desIndex = new int[] { startIndex[0] + offset[0], startIndex[1] + offset[1] };
        if (CheckBoundary(desIndex) == false)
            return false;
        int desTileState = GetTileState(desIndex);
        switch ((Map.TileState)desTileState)
        {
            case Map.TileState.floor:
            case Map.TileState.hole:
                if (boxCtl.HaveBox(desIndex))
                {
                    bool moveSuccess = boxCtl.MoveBoxToDestination(desIndex, offset);
                    if (moveSuccess == true)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return true;
                }
            case Map.TileState.obstacle:
                return false;
            default:
                return false;
        }
    }

    public bool CheckPass()
    {
        List<int[]> boxesIndex = boxCtl.GetBoxesIndex();
        foreach(int[] index in boxesIndex)
        {
            if (list[index[0], index[1]] != (int)TileState.hole)
            {
                return false;
            }
        }
        return true;
    }

    bool CheckBoundary(int[] index)
    {
        if (index[0] < 0 || index[0] >= list.GetLength(0))
            return false;
        if (index[1] < 0 || index[1] >= list.GetLength(1))
            return false;
        return true;
    }

    public void ToString()
    {
        for (int r = 0; r < list.GetLength(0); r++)
        {
            string str = r + " --- ";
            for (int c = 0; c < list.GetLength(1); c++)
            {
                str = str + list[r, c] + ", ";
            }
            Debug.Log(str);
        }
        Debug.Log("-------------------");
        boxCtl.ToString();
    }

    //public bool IsEmptyTile(int[] targetIndex)
    //{
    //    int targetState = GetTileState(targetIndex);
    //    switch ((Map.TileState)targetState)
    //    {
    //        case Map.TileState.floor:
    //        case Map.TileState.hole:
    //            if (boxCtl.HaveBox(targetIndex))
    //                return false;
    //            else
    //                return true;
    //        case Map.TileState.obstacle:
    //            return false;
    //        default:
    //            return false;
    //    }
    //}
}
