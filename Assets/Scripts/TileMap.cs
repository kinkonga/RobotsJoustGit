using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] List<GameObject> tiles;
    [SerializeField] Vector2Int mapSize = new Vector2Int(10, 10);
    [Header("Random Map")]
    [SerializeField] int nbrWall = 10;
    [SerializeField] int nbrBonus = 2;
    [Header("Text Map")]
    [SerializeField] TextAsset mapText;
    [Header("Tiles")]
    [SerializeField] List<GameObject> walls;
    [SerializeField] List<GameObject> bonusTile;
    [SerializeField] List<GameObject> bonus;
    [SerializeField] List<GameObject> waterTile;

    

    public GameObject[,] tileMap;
    List<Item> listItem = new List<Item>();

    char[,] textTileMap;

    private void Awake()
    {
        tileMap = new GameObject[mapSize.x, mapSize.y];
        textTileMap = new char[mapSize.x, mapSize.y];

        //CreateMap();
        Debug.Log("OK -> CreateMap");
       
        CreateTextMap();
       
        Debug.Log("OK -> CreateItem");
    }


    private void CreateMap()
    {

        GameObject nt;

        for (int i = 0; mapSize.x > i; i++)
        {
            for (int j = 0; mapSize.y > j; j++)
            {

                nt = Instantiate(tiles[Random.Range(0, tiles.Count)]);
                nt.transform.position = new Vector3(i, 0, j);
                nt.transform.parent = transform;
                nt.transform.name = "(" + i + "," + j + ")";
                tileMap[i, j] = nt;

            }
        }
        CreateItems();
    }
    private void CreateTextMap()
    {
        LoadTextMap();

        GameObject nt;

        for (int i = 0; mapSize.x > i; i++)
        {
            for (int j = 0; mapSize.y > j; j++)
            {
                switch (textTileMap[i, j])
                {
                    case '_': //Sol
                        nt = CreateGround(i, j);
                        break;
                    case 'w': //Sol + MUR
                        nt = CreateGround(i, j);
                        listItem.Add(new Item(new Vector2(i,j), "Wall"));
                        break;
                    case '.': //Water
                        listItem.Add(new Item(new Vector2(i, j), "Water"));
                        break;
                    case 'b': //Bonus
                        listItem.Add(new Item(new Vector2(i, j), "Bonus"));
                        break;
                    case '1':
                        nt = CreateGround(i, j);
                        listItem.Add(new Item(new Vector2(i, j), "Player1"));
                        break;
                    case '2':
                        nt = CreateGround(i, j);
                        listItem.Add(new Item(new Vector2(i, j), "Player2"));
                        break;
                }

            }
        }
        GameObject go;
        foreach (Item i in listItem)
        {
            switch (i.Id)
            {
                case "Wall":
                    go = Instantiate(walls[0]);
                    go.transform.position = new Vector3(i.Pos.x, 0, i.Pos.y);
                    go.transform.parent = transform;
                    break;
                case "Bonus":
                    go = Instantiate(bonusTile[Random.Range(0, bonusTile.Count)]);
                    go.transform.position = new Vector3(i.Pos.x, 0, i.Pos.y);
                    go.transform.parent = transform;
                    break;
                case "Water":
                    go = Instantiate(waterTile[Random.Range(0, waterTile.Count)]);
                    go.transform.position = new Vector3(i.Pos.x, 0, i.Pos.y);
                    go.transform.parent = transform;
                    break;
                default:
                    break;
            }
        }
    }

    private GameObject CreateGround(int i, int j)
    {
        GameObject nt = Instantiate(tiles[Random.Range(0, tiles.Count)]);
        nt.transform.position = new Vector3(i, 0, j);
        nt.transform.parent = transform;
        nt.transform.name = "(" + i + "," + j + ")";
        return nt;
    }
    private void CreateItems()
    {
        PlanRandomItem(listItem, nbrWall, "Wall");
        PlanRandomItem(listItem, nbrBonus, "Bonus");

        GameObject go;
        foreach (Item i in listItem)
        {
            switch (i.Id)
            {
                case "Wall":
                    go = Instantiate(walls[0]);
                    go.transform.position = new Vector3(i.Pos.x, 0, i.Pos.y);
                    go.transform.parent = transform;
                    break;
                case "Bonus":
                    go = Instantiate(bonus[Random.Range(0, bonus.Count)]);
                    go.transform.position = new Vector3(i.Pos.x, 0, i.Pos.y);
                    go.transform.parent = transform;
                    break;
                default:
                    break;
            }
        }
    }
    private void LoadTextMap()
    {
        string[] rows = Regex.Split(mapText.text, "\r\n|\r|\n");
        int indexI = 0;
        int indexJ = 0;
        foreach (string row in rows)
        {
            string tmp = " ";
            foreach (char c in row)
            {

                tmp += c + "/(" + indexI + "-" + indexJ + ") ";
                textTileMap[indexI, indexJ] = c;
                indexJ++;
            }
            indexI++;
            indexJ = 0;
            Debug.Log(tmp);
        }
    }

    private void PlanRandomItem(List<Item> list, int nbr, string id)
    {
        for (int i = 0; i < nbr; i++)
        {
            Vector2 tmp = new Vector2(Random.Range(0, mapSize.x), Random.Range(0, mapSize.y));
            if (!isAlreadyUse(tmp, list))
            {
                list.Add(new Item(tmp,id));
                list[i].Id = id;
            } 
                
            else 
                i--;
        }
    }
    private bool isAlreadyUse(Vector2 tmp, List<Item> list)
    {
        foreach (Item i in list)
        {
            if (tmp == i.Pos) return true;
        }
        return false;
    }

    public Vector2Int MapSize { get => mapSize; set => mapSize = value; }
    public List<GameObject> Bonus { get => bonus; set => bonus = value; }

    public GameObject GetBonus(int i)
    {
        return Bonus[i];
    }

    public Vector2 getPlayerStartPosition(string player)
    {
        foreach(Item i in listItem)
        {
            if(i.Id == player)
            {
                return i.Pos;
            }
        }
        Debug.Log("Player position not found");
        return Vector2.zero;
    }
}

public class Item
{
    Vector2 pos;
    string id;
    public Item(Vector2 p, string i)
    {
        pos = p;
        id = i;
    }
    

    public Vector2 Pos { get => pos; set => pos = value; }
    public string Id { get => id; set => id = value; }
}
