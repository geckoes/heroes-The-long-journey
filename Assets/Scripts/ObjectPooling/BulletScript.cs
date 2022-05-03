using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript: MonoBehaviour
{
    Rigidbody m_rigidbody;
    [SerializeField]
    float bulletSpeed = 100f;
    [SerializeField]
    float bulletTime = 2f;


    float _damage = 1f;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Invoke("Destroy", bulletTime);
    }

    private void FixedUpdate()
    {
        m_rigidbody.velocity = transform.forward * bulletSpeed;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Health health))
        {
            health.DealDamage(_damage);
        }
        Destroy();
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

}
