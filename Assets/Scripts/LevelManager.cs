﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public float splashDelay = 5.0f;
	public AudioClip splashSound;

	private ScoreManager scoreman;
	private string startPrefix = "Start";
	
	void Start() {
		scoreman = GameObject.FindObjectOfType<ScoreManager>();
		if (SceneManager.GetActiveScene().name == "Splash") {
			AudioSource.PlayClipAtPoint (splashSound, transform.position, 1.0f);
			Invoke("LoadNextLevel", splashDelay);
		}
	}
	
	public void LoadNextLevel() {
		Scene current = SceneManager.GetActiveScene();
		int newindex = current.buildIndex + 1;
		Debug.Log ("New Level load: " + newindex);
		CheckScore();
		SceneManager.LoadScene(newindex);
	}

	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		CheckScore();
		SceneManager.LoadScene(name);
	}

	private void CheckScore() {
		Brick.breakableCount = 0;
		scoreman.ResetCollisionCount();
		if (SceneManager.GetActiveScene().name.StartsWith(startPrefix)) {
			scoreman.ResetScore();
		}
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}
		
	public void BrickDestroyed() {
		Debug.Log("Ball destroyed Brick");
		scoreman.CollisionScore(new Collision(Time.time, Collision.CollisionType.Break));
		if (Brick.breakableCount <= 0) {
			LoadNextLevel();
		}
	}

	public void BrickBounce() {
		Debug.Log("Ball bounced off Brick");
		scoreman.CollisionScore(new Collision(Time.time, Collision.CollisionType.Crack));
	}

	public void PaddleBounce() {
		Debug.Log("Ball bounced off Paddle");
		scoreman.CollisionScore(new Collision(Time.time, Collision.CollisionType.Bat));
	}
}
