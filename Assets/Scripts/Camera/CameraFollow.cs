using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    private Vector3 _offset;
    private Player _playerRef;
    void Start()
    {
        _playerRef = GameManager.instance.player;
        _offset = this.transform.position - _playerRef.transform.position;
    }
    private void LateUpdate()
    {
        if (_playerRef == null) return;
        transform.position = Vector3.Lerp(this.transform.position, _playerRef.transform.position + _offset, 5 * Time.deltaTime);
    }
}
