using UnityEngine;
using System.Collections;

public class OpenWebLink : MonoBehaviour
{
    [SerializeField] private string generalInfoUrl, waterInfoUrl, energyInfoUrl, foodInfoUrl, shelterInfoUrl;
    [SerializeField] private BlockPreset water, energy, food, shelter;
    private Selection selection;

    private void Start()
    {
        selection = GetComponent<Selection>();
    }

    public void GeneralInfoUrl()
    {
        Application.OpenURL(generalInfoUrl);
    }

    public void BlockInfoUrl()
    {
        if (selection.selectedObject.GetComponent<UpdateBlockStats>().preset == water)
            Application.OpenURL(waterInfoUrl);
        if (selection.selectedObject.GetComponent<UpdateBlockStats>().preset == energy)
            Application.OpenURL(energyInfoUrl);
        if (selection.selectedObject.GetComponent<UpdateBlockStats>().preset == food)
            Application.OpenURL(foodInfoUrl);
        if (selection.selectedObject.GetComponent<UpdateBlockStats>().preset == shelter)
            Application.OpenURL(shelterInfoUrl);
    }
}