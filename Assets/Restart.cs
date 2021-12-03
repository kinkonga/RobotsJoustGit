using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Activate()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
