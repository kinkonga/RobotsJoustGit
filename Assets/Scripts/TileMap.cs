using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    [SerializeField] Vector2Int mapSize = new Vector2Int(10, 10);
    [SerializeField] GameObject tile;
    public GameObject[,] tileMap;

    private void Awake()
    {
        tileMap = new GameObject[mapSize.x, mapSize.y];
        createMap();
    }

    private void createMap()
    {
        for(int i = 0; mapSize.x > i; i++)
        {
            for(int j = 0; mapSize.y > j; j++)
            {
                Debug.Log("CreateTile : [" + i + "," + j + "]");
                GameObject nt = Instantiate(tile);
                nt.transform.position = new Vector3(i, 0, j);
                nt.transform.parent = transform;
                nt.transform.name = "(" + i + "," + j + ")";
                tileMap[i,j] = nt;
            }
        }
    }

    public Vector2Int MapSize { get => mapSize; set => mapSize = value; }

}
