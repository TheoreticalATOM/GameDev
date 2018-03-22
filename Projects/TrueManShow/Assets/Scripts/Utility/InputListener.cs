using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputListener : MonoBehaviour
{
    public string InputName;
    public bool OneUse;
    public UnityEvent OnInputPressed;

    private void Start()
    {
		enabled = false;
    }

    public void TurnOnListener()
    {
        enabled = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown(InputName))
        {
            OnInputPressed.Invoke();
            enabled = !OneUse;
        }
    }
}
