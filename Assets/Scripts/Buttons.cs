using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Buttons : MonoBehaviour
{
    public ScriptArrowTuto _arrow1; //fleche tuto ref
    #region tabs
    [SerializeField] GameObject tab1;
    [SerializeField] GameObject tab2;
    [SerializeField] GameObject tab3;
    [SerializeField] RectTransform _tab1;
    [SerializeField] RectTransform _tab2;
    [SerializeField] RectTransform _tab3;
    int actualselect = 1; //tab selectionné
    #endregion

    private void Awake()
    {
        MoveTabButton(_tab1); //move le tab de base au start
    }

    public void Click1() //active l'écran 1 
    {
        //set des actifs/non actifs
        tab1.SetActive(true);
        tab2.SetActive(false);
        tab3.SetActive(false);

        //arrow tuto
        if (_arrow1.bat1_used == false)
        {
            if (_arrow1.bat1_buyed == false)
            {
                if (actualselect != 2)
                {
                    _arrow1.posarrow = 0;
                    _arrow1.CheckPosArrow1();
                }
            }
            else
            {
                _arrow1.posarrow = 3;
                _arrow1.CheckPosArrow1();
            }
        }
        else if (_arrow1.posarrow == 5)
        {
            _arrow1.posarrow = 4;
            _arrow1.CheckPosArrow1();
        }
        
        //anim
        if (actualselect != 1)
        {
            MoveTabButton(_tab1);
            CheckRetract();
            actualselect = 1;
        } 
        
    }
    public void Click2() //active l'écran 2 
    {
        //set des actifs/non actifs
        tab2.SetActive(true);
        tab3.SetActive(false);

        //arrow tuto
        if (_arrow1.bat1_used == false)
        {
            if (_arrow1.bat1_buyed == false)
            {
                if (actualselect != 1)
                {
                    _arrow1.posarrow = 0;
                    _arrow1.CheckPosArrow1();
                }
            }
            else
            {
                if (actualselect != 3)
                {
                    _arrow1.posarrow = 2;
                    _arrow1.CheckPosArrow1();
                }
            }
        }
        else
        {
            _arrow1.posarrow = 5;
            _arrow1.CheckPosArrow1();
        }

        //anim
        if (actualselect != 2)
        {
            MoveTabButton(_tab2);
            CheckRetract();
            actualselect = 2;
        }
    }
    public void Click3() //active l'écran 3 
    {
        //set des actifs/non actifs
        tab2.SetActive(false);
        tab3.SetActive(true);

        //arrow tuto
        if (_arrow1.bat1_used == false)
        {
            if (_arrow1.bat1_buyed == false)
            {
                _arrow1.posarrow = 1;
                _arrow1.CheckPosArrow1();
            }
            else
            {
                if (actualselect != 2)
                {
                    _arrow1.posarrow = 2;
                    _arrow1.CheckPosArrow1();
                }
            }
        }
        else if (_arrow1.posarrow == 5)
        {
            _arrow1.posarrow = 4;
            _arrow1.CheckPosArrow1();
        }

        //anim
        if (actualselect != 3)
        {
            MoveTabButton(_tab3);
            CheckRetract();
            actualselect = 3;
        }

    }

    public void MoveTabButton(RectTransform tab) //anim move
    {
        tab.transform.DOComplete();
        tab.transform.DOMove(new Vector3(tab.transform.position.x + 0.3f , tab.transform.position.y, tab.transform.position.z), 0.5f, false);
    }
    public void RetractTabButton(RectTransform tab) //anim retract
    {
        tab.transform.DOComplete();
        tab.transform.DOMove(new Vector3(tab.transform.position.x - 0.3f, tab.transform.position.y, tab.transform.position.z), 0.5f, false);
    }
    public void CheckRetract() //donne le tab a retracté
    {
        if (actualselect == 1)
        {
            RetractTabButton(_tab1);
        }
        if (actualselect == 2)
        {
            RetractTabButton(_tab2);
        }
        if (actualselect == 3)
        {
            RetractTabButton(_tab3);
        }
    }
}
