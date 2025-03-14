using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Gun : Weapon
{
    // 원래는 Data를 받아서 load해놓을 것임
    [SerializeField]
    private GameObject bulletObj;
    [SerializeField]
    private float roationSpeed;
    [SerializeField]
    private Transform fireTrf;
    public override void Load()
    {
        return; 
        data = new sWeaponData();
        data.name = "Gun";
        data.delay = 0.5f;
        data.range = 10;
        data.damage = 3;
        // 스킨이라던지 데이터라든지...
    }
    public void Shoot(Vector2 _target)
    {
        // 물리 엔진을 사용하기 싫어 맞은척만 하기
        Vector2 dir = _target - (Vector2)transform.position;
        dir.Normalize();
        LayerMask targetLayerMask = LayerMask.GetMask("Zombie1", "Zombie2", "Zombie3");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, targetLayerMask);

        if (hit.collider != null)
        {
            switch (hit.collider.tag)
            {
                case "Enemy":
                    EnemyObject enemy = hit.collider.gameObject.GetComponent<EnemyObject>();
                    Bullet bullet = ObjectPoolManager.Instance.Pooling(eObjectType.Weapon, bulletObj).GetComponent<Bullet>();
                    bullet.transform.position = fireTrf.transform.position;
                    bullet.Fire(enemy);
                    break;
            }
        }
        
    }

    // 총의 방향을 적에게 맞추기
    public void MoveMent(Vector2 _destination)
    {
        transform.LookAt(_destination);
    }
    // 그냥 갖고 있는 총알 중에 
    public override void Attack(BaseObject _baseObject)
    {
        Debug.LogError("789"); 
        base.Attack(_baseObject);
        Shoot(_baseObject.transform.position);
    }
}
