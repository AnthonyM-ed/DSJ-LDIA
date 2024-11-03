using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject firstPersonCamera; // Cámara en primera persona
    public GameObject thirdPersonCamera; // Cámara en tercera persona

    void Start()
    {
        // Iniciar con la cámara en tercera persona activa
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
    }

    void Update()
    {
        // Cambiar a primera persona al presionar el botón derecho del mouse
        if (Input.GetMouseButton(1)) // 1 corresponde al botón derecho
        {
            SwitchToFirstPerson();
        }
        else // Regresar a tercera persona al soltar el botón
        {
            SwitchToThirdPerson();
        }
    }

    void SwitchToFirstPerson()
    {
        firstPersonCamera.SetActive(true);
        thirdPersonCamera.SetActive(false);
    }

    void SwitchToThirdPerson()
    {
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
    }
}