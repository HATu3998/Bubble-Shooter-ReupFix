using UnityEngine;

public class BallPoolScript : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate ( new Vector3(0,(float) (Time.deltaTime * speed * -0.1), 0));
    }
}
