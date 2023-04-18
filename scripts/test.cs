using UnityEngine;

public class test : MonoBehaviour
{
    public Transform pos3; // objeto público que se moverá
    private Vector3 lastHitPosition; // posición del último raycast

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            bool raycastHit = false;
            while (!raycastHit)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    lastHitPosition = hit.point;
                    raycastHit = true;
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            pos3.transform.position = lastHitPosition;
        }
    }
}
