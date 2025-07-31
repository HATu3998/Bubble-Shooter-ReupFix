using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public List<GameObject> ListBall;
    public static BallController controller;

        private void Awake()
    {
        controller = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetRamDomnBall()
    {
         return Instantiate(ListBall[Random.Range(0, ListBall.Count)], Vector3.zero, new Quaternion());

       
    }
}
