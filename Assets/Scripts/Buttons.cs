using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Buttons : MonoBehaviour
{
    public ScriptArrowTuto _arrow1;

    [SerializeField] GameObject tab1;
    [SerializeField] GameObject tab2;
    [SerializeField] GameObject tab3;

    [SerializeField] RectTransform _tab1;
    [SerializeField] RectTransform _tab2;
    [SerializeField] RectTransform _tab3;

    int actualselect = 1;

    private void Awake()
    {
        MoveTabButton(_tab1);
    }

    public void Click1() //active l'écran 1 
    {
        tab1.SetActive(true);
        tab2.SetActive(false);
        tab3.SetActive(false);

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

        if (actualselect != 1)
        {
            MoveTabButton(_tab1);
            CheckRetract();
            actualselect = 1;
        } 
        
    }
    public void Click2() //active l'écran 2 
    {
        tab2.SetActive(true);
        tab3.SetActive(false);

        if (_arrow1.bat1_buyed == false)
        {
            _arrow1.posarrow = 0;
            _arrow1.CheckPosArrow1();
        }
        else
        {
            _arrow1.posarrow = 2;
            _arrow1.CheckPosArrow1();
        }

        if (actualselect != 2)
        {
            MoveTabButton(_tab2);
            CheckRetract();
            actualselect = 2;
        }
    }
    public void Click3() //active l'écran 3 
    {
        tab2.SetActive(false);
        tab3.SetActive(true);

        if (actualselect != 3)
        {
            MoveTabButton(_tab3);
            CheckRetract();
            actualselect = 3;
        }
        if (_arrow1.bat1_buyed == false)
        {
            _arrow1.posarrow = 1;
            _arrow1.CheckPosArrow1();
        }
        else
        {
            _arrow1.posarrow = 2;
            _arrow1.CheckPosArrow1();
        }

    }

    public void MoveTabButton(RectTransform tab)
    {
        tab.transform.DOComplete();
        tab.transform.DOMove(new Vector3(tab.transform.position.x + 0.3f , tab.transform.position.y, tab.transform.position.z), 0.5f, false);
    }
    public void RetractTabButton(RectTransform tab)
    {
        tab.transform.DOComplete();
        tab.transform.DOMove(new Vector3(tab.transform.position.x - 0.3f, tab.transform.position.y, tab.transform.position.z), 0.5f, false);
    }
    public void CheckRetract()
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
