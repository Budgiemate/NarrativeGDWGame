using System.Collections;
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

                StartCoroutine(MrBoastEnding());
            }
            else if (collision.collider.name == "The Queen")
            {
                StartCoroutine(QueenEnding());
            }
            else if (collision.collider.name == "Kindergarden Teacher ")
            {
                StartCoroutine(TeacherEnding());
            }
        }
        else
        {
            StartCoroutine(MissEnding());
        }
        
    }
    IEnumerator MrBoastEnding()
    {
        MainCam.GetComponent<CameraScript>().isShooting = false;
        BackgroundMusic.SwapMusic(BackgroundMusic.dukeBoastS);
        yield return new WaitForSeconds(1f);
        SceneSwapper.NextScene(2);
    }
    IEnumerator TeacherEnding()
    {
        MainCam.GetComponent<CameraScript>().isShooting = false;
        BackgroundMusic.SwapMusic(BackgroundMusic.teacherS);
        yield return new WaitForSeconds(1f);
        SceneSwapper.NextScene(4);
    }
    IEnumerator QueenEnding()
    {
        MainCam.GetComponent<CameraScript>().isShooting = false;
        BackgroundMusic.SwapMusic(BackgroundMusic.shankingS);
        yield return new WaitForSeconds(0.1f);
        cam.SwapToRegularView();
        yield return new WaitForSeconds(5f);
        SceneSwapper.NextScene(3);
    }
    IEnumerator MissEnding()
    {
        MainCam.GetComponent<CameraScript>().isShooting = false;
        isMoving = false;
        yield return new WaitForSeconds(1f);
        SceneSwapper.NextScene(5);
    }
}
