using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera references and settings")]
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform cameraPole;
        [Space]
        [SerializeField] private LayerMask cameraObstacleLayer;
        [SerializeField] private float maxCameraDistance;
        public float cameraSensitivy;
        // Touch detection
        private int rightfingerId;
        private float halfScreenWidth;
        // Camera control
        private Vector2 lookInput;
        private float cameraPitch;

        private void Start()
        {
            rightfingerId = -1;
            halfScreenWidth = Screen.width / 2;
            cameraPitch = cameraPole.localRotation.eulerAngles.x;
            maxCameraDistance = cameraTransform.localPosition.z;
        }

        private void Update()
        {
            GetTouchInput();

            if (rightfingerId != -1)
                LookAround();
        }

        private void FixedUpdate()
        {
            MoveCamera();
        }

        private void GetTouchInput()
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                var t = Input.GetTouch(i);

                switch (t.phase)
                {
                    case TouchPhase.Began:
                        if (t.position.x > halfScreenWidth && rightfingerId == -1)
                            rightfingerId = t.fingerId;
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if (t.fingerId == rightfingerId)
                            rightfingerId = -1;
                        break;
                    case TouchPhase.Moved:
                        if (t.fingerId == rightfingerId)
                            lookInput = t.deltaPosition * (cameraSensitivy * Time.deltaTime);
                        break;
                    case TouchPhase.Stationary:
                        if (t.fingerId == rightfingerId)
                            lookInput = Vector2.zero;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void LookAround()
        {
            transform.Rotate(transform.up, lookInput.x);
        }

        private void MoveCamera()
        {
            var rayDir = cameraTransform.position - cameraPole.position;

            if (Physics.Raycast(cameraPole.position, rayDir, out RaycastHit hit, Mathf.Abs(maxCameraDistance), cameraObstacleLayer))
            {
                cameraTransform.position = hit.point;
            }
            else
            {
                cameraTransform.localPosition = new Vector3(0, 0, maxCameraDistance);
            }
        }
    }
}