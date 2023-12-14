using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

	public void LoadLevelByName(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}

	public void restartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
