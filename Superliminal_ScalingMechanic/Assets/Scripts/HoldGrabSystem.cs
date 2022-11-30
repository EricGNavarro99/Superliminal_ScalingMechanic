using UnityEngine;

public class HoldGrabSystem : MonoBehaviour
{
    private Transform _target = null;

    public LayerMask _targetMask;
    public LayerMask _ignoreTargetMask;
    public float _offsetFactor = 1f;

    private Vector3 _targetScale;
    private float _originalDistance;
    private float _originalScale;

    private void Update()
    {
        HandleInput();
        ResizeTarget();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_target == null)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, _targetMask))
                {
                    _target = hit.transform;
                    _target.GetComponent<Rigidbody>().isKinematic = true;
                    _originalDistance = Vector3.Distance(transform.position, _target.position);
                    _originalScale = _target.localScale.x;
                    _targetScale = _target.localScale;
                }
            }
            else
            {
                _target.GetComponent<Rigidbody>().isKinematic = false;
                _target = null;
            }
        }
    }

    private void ResizeTarget()
    {
        if (_target == null) return;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, _ignoreTargetMask))
        {
            _target.position = hit.point - transform.forward * _offsetFactor * _targetScale.x;

            float currentDistance = Vector3.Distance(transform.position, _target.position);
            _targetScale.x = _targetScale.y = _targetScale.z = currentDistance / _originalDistance;

            _target.localScale = _targetScale * _originalScale;
        }
    }
}
