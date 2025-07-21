using UnityEngine;
using Unity.Cinemachine;

public class CambiarCamara : MonoBehaviour
{
    [System.Obsolete]
    public CinemachineVirtualCamera terceraPersonaCam;
    [System.Obsolete]
    public CinemachineVirtualCamera primeraPersonaCam;

    private bool enPrimeraPersona = false;

    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            enPrimeraPersona = !enPrimeraPersona;

            if (enPrimeraPersona)
            {
                primeraPersonaCam.Priority = 20;
                terceraPersonaCam.Priority = 10;
            }
            else
            {
                primeraPersonaCam.Priority = 10;
                terceraPersonaCam.Priority = 20;
            }
        }
    }
}
