using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        fast();
        Destroy(this.gameObject);
        
        
        
    }

    public void fast()
    {
        Time.timeScale = 1.5f;
        Debug.Log("Collision occured");
        Invoke("setNormal",0.5f);
        
    }

    public void setNormal()
    {
        Time.timeScale = 1f;
    }
}
