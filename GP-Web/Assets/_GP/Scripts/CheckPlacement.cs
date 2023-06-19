using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    BuildingManager buildingManager;
    public bool isOnTop;

    private void Start()
    {
        buildingManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<BuildingManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("block"))
        {
            buildingManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("block"))
        {
            buildingManager.canPlace = true;
        }
    }

}
