using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform rightDoor;
    Rigidbody rightDoorRb;
    [SerializeField] Transform leftDoor;
    Rigidbody leftDoorRb;
    public float timeWhenStartClosing;
    public float additionToTimeWhenStartClosing;
    public float powerToClose;
    public float nearDist;
    private void Awake()
    {
        rightDoorRb = rightDoor.GetComponent<Rigidbody>();
        leftDoorRb = leftDoor.GetComponent<Rigidbody>();
    }
    private void OnTriggerExit(Collider other)
    {
        timeWhenStartClosing = Time.time + additionToTimeWhenStartClosing;
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    void Update()
    {
        if (rightDoor.localEulerAngles.y != 0 && leftDoor.localEulerAngles.y != 0)
        {
            if (Time.time > timeWhenStartClosing)
            {
                rightDoorRb.velocity = Vector3.zero;
                if (rightDoor.localEulerAngles.y > 0)
                {
                    rightDoor.localEulerAngles -= Vector3.up * powerToClose * Time.deltaTime;
                    //Vector3.Lerp(rightDoor.localEulerAngles, Vector3.zero, powerToClose);
                }else if(rightDoor.localEulerAngles.y < 0)
                {
                    rightDoor.localEulerAngles += Vector3.up * powerToClose * Time.deltaTime;
                }
                if(Mathf.Abs(rightDoor.localEulerAngles.y) < nearDist)
                {
                    rightDoor.localEulerAngles = Vector3.zero;
                }


                leftDoorRb.velocity = Vector3.zero;
                if (leftDoor.localEulerAngles.y > 0)
                {
                    leftDoor.localEulerAngles -= Vector3.up * powerToClose * Time.deltaTime;
                    //Vector3.Lerp(leftDoor.localEulerAngles, Vector3.zero, powerToClose);
                }else if(leftDoor.localEulerAngles.y < 0)
                {
                    leftDoor.localEulerAngles += Vector3.up * powerToClose * Time.deltaTime;
                }
                if (Mathf.Abs(leftDoor.localEulerAngles.y) < nearDist)
                {
                    leftDoor.localEulerAngles = Vector3.zero;
                }
            }
        }
    }
}
