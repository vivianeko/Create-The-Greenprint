using UnityEngine;
using TMPro;

public class SelectedBlockInfo : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI blockname;
    public TextMeshProUGUI laborin;
    public TextMeshProUGUI laborout;
    public TextMeshProUGUI powerin;
    public TextMeshProUGUI powerout;
    public TextMeshProUGUI waterin;
    public TextMeshProUGUI waterout;
    public TextMeshProUGUI foodin;
    public TextMeshProUGUI foodout;

    // Update is called once per frame
    public void updatestats(BlockPreset _preset)
    {
        canvas.SetActive(true);
        blockname.text = _preset.name;
        laborin.text = _preset.inLabor.ToString();
        laborout.text = _preset.outLabor.ToString();
        powerin.text = _preset.inPower.ToString();
        powerout.text = _preset.outPower.ToString();
        waterin.text = _preset.inWater.ToString();
        waterout.text = _preset.outWater.ToString();
        foodin.text = _preset.inFood.ToString();
        foodout.text = _preset.outFood.ToString();
    }

    public void hidestats()
    {
        canvas.SetActive(false);
    }
}
