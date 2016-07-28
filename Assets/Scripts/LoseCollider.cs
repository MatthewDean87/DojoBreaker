﻿using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

	private LevelManager levelManager;
	
	void OnTriggerEnter2D (Collider2D trigger) {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		levelManager.LoadLevel("LoseScreen");
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		print ("Collision with LoseCollider");	
	}
	
}
