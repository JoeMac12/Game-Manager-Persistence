using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public Button newGameButton;
	public Button loadLevel1Button;
	public Button loadLevel2Button;
	public Button loadLevel3Button;
	public Button clearDataButton;

	private void Start()
	{
		newGameButton.onClick.AddListener(StartNewGame);
		loadLevel1Button.onClick.AddListener(() => LoadLevel(1));
		loadLevel2Button.onClick.AddListener(() => LoadLevel(2));
		loadLevel3Button.onClick.AddListener(() => LoadLevel(3));
		clearDataButton.onClick.AddListener(ClearGameData);
	}

	// Load any save data and loads level 1
	private void StartNewGame()
	{
		GameManager.Instance.LoadGame();
		SceneManager.LoadScene(1);
	}

	// Load any save data into selected level
	private void LoadLevel(int levelIndex)
	{
		GameManager.Instance.LoadGame();
		SceneManager.LoadScene(levelIndex);
	}

	private void ClearGameData()
	{
		GameManager.Instance.ClearGameData();
		Debug.Log("Game data cleared!");
	}
}
