using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;

public class PlrLInput : MonoBehaviour    
{
    [SerializeField] private Animator _animator;
    [SerializeField] private InputActionProperty _gripAction;
    [SerializeField] private InputActionProperty _activateAction;
    [SerializeField] private GameObject lHand;
    [SerializeField] private GameObject hC;

    public bool canSteer;

    private void Start()
    {
        hC = GameObject.Find("hC");
    }

    private void Update()
    {
        var gripValue = _gripAction.action.ReadValue<float>();
        var actionValue = _gripAction.action.ReadValue<float>();

        _animator.SetFloat("Grip", gripValue);
        _animator.SetFloat("Trigger", actionValue);

        if (gripValue == 1 && hC.GetComponent<handCollides>().sCollides)
        {
            lHand.GetComponent<ActionBasedController>().enableInputTracking = false;
            canSteer = true;
        }
        else if (gripValue == 0)
        {
            lHand.GetComponent<ActionBasedController>().enableInputTracking = true;
            canSteer = false;
        }
    }
}
