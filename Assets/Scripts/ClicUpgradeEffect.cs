using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClicUpgradeEffect : MonoBehaviour
{
    public GameObject _prefab;
    public SpriteRenderer _sprite;

    //alpha
    float color_value = 1f;

    void Start()
    {
        _prefab.transform.DOScale(2f, 0.4f);
        GameObject.Destroy(_prefab, 0.4f);
    }
    private void Update()
    {
        //diminue petit à petit l'opacité
        Color tmp = _sprite.color;
        tmp.a = color_value;
        _sprite.color = tmp;
        color_value = color_value * 0.9f; //valeur
    }

}
