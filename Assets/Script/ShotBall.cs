using UnityEngine;
using UnityEngine.UIElements;

public class ShotBall : MonoBehaviour
{
    public GameObject BallShot, BallReload;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReloadBall();

        ChangeBallToBallShot();
    }
    void ReloadBall()
    {
        GameObject ball = BallController.controller.GetRamDomnBall();
        ball.transform.parent = BallReload.transform;
        ball.transform.localPosition = Vector3.zero; 
    }

    void ChangeBallToBallShot()
    {
        GameObject ball = BallReload.transform.GetChild(0).gameObject;
        ball.transform.parent = BallShot.transform;
        ball.transform.localPosition = Vector3.zero;
        ReloadBall();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBall();
           SoundController.Instance.PlayFireAudio();
        }
    }
    void ShootBall()
    {
        if(BallShot.transform.childCount != 0)
        {
            Vector3 PositionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PositionMouse.z = 0;
            Vector3 direction = (PositionMouse - BallShot.transform.GetChild(0).position).normalized;
            BallShot.transform.GetChild(0).GetComponent<BallScript>().Direction = direction;
            BallShot.transform.GetChild(0).parent = null;
            
            ChangeBallToBallShot();
        }
    }
}
