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
    [SerializeField] List<TextAsset> mapText;

    [Header("Tiles")]
    [SerializeField] List<GameObject> walls;
    [SerializeField] List<GameObject> bonusTile;
    [SerializeField] List<GameObject> bonus;
    

    

    public Tile[,] tileMap;
    List<Item> listItem = new List<Item>();

    char[,] textTileMap;

    private void Awake()
    {
        tileMap = new Tile[mapSize.x, mapSize.y];
        textTileMap = new char[mapSize.x, mapSize.y];

        //CreateMap();
        Debug.Log("OK -> CreateMap");
       
        CreateTextMap();
       
        Debug.Log("OK -> CreateItem");
    }


    private void CreateRandomMap()
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
                tileMap[i, j] = new Tile(new Vector2(i,j),true);

            }
        }
        CreateRandomItems();
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
                        CreateGround(i, j);
                        tileMap[i, j] = new Tile(new Vector2(i, j), true);
                        tileMap[i, j].Print();
                        break;
                    case 'w': //Sol + MUR
                        CreateGround(i, j);
                        tileMap[i, j] = new Tile(new Vector2(i, j), false);
                        listItem.Add(new Item(new Vector2(i,j), "Wall"));
                        tileMap[i, j].Print();
                        break;
                    case '.': //Water
                        tileMap[i, j] = new Tile(new Vector2(i, j), false);
                        listItem.Add(new Item(new Vector2(i, j), "Water"));
                        tileMap[i, j].Print();
                        break;
                    case 'h': //Health
                        tileMap[i, j] = new Tile(new Vector2(i, j), true);
                        listItem.Add(new Item(new Vector2(i, j), "Health"));
                        tileMap[i, j].Print();
                        break;
                    case 'e': //Energy
                        tileMap[i, j] = new Tile(new Vector2(i, j), true);
                        listItem.Add(new Item(new Vector2(i, j), "Energy"));
                        tileMap[i, j].Print();
                        break;
                    case '1':
                        nt = CreateGround(i, j);
                        tileMap[i, j] = new Tile(new Vector2(i, j), true);
                        listItem.Add(new Item(new Vector2(i, j), "Player1"));
                        tileMap[i, j].Print();
                        break;
                    case '2':
                        nt = CreateGround(i, j);
                        tileMap[i, j] = new Tile(new Vector2(i, j), true);
                        listItem.Add(new Item(new Vector2(i, j), "Player2"));
                        tileMap[i, j].Print();
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
                case "Health":
                    go = Instantiate(bonusTile[0]);
                    go.transform.position = new Vector3(i.Pos.x, 0, i.Pos.y);
                    go.transform.parent = transform;
                    break;
                case "Energy":
                    go = Instantiate(bonusTile[1]);
                    go.transform.position = new Vector3(i.Pos.x, 0, i.Pos.y);
                    go.transform.parent = transform;
                    break;
                case "Water":
                    
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
    private void CreateRandomItems()
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

        string[] rows = Regex.Split(mapText[Random.Range(0, mapText.Count)].text, "\r\n|\r|\n");
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

    public bool IsPassable(Vector3 v)
    {
        return tileMap[(int)v.x,(int)v.z].Passable;
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

public class Tile
{
    Vector2 pos;
    string id;
    bool passable;

    public Tile(Vector2 p, bool pass)
    {
        pos = p;
        passable = pass;
    }
    public void Print()
    {
        Debug.Log("Tile: " + Pos + " p=" + Passable);
    }
    public bool Passable { get => passable; set => passable = value; }
    public Vector2 Pos { get => pos; set => pos = value; }
    public string Id { get => id; set => id = value; }
}
