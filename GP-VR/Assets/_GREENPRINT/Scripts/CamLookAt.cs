using UnityEngine;

public class CamLookAt : MonoBehaviour
{
    void Update()
    {
        Transform target = Camera.main.transform;
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }
}
