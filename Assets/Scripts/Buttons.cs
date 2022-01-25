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
    [SerializeField] RectTransform _tab1;
    [SerializeField] RectTransform _tab2;
    int actualselect = 0; //tab selectionné
    #endregion

    public void Click1() //active l'écran 1 
    {
        
        if (actualselect != 1)
        {
            //set des actifs/non actifs
            tab1.SetActive(true);
            tab2.SetActive(false);
            CheckRetract();

            //arrow tuto
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
                if (_arrow1.bat1_used == false)
                {
                    _arrow1.posarrow = 200;
                    _arrow1.CheckPosArrow1();
                }
                else
                {
                    _arrow1.posarrow = 5;
                    _arrow1.CheckPosArrow1();
                }  
            }

            //anim
            MoveTabButton(_tab1);
            actualselect = 1;
        }
        else
        {
            tab1.SetActive(false);

            //Arrow
            if (_arrow1.bat1_used == false)
            {
                if (_arrow1.bat1_buyed == true)
                {
                    _arrow1.posarrow = 3;
                    _arrow1.CheckPosArrow1();
                }
            }
            else
            {
                _arrow1.posarrow = 4;
                _arrow1.CheckPosArrow1();
            }
            
            //anim
            CheckRetract();
            actualselect = 0;
        }
    }
    public void Click2() //active l'écran 2
    {
        
        if (actualselect != 2)
        {
            //set des actifs/non actifs
            tab1.SetActive(false);
            tab2.SetActive(true);
            CheckRetract();

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
            MoveTabButton(_tab2);
            actualselect = 2;
        }
        else
        {
            //arrow tuto
            if (_arrow1.bat1_used == false)
            {
                if (_arrow1.bat1_buyed == false)
                {
                    _arrow1.posarrow = 0;
                    _arrow1.CheckPosArrow1();
                }
                else
                {
                    _arrow1.posarrow = 3;
                    _arrow1.CheckPosArrow1();
                }
            }
            else if (_arrow1.posarrow == 4)
            {
                _arrow1.posarrow = 3;
                _arrow1.CheckPosArrow1();
            }

            tab2.SetActive(false);
            CheckRetract();
            actualselect = 0;
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
    }
}
