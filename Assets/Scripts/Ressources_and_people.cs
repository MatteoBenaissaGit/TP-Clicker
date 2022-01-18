using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ressources_and_people : MonoBehaviour
{
    public TextMeshProUGUI brick;
    public TextMeshProUGUI people;
    public int brick_number = 0;
    public int people_number = 100;

    private void Update()
    {
        brick.text = brick_number.ToString();
        people.text = people_number.ToString();
    }

    public void AddBrick(int number)
    {
        brick_number += number;
    }
    public void AddPeople(int number)
    {
        people_number += number;
    }
}
