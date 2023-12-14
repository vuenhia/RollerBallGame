using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;

public class MainMenuController : MonoBehaviour {

	[SerializeField]
	private RectTransform mainMenu = null;

	[SerializeField]
	private RectTransform levelSelect = null;

	[SerializeField]
	private RectTransform settingsMenu = null;

	private void Start()
	{
		hideAll();
		showMainMenu();
	}

	private void hideAll()
	{
		mainMenu.gameObject.SetActive(false);
		if (levelSelect != null) { levelSelect.gameObject.SetActive(false); }
		if (settingsMenu != null) { settingsMenu.gameObject.SetActive(false); }
	}

	public void showMainMenu()
	{
		hideAll();
		mainMenu.gameObject.SetActive(true);
	}

	public void showLevelSelectMenu()
	{
		hideAll();
		levelSelect.gameObject.SetActive(true);
	}

	public void showSettingsMenu()
	{
		hideAll();
		if (settingsMenu != null)
		{
			settingsMenu.gameObject.SetActive(true);
		}
		else
		{
			mainMenu.gameObject.SetActive(true);
		}
	}
}
