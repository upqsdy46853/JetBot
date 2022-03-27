using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getReward : MonoBehaviour
{
    public int reward = 0;
    public bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "CheckPoint"){
            other.gameObject.SetActive(false);
            reward = 1;
        }
        if(other.gameObject.tag == "End"){
            done = true;
        }
    }
}
