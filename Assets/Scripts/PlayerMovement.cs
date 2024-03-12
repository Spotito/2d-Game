using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveVector = Vector2.zero;
    private Vector2 rotateVector = Vector2.zero;
    private Rigidbody2D rb = null;
    private float moveSpeed = 10f;

    [SerializeField] private GameObject Hand;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform firingPoint1;
    [SerializeField] private Transform firingPoint2;
    [SerializeField] private Transform firingPoint3;
    [Range(0.1f, 2f)] [SerializeField] private float fireRate = 0.5f;

    [SerializeField] private bool isPlayer2;

    private float fireTimer;
    private bool attacking;

    private Vector2 pos;

    private void FixedUpdate()
    {
        rb.velocity = moveVector * moveSpeed;

        var angle = Mathf.Atan2(rotateVector.y, rotateVector.x) * Mathf.Rad2Deg;


        if (isPlayer2) {
            angle = Mathf.Atan2(- rotateVector.y, - rotateVector.x) * Mathf.Rad2Deg;
        }

        Hand.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        if (attacking && fireTimer <= 0f) {
            Shoot();

            fireTimer = fireRate;
        } else {
            fireTimer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
       Instantiate(BulletPrefab, firingPoint1.position, firingPoint1.rotation);
       Instantiate(BulletPrefab, firingPoint2.position, firingPoint2.rotation);
       Instantiate(BulletPrefab, firingPoint3.position, firingPoint3.rotation);
    }

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AttackPeformed(InputAction.CallbackContext context)
    {
        if (context.phase is InputActionPhase.Performed) {
            attacking = true;
        } else {
            attacking = false;
        }
    }

    public void OnMovementPeformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    public void OnRotationPeformed(InputAction.CallbackContext value)
    {
        Debug.Log(value.phase);

        if (value.phase is InputActionPhase.Performed) {
            rotateVector = value.ReadValue<Vector2>();
        }

    }

}
