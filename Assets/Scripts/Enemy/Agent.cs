using UnityEngine;
public abstract class Agent : MonoBehaviour
{
    protected Vector3 _velocity;
    [SerializeField]protected float _maxVelocity;
    [SerializeField]protected float _maxSteeringForce;
    [SerializeField]protected float _radiusArrive;
    [SerializeField]protected bool _canMove;
    protected virtual void Update()
    {
        if(!_canMove)return;
        this.transform.position += _velocity * Time.deltaTime;
    }
    protected void AddForce(Vector3 dir)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + dir, _maxVelocity);
        _velocity.y = 0;
    }
    protected Vector3 Seek(Vector3 target)
    {
        var desired = target - this.transform.position;
        desired.Normalize();
        desired *= _maxVelocity;
        var steer = desired - _velocity;
        steer = Vector3.ClampMagnitude(steer, _maxSteeringForce);
        return steer;
    }
    protected Vector3 Arrive(Vector3 target)
    {
        var desired = target - this.transform .position;
        var dis = desired.magnitude;
        if(dis > _radiusArrive)
            return Seek(target);
        desired.Normalize();
        desired *= _maxVelocity * (dis / _radiusArrive);
        var steer = desired - _velocity;
        steer = Vector3.ClampMagnitude(steer, _maxSteeringForce);
        return steer;
    }
    public void GetSeekForce(Vector3 target)
    {
        AddForce(Seek(target));
    }
    public void GetArriveForce(Vector3 target)
    {
        AddForce(Arrive(target));
    }
    public void ChangeMove(bool canMove)
    {
        _canMove = canMove;
    }
}
