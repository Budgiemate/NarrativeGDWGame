using UnityEngine;

public class Target : MonoBehaviour
{
    public Material highlight;
    public Material normal;
    public bool target;
 
    void Update()
    {
        if (target)
        {
            gameObject.GetComponent<Renderer>().material = highlight;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = normal;
        }
    }
}
