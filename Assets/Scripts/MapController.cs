using UnityEngine;
using UnityEngine.InputSystem;

public class MapAnimationController : MonoBehaviour
{
    private Animator mapAnimator;
    private bool isMapVisible = false;

    public InputActionProperty toggleMapAction;


    void Start()
    {
        mapAnimator = GetComponent<Animator>();
        toggleMapAction.action.performed += OnToggleMap;
        toggleMapAction.action.Enable();
    }

    private void OnToggleMap(InputAction.CallbackContext context)
    {
        isMapVisible = !isMapVisible;

        if (mapAnimator != null)
        {
            mapAnimator.SetBool("isMapVisible", isMapVisible);
        }
    }

}