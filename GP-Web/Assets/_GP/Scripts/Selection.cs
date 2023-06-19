using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class Selection : MonoBehaviour
{
    public GameObject selectedObject;
    private BuildingManager buildingManager;
    [SerializeField] private GameObject objUi, objUiInfo, objUiStack;
    [SerializeField] private TextMeshProUGUI blockname, laborin, laborout, powerin, 
        powerout, waterin, waterout, foodin, foodout;
    [SerializeField] private BlockPreset reference;
    
    private void Start()
    {
        buildingManager = GetComponent<BuildingManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUi())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("block"))
                    Select(hit.collider.gameObject);

                if (hit.collider.gameObject.CompareTag("ground") && selectedObject != null)
                    Deselect();                
            }
        }

        if (selectedObject != null)
            if(selectedObject.GetComponent<CheckPlacement>().isOnTop == true)
                Deselect();
    }

    private void Select(GameObject obj)
    {
        if (obj == selectedObject) return;
        if (selectedObject != null) Deselect();
        Outline outline = obj.GetComponent<Outline>();
        if (outline == null)
        {
            obj.AddComponent<Outline>();
            obj.GetComponent<Outline>().OutlineWidth = 10;
        }
        else outline.enabled = true;
        ShowInfo(obj);
        selectedObject = obj;
    }

    private void Deselect()
    {
        objUiInfo.SetActive(false);
        objUiStack.SetActive(false);
        objUi.SetActive(false);
        selectedObject.GetComponent<Outline>().enabled = false;
        selectedObject = null;
    }

    private void ShowInfo(GameObject obj)
    {
        objUi.SetActive(true);
        BlockPreset _preset = obj.GetComponent<UpdateBlockStats>().preset;

        blockname.text = _preset.name;
        laborin.text = _preset.inLabor.ToString();
        laborout.text = _preset.outLabor.ToString();
        powerin.text = _preset.inPower.ToString();
        powerout.text = _preset.outPower.ToString();
        if(_preset.inWater > 1000)
            waterin.text = (_preset.inWater / 1000).ToString() + "K";
        else
            waterin.text = _preset.inWater.ToString();
        if (_preset.outWater > 1000)
            waterout.text = (_preset.outWater / 1000).ToString() + "K";
        else
            waterout.text = _preset.outWater.ToString();
        foodin.text = _preset.inFood.ToString();
        foodout.text = _preset.outFood.ToString();
    }

    public void Move()
    {
        buildingManager.placementMaterials[2] = selectedObject.GetComponent<MeshRenderer>().material;
        buildingManager.pendingObj = selectedObject;
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");
        foreach (GameObject bl in blocks)
        {
            if (bl.transform.position.x == selectedObject.transform.position.x 
                && bl.transform.position.z == selectedObject.transform.position.z 
                && bl.transform.position.y == selectedObject.transform.position.y - 11f)
                bl.GetComponent<CheckPlacement>().isOnTop = false;
        }
    }

    public void Copy()
    {
        buildingManager.SelectBlock(selectedObject.GetComponent<UpdateBlockStats>().preset);
    }

    public void Stack(BlockPreset block)
    {
        if (block != reference)
            block = selectedObject.GetComponent<UpdateBlockStats>().preset;
        
        GameObject stack = Instantiate(block.Prefab, new Vector3
            (selectedObject.transform.position.x, 
            selectedObject.transform.position.y + 11f, 
            selectedObject.transform.position.z), 
            selectedObject.transform.rotation);

        selectedObject.GetComponent<CheckPlacement>().isOnTop = true;

        InvokeCheckBlocks();
        Deselect();
        Select(stack);
    }

    public void Delete()
    {
        GameObject objToDestroy = selectedObject;
        Deselect();
        Destroy(objToDestroy);
        InvokeCheckBlocks();
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");
        foreach (GameObject bl in blocks)
        {
            if (bl.transform.position.x == objToDestroy.transform.position.x 
                && bl.transform.position.z == objToDestroy.transform.position.z 
                && bl.transform.position.y == objToDestroy.transform.position.y - 11f)
                bl.GetComponent<CheckPlacement>().isOnTop = false;
        }
    }

    private void InvokeCheckBlocks()
    {

        buildingManager.Invoke("CheckBlocks", 0.5f);
    }

    private bool IsMouseOverUi()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
