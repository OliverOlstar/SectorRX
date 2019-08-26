using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockOnScript : MonoBehaviour
{
    // OLIVER - This script tells the PlayerCamera Script weather to be locked on or not and who to lock on too.

    private PlayerCamera cameraScript;
    private LayerMask enemiesLayer;
    [SerializeField] private float lockOnRange = 10.0f;

    [HideInInspector] public bool lockOnInput = false;
    [HideInInspector] public bool focusedOnScreen = false;
    [HideInInspector] public bool unfocusedOnScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraScript = Camera.main.GetComponent<PlayerCamera>();
        enemiesLayer = LayerMask.NameToLayer("Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        // TODO make work with pause screen
        
        if (lockOnInput == true)
        {
            lockOnInput = false;

            if (cameraScript.lockOnTarget == null)
            {
                cameraScript.lockOnTarget = pickNewTarget();
            }
            else
            {
                cameraScript.lockOnTarget = null;
            }
            //else if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.05f && cameraScript.lockOnTarget != null)
            //{
            //cameraScript.lockOnTarget = pickNewTarget();
            //}
        }
    }

    Transform pickNewTarget()
    {
        Collider[] possibleTargets = Physics.OverlapSphere(transform.position, lockOnRange, 1<<enemiesLayer);

        if (possibleTargets.Length == 0)
            return null;

        int currentClosest = 0;
        int secondClosest = 0;

        //Finds the two closest options
        for (int i = 0; i < possibleTargets.Length; i++)
        {

            float view = Camera.main.WorldToViewportPoint(possibleTargets[i].transform.position).x;
            float bestView = Camera.main.WorldToViewportPoint(possibleTargets[currentClosest].transform.position).x;

            if (Mathf.Abs(view - 0.5f) < Mathf.Abs(bestView - 0.5f))
            {
                //if (Vector3.Distance(transform.forward * 5, possibleTargets[i].transform.position) < Vector3.Distance(transform.forward * 5, possibleTargets[currentClosest].transform.position))
                secondClosest = currentClosest;
                currentClosest = i;
            }
        }
        
        if (cameraScript.lockOnTarget == possibleTargets[currentClosest].gameObject)
        {
            return possibleTargets[secondClosest].gameObject.transform;
        }

        return possibleTargets[currentClosest].gameObject.transform;
    }
}
