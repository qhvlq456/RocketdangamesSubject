using System;
using System.Collections.Generic;

[System.Serializable]
public struct sStageInfo
{
    public List<int> monsterIdxIndex;
    public int caveHealth;
    public float enemySpawnTime;
    public int enemyCount;
}
public enum eObjectType { Hero, Monster, Weapon }
[System.Serializable]
public struct sHeroData
{
    public int index;
    public eObjectType objectType;
    public int damage;
    public float range;
    public string name;
    public int health;
}
[System.Serializable]
public struct sEnemyData
{
    public int index;
    public eObjectType objectType;
    public int damage;
    public int speed;
    public int health;
    public float range;
}
public enum eWeaponType { Gun, Snap }
[System.Serializable]
public struct sWeaponData
{
    public int index;
    public string name;
    public string resName;
    public int damage;
    public float delay;
    public float range;
    public eWeaponType type;
}
public enum eBulletType { Normal }
[Serializable]
public struct sBulletData
{
    public int index;
    public int damage;
    public float speed;
    public eBulletType type;

}
