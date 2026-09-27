using UnityEngine;

public class CoinAnimator : MonoBehaviour
{
    public float rotationSpeed = 1;
    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0, Space.Self);
    }
}
