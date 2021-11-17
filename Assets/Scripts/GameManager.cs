using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{

    //SERIALIZE FIELD
    [SerializeField] Player playerOne;
    [SerializeField] Player playerTwo;
    [SerializeField] TextMeshPro player1Label;
    [SerializeField] TextMeshPro player2Label;
    [SerializeField] TextMeshPro player1LifeLabel;
    [SerializeField] TextMeshPro player2LifeLabel;


    //REFERENCE
    public List<Entity> listEntities = new List<Entity>();
    TileMap map;
    //PlayerAction inputActions;
    
    //STATES
    bool isPlaying = false;

    //INIT
    private void Awake()
    {
        map = GetComponent<TileMap>();
        
        createPlayer(ref playerOne, 3, 3, 0);
        createPlayer(ref playerTwo, 6, 6, 180);
        
        playerOne.setLabels(ref player1LifeLabel, ref player1Label);
        playerTwo.setLabels(ref player2LifeLabel, ref player2Label);
      
    }
    

    private void Update()
    {
        if (playerOne.NumberOfActions == playerOne.actionList.Count
            && playerTwo.NumberOfActions == playerTwo.actionList.Count 
            && !isPlaying )
        {
            StartCoroutine(PlayAction());
        }
    }
    
    //PLAY ACTION
    private IEnumerator PlayAction()
    {
        //PLAY ACTION
        isPlaying = true;
        Debug.Log("---------- IS PLAYING ----------");
        for (int i = 0; playerOne.NumberOfActions > i; i++)
        {
            playerOne.DoAction(playerOne.actionList[i]);
            playerTwo.DoAction(playerTwo.actionList[i]);

            while (playerOne.IsInAction || playerTwo.IsInAction)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        isPlaying = false;
        Debug.Log("---------- STOP PLAYING ----------");
        LabelReset();
        LifeCheck();
    }



    //METHODE
    private void LifeCheck()
    {
        foreach(Entity e in listEntities)
        {
            if(e.Life <= 0)
            {
                Destroy(e.gameObject);
            }
        }
    }
    private void LabelReset()
    {
        playerOne.actionList.Clear();
        playerTwo.actionList.Clear();
        player1Label.text = "";
        player2Label.text = "";
    }
    private void createPlayer(ref Player player, float x, float z, float r)
    {
        player = Instantiate(player);
        player.transform.parent = transform;
        player.transform.position = new Vector3(x, 0, z);
        player.transform.rotation = Quaternion.Euler(new Vector3(0, r, 0));
        player.TargetPosition = player.transform.position;
        player.TargetRotation = player.transform.rotation;
        listEntities.Add(player);

    }

}
