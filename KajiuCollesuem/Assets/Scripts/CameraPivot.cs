using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public GameObject Target;
    private PlayerCamera _camera;
    public Vector3 offSet = new Vector3(0, 1, 0);

    public bool runFunc = false;
    public Vector3 DemoVector;
    public float DemoVar1;
    public float DemoVar2;
    public float DemoVar3;
    public float DemoVar4;
    public float DemoVar5;
    public float DemoVar6;

    void Start()
    {
        //Getting Reference to the camera
        _camera = GetComponentInChildren<PlayerCamera>();
    }

    void Update()
    {
        //Position the camera pivot on the player
        transform.position = Target.transform.position + offSet;

        if (runFunc)
        {
            ChangePlayerCamera(DemoVector, DemoVar1, DemoVar2, DemoVar3, DemoVar4, DemoVar5, DemoVar6);
            runFunc = false;
        }
    }

    public void ChangePlayerCamera(Vector3 pOffSet, float pMouseSensitivity, float pTurnDampening, float pCameraDistance, float pCameraMinHeight, float pCameraMaxHeight, float pTransitionSpeed)
    {
        StopCoroutine("CameraOffSetTransition");
        StartCoroutine(CameraOffSetTransition(pOffSet, pTransitionSpeed));
        _camera.ChangePlayerCamera(pMouseSensitivity, pTurnDampening, pCameraDistance, pCameraMinHeight, pCameraMaxHeight, pTransitionSpeed);
    }

    private IEnumerator CameraOffSetTransition(Vector3 pOffSet, float pTransitionSpeed)
    {
        while (offSet != pOffSet)
        {
            offSet = Vector3.Lerp(offSet, pOffSet, pTransitionSpeed * Time.deltaTime);
            if (Vector3.Distance(offSet, pOffSet) <= 0.01f)
                offSet = pOffSet;

            yield return null;
        }
    }
}
