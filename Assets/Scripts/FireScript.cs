using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireScript : MonoBehaviour
{
    public bool tuto_done = false;
    public Transform _fire;
    public Collider2D firecoll;
    public Bat1 bat1;

    public SoundManager soundmanager;

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
                soundmanager.ClicFire();
                _fire.DOComplete();
                _fire.transform.DOScale(new Vector3(_fire.transform.localScale.x - 0.5f, _fire.transform.localScale.y - 0.5f, 0), 0.2f); //r?duit la taille
                bat1.transform.DOPunchScale(new Vector3(-0.02f, -0.02f, 0), 0.3f);
                if (_fire.transform.localScale.x <= 2f) //si le feu est d?truit
                {
                    bat1.FireEnd();
                    if (tuto_done == false)
                    {
                        soundmanager.SoundUpgrade();
                        //dialog
                        bat1._Dialog_box.dialog_number= 6;
                        bat1._Dialog_box.DialogUpdateCall();
                        bat1._Dialog_box.BumpBox();
                        StartCoroutine(bat1._Dialog_box.CloseAfterTimer(5f)); //dialogbox se ferme
                        tuto_done = true;
                    }  
                }
            }
        } 
        if (bat1.is_on_fire == true)
        {
            transform.position = new Vector3(bat1.transform.position.x + 0.5f, bat1.transform.position.y + 0.6f, 0); //suit la position du batiment 1
        }
    }
}
