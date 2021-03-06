﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMaker : MonoBehaviour
{
    [SerializeField] GameObject[] background_prefab = new GameObject[3];
    public GameObject[] background_Clone { get; set; } = new GameObject[3];


    int background_speed = 7; //背景スピード
    
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetBackGround()
    {
        for (int i = 0; i < 3; i++)
        {

            background_Clone[i] = Instantiate(background_prefab[i],  this.transform) as GameObject;
            background_Clone[i].transform.localPosition = new Vector3(-15 + i * 15, 0, 0);

        }
    }

    public void BackGroundMove()
    {
        foreach (var background in background_Clone)
        {
            Vector3 this_position = background.transform.localPosition;
            this_position.x -= background_speed*Time.deltaTime;
            background.transform.localPosition = this_position;

            if (background.transform.localPosition.x < -30)
            {
                background.transform.localPosition = new Vector3(15, 0, 0);
            }
          //  Debug.Log("koko2");
        }

    }
}
