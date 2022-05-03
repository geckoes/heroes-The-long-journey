using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawnScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Distanza tra collide")]
    float distanceFromColliderAndSpawn = 2f;

    float newSpawnDistance;

    void Awake()
    {
        newSpawnDistance = GetComponent<SphereCollider>().radius * 2 - distanceFromColliderAndSpawn;
        //        myAnimator = GetComponent<Animator>();
        //        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public float GetDistanceSpawn()
    {
        return newSpawnDistance;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            collider.transform.position += (transform.position - collider.transform.position).normalized * newSpawnDistance;

            Vector3 rot = collider.transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            collider.transform.rotation = Quaternion.Euler(rot);
        }
    }

}
