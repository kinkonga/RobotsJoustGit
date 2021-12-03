using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class boutontest : MonoBehaviour
{
    [SerializeField] GameObject ui_canvas;
    GraphicRaycaster ui_raycaster;

    PointerEventData click_data;
    List<RaycastResult> click_results;

    PointerEventData position_data;
    List<RaycastResult> position_results;

    private void Start()
    {
        ui_raycaster = ui_canvas.GetComponent<GraphicRaycaster>();
        click_data = new PointerEventData(EventSystem.current);
        click_results = new List<RaycastResult>();
        position_data = new PointerEventData(EventSystem.current);
        position_results = new List<RaycastResult>();
    }
    private void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Debug.Log("click");
            GetUIElementsClicked();
        }
        GetUIElements();
    }

    void GetUIElementsClicked()
    {
        click_data.position = Mouse.current.position.ReadValue();
        click_results.Clear();

        ui_raycaster.Raycast(click_data, click_results);
        foreach(RaycastResult result in click_results)
        {
            GameObject ui_element = result.gameObject; 
            Debug.Log("click "+ui_element.name);
            switch (ui_element.name)
            {
                case "Restart":
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
                case "Exit":
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }
    }
    void GetUIElements()
    {
        position_data.position = Mouse.current.position.ReadValue();
        position_results.Clear();

        ui_raycaster.Raycast(position_data, position_results);
        foreach (RaycastResult result in position_results)
        {
            GameObject ui_element = result.gameObject;
            ui_element.GetComponent<BumpColor>().ActiveBump();
        }
    }
}
