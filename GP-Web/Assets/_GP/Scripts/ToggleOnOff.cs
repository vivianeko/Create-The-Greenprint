using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnOff : MonoBehaviour
{
    public void SetActive(GameObject obj)
    {
        obj.SetActive(!obj.activeInHierarchy);
    }

}
