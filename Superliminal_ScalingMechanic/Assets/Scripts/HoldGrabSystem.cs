using UnityEngine;

public class HoldGrabSystem : MonoBehaviour
{
    private Transform _target = null;

    public LayerMask _targetMask;
    public LayerMask _ignoreTargetMask;

    private float _originalDistance;
    private float _originalScale;

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
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

            var positionOffset = transform.forward * _target.localScale.x;

            _target.position = hit.point - positionOffset;

            float distance = Vector3.Distance(transform.position, _target.position);
            float scaleMultiplier = distance / _originalDistance;

            _target.localScale = scaleMultiplier * _originalScale * Vector3.one;
        }
    }
}
