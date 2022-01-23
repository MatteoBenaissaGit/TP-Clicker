using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireScript : MonoBehaviour
{
    public Transform _fire;
    public Collider2D firecoll;
    public Bat1 bat1;
    float fire_multiplicator = 1f;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
            //action si on clic sur un batiment
            if (hit.collider == firecoll && bat1.is_on_fire == true)
            {
                _fire.DOComplete();
                _fire.transform.DOScale(new Vector3(_fire.transform.localScale.x - 0.5f, _fire.transform.localScale.y - 0.5f, 0), 0.2f); //réduit la taille
                bat1.transform.DOPunchScale(new Vector3(0.02f, 0.02f, 0), 0.3f);
                if (_fire.transform.localScale.x <= 2f) //si le feu est détruit
                {
                    bat1.FireEnd();
                }
            }
        } 
        if (bat1.is_on_fire == true)
        {
            transform.position = new Vector3(bat1.transform.position.x, bat1.transform.position.y, 0); //suit la position du batiment 1
        }
    }
}
