using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField]
    private Transform _RightHandAnchor;
    [SerializeField]
    private Transform _LeftHandAnchor;
    [SerializeField]
    private Transform _CenterEyeAnchor;
    [SerializeField]
    private float _MaxDistance = 100.0f;
    private float _MinDistance = 0.0f;
    [SerializeField]
    private LineRenderer _LaserPointerRenderer;

    private Transform Pointer
    {
        get
        {
            var controller = OVRInput.GetActiveController();
            if (controller == OVRInput.Controller.RTrackedRemote)
            {
                return _RightHandAnchor;
            }
            else if (controller == OVRInput.Controller.LTrackedRemote)
            {
                return _LeftHandAnchor;
            }
            return _CenterEyeAnchor;
        }
    }
    void Update()
    {
        var pointer = Pointer;
        if (pointer == null || _LaserPointerRenderer == null)
        {
            return;
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            Ray pointerRay = new Ray(pointer.position, pointer.forward);

            _LaserPointerRenderer.SetPosition(0, pointerRay.origin);

            RaycastHit hitInfo;
            if (Physics.Raycast(pointerRay, out hitInfo, _MaxDistance))
            {

                _LaserPointerRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                _LaserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * _MaxDistance);
            }
        }
        else
        {
            Ray pointerRay = new Ray(pointer.position, pointer.forward);

            _LaserPointerRenderer.SetPosition(0, pointerRay.origin);

            _LaserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * _MinDistance);
        }
    }
}