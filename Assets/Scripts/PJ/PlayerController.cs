using UnityEngine;
public class PlayerController
{
    private PlayerMovement _playerMovement;
    private Transform _transform;
    private Vector2 _moveInputs;
    public PlayerController(PlayerMovement playerMovement,Transform transform)
    {
        _playerMovement = playerMovement;
        _transform = transform;
    }
    public void OnUpdate()
    {
        GetMoveInputs();
        if(!ObstacleInFront())
            _playerMovement.MovePlayer(_moveInputs);
        else
            _playerMovement.OnlyRotate(_moveInputs);
    }
    private void GetMoveInputs()
    {
        _moveInputs = PlayerInputsManager.instance.MoveVector();
    }
    private bool ObstacleInFront()
    {
        Vector3 origin = _transform.position + Vector3.up * 0.2f;
        Vector3 dir = _transform.forward;
        bool hit = Physics.Raycast(origin, dir, 1f, LayerMask.GetMask("Wall"));
        Debug.DrawRay(origin, dir *1f, hit ? Color.green : Color.red);
        return hit;
    }
}
