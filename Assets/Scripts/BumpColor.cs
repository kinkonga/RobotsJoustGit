using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BumpColor : MonoBehaviour
{
    [SerializeField]Color baseColor;
    [SerializeField]Color bumpColor;
    [SerializeField]float bumpSpeed;

    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.color = baseColor;
    }

    // Update is called once per frame
    void Update()
    {
        float tmpSpeed = bumpSpeed * Time.deltaTime;
        if(text.color != baseColor)
        {
            float rDif =  baseColor.r - bumpColor.r;
            float gDif =  baseColor.g - bumpColor.g;
            float bDif =  baseColor.b - bumpColor.b;
            float aDif =  baseColor.a - bumpColor.a;
            text.color += new Color(rDif* tmpSpeed,gDif* tmpSpeed,bDif* tmpSpeed, aDif * tmpSpeed);
        }
        //red
        if (bumpColor.r > baseColor.r && text.color.r < baseColor.r)
        {
            text.color = new Color(baseColor.r, text.color.g, text.color.b, text.color.a);
        }
        else if (bumpColor.r < baseColor.r && text.color.r > baseColor.r)
        {
            text.color = new Color(baseColor.r, text.color.g, text.color.b, text.color.a);
        }
        //green
        if (bumpColor.g > baseColor.g && text.color.g < baseColor.g)
        {
            text.color = new Color(text.color.r, baseColor.g, text.color.b, text.color.a);
        }
        else if (bumpColor.g < baseColor.g && text.color.g > baseColor.g)
        {
            text.color = new Color(text.color.r, baseColor.g, text.color.b, text.color.a);
        }
        //blue
        if (bumpColor.b > baseColor.b && text.color.b < baseColor.b)
        {
            text.color = new Color(text.color.r, text.color.g, baseColor.b, text.color.a);
        }
        else if (bumpColor.b < baseColor.b && text.color.b > baseColor.b)
        {
            text.color = new Color(text.color.r, text.color.g, baseColor.b, text.color.a);
        }
        //alpha
        if (bumpColor.a > baseColor.a && text.color.a < baseColor.a)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, baseColor.a);
        }
        else if (bumpColor.a < baseColor.a && text.color.a > baseColor.a)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, baseColor.a);
        }
    }

    public void ActiveBump()
    {
        text.color = bumpColor;
    }
}
