using UnityEngine;

[CreateAssetMenu(fileName = "Block Preset", menuName = "New Block Preset")]
public class BlockPreset : ScriptableObject
{
    public string Sector;
    public GameObject Prefab;

    public int Population;
    [Space]
    public int inLabor;
    public int outLabor;
    [Space]
    public int inPower;
    public int outPower;
    [Space]
    public int inWater;
    public int outWater;
    [Space]
    public int inFood;
    public int outFood;
}

