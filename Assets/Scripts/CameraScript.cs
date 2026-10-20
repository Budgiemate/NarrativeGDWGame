using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool isShooting;
    public GameObject sniperSlot;
    public GameObject regularSlot;
    void Start()
    {
        SwapToRegularView();
    }

    // Update is called once per frame
    void Update()
    {
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
