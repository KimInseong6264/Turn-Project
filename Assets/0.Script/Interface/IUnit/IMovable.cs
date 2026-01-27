using UnityEngine;

public interface IMovable
{
    Transform Move(Vector3 targetPos, float speed);
}