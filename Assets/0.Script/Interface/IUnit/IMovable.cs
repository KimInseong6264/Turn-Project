using UnityEngine;

public interface IMovable
{
    Transform Move(Transform targetTransform, float speed);
}