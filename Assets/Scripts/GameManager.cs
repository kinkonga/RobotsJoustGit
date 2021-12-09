using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{

    //SERIALIZE FIELD
    [SerializeField] Player player1;
    [SerializeField] Player player2;
    [SerializeField] UiSliderBar player1LifeSlider;
    [SerializeField] UiSliderBar player2LifeSlider;
    [SerializeField] UiSliderBar player1EnergySlider;
    [SerializeField] UiSliderBar player2EnergySlider;
    [SerializeField] UIActionBars2D player1ActionBars;
    [SerializeField] UIActionBars2D player2ActionBars;
    [SerializeField] ActionPool player1ActionPool;
    [SerializeField] ActionPool player2ActionPool;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject menuPanel;

    


    //REFERENCE
    public List<Entity> listEntities = new List<Entity>();
    TileMap map;
    ActionHandler actionHandler;
    WeaponsManager weaponsManager;

    //STATES
    bool isTimer = false;
    int timer = 5;
    float lastTimer = 0;
    bool isPlaying = false;
    int activeAction = 0;
    int nbrOfTurn = 0;
    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }


    //INIT
    private void Awake()
    {
        //Mise en Place des references 
        weaponsManager = GetComponent<WeaponsManager>();
        actionHandler = GetComponent<ActionHandler>();
        map = GetComponent<TileMap>();

        //Creation des joueurs

        menuPanel.SetActive(false);
        
        createPlayer(ref player1, map.getPlayerStartPosition("Player1"), 0);
        createPlayer(ref player2, map.getPlayerStartPosition("Player2"), 180);

        

        player1.setLabels(ref player1ActionBars, ref actionHandler, ref player1ActionPool, ref player1LifeSlider, ref player1EnergySlider);
        player2.setLabels(ref player2ActionBars, ref actionHandler, ref player2ActionPool, ref player2LifeSlider, ref player2EnergySlider);

        GameObject[] bt = GameObject.FindGameObjectsWithTag("BonusTiles");
        for (int i = 0; bt.Length > i; i++)
        {
            bt[i].GetComponent<BonusTile>().Spawn(nbrOfTurn);
        }

    }
    private void Start()
    {
        player1.giveWeapon(weaponsManager.getWeapon(0));
        player1.giveWeapon(weaponsManager.getWeapon(1));
        player2.giveWeapon(weaponsManager.getWeapon(0));
        player2.giveWeapon(weaponsManager.getWeapon(1));

        player1.Print();
        player2.Print();
    }

    private void Update()
    {
        if (!isTimer && !isPlaying)
        {
            if (player1.actionList.Count == player1.NumberOfActions || player2.actionList.Count == player2.NumberOfActions)
            {
                isTimer = true;
                timerText.text = timer.ToString();
                lastTimer = Time.realtimeSinceStartup;
            }
        }
        if (isTimer)
        {
            if(lastTimer + 1 <= Time.realtimeSinceStartup)
            {
                timer--;
                lastTimer = Time.realtimeSinceStartup;
                timerText.text = timer.ToString();
            }
            if(timer == 0)
            {
                LaunchPlay();
            }
        }
        

        //Si les deux joueurs ont prevu ttes leurs action -> Lance la phase d'action
        if (player1.IsReady && player2.IsReady && !isPlaying)
        {
            LaunchPlay();
        }

        //Si il n'y a pas d'action en cours faire la prochaine action.
        if (!player1.IsInAction && !player2.IsInAction && isPlaying)
        {

            Action a = actionHandler.GetAction(activeAction);
            Debug.Log("ACTIVE ACTION : " + activeAction);
            a.Print();
            a.Player.DoAction(a);

            //si il y a encore des actions à faire
            if(activeAction < actionHandler.Size()-1)
            {
                activeAction++;
            }
            //fin de phase d'action
            else
            {
                OutPlay();
            }
        }
    }

    private void OutPlay()
    {
        isPlaying = false;
        Debug.Log("----- OUT PLAYING -----");

        LifeCheck();
        player1.turnRefuel();
        player2.turnRefuel();
        LabelReset();
        nbrOfTurn++;
    }

    private void LaunchPlay()
    {
        actionHandler.Print();
        isPlaying = true;
        Debug.Log("----- IS PLAYING ----- TURN : " + nbrOfTurn);
        ResetTimer();
    }

    private void ResetTimer()
    {
        isTimer = false;
        timer = 5;
        timerText.text = "--";
    }


    //METHODE
    private void LifeCheck()
    {
        foreach(Movable e in listEntities)
        {
            if(e.Life <= 0)
            {
                Destroy(e.gameObject);
            }
        }
    }
    private void LabelReset()
    {
        activeAction = 0;
        actionHandler.ClearAction();
        player1.ResetLabel();
        player2.ResetLabel();

        GameObject[] bt = GameObject.FindGameObjectsWithTag("BonusTiles");
        for(int i = 0;bt.Length > i;i++)
        {
            bt[i].GetComponent<BonusTile>().UpdateState(nbrOfTurn);
        }
        
    }
    private void createPlayer(ref Player player, Vector2 pos, float r)
    {
        player = Instantiate(player);
        player.transform.parent = transform;
        player.transform.position = new Vector3(pos.x, 0, pos.y);
        player.transform.rotation = Quaternion.Euler(new Vector3(0, r, 0));
        player.TargetPosition = player.transform.position;
        player.TargetRotation = player.transform.rotation;
        listEntities.Add(player);
        
    }

    void OnMenu()
    {
        Debug.Log("menu hid");
        menuPanel.SetActive(!menuPanel.active);
    }

    

}
