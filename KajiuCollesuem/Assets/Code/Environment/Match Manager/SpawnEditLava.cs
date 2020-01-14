/*
Programmer: Kavian Kermani
Additional Programmers: Other people who worked on the script
Description: Spawns lava, which rises to two set heights, with an invisible timer.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEditLava : MonoBehaviour
{
    public float nextActionTime;

    [SerializeField] private Transform[] _heightTransforms;
    [SerializeField] private float[] _riseLength;
    [SerializeField] private float[] _riseStoppedTimes;
    private int _index;
    private float _velocity;

    private void Start()
    {
        _velocity = (_heightTransforms[_index].position.y - transform.position.y) / _riseLength[_index];
        nextActionTime = Time.time + _riseStoppedTimes[0];
    }

    public void lavaTimer()
    {
        if (nextActionTime <= Time.time && nextActionTime != -1)
        {
            lavaRise();

            if (transform.position.y >= _heightTransforms[_index].transform.position.y)
            {
                _index++;

                if (_heightTransforms.Length == _index)
                {
                    // Done Move all together
                    nextActionTime = -1;
                }
                else
                {
                    nextActionTime = Time.time + _riseStoppedTimes[_index];
                    _velocity = (_heightTransforms[_index].position.y - transform.position.y) / _riseLength[_index];
                }
            }
        }
    }

    public void lavaRise()
    {

        transform.Translate(Vector3.up * _velocity * Time.deltaTime, Space.World);
    }
}
