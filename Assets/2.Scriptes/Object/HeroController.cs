using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    private List<HeroObject> heroObjectList = new List<HeroObject>();


    public void StartHeroLoad()
    {
        foreach (var hero in heroObjectList)
        {
            hero.Load();
        }
    }
}
