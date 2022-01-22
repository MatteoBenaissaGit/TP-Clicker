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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
            if (hit.collider == Collider_Etiquette)
            {
                MouseIsOver();
            }
            else
            {
                MouseIsExit();
            }
        }
    }

    public void MouseIsOver()
    {
        Debug.Log("ENTER");
        Etiquette.DOScale(1f, 0.2f);
    }
    public void MouseIsExit()
    {
        Etiquette.DOScale(0f, 0.2f);
    }
}
