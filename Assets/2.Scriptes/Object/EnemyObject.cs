using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class EnemyObject : BaseObject
{
    // isAttacking
    // IsDead
    // IsIdle
    //private readonly string isAttack = "isAttacking";
    //private readonly string isDeath = "IsDead";
    //private readonly string isIdle = "IsIdle";
    public Transform head;
    [SerializeField]
    private Slider hpSlider;

    public Rigidbody2D rigid;

    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private Transform weaponTrf;

    public bool isRise = false;
    public bool isBackForce = false;

    [SerializeField]
    private float moveSpeed = 1.5f;
    [SerializeField]
    private float riseForce;
    [SerializeField]
    private float backForce;

    [SerializeField]
    private float riseTimer;
    [SerializeField]
    private float backForceTimer;

    [SerializeField]
    private float riseLimitTime;
    [SerializeField]
    private float backForceLimitTime;

    [SerializeField]
    private Vector2 rangeOffset = Vector2.zero;

    [SerializeField]
    private sEnemyData enemyData;


    public override void Load()
    {
        // Test
        enemyData = new sEnemyData();
        enemyData.objectType = eObjectType.Monster;
        enemyData.range = 1;
        enemyData.damage = 1;
        enemyData.speed = 7;
        enemyData.index = 1;
        enemyData.health = 100;

        health = new GameUtil.Health(enemyData.health);


        if (weapon == null)
        {
            GameObject Snap = GameManager.Instance.weaponCotainer.GetWeapon(eWeaponType.Snap);
            weapon = ObjectPoolManager.Instance.Pooling(eObjectType.Weapon, Snap).GetComponent<Snap>();
            weapon.transform.parent = weaponTrf;
            weapon.transform.localPosition = Vector3.zero;
            weapon.mObject = this;
            weapon.Load();
        }

        Anim(AnimState.Idle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDie) 
        {
            return;
        }

        if(isRise)
        {
            StartRise();
        }
        else if(isBackForce)
        {
            StartBackForce();
        }
        else
        {
            MoveMent(Vector2.zero);
        }
    }
    public void StartRise()
    {
        if (riseTimer < riseLimitTime)
        {
            riseTimer += Time.fixedDeltaTime;
            rigid.velocity = new Vector2(moveSpeed * -1, riseForce);
        }
        else
        {
            isRise = false;
            riseTimer = 0;
        }
    }
    public void StartBackForce()
    {
        if (backForceTimer < backForceLimitTime)
        {
            backForceTimer += Time.fixedDeltaTime;
            rigid.velocity = new Vector2(backForce, rigid.velocity.y); // MovePosition 대신 velocity 사용
        }
        else
        {
            isBackForce = false;
            backForceTimer = 0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.collider.GetComponent<EnemyObject>() is EnemyObject zm)
        {
            // map상으론 작은쪽이 앞에있음
            if(transform.position.x > zm.transform.position.x 
                && Mathf.Abs(transform.position.y - zm.transform.position.y) <= 0.1f
                && !isRise)
            {
                isRise = true;
                zm.isBackForce = true;
            }
        }
    }
    public override void TakeDamage(int _damage)
    {
        RectTransform damageUI = UIManager.Instance.GetDamangeText(_damage).GetComponent<RectTransform>();
        damageUI.position = transform.position + Vector3.up * 1.75f;

        health.TakeDamage(_damage);
        hpSlider.value = health.GetHealthRatio();

        if(health.currentHealth <= 0 && gameObject.activeSelf)
        {
            StartCoroutine(CoDeath());
        }
    }
    public void OnAttack()
    {

    }
    public override void MoveMent(Vector2 _destination)
    {
        rigid.velocity = new Vector2(moveSpeed * -1, 0);
    }

    IEnumerator CoDeath()
    {
        isDie = true;
        yield return new WaitForSeconds(0.5f);
        Dispose();
    }

    public override void Dispose()
    {
        ObjectPoolManager.Instance.Retrieve(enemyData.objectType, transform);
    }
}
