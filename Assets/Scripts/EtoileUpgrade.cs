using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EtoileUpgrade : MonoBehaviour
{
    public GameObject _prefab;
    public SpriteRenderer _sprite;

    //alpha
    float color_value = 1f;

    void Start()
    {
        GameObject.Destroy(_prefab, 0.6f);
    }
    private void Update()
    {
        //diminue petit à petit l'opacité
        Color tmp = _sprite.color;
        tmp.a = color_value;
        _sprite.color = tmp;
        color_value = color_value * 0.99f; //valeur
    }
    
}
