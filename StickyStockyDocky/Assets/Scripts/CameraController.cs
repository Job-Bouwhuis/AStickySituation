using UnityEngine;
using SF = UnityEngine.SerializeField;
using H = UnityEngine.HeaderAttribute;

public class CameraController : MonoBehaviour
{
    [H("Settings")]
    [SF] private Transform target;
    [SF] private float followSpeed = 5000f;
    [SF] private Transform background;

    // Update is called once per frame
    void Update()
    { 
        if(Time.timeSinceLevelLoad > 1)
        {
            transform.position = Vector3.Lerp(transform.position, new(target.position.x, target.position.y, -10), Time.deltaTime * followSpeed);
            background.transform.position = Vector3.Lerp(background.transform.position, new(target.position.x, target.position.y, 0), Time.deltaTime * followSpeed);
        }
    }
}
