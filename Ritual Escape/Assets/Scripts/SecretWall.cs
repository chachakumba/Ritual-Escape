using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretWall : MonoBehaviour
{
    bool opened = false;
    bool opening = false;
    public float openTime = 3;
    public float openSpeed = 1;
    public float scaleSpeed = 1;
    public Vector3 doorOpenDir = Vector3.right;
    public float openAmount = 2;
    public float openDepth = 0.1f;
    Coroutine cour;
    [SerializeField] Transform insideWall;
    [SerializeField] Transform outsideWall;
    Vector3 startPosOut, startPosIn;
    float baseMagnitudeIn, baseMagnitudeOut;
     void Awake()
    {
        startPosIn = insideWall.localPosition;
        startPosOut = outsideWall.localPosition;
        baseMagnitudeIn = startPosIn.magnitude;
        baseMagnitudeOut = startPosOut.magnitude;
    }
    public void Use()
    {
        opened = !opened;
        Debug.Log($"Started opening. state {opened}");
        if (cour == null)
            cour = StartCoroutine(OpenCour());
    }
    IEnumerator OpenCour()
    {
        Debug.Log("StartedCour");
        if (opened)
        {
            Debug.Log("Started squishing");
            while (insideWall.localScale.z > openDepth) {
                insideWall.localScale -= Vector3.forward * Time.deltaTime * scaleSpeed;
                outsideWall.localScale -= Vector3.forward * Time.deltaTime * scaleSpeed;
                yield return null;
            }
            insideWall.localScale = new Vector3(insideWall.localScale.x, insideWall.localScale.y,openDepth);
            outsideWall.localScale = new Vector3(outsideWall.localScale.x, outsideWall.localScale.y, openDepth);
        }
        while (outsideWall.localPosition.magnitude- baseMagnitudeOut <= openAmount && outsideWall.localPosition.magnitude-baseMagnitudeOut >= 0)
        {
            Debug.Log("moving");
            if (opened)
            {
                insideWall.localPosition += doorOpenDir * Time.deltaTime * openSpeed;
                outsideWall.localPosition += doorOpenDir * Time.deltaTime * openSpeed;
            }
            else
            {
                insideWall.localPosition -= doorOpenDir * Time.deltaTime * openSpeed;
                outsideWall.localPosition -= doorOpenDir * Time.deltaTime * openSpeed;
            }
            yield return null;
        }

        if (opened)
        {
            insideWall.localPosition = startPosIn + doorOpenDir * openAmount;
            outsideWall.localPosition = startPosOut + doorOpenDir * openAmount;
        }
        else
        {
            insideWall.localPosition = startPosIn;
            outsideWall.localPosition = startPosOut;
        }

        if (!opened)
        {
            Debug.Log("Started inflating");
            while (insideWall.localScale.z < 1)
            {
                insideWall.localScale += Vector3.forward * Time.deltaTime * scaleSpeed;
                outsideWall.localScale += Vector3.forward * Time.deltaTime * scaleSpeed;
                yield return null;
            }
            insideWall.localScale = new Vector3(insideWall.localScale.x, insideWall.localScale.y, 1);
            outsideWall.localScale = new Vector3(outsideWall.localScale.x, outsideWall.localScale.y, 1);
            opened = false;
        }
        else
            opened = true;
        cour = null;
    }
}