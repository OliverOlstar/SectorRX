using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform camTransform;

    public float DistanceToPlayer = 0f;
    public float FollowSpeed = 0f;
    public float RotateSpeed = 0f;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //camTransform = Camera.main.transform;

        //DistanceToPlayer = 5;
        //FollowSpeed = 8;
        //RotateSpeed = 3.5f;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        UpdateCameraPosition();
        UpdateCameraRotation();
    }

    void UpdateCameraPosition()
    {
        Vector3 newPos = Vector3.zero;
        Vector3 playerPos = Vector3.zero;
        Vector3 playerDirection = Vector3.zero;

        playerPos = player.transform.position;
        playerDirection = player.transform.forward;

        newPos = playerPos - (playerDirection * DistanceToPlayer);

        camTransform.position = Vector3.Lerp(camTransform.position, newPos, FollowSpeed * Time.fixedDeltaTime);
    }

    void UpdateCameraRotation()
    {
        Quaternion newRotation = Quaternion.Euler(Vector3.zero);
        Vector3 playerRotation = Vector3.zero;

        playerRotation = player.transform.rotation.eulerAngles;
        newRotation = Quaternion.Euler(playerRotation);

        camTransform.rotation = Quaternion.Slerp(camTransform.rotation, newRotation, RotateSpeed * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(camTransform.position, player.transform.position, Color.red);
    }
}
