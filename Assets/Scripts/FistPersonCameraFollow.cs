using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPersonCameraFollow : MonoBehaviour
{
    public float mouseSensitivity = 0f; // Sensibilidad del mouse
    public Transform playerBody; // Transform del personaje al que sigue la c�mara
    public Vector3 offset; // Posici�n de la c�mara en relaci�n con el personaje

    private float xRotation = 0f; // Controla la rotaci�n en el eje X

    void Start()
    {
        // Bloquea el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Actualizar la posici�n de la c�mara para que siga al personaje con el offset
        transform.position = playerBody.position + offset;

        // Obtener la entrada del mouse sin suavizado
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Ajustar la rotaci�n en el eje X para la vista vertical (mirar arriba y abajo)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotaci�n en el eje X

        // Aplicar la rotaci�n en el eje X para la vista vertical
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotar el personaje en el eje Y para moverlo hacia los lados
        playerBody.Rotate(Vector3.up * mouseX);
    }
}