using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HeroObject : BaseObject
{
    [SerializeField]
    private sHeroData heroData;

    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private Transform weaponTrf;

    [SerializeField]
    private Weapon weapon;
    public override void Load()
    {
        heroData = new sHeroData();
        heroData.objectType = eObjectType.Hero;
        heroData.range = 5;
        heroData.damage = 1;
        heroData.index = 1;
        heroData.health = 50;

        health = new GameUtil.Health(heroData.health);


        //if (weapon == null)
        //{
        //    GameObject gun = GameManager.Instance.weaponCotainer.GetWeapon(eWeaponType.Gun);
        //    weapon = ObjectPoolManager.Instance.Pooling(eObjectType.Weapon, gun).GetComponent<Gun>();
        //    weapon.transform.parent = weaponTrf;
        //    weapon.transform.localPosition = Vector3.zero;
        //    weapon.mObject = this;
        //    weapon.Load();
        //}
    }

    public override void TakeDamage(int _damage)
    {
        health.TakeDamage(_damage);
        hpSlider.value = health.GetHealthRatio();

        if (health.currentHealth <= 0)
        {
            StartCoroutine(CoDeath());
        }

    }


    IEnumerator CoDeath()
    {
        isDie = true;
        yield return null;
        Dispose();
    }


}
