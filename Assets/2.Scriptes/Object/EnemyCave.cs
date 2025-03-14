using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCave : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObj;

    [SerializeField]
    public List<Vector3> spawnPosList = new List<Vector3>();

    [SerializeField]
    private List<EnemyObject> enemyList = new List<EnemyObject>();

    [SerializeField]
    private int currentEnemyCount = 0;

    [SerializeField]
    private List<int> layerList = new List<int>();

    public sStageInfo stageInfo;

    IEnumerator startSpawn = null;

    private void Awake()
    {
        layerList.Add(LayerMask.NameToLayer("Zombie1"));
        layerList.Add(LayerMask.NameToLayer("Zombie2"));
        layerList.Add(LayerMask.NameToLayer("Zombie3"));
    }
    public void StartSpawn(sStageInfo _stageInfo)
    {
        stageInfo = _stageInfo;

        if(startSpawn != null)
        {
            StopCoroutine(startSpawn);
        }

        startSpawn = CoStartSpawn();
        StartCoroutine(startSpawn);
    }

    public void StopSpawn()
    {
        if (startSpawn != null)
        {
            StopCoroutine(startSpawn);
        }

        startSpawn = null;
    }

    private IEnumerator CoStartSpawn()
    {
        for(int i = 0; i < stageInfo.enemyCount; i++)
        {
            Transform trf = ObjectPoolManager.Instance.Pooling(eObjectType.Monster, enemyObj).transform;
            int rnd = Random.Range(0,spawnPosList.Count);
            trf.transform.position = spawnPosList[rnd];
            trf.gameObject.layer = layerList[rnd];
            EnemyObject inst = trf.GetComponent<EnemyObject>();
            inst.Load();
            enemyList.Add(inst);

            yield return new WaitForSeconds(stageInfo.enemySpawnTime);
            currentEnemyCount++;
        }
    }


}
