using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fondblackscript : MonoBehaviour
{
    float color_value = 1f;
    public Image fond;
    void Update()
    {
        //diminue petit à petit l'opacité
        Color tmp = fond.color;
        tmp.a = color_value;
        fond.color = tmp;
        color_value = color_value * 0.95f; //valeur
    }
}
