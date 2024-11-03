using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // Velocidad de movimiento
    public float jumpForce = 5f; // Fuerza de salto
    private bool isGrounded; // Para verificar si el personaje est� en el suelo

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimiento con WASD (o flechas por defecto)
        float horizontal = Input.GetAxis("Horizontal"); // Movimiento en el eje X (izquierda y derecha)
        float vertical = Input.GetAxis("Vertical"); // Movimiento en el eje Z (adelante y atr�s)

        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        movement = transform.TransformDirection(movement); // Ajusta el movimiento seg�n la rotaci�n del personaje
        rb.MovePosition(transform.position + movement);

        // Salto con Espacio
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detectar colisi�n con el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
