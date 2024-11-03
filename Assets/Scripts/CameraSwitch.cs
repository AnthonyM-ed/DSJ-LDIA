using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject firstPersonCamera; // C�mara en primera persona
    public GameObject thirdPersonCamera; // C�mara en tercera persona

    void Start()
    {
        // Iniciar con la c�mara en tercera persona activa
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
    }

    void Update()
    {
        // Cambiar a primera persona al presionar el bot�n derecho del mouse
        if (Input.GetMouseButton(1)) // 1 corresponde al bot�n derecho
        {
            SwitchToFirstPerson();
        }
        else // Regresar a tercera persona al soltar el bot�n
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