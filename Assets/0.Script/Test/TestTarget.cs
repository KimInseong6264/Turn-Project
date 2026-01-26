using UnityEngine;

public class TestTarget : MonoBehaviour, IHitable
{
    public Transform GetTransform =>  transform;
}
