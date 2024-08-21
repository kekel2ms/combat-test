using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField]
    private InputProcessor _inputProcessor;
    [SerializeField]
    private Transform _cinemachineTarget;
    [SerializeField]
    private float _cameraMinAngle = -40f;
    [SerializeField]
    private float _cameraMaxAngle = 40f;
    [SerializeField]
    private float _mouseSensitivity = 100f;

    private Vector3 _cameraAngle;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _cameraAngle = _cinemachineTarget.eulerAngles;
    }

    private void Update()
    {
        var mouseAxis = _inputProcessor.GetMouseAxis();
        _cameraAngle.x -= mouseAxis.y * _mouseSensitivity * Time.unscaledDeltaTime;
        _cameraAngle.y += mouseAxis.x * _mouseSensitivity * Time.unscaledDeltaTime;

        _cameraAngle.x = Mathf.Clamp(_cameraAngle.x, _cameraMinAngle, _cameraMaxAngle);

        _cinemachineTarget.eulerAngles = _cameraAngle;
        _cinemachineTarget.rotation = _cinemachineTarget.rotation.normalized;
    }
}
