using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaisonetteScript : MonoBehaviour
{
    public int level = 0;
    public List<ListMaisonette> ListMaisonette;
    public List<ListMaisonette> ListMaisonetteDestroy;

    public void MaisonetteNext() //rajoute une maisonette
    {
        if (level <= ListMaisonette.Count)
        {
            ListMaisonetteDestroy[level].Maisonette.transform.DOScale(0f, 1);
            ListMaisonette[level].Maisonette.transform.DOScale(1f, 2);
            level++;
        }
        
    }
}

[Serializable]
public class ListMaisonette
{
    public GameObject Maisonette;
}
