using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{

    [SerializeField] private int _roomNumber;
    private GameObject _camera;
    private CameraFollow _cameraScript;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("Main Camera");
        _cameraScript = _camera.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.Constants.TAG_PLAYER))
        {
            _cameraScript.goToRoom(_roomNumber);
        }
    }

    void OnTriggerExit(Collider other)
    {
        
    }
}
