using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ouvrier : MonoBehaviour
{
    [SerializeField] List<OuvrierInfos> OuvrierInfos;
    public TextMeshProUGUI ouvrier_text_price;
    public Ressources_and_people R_and_P;
    public Bat1 bat1;
    public int value_to_upgrade;
    int level = 0;

    public GameObject Blocage;

    public void UnlockOuvrier()
    {
        Blocage.SetActive(false);
    }

    public void OuvrierNextUpdate()
    {
        if (R_and_P.brick_number >= value_to_upgrade)
        {
            R_and_P.brick_number -= value_to_upgrade;
            level++;
            ButtonOuvrierUpdate(OuvrierInfos[level]);
        }  
    }

    public void ButtonOuvrierUpdate(OuvrierInfos _OuvrierInfos)
    {
        //change le prix
        value_to_upgrade = value_to_upgrade * 2;
        ouvrier_text_price.text = value_to_upgrade.ToString();
        bat1.AddOuvrier();
    }

}
