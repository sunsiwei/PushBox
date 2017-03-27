using UnityEngine;
using System.Collections;

public class Player
{
    Map map;
    
    Transform transform;
    int[] index;

    public Map Map
    {
        set { map = value; }
    }
    

    public Player(int row, int column)
    {
        index = new int[2] { row, column };
    }

    public void Move(int[] moveOffset)
    {
        bool success = map.MoveToDestination(index, moveOffset);
        if (success == true)
        {
            index = new int[] { index[0] + moveOffset[0], index[1] + moveOffset[1] };
        }
        else
        {
            Debug.Log("Player move failed.");
        }

        bool pass = map.CheckPass();

        if (pass)
            Debug.Log("pass.");
        map.ToString();
    }

}
