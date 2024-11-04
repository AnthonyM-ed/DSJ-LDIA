using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPersonCameraFollow : MonoBehaviour
{
    public float mouseSensitivity = 0f; // Sensibilidad del mouse
    public Transform playerBody; // Transform del personaje al que sigue la cámara
    public Vector3 offset; // Posición de la cámara en relación con el personaje

    private float xRotation = 0f; // Controla la rotación en el eje X

    void Start()
    {
        // Bloquea el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Actualizar la posición de la cámara para que siga al personaje con el offset
        transform.position = playerBody.position + offset;

        // Obtener la entrada del mouse sin suavizado
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Ajustar la rotación en el eje X para la vista vertical (mirar arriba y abajo)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotación en el eje X

        // Aplicar la rotación en el eje X para la vista vertical
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotar el personaje en el eje Y para moverlo hacia los lados
        playerBody.Rotate(Vector3.up * mouseX);
    }
}