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
    public TextMeshPro player1LifeLabel;
    public TextMeshPro player2LifeLabel;


    //REFERENCE
    public List<Entity> listEntities = new List<Entity>();
    TileMap map;
    PlayerAction inputActions;
    
    //STATES
    bool isPlaying = false;

    //INIT
    private void Awake()
    {
        map = GetComponent<TileMap>();
        
        createPlayer(ref playerOne, 3, 3, 0);
        createPlayer(ref playerTwo, 6, 6, 180);
        inputActions = new PlayerAction();
        player1Label.text = "";
        player2Label.text = "";
        playerOne.setLifeLabel(ref player1LifeLabel);
        playerTwo.setLifeLabel(ref player2LifeLabel);
        player1LifeLabel.text = playerOne.Life.ToString();
        player2LifeLabel.text = playerTwo.Life.ToString();
    }
    private void OnEnable()
    {
        inputActions.Player1.Move.performed += AddActionPlayerOne;
        inputActions.Player1.Rotate.performed += AddActionPlayerOne;
        inputActions.Player1.Shoot.performed += AddActionPlayerOne;
        inputActions.Player2.Move.performed += AddActionPlayerTwo;
        inputActions.Player2.Rotate.performed += AddActionPlayerTwo;
        inputActions.Player2.Shoot.performed += AddActionPlayerTwo;
        inputActions.Player1.Move.Enable();
        inputActions.Player1.Rotate.Enable();
        inputActions.Player1.Shoot.Enable();
        inputActions.Player2.Move.Enable();
        inputActions.Player2.Rotate.Enable();
        inputActions.Player2.Shoot.Enable();

    }
    private void OnDisable()
    {
        inputActions.Player1.Move.Disable();
        inputActions.Player1.Rotate.Disable();
        inputActions.Player1.Shoot.Disable();
        inputActions.Player2.Move.Disable();
        inputActions.Player2.Rotate.Disable();
        inputActions.Player2.Shoot.Disable();


    }
    
    //INPUTS ACTION
    private void AddActionPlayerOne(InputAction.CallbackContext obj)
    {
        //ADD ACTION
        if (playerOne.NumberOfActions > playerOne.actionList.Count && !isPlaying)
        {
            //FORWARD
            if (inputActions.Player1.Move.ReadValue<float>() != 0)
            {
                Debug.Log("AddForward " + inputActions.Player1.Move.ReadValue<float>());
                playerOne.actionList.Add(new Foward());
                player1Label.text += "M";
            }
            //ROTATE
            if (inputActions.Player1.Rotate.ReadValue<float>() != 0)
            {
                Debug.Log("AddRotate " + inputActions.Player1.Rotate.ReadValue<float>());
                playerOne.actionList.Add(new Rotate(inputActions.Player1.Rotate.ReadValue<float>()));
                player1Label.text += "R";
            }
            //ROTATE
            if (inputActions.Player1.Shoot.ReadValue<float>() != 0)
            {
                Debug.Log("AddShoot " + inputActions.Player1.Shoot.ReadValue<float>());
                playerOne.actionList.Add(new Shoot());
                player1Label.text += "S";
            }
        }
        if (playerOne.NumberOfActions == playerOne.actionList.Count && !isPlaying && playerTwo.NumberOfActions == playerTwo.actionList.Count)
        {
            StartCoroutine(PlayAction());
        }
            
    }
    private void AddActionPlayerTwo(InputAction.CallbackContext obj)
    {
        //ADD ACTION
        if (playerTwo.NumberOfActions > playerTwo.actionList.Count && !isPlaying)
        {
            //FORWARD
            if (inputActions.Player2.Move.ReadValue<float>() != 0)
            {
                Debug.Log("AddForward " + inputActions.Player2.Move.ReadValue<float>());
                playerTwo.actionList.Add(new Foward());
                player2Label.text += "M";
            }
            //ROTATE
            if (inputActions.Player2.Rotate.ReadValue<float>() != 0)
            {
                Debug.Log("AddRotate " + inputActions.Player2.Rotate.ReadValue<float>());
                playerTwo.actionList.Add(new Rotate(inputActions.Player2.Rotate.ReadValue<float>()));
                player2Label.text += "R";
            }
            //ROTATE
            if (inputActions.Player2.Shoot.ReadValue<float>() != 0)
            {
                Debug.Log("AddShoot " + inputActions.Player2.Shoot.ReadValue<float>());
                playerTwo.actionList.Add(new Shoot());
                player2Label.text += "S";
            }
        }
        if (playerTwo.NumberOfActions == playerTwo.actionList.Count && !isPlaying && playerOne.NumberOfActions == playerOne.actionList.Count)
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
        for(int i = 0; playerOne.NumberOfActions > i; i++)
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
        playerOne.actionList.Clear();
        playerTwo.actionList.Clear();
        player1Label.text = "";
        player2Label.text = "";  
    }

    //METHODE
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
