using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseObject
{
    public sBulletData data;
    [SerializeField]
    private float time;

    public override void Load()
    {
        
    }
    public void Fire(EnemyObject _enemyObject)
    {
        StartCoroutine(CoShoot(_enemyObject));
    }
    IEnumerator CoShoot(EnemyObject _enemy)
    {
        float timer = 0f;

        Vector2 target = _enemy.transform.position;

        while (Vector2.Distance((Vector2)transform.position, target) > 0.15f && time > timer)
        {
            timer += Time.deltaTime;

            transform.position = Vector2.Lerp(transform.position, target, data.speed * Time.deltaTime);
            yield return null;
        }

        _enemy.TakeDamage(data.damage);
        Dispose();
    }

    public override void Dispose()
    {
        ObjectPoolManager.Instance.Retrieve(eObjectType.Weapon, transform);
    }
}
