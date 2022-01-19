using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Brick_Click : MonoBehaviour
{
    public SpriteRenderer _prefab;

    //alpha
    
    float color_value = 1f;
    

    // Update is called once per frame
    void Update()
    {
        Color tmp = _prefab.color;
        tmp.a = color_value;
        _prefab.color = tmp;
        color_value -= 0.003f;
    }
}
