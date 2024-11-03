using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraFollow : MonoBehaviour
{
    public Transform playerBody; // Transform del jugador
    public float distance = 5f; // Distancia de la cámara al jugador
    public float height = 2f; // Altura de la cámara respecto al jugador
    public float mouseSensitivity = 200f; // Sensibilidad del mouse
    public float damping = 5f; // Suavizado del movimiento de la cámara

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor
    }

    void LateUpdate()
    {
        // Obtener la entrada del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Rotar el cuerpo del jugador
        playerBody.Rotate(Vector3.up * mouseX);

        // Calcular la posición deseada de la cámara
        Vector3 desiredPosition = playerBody.position - playerBody.forward * distance + Vector3.up * height;

        // Suavizar el movimiento de la cámara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);

        // Asegurarse de que la cámara siempre mire al jugador
        transform.LookAt(playerBody);
    }
}
