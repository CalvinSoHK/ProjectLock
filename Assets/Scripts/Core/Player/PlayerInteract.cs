using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;
using CustomInput;

/// <summary>
/// Allows player to interact with interactables
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    bool interactOn = true;

    [SerializeField]
    float interactRange;

    [SerializeField]
    LayerMask layers;

    private const float INTERACT_ENABLE_TICK = 1f;

    /// <summary>
    /// Checks to see if already enabling. If so prevent further enable interact calls.
    /// </summary>
    private bool enabling = false;

    private void Update()
    {
        CheckInteract();
    }

    private void CheckInteract()
    {
        if (interactOn)
        {
            if (Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Interact, InputEnums.InputAction.Down))
            {
                RaycastHit info = new RaycastHit();
                if (Physics.Raycast(transform.position, transform.forward, out info, interactRange, layers))
                {
                    InteractableObject obj = info.collider.GetComponent<InteractableObject>();
                    if (obj)
                    {
                        obj.Interact();
                    }
                }
            }
        }
    }
       
    /// <summary>
    /// Disables player interaction
    /// </summary>
    public void DisableInteract()
    {
        interactOn = false;
    }

    /// <summary>
    /// Enables player interaction (technically on the frame after).
    /// </summary>
    public void EnableInteract()
    {
        if (!enabling)
        {
            StartCoroutine(EnableOnNextFrame());
            enabling = true;
        }   
    }

    /// <summary>
    /// Enables interact on next frame.
    /// NOTE: Using this helps with consistency. Without it sometimes you will interact with the thing you are looking at after finishing it.
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnableOnNextFrame()
    {
        yield return new WaitForSeconds(INTERACT_ENABLE_TICK);
        interactOn = true;
        enabling = false;
    }
}
