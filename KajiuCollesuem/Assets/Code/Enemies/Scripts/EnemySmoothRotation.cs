using UnityEngine;

public class EnemySmoothRotation : MonoBehaviour
{
    private Decision _decision;
    [SerializeField] private float _rotationSpeed = 1.0f;

    void Awake()
    {
        _decision = GetComponent<Decision>();
        _decision.enemyRotation = this;
    }

    void Update()
    {
        Quaternion targetQ = Quaternion.LookRotation(yLess(_decision.target.position) - yLess(transform.position));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQ, Time.deltaTime * _rotationSpeed);
    }

    Vector3 yLess(Vector3 pVector)
    {
        return new Vector3(pVector.x, 0, pVector.z);
    }
}
