using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 Direction; //huong bay cua bog
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Direction != Vector3.zero)
        {
            transform.Translate(Direction * Time.deltaTime * 5f);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.parent == null && gameObject.transform.parent.name == "BallPool")
        {
            collision.transform.parent = gameObject.transform.parent;
            collision.GetComponent<BallScript>().Direction = Vector3.zero;

            BallSpawn.Spawn.SoftBall(collision.gameObject, gameObject);
        }
    }
}
