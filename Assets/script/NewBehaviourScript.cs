using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public ARCameraManager ARCameraManager;
    public LineRenderer line;
    private List<ARRaycastHit> hitpoint = new List<ARRaycastHit>();
    public GameObject Mark;
    public GameObject point;
    private GameObject p1;
    private GameObject p2;
    private GameObject startPoint;
    private GameObject endPoint;
    private LineRenderer line1;
    bool isComplete = false;
    bool isPlace = false;
    public TMP_Text distanceText;
    private TMP_Text text1;
    private Transform OtherObjectA; // First object of pair
    private Transform OtherObjectB; // Second object of pair
    // public TextMeshProUGUI output;
    double distance;
    int value;
    string unit = "m";

    public void HandleInputData(int val)
    {
        if(val == 0)
        {
            value=0;
            unit = "m";
        }
        if(val == 1)
        {
            value=1;
            unit = "cm";
        }
        if(val == 2)
        {
            value=2;
            unit = "feet";
        }
        if(val == 3)
        {
            value =3;
            unit = "inch";
        }
    }

    void Update()
    {
        arRaycastManager.Raycast(new Vector2(Screen.width/2,Screen.height/2),hitpoint,TrackableType.Planes);
        if(hitpoint.Count >0)
        {
        Mark.transform.position = hitpoint[0].pose.position;
        Mark.transform.rotation = hitpoint[0].pose.rotation;
        }
        if(isPlace==true)
        {
            line1.SetPosition(1,Mark.transform.position);
            endPoint = Mark;
        }
        if(isComplete == true)
        {
            OtherObjectA = startPoint.transform;
            OtherObjectB = endPoint.transform;
            distance = Vector3.Distance(startPoint.transform.position,endPoint.transform.position);
            if(value==0)
            {
                text1.text = distance.ToString("F2") + unit;
            }
            else if(value==1)
            {
                text1.text = (distance*100).ToString("F2") + unit;
            }
            else if(value==3)
            {
                text1.text = (distance*39.37).ToString("F2") + unit;
            }
            else if(value==2)
            {
                text1.text = (distance*3.281).ToString("F2") + unit;
            }
            
            Vector3 directionCtoA = OtherObjectA.position - new Vector3(0,0,0); // directionCtoA = positionA - positionC
            Vector3 directionCtoB = OtherObjectB.position - new Vector3(0,0,0); // directionCtoB = positionB - positionC
            Vector3 directionAtoB = OtherObjectB.position - OtherObjectA.position; // directionAtoB = target.position - source.position
            text1.transform.position = new Vector3((directionCtoA.x+directionCtoB.x)/2.0f,(directionCtoA.y+directionCtoB.y)/2.0f,(directionCtoA.z+directionCtoB.z)/2.0f); // midpoint between A B
            text1.transform.rotation = ARCameraManager.transform.rotation;
        }

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void GameobjectDestroy()
    {
        GameObject[] deletes = GameObject.FindGameObjectsWithTag("delete");
        foreach(GameObject delete in deletes)
        GameObject.Destroy(delete);
    }
    public void Btn_pressed()
    {
       if(isPlace == false)
       {
        p1 = Instantiate(point,Mark.transform.position,Quaternion.identity);
        line1 = Instantiate(line);
        text1 = Instantiate(distanceText);
        line1.SetPosition(0,p1.transform.position);
        isPlace = true;
        startPoint = p1;
        isComplete =true;
       }
       else
       {
           p2 = Instantiate(point,Mark.transform.position,Quaternion.identity);
           line1.SetPosition(1,p2.transform.position);
           isPlace = false;
           isComplete = false;
           endPoint.transform.position = p2.transform.position;
       }
    }
}
