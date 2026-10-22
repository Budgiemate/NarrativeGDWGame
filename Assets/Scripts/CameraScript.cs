using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CameraScript : MonoBehaviour
{
    public bool isShooting;
    public GameObject sniperSlot;
    public GameObject regularSlot;
    public GameObject scope;
    public GameObject note;
    public float sensitivity = 2.0f;    // Mouse speed
    public float minPitch = -10f;       // Angle limiter for bottom view
    public float maxPitch = 50f;        // Angle limiter for top view
    public float minYaw = 250f;         // Angle limiter for left view
    public float maxYaw = 310f;         // Angle limiter for right views
    private float pitch;                // vertical view 
    private float yaw;                  // horizontal view
    private float drunkPitch;           // vertical change for drunken shake 
    private float drunkYaw;             // horizontal change for drunken shake
    private float duration = 0.3f;      //length of each random drunken shake
    private float mouseX;           
    private float mouseY;
    void Start()
    {
        SwapToRegularView();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        BackgroundMusic.SwapMusic(BackgroundMusic.mainThemeS);
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse inputs and multiply by the speed you want the camera to move
        mouseX = Input.GetAxis("Mouse X") * sensitivity; 
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        
        // Apply limiter so camera can't be rotated past max or min
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        yaw = Mathf.Clamp(yaw, minYaw, maxYaw);
        
        // If in sniper position... 
        if (isShooting)
        {
            //Count down till next direction change for drunken shake
            duration -= Time.deltaTime;
            
            //Randomize the drunken shake
            if (duration <= 0f)
            {
                drunkYaw = Random.Range(-0.08f,0.08f);
                drunkPitch = Random.Range(-0.08f,0.08f);
                duration = 0.3f;
            }
            
            // Set the vertical on horizontal to the correct pos or neg of the mouse so it tracks
            pitch -= mouseY;
            yaw += mouseX;

            //Add drunken shake to the pitch and yaw
            pitch += drunkPitch;
            yaw += drunkYaw;
            
            //apply rotations
            transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        }
        
        if (isShooting && Input.GetKeyDown(KeyCode.Tab))
        {
           // SwapToRegularView();
        }
        else if (!isShooting && Input.GetKeyDown(KeyCode.Tab))
        {
            SwapToSniperView();
        }
    }
    public void SwapToSniperView()
    {
        scope.SetActive(true);
        transform.position = sniperSlot.transform.position;
        transform.rotation = sniperSlot.transform.rotation;
        note.SetActive(false);
        isShooting = true;
        gameObject.GetComponent<Gun>().hasShot = false;
    }
    public void SwapToRegularView()
    {
        isShooting = false;
        scope.SetActive(false);
        transform.position = regularSlot.transform.position;
        transform.localRotation = regularSlot.transform.localRotation;
        
    }
    public void SwapToBulletView()
    {
        scope.SetActive(false);
    }
}
