using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 무기의 저장소 추후 만들거
/// </summary>
public class WeaponCotainer : MonoBehaviour
{
    [SerializeField]
    private List<Weapon> weaponList = new List<Weapon>();

    private void Awake()
    {
        
    }
    public GameObject GetWeapon(eWeaponType _type) 
    {
        GameObject ret = null;

        for (int i = 0; i < weaponList.Count; i++)
        {
            if (weaponList[i].data.type == _type)
            {
                ret = weaponList[i].gameObject;
                break;
            }
        }

        return ret;
    }

    
}
