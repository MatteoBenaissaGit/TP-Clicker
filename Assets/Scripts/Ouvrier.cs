using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ouvrier : MonoBehaviour
{
    #region referencement objets/script
    public Ressources_and_people R_and_P;
    public maingame maingame;
    public GameObject Blocage;
    public Bat1 bat1;
    #endregion

    //[SerializeField] List<OuvrierInfos> OuvrierInfos; //liste valeurs pour ouvrier upgrade
    public TextMeshProUGUI ouvrier_text_price; //texte du prix
    
    public int value_to_upgrade =10; //prix 
    int level = 0; //niveau de l'ouvrier
    
    void Update()
    {
        maingame.CheckCanBuy(ouvrier_text_price, R_and_P.brick_number, value_to_upgrade); //vérifier si achetable pour afficher texte couleur
    }

    public void UnlockOuvrier() //debloque l'ouvrier
    {
        Blocage.SetActive(false);
    }

    public void OuvrierNextUpdate() //passe à l'upgrade d'ouvrier suivante
    {
        if (R_and_P.brick_number >= value_to_upgrade) //si achetable
        {
            maingame.arrow1 = false; //supprime la fleche
            R_and_P.brick_number -= value_to_upgrade; //retire les briques
            level++; // +1 niveau
            ButtonOuvrierUpdate(); //fonction pour update l'affichage
        }  
    }

    public void ButtonOuvrierUpdate() //modifie l'affichae de l'ouvrier
    {
        //change le prix
        value_to_upgrade = value_to_upgrade * 2;
        ouvrier_text_price.text = value_to_upgrade.ToString();
        bat1.AddOuvrier(); //ajoute un ouvrier
    }  

}
