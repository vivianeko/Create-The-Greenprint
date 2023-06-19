using UnityEngine;
using UnityEngine.InputSystem;

public class WristMenu : MonoBehaviour
{

    public GameObject wristUI;
    public bool activeWristUI = true;
    public InputActionReference wristUiReference = null;

    void Start()
    {
        DisplayWristUI();
        
    }

    private void Awake()
    {
        wristUiReference.action.started += MenuPressed;
    }


    private void OnDestroy()
    {
        wristUiReference.action.started -= MenuPressed;
    }

    private void MenuPressed(InputAction.CallbackContext context)
    {
        DisplayWristUI();
    }
    public void DisplayWristUI()
    {
        if (activeWristUI)
        {
            wristUI.SetActive(false);
            activeWristUI = false;
        }

        else if (!activeWristUI)
        {
            wristUI.SetActive(true);
            activeWristUI = true;
        }
    }
}
