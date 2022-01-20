using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Dialogue_Box : MonoBehaviour
{
    public Transform _box;

    //dialogues update
    public TextMeshProUGUI _desc;
    public TextMeshProUGUI _name;
    public Image _image;
    public int dialog_number = 0;
    [SerializeField] List<DialogInfos> DialogInfos;

    private void Awake()
    {
        _box.localScale = new Vector3(0, 0, 0);
    }
    void Start()
    {
        DialogUpdate(DialogInfos[dialog_number]);
        ActivateBox();
    }

    public void DialogUpdateCall()
    {
        DialogUpdate(DialogInfos[dialog_number]);
    }

    public void ActivateBox() //montre la boite de dialogue 
    {
        _box.DOComplete();
        _box.DOScale(1, 0.5f);
    }

    public void DesactivateBox() //ferme la boite de dialogue 
    {
        _box.DOComplete();
        _box.DOScale(0, 0.3f);
    }
    public void BumpBox() //ferme la boite de dialogue 
    {
        _box.DOComplete();
        _box.DOPunchScale(new Vector3(0.15f, 0.15f, 0f), 0.5f);
    }

    public void DialogUpdate(DialogInfos DialogInfos) //modifier visuelement la box
    {
        _desc.text = DialogInfos.Description;
        _name.text = DialogInfos.Name;
        _image.sprite = DialogInfos.sprite;
        //taille police
        if (_desc.text.Length <= 85)
        {
            _desc.fontSize = 0.44f;
        }
        if (_desc.text.Length > 100)
        {
            _desc.fontSize = 0.32f;
        }
    }
    public IEnumerator CloseAfterTimer(float time)
    {
        yield return new WaitForSeconds(time);
        DesactivateBox();
    }

}