using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetMap : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject[] checkpoints;
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
        GameObject.Find("JetBot").GetComponent<getReward>().done = false;
    }
}
