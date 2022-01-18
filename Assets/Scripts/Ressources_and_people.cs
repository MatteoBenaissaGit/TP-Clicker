using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ressources_and_people : MonoBehaviour
{
    public Bat1 bat1;
    public maingame maingame;
    public Transform Bat1_Pos;

    public TextMeshProUGUI brick;
    public TextMeshProUGUI people;
    public TextMeshProUGUI people_per_second;
    public TextMeshProUGUI brick_per_second;
    public int brick_number = 0;
    public int people_number = 0;
    [HideInInspector] public int brick_augmentation = 0;
    [HideInInspector] public int people_augmentation = 0;

    float brick_number_before = 0f;
    [HideInInspector] public float brick_number_temp = 0f;
    float people_number_before = 0f;

    float timer = 0f;

    float lerp = 0f, duration = 1f;
    [HideInInspector] public float brick_shown = 0f;
    float people_shown = 0f;

    private void Awake()
    {
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
            UpdateBrick_and_PeopleNumber();
            timer = 0f;
            AddBrickAndPeople();
            lerp = 0f;
        }

        //affichage dynamique
        lerp += Time.deltaTime / duration;
        brick_shown = (int)Mathf.Lerp(brick_number_before, brick_number, lerp);
        people_shown = (int)Mathf.Lerp(people_number_before, people_number, lerp);
        //affichage des briques 1 par 1 quand fabriquée
        if (brick_number_temp < brick_shown)
        {
            brick_number_temp = brick_shown;
            if (brick_augmentation > 0)
            {
                bat1.ShowClickBrick(Bat1_Pos);
            }
        }
        if (brick_number_temp > brick_shown)
        {
            brick_number_temp = brick_shown;
        }
        //affichage des valeurs
        brick.text = brick_shown.ToString();
        people.text = people_shown.ToString();
        //valeurs par seconde
        brick_per_second.text = "+ " + brick_augmentation.ToString() + "/s";
        people_per_second.text = "+ " + people_augmentation.ToString() + "/s";
    }

    public void AddBrick(int number)
    {
        brick_number += number;
    }
    public void AddPeople(int number)
    {
        people_number += number;
    }

    public void AddBrickPerSeconde(int value)
    {
        brick_augmentation += value;
    }

    public void AddBrickAndPeople()
    {
        brick_number += brick_augmentation;
        people_number += people_augmentation;
    }

    void UpdateBrick_and_PeopleNumber()
    {
        brick_number_before = brick_number;
        people_number_before = people_number;
    }

}
