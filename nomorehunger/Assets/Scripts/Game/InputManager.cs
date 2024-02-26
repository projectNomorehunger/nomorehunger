using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* NOT USED CODE */
public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public bool MenuOpenCloseInput {  get; private set; }

    private PlayerInput _playerInput;

    private InputAction _menuOpenCloseAction;
    private InputAction _questMenuOpenCloseAction;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } 

        _playerInput = GetComponent<PlayerInput>();
        _menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
    }                                                                                                                                                                                                                       

    private void Update()
    {
        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
    }


}
