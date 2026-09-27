using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    public GameObject targetObject;
    private Vector3 positionOffset;

    void Start()
    {
        if (targetObject == null)
        {
            targetObject = this.gameObject;
            Debug.Log("defaultTarget target not specified. Defaulting to parent GameObject");
        }

        positionOffset = transform.position - targetObject.transform.position;
    }

    void Update()
    {
        transform.position = positionOffset + targetObject.transform.position;
        transform.LookAt(targetObject.transform);
    }
}
