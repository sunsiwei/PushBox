using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
    public static int[,] mapCfg = new int[,]{
                                                {0,0,0,0},
                                                {0,0,1,0},
                                                {0,0,2,0},
                                                {0,0,1,0},
                                                {0,0,1,0}
                                                };
    public static int[,] boxCfg = new int[,]{
                                                {0,0,0,0},
                                                {0,0,0,0},
                                                {0,0,0,0},
                                                {0,1,0,0},
                                                {0,0,0,0}
                                                };

    Player player;

	// Use this for initialization
	void Start () {
        Map map = new Map(mapCfg, boxCfg);
        player = new Player(0, 0);
        player.Map = map;
        map.ToString();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.Move(new int[] { -1, 0 });
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player.Move(new int[] { 1, 0 });
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.Move(new int[] { 0, -1 });
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.Move(new int[] { 0, 1 });
        }
    }

}
