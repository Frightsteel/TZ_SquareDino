using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private GameInputs _input;

    public bool _isShooted { get; private set; }

    private void Awake()
    {
        _input = new GameInputs();

        _input.Player.Shoot.started += context => _isShooted = context.ReadValueAsButton();
        _input.Player.Shoot.performed += context => _isShooted = context.ReadValueAsButton();

    }

    private void OnEnable()
    {
        _input.Player.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Disable();
    }
}