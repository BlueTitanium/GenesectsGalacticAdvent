using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
 
    public Transform player;

    public float camPosX;
    public float camPosY;
    public float camPosZ;

    public float sensitivity;
 
    void Start()
    {
        offset = new Vector3(player.position.x + camPosX, player.position.y + camPosY, player.position.z + camPosZ);
    }
 
    
    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensitivity, Vector3.up) 
                 * offset;
        offset.x = Mathf.Clamp(offset.x, 0, 0);
        offset.y = Mathf.Clamp(offset.y, 5, 10);
        offset.z = Mathf.Clamp(offset.z, -20, -20);
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}