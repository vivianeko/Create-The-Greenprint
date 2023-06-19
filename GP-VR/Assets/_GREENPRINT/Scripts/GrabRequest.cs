using UnityEngine;
using Normal.Realtime;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabRequest : MonoBehaviour
{
    private RealtimeTransform realtimeTransform;
    private XRGrabInteractable interactable;

    void Awake()
    {
        realtimeTransform = GetComponent<RealtimeTransform>();
    }

    void Update()
    {
        if (interactable.isSelected && !realtimeTransform.isOwnedLocallySelf)
        {
            interactable.interactionManager.CancelInteractableSelection(interactable);
        }
    }

    protected void OnEnable()
    {
        interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnSelectEntered);
        }
    }

    protected void OnDisable()
    {
        if (interactable != null)
        {
            interactable.hoverEntered.RemoveListener(OnSelectEntered);
        }
    }

    public void OnSelectEntered(HoverEnterEventArgs args)
    {
        realtimeTransform.RequestOwnership();
    }

}
