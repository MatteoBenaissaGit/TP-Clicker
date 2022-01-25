using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EtiquetteShower : MonoBehaviour
{
    public Transform Etiquette;
    public BoxCollider2D Collider_Etiquette;
    public Collider2D Collider_Fire;
    public Transform ScrollRect;
    public Transform FocusCamObject;
    bool isfocusing = false;
    public bool first_focus = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
            //si on clic sur le batiment ou sur le feu du batiment ça marche
            if (hit.collider == Collider_Etiquette)
            {
                MouseIsOver();
                isfocusing = true;
            }
            else if (hit.collider == Collider_Fire && first_focus == false)
            {
                isfocusing = true;
                first_focus = true;
            }
            else if (hit.collider != Collider_Etiquette && hit.collider != Collider_Fire)
            {
                MouseIsExit();
            }

        }
        if (isfocusing == true)
        {
            FocusCam();
            isfocusing = false;
        }
    }

    public void FocusCam()
    {
        ScrollRect.DOMove(FocusCamObject.position, 0.5f);
    }

    public void MouseIsOver()
    {
        Etiquette.DOScale(1f, 0.2f);
    }
    public void MouseIsExit()
    {
        Etiquette.DOScale(0f, 0.2f);
    }
}
