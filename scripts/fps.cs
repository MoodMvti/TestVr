using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Velocidad de movimiento
    public float lookSpeed = 3.0f; // Velocidad de rotación
    public float jumpForce = 5.0f; // Fuerza de salto

    private CharacterController controller;
    private Camera cam;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        // Movimiento
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        movement = Vector3.ClampMagnitude(movement, 1.0f);

        moveDirection.y -= 9.81f * Time.deltaTime;
        controller.Move(movement * moveSpeed * Time.deltaTime + moveDirection * Time.deltaTime);

        // Salto
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            moveDirection.y = jumpForce;
        }

        // Rotación
        float lookX = Input.GetAxis("Mouse X");
        float lookY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, lookX * lookSpeed);
        cam.transform.Rotate(Vector3.left, lookY * lookSpeed);
    }
}
