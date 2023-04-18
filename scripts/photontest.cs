using UnityEngine;
using Photon.Pun;
using OculusSampleFramework;

public class photontest : MonoBehaviourPunCallbacks, IPunObservable
{
    public Transform pos3; // objeto público que se moverá
    private Vector3 lastHitPosition; // posición del último raycast
    private bool isPositionUpdated = false; // verifica si la posición ha sido actualizada en la red

    void Update()
    {
        if (photonView.IsMine) // verifica si este script está siendo ejecutado en el cliente local
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
                isPositionUpdated = true;
            }

            if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) // Verifica si el gatillo derecho ha sido liberado
            {
                pos3.transform.position = lastHitPosition;
                photonView.RPC("UpdatePosition", RpcTarget.OthersBuffered, lastHitPosition);
                isPositionUpdated = false;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Envía la posición actual del objeto a través de la red
            stream.SendNext(pos3.transform.position);
        }
        else
        {
            // Recibe la posición actual del objeto desde la red
            pos3.transform.position = (Vector3)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void UpdatePosition(Vector3 position)
    {
        pos3.transform.position = position;
    }
}
