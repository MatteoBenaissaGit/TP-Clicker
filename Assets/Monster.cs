using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Monster : MonoBehaviour
{
    [SerializeField] int _life;
    public TextMesh TextLife;
    public Image ImageLife;
    int _lifeMax;
    public GameObject Visual;
    public Canvas Canvas;

    private void Awake()
    {
        //met la vie du monstre au max
        _lifeMax = _life;
        UpdateLife();
    }

    private void UpdateLife()
    {
        //modifie le texte de vie 
        TextLife.text = $"{_life}/{_lifeMax}";
        //modifie la barre de vie
        float percent = (float)_life / (float)_lifeMax;
        ImageLife.fillAmount = percent;
    }

    public void Hit(int damage)
    {
        //met les dégats au monstre
        _life -= damage;
        UpdateLife();
        //animation de hit sur le monstre
        Visual.transform.DOComplete();
        Visual.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.3f);
    }

    public void SetMonster(MonsterInfos infos)
    {
        //attribue la vie du nouveau monstre
        _lifeMax = infos.Life;
        _life = _lifeMax;
        //change le sprite du nouveau monstre
        Visual.GetComponent<SpriteRenderer>().sprite = infos.Sprite;
        UpdateLife();
    }

    public bool IsAlive()
    {
        //dit si le monstre est vivant ou pas
        return _life > 0;
    }
}
