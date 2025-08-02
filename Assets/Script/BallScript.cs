using System.Collections;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 Direction; //huong bay cua bog
    void Start()
    {
        
    }
    IEnumerator CheckAnchor()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if(transform.parent != null && transform.parent.name ==  "BallPool")
            {
                if(BallSpawn.Spawn.isAllowCheck && !BallSpawn.Spawn.ListBallCurrent.Contains(gameObject))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Direction != Vector3.zero)
        {
            transform.Translate(Direction * Time.deltaTime * 5f);
        }
     //   StartCoroutine(CheckAnchor());
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Trigger with: " + collision.gameObject.name);

        //if (collision.transform.parent == null)
        //    Debug.Log("collision.transform.parent is NULL");

        //if (gameObject.transform.parent == null)
        //    Debug.Log("THIS BALL's parent is NULL!");
        
        
        if (gameObject.transform.parent != null &&  
            collision.transform.parent == null &&
            gameObject.transform.parent.name == "BallPool")
        {
            collision.transform.parent = gameObject.transform.parent;
            BallScript otherBall = collision.GetComponent<BallScript>();
            if (otherBall != null)
            {
                otherBall.Direction = Vector3.zero;
                BallSpawn.Spawn.SoftBall(collision.gameObject, gameObject);
            }
        }
    }
}
