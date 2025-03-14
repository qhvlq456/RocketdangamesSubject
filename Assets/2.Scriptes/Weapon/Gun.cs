using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Gun : Weapon
{
    // ������ Data�� �޾Ƽ� load�س��� ����
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
        // ��Ų�̶���� �����Ͷ����...
    }
    public void Shoot(Vector2 _target)
    {
        // ���� ������ ����ϱ� �Ⱦ� ����ô�� �ϱ�
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

    // ���� ������ ������ ���߱�
    public void MoveMent(Vector2 _destination)
    {
        transform.LookAt(_destination);
    }
    // �׳� ���� �ִ� �Ѿ� �߿� 
    public override void Attack(BaseObject _baseObject)
    {
        Debug.LogError("789"); 
        base.Attack(_baseObject);
        Shoot(_baseObject.transform.position);
    }
}
