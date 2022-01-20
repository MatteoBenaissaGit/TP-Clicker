using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScriptArrowTuto : MonoBehaviour
{
    public Transform _prefab;
    public Transform pos0;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;

    float anim_end_pos;

    bool movement = false;
    public bool bat1_buyed = false;

    float timer = 0f;

    [HideInInspector] public int posarrow = 0;

    float addposx = 0f;

    private void Awake()
    {
        anim_end_pos = pos0.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.6f)
        {
            if (movement == false)
            {
                _prefab.DOComplete();
                _prefab.DOMoveX(anim_end_pos - 0.35f , 0.6f);
                movement = true;
            }
            else
            {
                _prefab.DOComplete();
                _prefab.DOMoveX(anim_end_pos + 0.35f, 0.6f);
                movement = false;
            }
            timer = 0f;
        }
    }

    public void CheckPosArrow1()
    {
        if (posarrow == 0)
        {
            _prefab.DOComplete();
            _prefab.position = pos0.position;
            anim_end_pos = pos0.position.x;
        }
        if (posarrow == 1)
        {
            _prefab.DOComplete();
            _prefab.position = pos1.position;
            anim_end_pos = pos1.position.x;
        }
        if (posarrow == 2)
        {
            _prefab.DOComplete();
            _prefab.position = pos2.position;
            anim_end_pos = pos2.position.x;
        }
        if (posarrow == 3)
        {
            _prefab.DOComplete();
            _prefab.position = pos3.position;
            anim_end_pos = pos3.position.x;
        }
    }

}

