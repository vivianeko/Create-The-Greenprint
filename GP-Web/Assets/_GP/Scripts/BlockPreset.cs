using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[CreateAssetMenu(fileName = "Block Preset", menuName = "New Block Preset")]
public class BlockPreset : ScriptableObject
{
    public string Sector;
    public GameObject Prefab;

    public int Population;
    [Space]
    public float inLabor;
    public float outLabor;
    [Space]
    public float inPower;
    public float outPower;
    [Space]
    public float inWater;
    public float outWater;
    [Space]
    public float inFood;
    public float outFood;
}

