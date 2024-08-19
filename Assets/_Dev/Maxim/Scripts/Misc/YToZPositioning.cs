using UnityEngine;

public class YToZPositioning : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            transform.position.y * 0.01f
        );
    }
}
