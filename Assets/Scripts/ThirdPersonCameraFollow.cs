using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraFollow : MonoBehaviour
{
    public Transform playerBody; // Transform del jugador
    public float distance = 5f; // Distancia deseada de la cámara al jugador
    public float height = 2f; // Altura de la cámara respecto al jugador
    public float mouseSensitivity = 0f; // Sensibilidad del mouse
    public LayerMask obstacleMask; // Mascara para detectar colisiones con obstáculos
    public float collisionOffset = 0.3f; // Margen para evitar atravesar obstáculos

    private float xRotation = 0f; // Control de la rotación en el eje X para mirar arriba y abajo

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor
    }

    void LateUpdate()
    {
        // Obtener la entrada del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotar el cuerpo del jugador en el eje Y
        playerBody.Rotate(Vector3.up * mouseX);

        // Ajustar la rotación vertical en el eje X para la cámara
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 60f); // Limitar el ángulo de rotación

        // Calcular la posición deseada de la cámara
        Quaternion rotation = Quaternion.Euler(xRotation, playerBody.eulerAngles.y, 0);
        Vector3 desiredPosition = playerBody.position + rotation * new Vector3(0, height, -distance);

        // Raycast para detectar si hay obstáculos entre el jugador y la posición deseada de la cámara
        RaycastHit hit;
        if (Physics.Raycast(playerBody.position + Vector3.up * height, desiredPosition - playerBody.position, out hit, distance, obstacleMask))
        {
            // Si hay un obstáculo, establecer la posición de la cámara cerca del punto de colisión, ajustada con un margen
            desiredPosition = hit.point - (desiredPosition - playerBody.position).normalized * collisionOffset;
        }

        // Actualizar la posición de la cámara
        transform.position = desiredPosition;

        // Asegurarse de que la cámara siempre mire al jugador
        transform.LookAt(playerBody.position + Vector3.up * height);
    }
}