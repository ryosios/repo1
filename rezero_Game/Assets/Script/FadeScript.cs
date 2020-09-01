using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeScript : MonoBehaviour
{
    public float cutoff;
    // Start is called before the first frame update
    void Start()
    {
       // GetComponent<Image>().material.SetFloat("_Cutoff", cutoff);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().material.SetFloat("_Cutoff", cutoff);
    }
}
