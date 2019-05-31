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
                cameraScript.lockOnTarget= pickNewTarget();
            } else 
            {
                cameraScript.lockOnTarget = null;
            }
        } else if (Input.GetAxis("MouseX") > 0.1f)
        {
            cameraScript.lockOnTarget = pickNewTarget();
        }
    }

    Transform pickNewTarget()
    {
        Collider[] possibleTargets = Physics.OverlapSphere(transform.position, lockOnRange/*, enemiesLayer*/);
        Debug.Log(possibleTargets);

        if (possibleTargets.Length == 0)
        {
            Debug.Log("Returned NULL");
            return null;
        }
        
        // TODO Check for what is the optimal thing to LockOnTo
        // TODO If already locked onto get second most optimal
        for (int i = 0; i < possibleTargets.Length; i++)
        {
            if (possibleTargets[i].gameObject.layer == enemiesLayer)
            {
                Debug.Log("Returned Success");
                return possibleTargets[i].gameObject.transform;
            }
        }

        Debug.Log("Returned NULL 2");
        return null;
    }
}
