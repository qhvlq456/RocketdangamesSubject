using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private bool isGameStart = false;

    public HeroController heroController;
    public EnemyCave enemyCave;
    public WeaponCotainer weaponCotainer;

    public sStageInfo stageInfo;
    // GameStart
    public void StartGame()
    {
        heroController.StartHeroLoad();
        enemyCave.StartSpawn(stageInfo);
    }
    public void Update()
    {
        if (isGameStart)
        {
            StartGame();
            isGameStart = false;
        }
    }
    public void EndGame()
    {

    }
}
