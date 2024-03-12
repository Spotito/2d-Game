using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CustomInput input = null;
    private Vector2 moveVector = Vector2.zero;
    private Vector2 rotateVector = Vector2.zero;
    private Rigidbody2D rb = null;
    private float moveSpeed = 10f;

    private void Awake() 
    {
        input = new CustomInput();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovementPeformed;
        input.Player.Movement.canceled += OnMovementCancelled;

        input.Player.Rotation.performed += OnRotationPeformed;
        input.Player.Rotation.canceled += OnRotationCancelled;
        
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPeformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
        
        input.Player.Rotation.performed -= OnRotationPeformed;
        input.Player.Rotation.canceled -= OnRotationCancelled;
    }

    private void FixedUpdate()
    {
        // Debug.Log(moveVector);
        Debug.Log(rotateVector);

        rb.velocity = moveVector * moveSpeed;

        var angle = Mathf.Atan2(rotateVector.y, rotateVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnMovementPeformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    private void OnRotationPeformed(InputAction.CallbackContext value)
    {
        rotateVector = value.ReadValue<Vector2>();
    }

    private void OnRotationCancelled(InputAction.CallbackContext value)
    {
        rotateVector = Vector2.zero;
    }
}
