using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraScript : MonoBehaviour
{
    public bool isShooting;
    public GameObject sniperSlot;
    public GameObject regularSlot;
    public float sensitivity = 2.0f;    // Mouse speed
    public float minPitch = -10f;       // Angle limiter for bottom view
    public float maxPitch = 50f;        // Angle limiter for top view
    public float minYaw = 250f;         // Angle limiter for left view
    public float maxYaw = 310f;         // Angle limiter for right views
    private float pitch = 30f;          // vertical view 
    private float drunkPitch = 30f;          // vertical view 
    private float yaw = 280f;           // horizontal view
    private float drunkYaw = 280f;           // horizontal view
    private float mouseX;           
    private float mouseY;
    public float drunkMultiplier;
    void Start()
    {
        SwapToRegularView();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse inputs and multiply by the speed you want the camera to move
        mouseX = Input.GetAxis("Mouse X") * sensitivity; 
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Set the vertical on horizontal to the correct pos or neg of the mouse so it tracks
        pitch -= mouseY;
        yaw += mouseX;
        
        // Apply limiter so camera can't be rotated past max or min
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        yaw = Mathf.Clamp(yaw, minYaw, maxYaw);
        /*drunkPitch = Mathf.Clamp(Random.Range(0,drunkMultiplier), minPitch, maxPitch);
        drunkYaw = Mathf.Clamp(Random.Range(0,drunkMultiplier), minYaw, maxYaw);*/
        
        // If in sniper position apply rotations
        if (isShooting)
        {
            transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
            /*transform.localRotation = Quaternion.Euler(drunkPitch, drunkYaw, 0f);*/
            
        }
        
        
        
        if (isShooting && Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("AGGGGG");
            SwapToRegularView();
        }
        else if (!isShooting && Input.GetKeyDown(KeyCode.Tab))
        {
            SwapToSniperView();
        }
    }
    public void SwapToSniperView()
    {
        transform.position = sniperSlot.transform.position;
        transform.rotation = sniperSlot.transform.rotation;
        isShooting = true;
    }
    public void SwapToRegularView()
    {
        Debug.Log("AGGGGG");
        transform.position = regularSlot.transform.position;
        transform.rotation = regularSlot.transform.rotation;
        isShooting = false;
    }
}
