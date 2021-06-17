using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class VacuumController : MonoBehaviour
    {
        [SerializeField] private Transform _playerBody;
        [SerializeField] private Transform _vacuumRotation;
        // [SerializeField] private Transform _shouldersRotations;
        // [SerializeField] private Transform _shouldersPoint;
        [SerializeField] private Transform _cameraPointer;

        private Camera _mainCamera;
        void Start()
        {
            _mainCamera = Camera.main;
        }

        void Update()
        {
            _cameraPointer.position = _mainCamera.WorldToScreenPoint(_vacuumRotation.position);
            _cameraPointer.LookAt(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cameraPointer.position.z), Vector3.up);

            ClampRotation(_cameraPointer);
            // _vacuumRotation.rotation = _cameraPointer.rotation;

            // _shouldersRotations.LookAt(_shouldersPoint, Vector3.up);

            if (Input.mousePosition.x > _cameraPointer.position.x + 10)
                _playerBody.rotation = Quaternion.Euler(-90, 90, _playerBody.transform.position.z);
            else if (Input.mousePosition.x < _cameraPointer.position.x - 10)
                _playerBody.rotation = Quaternion.Euler(-90, -90, _playerBody.transform.position.z);
        }   


        private void ClampRotation(Transform rot)
        {
            float val = rot.localEulerAngles.x;
            while (val < 0)
            {
                val = val + 360f;
            }
            while (val > 360)
            {
                val = val - 360f;
            }

            if (val < 40 || (val > 300) || (val < 240 && val > 140))
            {
                _vacuumRotation.rotation = rot.rotation;
            }
        }
    }
}
