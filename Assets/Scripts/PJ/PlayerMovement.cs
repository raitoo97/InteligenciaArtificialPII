using UnityEngine;
public class PlayerMovement
{
    private float _speed;
    private Transform _transform;
    public PlayerMovement(float speed,Transform transform)
    {
        _speed = speed;
        _transform = transform;
    }
    public void MovePlayer(Vector2 movVector)
    {
        var vectorMove = new Vector3(movVector.x, 0, movVector.y);
        if(vectorMove != Vector3.zero)
        {
            Quaternion desiredRot = Quaternion.LookRotation(vectorMove);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, desiredRot, 180 * Time.deltaTime);
        }
        _transform.position += vectorMove *  _speed *Time.deltaTime;
    }
    public void OnlyRotate(Vector2 movVector)
    {
        var vectorMove = new Vector3(movVector.x, 0, movVector.y);
        if (vectorMove != Vector3.zero)
        {
            Quaternion desiredRot = Quaternion.LookRotation(vectorMove);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, desiredRot, 180 * Time.deltaTime);
        }
    }
}