using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public GameObject target;
    private PlayerCamera _camera;
    public float offSetUp = 0.6f;

    //For Experimenting
    [Space]
    [SerializeField] private bool runFunc1 = false;
    [SerializeField] private SOCamera DemoVarsPreset;

    //For Experimenting
    [Space]
    [SerializeField] private bool runFunc2 = false;
    [SerializeField] private float DemoVarOffup = 0.6f;
    [SerializeField] private float DemoVarOffleft = 0;
    [SerializeField] private float DemoVarSensi = 3;
    [SerializeField] private float DemoVarDamp = 20;
    [SerializeField] private float DemoVarDis = 6;
    [SerializeField] private float DemoVarMiny = -8;
    [SerializeField] private float DemoVarMaxy = 70;
    [SerializeField] private float DemoVarTrnsSpd = 1;

    void Start()
    {
        //Getting Reference to the camera
        _camera = GetComponentInChildren<PlayerCamera>();
    }

    void Update()
    {
        //Position the camera pivot on the player
        transform.position = target.transform.position + (Vector3.up * offSetUp);


        //For Experimenting
        if (runFunc1)
        {
            ChangePlayerCamera(DemoVarsPreset, DemoVarTrnsSpd);
            runFunc1 = false;
        }

        //For Experimenting
        if (runFunc2)
        {
            ChangePlayerCamera(DemoVarOffup, DemoVarOffleft, DemoVarSensi, DemoVarDamp, DemoVarDis, DemoVarMiny, DemoVarMaxy, DemoVarTrnsSpd);
            runFunc2 = false;
        }
    }

    

    // Camera Transition //////////////
    public void ChangePlayerCamera(float pOffSetUp, float pOffSetLeft, float pMouseSensitivity, float pTurnDampening, float pCameraDistance, float pCameraMinHeight, float pCameraMaxHeight, float pTransitionSpeed)
    {
        StopCoroutine("CameraOffSetTransition");
        StartCoroutine(CameraOffSetTransition(pOffSetUp, pTransitionSpeed));
        _camera.ChangePlayerCamera(pOffSetLeft, pMouseSensitivity, pTurnDampening, pCameraDistance, pCameraMinHeight, pCameraMaxHeight, pTransitionSpeed);
    }

    public void ChangePlayerCamera(SOCamera pPreset, float pTransitionSpeed)
    {
        StopCoroutine("CameraOffSetTransition");
        StartCoroutine(CameraOffSetTransition(pPreset.UpOffset, pTransitionSpeed));
        _camera.ChangePlayerCamera(pPreset.LeftOffset, pPreset.Sensitivity, pPreset.TurnDampening, pPreset.Distance, pPreset.MinY, pPreset.MaxY, pTransitionSpeed);
    }

    private IEnumerator CameraOffSetTransition(float pOffSetUp, float pTransitionSpeed)
    {
        while (offSetUp != pOffSetUp)
        {
            offSetUp = Mathf.Lerp(offSetUp, pOffSetUp, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(offSetUp - pOffSetUp) <= 0.01f)
                offSetUp = pOffSetUp;

            yield return null;
        }
    }
}
