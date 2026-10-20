using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject victim;
    void Update()
    {
        // Create a ray from the object's position in its forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Cast the ray up to 100 units
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            if (hit.collider.CompareTag("Victim"))
            {
                hit.collider.gameObject.GetComponent<Target>().target = true;
                victim = hit.collider.gameObject;
            }
            else
            {
                victim.GetComponent<Target>().target = false;
            }
        }
    }
}
