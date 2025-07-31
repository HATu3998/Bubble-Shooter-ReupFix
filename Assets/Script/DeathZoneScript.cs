using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.transform.parent != null &&  collision.gameObject.transform.parent.name == "BallPool" )
        {
            GameController.controller.SetGameOver();
        }
    }
}
