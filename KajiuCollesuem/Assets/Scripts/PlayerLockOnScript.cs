using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockOnScript : MonoBehaviour
{
    private PlayerCamera cameraScript;
    private LayerMask enemiesLayer;
    public float lockOnRange = 10;

    // Start is called before the first frame update
    void Start()
    {
        cameraScript = Camera.main.GetComponent<PlayerCamera>();
        enemiesLayer = LayerMask.NameToLayer("Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if (cameraScript.lockOnTarget == null)
            {
                cameraScript.lockOnTarget = pickNewTarget();
            } else 
            {
                cameraScript.lockOnTarget = null;
            }
        //} else if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.05f && cameraScript.lockOnTarget != null)
        //{
            //cameraScript.lockOnTarget = pickNewTarget();
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
                if (Vector3.Distance(transform.forward * 5, possibleTargets[i].transform.position) < Vector3.Distance(transform.forward * 5, possibleTargets[currentClosest].transform.position))
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
