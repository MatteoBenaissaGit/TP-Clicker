using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScriptArrowTuto : MonoBehaviour
{
    public Transform _prefab; //ref de la fleche
    public Transform _parent; //reef du parent
    #region variables des différentes position
    public Transform pos0;
    public Transform pos1;
    public Transform pos2;
    public Transform pos200;
    public Transform pos3;
    public Transform pos4;
    public Transform pos5;
    #endregion

    float anim_end_pos; //valeur x pour les anim
    bool movement = false; //bool pour savoir si il faut aller a droite ou a gauche dans l'anim

    public bool bat1_buyed = false; //bool qui dit si le bat1 à été acheter ou non
    public bool bat1_used = false; //bool qui dit si le joueur a miner une brique
    public bool bat2_buyed = false;

    float timer = 0f; //timer
    [HideInInspector] public int posarrow = 0; //valeur de pos pour dire ou se trouve la fleche

    private void Awake()
    {
        anim_end_pos = pos0.position.x; //position au start
    }

    void Update()
    {
        
        //gestion du timer et de l'animation
        timer += Time.deltaTime; //augmente le timer
        if (timer > 0.6f)
        {
            _prefab.DOComplete();
            if (movement == false) //mov1
            {    
                _prefab.DOMoveX(anim_end_pos - 0.35f , 0.6f);
                movement = true;
            }
            else //mov2
            {
                _prefab.DOMoveX(anim_end_pos + 0.35f, 0.6f);
                movement = false;
            }
            timer = 0f;
        }
    }

    public void CheckPosArrow1() //change la position de reference de la fleche
    {
        _prefab.DOComplete();
        if (posarrow == 0)
        { 
            _prefab.position = pos0.position;
            anim_end_pos = pos0.position.x;
        }
        if (posarrow == 1)
        {
            _prefab.position = pos1.position;
            anim_end_pos = pos1.position.x;
        }
        if (posarrow == 2)
        {
            _prefab.position = pos2.position;
            anim_end_pos = pos2.position.x;
        }
        if (posarrow == 200)
        {
            _prefab.position = pos200.position;
            anim_end_pos = pos200.position.x;
        }
        if (posarrow == 3)
        {
            _prefab.position = pos3.position;
            anim_end_pos = pos3.position.x;
        }
        if (posarrow == 4)
        {
            _prefab.position = pos4.position;
            anim_end_pos = pos4.position.x;
        }
        if (posarrow == 5)
        {
            _prefab.position = pos5.position;
            anim_end_pos = pos5.position.x;
        }
    }

}

