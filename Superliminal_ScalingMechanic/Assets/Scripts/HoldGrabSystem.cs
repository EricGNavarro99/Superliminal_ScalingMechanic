using UnityEngine;

public class HoldGrabSystem : MonoBehaviour
{
    private Transform _target = null;

    public LayerMask _targetMask;
    public LayerMask _ignoreTargetMask;

    private float _originalDistance;
    private float _originalScale;

    private float _targetMaxScale = 0f;
    private float _targetMinScale = 0f;
    private bool _targetLimitsGetted = false;

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
                    _originalScale = _target.localScale.x;
                    _originalDistance = Vector3.Distance(transform.position, _target.position);
                    GetTargetLimits();
                }
            }
            else
            {
                _target.GetComponent<Rigidbody>().isKinematic = false;
                _target = null;
                _targetLimitsGetted = false;
            }
        }
    }

    private void ResizeTarget()    
    {
        if (_target == null && !_targetLimitsGetted) return;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, _ignoreTargetMask))
        {
            float distance = Vector3.Distance(transform.position, _target.position);
            float scaleMultiplier = distance / _originalDistance;

            _target.position = hit.point - (transform.forward * _target.localScale.x);

            if (_target.localScale.x < _targetMinScale || _target.localScale.y < _targetMinScale || _target.localScale.z < _targetMinScale)
            {
                _target.localScale = _targetMinScale * Vector3.one;
            }
            else if (_target.localScale.x > _targetMaxScale || _target.localScale.y > _targetMaxScale || _target.localScale.z > _targetMaxScale)
            {
                _target.localScale = _targetMaxScale * Vector3.one;
            }            
            else _target.localScale = scaleMultiplier * _originalScale * Vector3.one;
        }
    }

    private void GetTargetLimits()
    {
        if (_target == null || _targetLimitsGetted) return;

        _targetMinScale = _target.GetComponent<TargetLimits>()._minScale;
        _targetMaxScale = _target.GetComponent<TargetLimits>()._maxScale;

        _targetLimitsGetted = true;
    }
}
