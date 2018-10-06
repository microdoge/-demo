using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
	public string loadLevel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SceneManager.LoadScene(loadLevel);
		}

		//throw new System.NotImplementedException();
		
	}
}
