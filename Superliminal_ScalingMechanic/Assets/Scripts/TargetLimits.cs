using UnityEngine;

public class TargetLimits : MonoBehaviour
{
    [Range(0, 50)] public float _maxScale = 30f;
    [Range(0, 50)] public float _minScale = 1f;
}
