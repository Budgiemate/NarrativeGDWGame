using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool isMoving = true;
    public GameObject bulletMesh;
    private CameraScript cam;
    public float bulletSpeed;
    public float bulletRotateSpeed;
    public GameObject MainCam;
    public float camX;
    public float camY;
    public float camZ;
    void Start()
    {
        MainCam = GameObject.Find("Main Camera");
        cam = MainCam.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMoving)
        {
            MainCam.transform.LookAt(transform.position);   
            MainCam.transform.position = new Vector3(transform.position.x + camX, transform.position.y + camY, transform.position.z + camZ);
            transform.position += transform.forward * Time.deltaTime*bulletSpeed;
            bulletMesh.transform.Rotate(new Vector3(0, bulletRotateSpeed, 0  * Time.deltaTime));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Victim"))
        {
            Debug.Log("NOOOO");
            isMoving = false;
            bulletMesh.SetActive(false);
            if (collision.collider.name == "MR BOAST")
            {
                SceneSwapper.NextScene(2);
            }
            else if (collision.collider.name == "The Queen")
            {
                cam.SwapToRegularView();
            }
            else if (collision.collider.name == "Kindergarden Teacher ")
            {
                SceneSwapper.NextScene(4);
            }
        }
        else
        {
            SceneSwapper.NextScene(5);
        }
        
    }
}
