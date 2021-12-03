using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class AppQuit : MonoBehaviour
{
    bool isCollide = false;
    PlayerAction inputActions;
    void OnExit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
