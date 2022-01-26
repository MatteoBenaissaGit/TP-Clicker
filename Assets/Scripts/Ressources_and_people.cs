using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ressources_and_people : MonoBehaviour
{
    #region referencement objets/scirpt
    public Bat1 bat1;
    public maingame maingame;
    public Transform Bat1_Pos;
    #endregion

    //brick
    public TextMeshProUGUI brick;
    public TextMeshProUGUI brick_per_second;
    public int brick_number = 0;
    [HideInInspector] public int brick_augmentation = 0;
    public int brick_multiplier = 1;
    public float brick_number_before = 0f;
    public float brick_shown = 0f;
    public float brick_number_temp = 0f;

    //people
    public TextMeshProUGUI people;
    public TextMeshProUGUI people_per_second;
    public int people_number = 0;
    [HideInInspector] public int people_augmentation = 0;
    public int people_multiplier = 1;
    float people_number_before = 0f;
    [HideInInspector] public float people_shown = 0f;

    float timer = 0f; //timer
    float lerp = 0f, duration = 1f; //anim variables
    
    private void Awake()
    {
        //setup valeurs
        brick_number = 100;
        brick_number_before = 100;
        brick_number_temp = 100;
    }

    private void Update()
    {
        //timer
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            if (bat1.is_on_fire == false) //si la carrière ne brule pas
            {
                UpdateBrick_and_PeopleNumber(); //maj affichage
                AddBrickAndPeople(); //ajoute les valeurs auto
            }
            timer = 0f;
            lerp = 0f;
        }

        //affichage dynamique
        lerp += Time.deltaTime / duration;
        //affichage selon le nombre
        if (brick_number < 10000) 
        {
            brick_shown = ((int)Mathf.Lerp(brick_number_before, brick_number, lerp));
            brick.text = brick_shown.ToString(); //affichage des valeurs
        }
        else if (brick_number < 1000000)
        {
            brick_shown = (int)Mathf.Lerp(brick_number_before/1000, brick_number/ 1000, lerp);
            brick.text = brick_shown.ToString() + " k"; //affichage des valeurs
        }
        else
        {
            brick_shown = (int)Mathf.Lerp(brick_number_before / 100000, brick_number / 100000, lerp);
            brick.text = brick_shown.ToString() + " m"; //affichage des valeurs
        }
        people_shown = (int)Mathf.Lerp(people_number_before, people_number, lerp); 
        //affichage des briques 1 par 1 quand fabriquée
        if (brick_number_temp < brick_shown)
        {
            brick_number_temp = brick_shown;
            bat1.ShowClickBrick(Bat1_Pos, 1); //sort une brique visuelement
        }
        if (brick_number_temp > brick_shown)
        {
            brick_number_temp = brick_shown;
        }
        //affichage des valeurs
        people.text = people_shown.ToString();
        //valeurs par seconde
        brick_per_second.text = "+ " + (brick_augmentation*brick_multiplier).ToString() + "/s";
        people_per_second.text = "+ " + (people_augmentation*people_multiplier).ToString() + "/s";
    }

    public void AddBrick(int number) //ajoute une brique manuelement
    {
        brick_number += number;
    }
    public void AddPeople(int number) //ajoute un habitant manuelement
    {
        people_number += number;
    }

    public void AddBrickPerSeconde(int value)  //ajoute au nb de brique par seconde
    {
        brick_augmentation += value;
    }

    public void AddBrickAndPeople() //ajoute les valeurs auto
    {
        if (bat1.isUpgrading == false && bat1.is_on_fire == false) //si la briqueterie n'est pas en train d'etre améliorer
        {
            brick_number += brick_augmentation * brick_multiplier;
        }
        people_number += people_augmentation * people_multiplier;
    }

    void UpdateBrick_and_PeopleNumber() //reajuste les variables qui servent de referentiel à l'affichage dynamique
    {
        brick_number_before = brick_number;
        people_number_before = people_number;
    }

}
