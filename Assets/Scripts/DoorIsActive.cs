using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorIsActive : MonoBehaviour
{
    MonoBehaviour[] components;
    [SerializeField] bool isDoorEnable = true;

    private void Awake()
    {
        components = new MonoBehaviour[3];

        components[0] = GetComponentInChildren<Outline>();
        components[1] = GetComponentInChildren<OutlineObjects>();
        components[2] = GetComponentInChildren<InteractDoor>();
    }

    private void Start()
    {
        ToggleComponents(isDoorEnable);
    }

    private void OnEnable()
    {
        ToggleComponents(true);
    }

    private void OnDisable()
    {
        ToggleComponents(false);
    }

    public void ToggleComponents(bool isActive)
    {
        foreach (MonoBehaviour component in components)
        {
            if (component != null) // Verifica si el componente existe antes de habilitarlo/deshabilitarlo
            {
                component.enabled = isActive;
            }
        }
    }
}