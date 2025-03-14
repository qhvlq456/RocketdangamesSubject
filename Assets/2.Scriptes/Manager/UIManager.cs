using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject damangeText;
    public GameObject GetDamangeText(int _value)
    {
        DamageText ret = GameUtil.InstantiateGameObject<DamageText>(damangeText);
        ret.StartDamage(_value.ToString());
        return ret.gameObject;
    }
}
