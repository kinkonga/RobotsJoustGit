using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    [SerializeField] List<GameObject> tiles;
    [SerializeField] Vector2Int mapSize = new Vector2Int(10, 10);
    [SerializeField] int nbrWall = 10;
    [SerializeField] List<GameObject> walls;
    [SerializeField] int nbrBonus = 2;
    [SerializeField] List<GameObject> bonus;
    public GameObject[,] tileMap;
    List<Vector2> listWall = new List<Vector2>();
    List<Vector2> listBonus = new List<Vector2>();
    
    private void Awake()
    {
        tileMap = new GameObject[mapSize.x, mapSize.y];

        PlanRandomWall();
        PlanRandomBonus();
        CreateMap();

        DrawBonus();
        DrawWalls();
    }

    private void DrawWalls()
    {
        GameObject w;
        foreach (Vector2 v in listWall)
        {
            w = Instantiate(walls[0]);
            w.transform.position = new Vector3(v.x, 0, v.y);
            w.transform.parent = transform;
        }
    }

    private void DrawBonus()
    {
        GameObject b;
        foreach (Vector2 v in listBonus)
        {
            b = Instantiate(bonus[Random.Range(0, bonus.Count)]);
            b.transform.position = new Vector3(v.x, 0, v.y);
            b.transform.parent = transform;
        }
    }

    private void CreateMap()
    {

        GameObject nt;

        for (int i = 0; mapSize.x > i; i++)
        {
            for (int j = 0; mapSize.y > j; j++)
            {


                Debug.Log("CreateTile : [" + i + "," + j + "]");
                nt = Instantiate(tiles[Random.Range(0,tiles.Count)]);
                nt.transform.position = new Vector3(i, 0, j);
                nt.transform.parent = transform;
                nt.transform.name = "(" + i + "," + j + ")";
                tileMap[i, j] = nt;

            }
        }
    }

    private void PlanRandomWall()
    {
        for (int i = 0; i < nbrWall; i++)
        {
            listWall.Add(new Vector2(Random.Range(0, mapSize.x), Random.Range(0, mapSize.y)));
        }
    }
    private void PlanRandomBonus()
    {
        for (int i = 0; i < nbrBonus; i++)
        {
            listBonus.Add(new Vector2(Random.Range(0, mapSize.x), Random.Range(0, mapSize.y)));
        }
    }

    public Vector2Int MapSize { get => mapSize; set => mapSize = value; }

}
