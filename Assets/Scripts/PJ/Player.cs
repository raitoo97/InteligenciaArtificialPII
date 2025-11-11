using UnityEngine;
public class Player : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerController _playerController;
    [SerializeField]private float _playerSpeed;
    private void OnEnable()
    {
        _playerMovement = new PlayerMovement(_playerSpeed, this.transform);
        _playerController = new PlayerController(_playerMovement,this.transform);
    }
    private void Update()
    {
        _playerController.OnUpdate();
    }
    private void OnDisable()
    {
        _playerMovement = null;
        _playerController = null;
    }
}
