using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _zOffset;
    [SerializeField] public List<Transform> _cameraPointList;
    private Transform currentPointTransform;

    private void Start()
    {
        currentPointTransform = transform;
    }

    void LateUpdate()
    {
        /*
        transform.position = Vector3.Lerp(transform.position,
                                        new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z - _zOffset),
                                        Time.deltaTime * _followSpeed);
                                        */
        transform.position = Vector3.Lerp(transform.position, currentPointTransform.position, Time.deltaTime * _followSpeed);
    }

    public void goToRoom(int roomNumber)
    {
        currentPointTransform = _cameraPointList[roomNumber];
    }


}
