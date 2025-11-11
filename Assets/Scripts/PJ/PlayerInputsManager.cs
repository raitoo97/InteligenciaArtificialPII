using UnityEngine;
[DefaultExecutionOrder(-70)]
public class PlayerInputsManager : MonoBehaviour
{
    private PlayerInputs _inputAction;
    public static PlayerInputsManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    private void OnEnable()
    {
        _inputAction = new PlayerInputs();
        _inputAction.Enable();
    }
    public Vector2 MoveVector()
    {
        return _inputAction.PlayerMovement.Move.ReadValue<Vector2>();
    }
    private void OnDisable()
    {
        _inputAction.Disable();
        _inputAction = null;
    }
}