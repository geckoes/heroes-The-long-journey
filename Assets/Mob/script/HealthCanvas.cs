using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
    [SerializeField] GameObject mob;
    [SerializeField] private Image healthImage;
    [SerializeField] private Image scudoImage;

    float maxScudo;
    float maxHealth;

    StatsEnemyManager statsEnemy;

    // Start is called before the first frame update
    void Start()
    {
        statsEnemy = mob.GetComponent<StatsEnemyManager>();
        maxScudo = statsEnemy.scudo;
        maxHealth = statsEnemy.hp;
    }

    public void updateHealthCanvas()
    {
        if (scudoImage != null)
        {
            scudoImage.fillAmount = statsEnemy.scudo / maxScudo;
        }
        healthImage.fillAmount = statsEnemy.hp / maxHealth;
    }

}
