using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void startGame ()
	{
		SceneManager.LoadScene ("Lobby");
	}

	public void Settings ()
	{
		SceneManager.LoadScene ("Settings");
	}

	public void exitGame ()
	{
		Application.Quit ();
	}
}
