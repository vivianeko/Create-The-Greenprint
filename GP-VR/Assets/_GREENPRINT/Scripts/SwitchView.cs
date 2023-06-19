using UnityEngine;

public class SwitchView : MonoBehaviour
{
    public Vector3 realtimePosition;
    public Vector3 buildModePosition;


    public void BuildMode()
    {
        transform.position = buildModePosition;
        transform.eulerAngles = new Vector3(0,0,0);
    }

    public void RealtimeMode()
    {
        transform.position = realtimePosition;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
