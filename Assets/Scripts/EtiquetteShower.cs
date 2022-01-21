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

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        GetComponent<Transform>().position.x = mousePos.x;
        GetComponent<Transform>().position.y = mousePos.y;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other == Collider_Etiquette)
        {
            Debug.Log("DETECTION");
            MouseIsOver();
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
