using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class controllermask : MonoBehaviour
{

    public XRRayInteractor ray;

    public void buildmodemask()
    {
        ray.raycastMask = 224;
    }

    public void realtimemodemask()
    {
        ray.raycastMask = 96;
    }
}
