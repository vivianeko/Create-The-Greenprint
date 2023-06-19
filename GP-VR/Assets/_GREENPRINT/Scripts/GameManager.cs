using System;
using UnityEngine;
using TMPro;
using Normal.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton

    static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this);
    }
    #endregion

    public BlockPreset block = null;
    public GameObject[] blocks;    
    [Space]
    public Image LaborIcon;
    public Image PowerIcon;
    public Image WaterIcon;
    public Image FoodIcon;    
    [Space]
    public int Population = 0;
    public int CurrentLabor = 0;
    public int CurrentPower = 0;
    public int CurrentWater = 0;
    public int CurrentFood = 0;

    [Space]
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI populationCount;   

    #region placeblock
    public void Start()
    {
        InvokeRepeating("CheckBlocks", 0, 1);
    }


    public void updateblock(BlockPreset _block)
    {
        block = _block;
    }

    public void placeblock()
    {
        Transform reticle = GameObject.FindGameObjectWithTag("reticle").transform;
        Realtime.Instantiate(block.Prefab.name, reticle.position, Quaternion.identity, new Realtime.InstantiateOptions
        {
            ownedByClient = false,
            preventOwnershipTakeover = false,
            destroyWhenOwnerLeaves = false,
            destroyWhenLastClientLeaves = true
        });
        CheckBlocks();
    }
    #endregion

    public void CheckBlocks()
    {
        Population = 0;
        CurrentLabor = 0;
        CurrentPower = 0;
        CurrentWater = 0;
        CurrentFood = 0;

        blocks = GameObject.FindGameObjectsWithTag("block");
        foreach(GameObject bl in blocks)
        {
            BlockPreset pr = bl.GetComponent<UpdateBlockStats>().preset;
            Population += pr.Population;
            CurrentLabor = CurrentLabor + pr.outLabor - pr.inLabor;
            CurrentPower = CurrentPower + pr.outPower - pr.inPower;
            CurrentWater = CurrentWater + pr.outWater - pr.inWater;
            CurrentFood = CurrentFood + pr.outFood - pr.inFood;
        }
        UpdateStats();        
    }
    

    #region stats
    public void UpdateStats()
    {
        if (CurrentLabor < 0)
            LaborIcon.enabled = true;
        else
            LaborIcon.enabled = false;

        if (CurrentPower < 0)
            PowerIcon.enabled = true;
        else
            PowerIcon.enabled = false;

        if (CurrentWater < 0)
            WaterIcon.enabled = true;
        else
            WaterIcon.enabled = false;

        if (CurrentFood < 0)
            FoodIcon.enabled = true;
        else
            FoodIcon.enabled = false;

        string n = Environment.NewLine;
        statsText.text ="  // Available Labor: " + CurrentLabor + "hrs" + "  // Available Power: " + CurrentPower + "kw/hr" + n+ "  // Available Water: " + CurrentWater + "gl" + "  // Available Food: " + CurrentFood + "lbs";
        populationCount.text = Population.ToString();       
    }     
    #endregion

    

}
