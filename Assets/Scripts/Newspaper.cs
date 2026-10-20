using UnityEngine;

public class Newspaper : MonoBehaviour
{
    public float spinSpeed;
    public float startSize;
    private Vector3 startScale;
    public float moveProgress;
    private Vector3 endSize =  new Vector3(1,1,1);
    
    void Start()
    {
        transform.localScale = new Vector3(startSize, startSize, startSize);
        startScale = transform.localScale;
    }

    void Update()
    {
        if (transform.localScale.x >= endSize.x)
        {
            transform.localScale = endSize;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.2f,1.2f,1.2f), moveProgress * Time.deltaTime);
        }

        if (spinSpeed > 10)
        {
            transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
            spinSpeed -= Time.deltaTime * spinSpeed;
        }
        
        
        
    }
}
