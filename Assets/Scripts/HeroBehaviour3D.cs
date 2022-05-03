using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroBehaviour3D : MonoBehaviour
{
    [SerializeField]
    float runSpeed = 10f;

    [SerializeField]
    GameObject gun;

    Vector2 moveInput;
    Rigidbody myRigidbody;
    //    Animator myAnimator;
    //    CapsuleCollider2D myCapsuleCollider;

    private void Awake() => myRigidbody = GetComponent<Rigidbody>();

    void Start()
    {
//        myAnimator = GetComponent<Animator>();
//        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Walk();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        GameObject obj = ObjectPoolerScript.current.GetPooledObject();

        if (obj == null)
        {
            return;
        }
        obj.transform.position = gun.transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
    }

    void Walk()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        myRigidbody.velocity = playerVelocity * runSpeed;

        if (playerVelocity == Vector3.zero)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(playerVelocity);

        targetRotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            360 * Time.fixedDeltaTime);

        myRigidbody.MoveRotation(targetRotation);
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
//        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

}
