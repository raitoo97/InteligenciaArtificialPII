using UnityEngine;
public static class FOV
{
    public static bool InFOV(Transform target,Transform originPosition,float viewRadius,float viewAngle)
    {
        var dis = target.position - originPosition.position;
        if (dis.magnitude < viewRadius)
        {
            var angleToTarget = Vector3.Angle(originPosition.forward, dis);
            if (angleToTarget < viewAngle * 0.5f)
            {
                return LineOfSight.IsOnSight(originPosition.position, target.position);
            }
        }
        return false;
    }
}