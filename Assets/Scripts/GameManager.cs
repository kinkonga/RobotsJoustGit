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


    //REFERENCE
    TileMap map;

    PlayerAction inputActions;
    TextMeshPro actionLabel;


    //STATES
    bool isPlaying = false;


    private void Awake()
    {
        map = GetComponent<TileMap>();
        
        createPlayer(ref playerOne, 3, 3, 0);
        createPlayer(ref playerTwo, 6, 6, 180);
        inputActions = new PlayerAction();
        actionLabel = GetComponentInChildren<TextMeshPro>();
        actionLabel.text = "";
    }

    private void OnEnable()
    {
        inputActions.Basic.Move.performed += AddAction;
        inputActions.Basic.Rotate.performed += AddAction;
        inputActions.Basic.Shoot.performed += AddAction;
        inputActions.Basic.Move.Enable();
        inputActions.Basic.Rotate.Enable();
        inputActions.Basic.Shoot.Enable();

    }

    private void OnDisable()
    {
        inputActions.Basic.Move.Disable();
        inputActions.Basic.Rotate.Disable();
        inputActions.Basic.Shoot.Disable();

    }
     //INPUTS ACTION
    private void AddAction(InputAction.CallbackContext obj)
    {
        //ADD ACTION
        if (playerOne.NumberOfActions > playerOne.actionList.Count && !isPlaying)
        {
            //FORWARD
            if (inputActions.Basic.Move.ReadValue<float>() != 0)
            {
                Debug.Log("AddForward " + inputActions.Basic.Move.ReadValue<float>());
                playerOne.actionList.Add(new Foward());
                actionLabel.text += "M";
            }
            //ROTATE
            if (inputActions.Basic.Rotate.ReadValue<float>() != 0)
            {
                Debug.Log("AddRotate " + inputActions.Basic.Rotate.ReadValue<float>());
                playerOne.actionList.Add(new Rotate(inputActions.Basic.Rotate.ReadValue<float>()));
                actionLabel.text += "R";
            }
            //ROTATE
            if (inputActions.Basic.Shoot.ReadValue<float>() != 0)
            {
                Debug.Log("AddShoot " + inputActions.Basic.Shoot.ReadValue<float>());
                playerOne.actionList.Add(new Shoot());
                actionLabel.text += "S";
            }
        }
        if (playerOne.NumberOfActions == playerOne.actionList.Count && !isPlaying)
        {
            StartCoroutine(PlayAction());
        }
            
    }
    private IEnumerator PlayAction() 
    {
        //PLAY ACTION
        isPlaying = true;
        Debug.Log("---------- IS PLAYING ----------");
        foreach (Action a in playerOne.actionList)
        {
            //PLAY LOOP
            playerOne.DoAction(a);

            while (playerOne.IsInAction)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        isPlaying = false;
        Debug.Log("---------- STOP PLAYING ----------");
        playerOne.actionList.Clear();
        actionLabel.text = "";  
    }

    private void createPlayer(ref Player player, float x, float z, float r)
    {
        player = Instantiate(player);
        player.transform.parent = transform;
        player.transform.position = new Vector3(x, 0, z);
        player.transform.rotation = Quaternion.Euler(new Vector3(0, r, 0));
        player.TargetPosition = player.transform.position;
        player.TargetRotation = player.transform.rotation;

    }

}
