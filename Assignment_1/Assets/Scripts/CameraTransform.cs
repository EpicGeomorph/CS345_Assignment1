using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    public GameObject targetObject;
    private Vector3 positionOffset;
    private bool cameraType = false;
    public float lastCamSwap = 0;

    void Start()
    {
        if (targetObject == null)
        {
            targetObject = this.gameObject;
            Debug.Log("defaultTarget target not specified. Defaulting to parent GameObject");
        }

        positionOffset = transform.position - targetObject.transform.position;
        transform.position = new Vector3(0, 40, 0);
        transform.LookAt(GameObject.Find("Tables").transform);
    }

    void Update()
    {
        Debug.Log(Time.time - lastCamSwap);
        if(Input.GetKey(KeyCode.Space) && (Time.time - lastCamSwap > 1))
        {
            lastCamSwap = Time.time;
            cameraType = !cameraType;
        }

        if (cameraType) // false = ball view, true = table view
        {
            transform.position = new Vector3(0, 40, 0);
            transform.LookAt(GameObject.Find("Tables").transform);
        } else
        {
            transform.position = positionOffset + targetObject.transform.position;
            transform.LookAt(targetObject.transform);
        }
    }
}
