﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMgr : MonoBehaviour {

    public void OnAnimatorFinish()
    {
        SceneManager.LoadScene("Menu");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
