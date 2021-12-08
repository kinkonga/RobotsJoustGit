using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiSliderBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textCurrent;
    [SerializeField] TextMeshProUGUI textMax;
    [SerializeField] GameObject img_bar;

    float current = 10;
    float max = 10;

    public void setCurrent(int i)
    {
        current = i;
        textCurrent.text = i.ToString();
        img_bar.transform.localScale = new Vector3(current / max,1,1)  ;
    }
    public void setMax(int i)
    {
        max = i;
        textMax.text = i.ToString();
    }
}
