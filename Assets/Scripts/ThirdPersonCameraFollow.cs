using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraFollow : MonoBehaviour
{
    public Transform playerBody; // Transform del jugador
    public float distance = 5f; // Distancia deseada de la c�mara al jugador
    public float height = 2f; // Altura de la c�mara respecto al jugador
    public float mouseSensitivity = 0f; // Sensibilidad del mouse
    public LayerMask obstacleMask; // Mascara para detectar colisiones con obst�culos
    public float collisionOffset = 0.3f; // Margen para evitar atravesar obst�culos

    private float xRotation = 0f; // Control de la rotaci�n en el eje X para mirar arriba y abajo

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

        // Ajustar la rotaci�n vertical en el eje X para la c�mara
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 60f); // Limitar el �ngulo de rotaci�n

        // Calcular la posici�n deseada de la c�mara
        Quaternion rotation = Quaternion.Euler(xRotation, playerBody.eulerAngles.y, 0);
        Vector3 desiredPosition = playerBody.position + rotation * new Vector3(0, height, -distance);

        // Raycast para detectar si hay obst�culos entre el jugador y la posici�n deseada de la c�mara
        RaycastHit hit;
        if (Physics.Raycast(playerBody.position + Vector3.up * height, desiredPosition - playerBody.position, out hit, distance, obstacleMask))
        {
            // Si hay un obst�culo, establecer la posici�n de la c�mara cerca del punto de colisi�n, ajustada con un margen
            desiredPosition = hit.point - (desiredPosition - playerBody.position).normalized * collisionOffset;
        }

        // Actualizar la posici�n de la c�mara
        transform.position = desiredPosition;

        // Asegurarse de que la c�mara siempre mire al jugador
        transform.LookAt(playerBody.position + Vector3.up * height);
    }
}