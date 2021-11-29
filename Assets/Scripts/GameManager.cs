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
    [SerializeField] TextMeshProUGUI player1Life;
    [SerializeField] TextMeshProUGUI player2Life;
    [SerializeField] UIActionBars2D player1ActionBars;
    [SerializeField] UIActionBars2D player2ActionBars;
    [SerializeField] ActionPool player1ActionPool;
    [SerializeField] ActionPool player2ActionPool;
    [SerializeField] TextMeshProUGUI timerText;

    int nbrOfTurn;


    //REFERENCE
    public List<Entity> listEntities = new List<Entity>();
    TileMap map;
    ActionHandler actionHandler;

    //STATES
    bool isTimer = false;
    int timer = 5;
    float lastTimer = 0;
    bool isPlaying = false;
    int activeAction = 0;


    //INIT
    private void Awake()
    {
        //Mise en Place des references 
        actionHandler = GetComponent<ActionHandler>();
        map = GetComponent<TileMap>();
        
        //Creation des joueurs
        
        createPlayer(ref player1, map.getPlayerStartPosition("Player1"), 0);
        createPlayer(ref player2, map.getPlayerStartPosition("Player2"), 180);
        
        player1.setLabels(ref player1Life, ref player1ActionBars, ref actionHandler, ref player1ActionPool);
        player2.setLabels(ref player2Life, ref player2ActionBars, ref actionHandler, ref player2ActionPool);
      
    }
    

    private void Update()
    {
        if (!isTimer && !isPlaying)
        {
            if (player1.actionList.Count == player1.NumberOfActions || player2.actionList.Count == player2.NumberOfActions)
            {
                //launch Timer
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
                isTimer = false;
                timer = 5;
                timerText.text = "--";
                isPlaying = true;
            }
        }
       
        //Si les deux joueurs ont prevu ttes leurs action -> Lance la phase d'action
        if(actionHandler.Size() == player1.NumberOfActions + player2.NumberOfActions && !isPlaying)
        {
            actionHandler.Print();
            isPlaying = true;
            Debug.Log("----- IS PLAYING -----");
        }

        //Si il n'y a pas d'action en cours faire la prochaine action.
        if(!player1.IsInAction && !player2.IsInAction && isPlaying)
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
                isPlaying = false;
                Debug.Log("----- OUT PLAYING -----");
                activeAction = 0;
                actionHandler.ClearAction();
                LabelReset();
                LifeCheck();
                nbrOfTurn++;
            }
        }
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
        player1.ResetLabel();
        player2.ResetLabel();
        
        
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

}
