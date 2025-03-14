using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Snap : Weapon
{
    public override void Load()
    {
        return;
        data = new sWeaponData();
        data.name = "Snap";
        data.delay = 0.3f;
        data.range = 3;
        data.damage = 1;
        // 스킨이라던지 데이터라든지...
    }
    public override void Attack(BaseObject _baseObject)
    {
        base.Attack(_baseObject);

        if (isAttack)
        {
            mObject.Anim(BaseObject.AnimState.Attack);
            _baseObject.TakeDamage(data.damage);
        }
        else
        {
            mObject.Anim(BaseObject.AnimState.Idle);
        }
    }
}
