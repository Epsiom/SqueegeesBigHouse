using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [SerializeField] private Transform ShoulderParent;
    [SerializeField] private GameObject armature;
    [SerializeField] private GameObject testCube;
    private Vector3 mousePos;
    private Vector3 direction;
    [SerializeField] private float angleOffset;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(ShoulderParent.transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        
        //Ta Daaa
        
        while(angle < 0)
        {
            angle = angle + 360f;
        }
        while(angle > 360)
        {
            angle = angle - 360f;
        }
        //Debug.Log(angle);

        if (angle < 240 && angle > 140)
        {

            //angle = Mathf.Clamp(angle, 0,0 );
            ShoulderParent.transform.localRotation = Quaternion.Euler(new Vector3(180 + -angle + angleOffset, 0f, 0f));

        }
        else if(angle < 40 || angle > 300)
        {
            
            //angle = Mathf.Clamp(angle,120,230);
            ShoulderParent.transform.localRotation = Quaternion.Euler(new Vector3(angle+ angleOffset, 0f, 0f));
        }

        if( mouseOnScreen.x > positionOnScreen.x + 0.05)
        {
            armature.transform.rotation = Quaternion.Euler(new Vector3(-90f, -90f, 180f));
        }
        else if(mouseOnScreen.x < positionOnScreen.x - 0.05)
        {
            armature.transform.rotation = Quaternion.Euler(new Vector3(-90f, -90f, 0));
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
