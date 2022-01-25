using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Brick_Click : MonoBehaviour
{
    public SpriteRenderer _prefab; //ref du sprite de la brique
    public float multiplicator;

    //alpha
    float color_value = 1f;
    
    void Update()
    {
        //diminue petit à petit l'opacité
        Color tmp = _prefab.color;
        tmp.a = color_value;
        _prefab.color = tmp;
        color_value = color_value * multiplicator; //valeur
    }
}
