using UnityEngine;
using OculusSampleFramework;

public class test1 : MonoBehaviour
{
    public Transform pos3; // objeto público que se moverá
    private Vector3 lastHitPosition; // posición del último raycast

    void Update()
    {
        OVRInput.Update(); // Actualiza los estados de entrada de Oculus
        
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) // Verifica si el gatillo derecho está presionado
        {
            bool raycastHit = false;
            while (!raycastHit)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
                {
                    lastHitPosition = hit.point;
                    raycastHit = true;
                }
            }
        }
        
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) // Verifica si el gatillo derecho ha sido liberado
        {
            pos3.transform.position = lastHitPosition;
        }
    }
}
