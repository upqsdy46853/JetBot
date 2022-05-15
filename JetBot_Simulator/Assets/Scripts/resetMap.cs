using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
public class resetMap : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Obstacles;
    Random rnd = new Random();
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reset(){
        foreach(Transform child in transform)
                child.gameObject.SetActive(true);
        GameObject.Find("sensor").GetComponent<getReward>().done = false;

        int num = rnd.Next(2);
        Obstacles[num].SetActive(true);
        Obstacles[1-num].SetActive(false);
    }
}
