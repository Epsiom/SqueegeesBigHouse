using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUpgrade : MonoBehaviour
{

    [SerializeField] private int _upgradeType;
    private BoxCollider _playerVacuumCollider;

    // Start is called before the first frame update
    void Start()
    {
        _playerVacuumCollider = GameObject.Find("vacuum").GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        transform.position = transform.position + new Vector3(0.0f, Mathf.Sin(Time.time)/2, 0.0f) * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utils.Constants.TAG_PLAYER))
        {
            switch (_upgradeType)
            {
                //TODO
                case 0:
                    //Upgrade player range
                    _playerVacuumCollider.center = _playerVacuumCollider.center + new Vector3(0f, 0f, -0.23f);
                    _playerVacuumCollider.size = _playerVacuumCollider.size + new Vector3(0f,0f,0.45f);
                    break;
                case 1:
                    
                    break;
                case 2:
                    
                    break;
                default:
                    
                    break;
            }
            Destroy(gameObject);
        }
    }
}
