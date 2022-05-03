using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 2f;
    [SerializeField] GameObject deathVFX;

    float health;

    public delegate void RespawnEnemies(GameObject go);
    public static event RespawnEnemies Died;

    private void OnEnable()
    {
        health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            TriggerDeathVFX();
            Die();
        }
    }

    private void TriggerDeathVFX()
    {
        if (!deathVFX) { return; }

        GameObject deathVFXObject = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(deathVFXObject, 1f);
    }

    private void Die()
    {
        gameObject.SetActive(false);
        Died(this.gameObject);
    }
}
