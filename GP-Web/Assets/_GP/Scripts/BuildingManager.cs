using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public bool canPlace;
    public GameObject pendingObj;
    private GameObject[] blocks;
    private Vector3 pos;
    private RaycastHit hit;
    private int week = 0;
    
    [SerializeField] private float gridSize;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rotationAmount;
    public Material[] placementMaterials;

    [SerializeField] private TextMeshProUGUI CurrentLaborText, CurrentPowerText, CurrentWaterText, CurrentFoodText, populationCount, weekText,
        shelterCountText, powerCountText, waterCountText, foodCountText;
    [SerializeField] private Button statsLogo;

    private int Population = 0;
    private float CurrentLabor = 0, CurrentPower = 0, CurrentWater = 0, CurrentFood = 0, 
        shelterCount = 0, powerCount = 0, waterCount = 0, foodCount = 0;


    [SerializeField] private BlockPreset water, energy, food, shelter;


    private void Start()
    {
        InvokeRepeating("NextWeek", 0.1f, 60f);
    }


    private void Update()
    {
        if(pendingObj != null)
        {
            UpdateMaterials();
            pendingObj.transform.position = new Vector3(
                RoundToNearestGrid(pos.x),
                RoundToNearestGrid(pos.y),
                RoundToNearestGrid(pos.z));

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceBlock();
            }

            if (Input.GetMouseButtonDown(1))
            {
                RotateBlock();
            }
        }
    }

    private void PlaceBlock()
    {
        pendingObj.transform.position = new Vector3(pendingObj.transform.position.x, pendingObj.transform.position.y + 5f, pendingObj.transform.position.z);
        pendingObj.GetComponent<MeshRenderer>().material = placementMaterials[2];
        pendingObj = null;
        CheckBlocks();
    }

    private void RotateBlock()
    {
        pendingObj.transform.Rotate(Vector3.up, rotationAmount);
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectBlock(BlockPreset block)
    {
        pendingObj = Instantiate(block.Prefab, pos, transform.rotation);
        placementMaterials[2] = pendingObj.GetComponent<MeshRenderer>().material;
    }

    float RoundToNearestGrid(float pos)
    {
        float dif = pos % gridSize;
        pos -= dif;
        if(dif > (gridSize / 2))
        {
            pos += gridSize;
        }

        return pos;
    }

    private void UpdateMaterials()
    {
        if (canPlace)
        {
            pendingObj.GetComponent<MeshRenderer>().material = placementMaterials[0];
        }

        if (!canPlace)
        {
            pendingObj.GetComponent<MeshRenderer>().material = placementMaterials[1];
        }
    }


    public void CheckBlocks()
    {
        Population = 0;
        CurrentLabor = 0;
        CurrentPower = 0;
        CurrentWater = 0;
        CurrentFood = 0;

        shelterCount = 0;
        powerCount = 0;
        waterCount = 0;
        foodCount = 0;

        blocks = GameObject.FindGameObjectsWithTag("block");
        foreach (GameObject bl in blocks)
        {
            BlockPreset pr = bl.GetComponent<UpdateBlockStats>().preset;
            Population += pr.Population;
            CurrentLabor = CurrentLabor + pr.outLabor - pr.inLabor;
            CurrentPower = CurrentPower + pr.outPower - pr.inPower;
            CurrentWater = CurrentWater + pr.outWater - pr.inWater;
            CurrentFood = CurrentFood + pr.outFood - pr.inFood;

            if (pr == food)
                foodCount++;
            if (pr == water)
                waterCount++;
            if (pr == energy)
                powerCount++;
            if (pr == shelter)
                shelterCount++;
        }
        UpdateStats();
    }

    public void UpdateStats()
    {
        populationCount.text = Population.ToString();
        CurrentFoodText.text = CurrentFood.ToString();
        CurrentLaborText.text = CurrentLabor.ToString();
        CurrentPowerText.text = CurrentPower.ToString();
        if(CurrentWater>1000)
            CurrentWaterText.text = (CurrentWater/1000).ToString() +"K";
        else
            CurrentWaterText.text = CurrentWater.ToString();

        foodCountText.text = foodCount.ToString();
        shelterCountText.text = shelterCount.ToString();
        powerCountText.text = powerCount.ToString();
        waterCountText.text = waterCount.ToString();

        if (CurrentFood < 0)
            CurrentFoodText.color = Color.red;
        else
            CurrentFoodText.color = Color.green;

        if (CurrentLabor < 0)
            CurrentLaborText.color = Color.red;
        else
            CurrentLaborText.color = Color.green;
        
        if (CurrentPower < 0)
            CurrentPowerText.color = Color.red;
        else
            CurrentPowerText.color = Color.green;
        
        if (CurrentWater < 0)
            CurrentWaterText.color = Color.red;
        else
            CurrentWaterText.color = Color.green;

        if (CurrentFood < 0 || CurrentLabor < 0 || CurrentPower < 0 || CurrentWater < 0)
            statsLogo.image.color = Color.red;
        else
            statsLogo.image.color = Color.green;

    }

    private void NextWeek()
    {
        week++;
        weekText.text = "Week "+ week;
    }
}
