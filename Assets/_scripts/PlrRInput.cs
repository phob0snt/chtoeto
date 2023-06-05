using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlrRInput : MonoBehaviour    
{
    [SerializeField] private Animator _animator;
    [SerializeField] private InputActionProperty _gripAction;
    [SerializeField] private InputActionProperty _activateAction;


    private void Update()
    {
        var gripValue = _gripAction.action.ReadValue<float>();
        var actionValue = _gripAction.action.ReadValue<float>();

        _animator.SetFloat("Grip", gripValue);
        _animator.SetFloat("Trigger", actionValue);
    }
}
