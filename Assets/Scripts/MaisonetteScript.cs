using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaisonetteScript : MonoBehaviour
{
    public int level = 0;
    public List<ListMaisonette> ListMaisonette;

    public void MaisonetteNext() //rajoute une maisonette
    {
        if (level <= ListMaisonette.Count)
        {
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
