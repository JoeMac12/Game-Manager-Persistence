using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public static int ManagerCount { get; private set; }

	// Game state variables
	public int Health { get; private set; } = 100;
	public int XP { get; private set; } = 0;
	public int Score { get; private set; } = 0;
	public int Coins { get; private set; } = 0;
	public int Level { get; private set; } = 1;
	public float PlayTime { get; private set; } = 0f;
	public string CurrentSceneName { get; private set; }

	private const int smallValue = 1;
	private const int largeValue = 10;

	// Singleton
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			ManagerCount++;
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// For getting the current scene level text. Get active scene would also work but might be buggy for this
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		CurrentSceneName = scene.name;
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void Update()
	{
		HandleSceneNavigation();
		UpdatePlayTime();
		HandleNumpadInput();
	}

	// Key shortcut for loading levels
	private void HandleSceneNavigation()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1)) SceneManager.LoadScene(0); // Main Menu
		if (Input.GetKeyDown(KeyCode.Alpha2)) SceneManager.LoadScene(1); // Level 1
		if (Input.GetKeyDown(KeyCode.Alpha3)) SceneManager.LoadScene(2); // Level 2
		if (Input.GetKeyDown(KeyCode.Alpha4)) SceneManager.LoadScene(3); // Level 3
	}

	private void UpdatePlayTime()
	{
		PlayTime += Time.deltaTime;
	}

	// Numpad controls
	private void HandleNumpadInput()
	{
		// Health
		if (Input.GetKeyDown(KeyCode.Keypad7)) ModifyHealth(smallValue);
		if (Input.GetKeyDown(KeyCode.Keypad8)) ModifyHealth(-smallValue);

		// XP
		if (Input.GetKeyDown(KeyCode.Keypad4)) ModifyXP(smallValue);
		if (Input.GetKeyDown(KeyCode.Keypad5)) ModifyXP(-smallValue);

		// Score
		if (Input.GetKeyDown(KeyCode.Keypad1)) ModifyScore(largeValue);
		if (Input.GetKeyDown(KeyCode.Keypad2)) ModifyScore(-largeValue);

		// Coins
		if (Input.GetKeyDown(KeyCode.KeypadPlus)) ModifyCoins(smallValue);
		if (Input.GetKeyDown(KeyCode.KeypadMinus)) ModifyCoins(-smallValue);

		// Level
		if (Input.GetKeyDown(KeyCode.KeypadMultiply)) ModifyLevel(smallValue);
		if (Input.GetKeyDown(KeyCode.KeypadDivide)) ModifyLevel(-smallValue);

		// Play Time
		if (Input.GetKeyDown(KeyCode.Keypad6)) ModifyPlayTime(60f);
		if (Input.GetKeyDown(KeyCode.Keypad3)) ModifyPlayTime(-60f);
	}

	// Simple methods for da stats
	private void ModifyHealth(int amount) => Health = Mathf.Max(0, Health + amount);
	private void ModifyXP(int amount) => XP = Mathf.Max(0, XP + amount);
	private void ModifyScore(int amount) => Score = Mathf.Max(0, Score + amount);
	private void ModifyCoins(int amount) => Coins = Mathf.Max(0, Coins + amount);
	private void ModifyLevel(int amount) => Level = Mathf.Max(1, Level + amount);
	private void ModifyPlayTime(float amount) => PlayTime = Mathf.Max(0f, PlayTime + amount);

	// Save game data
	public void SaveGame()
	{
		SaveData data = new SaveData
		{
			health = Health,
			xp = XP,
			score = Score,
			coins = Coins,
			level = Level,
			playTime = PlayTime
		};

		string json = JsonUtility.ToJson(data);
		File.WriteAllText(Application.persistentDataPath + "/savegame.json", json);
	}

	// Load game with data
	public void LoadGame()
	{
		string path = Application.persistentDataPath + "/savegame.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			Health = data.health;
			XP = data.xp;
			Score = data.score;
			Coins = data.coins;
			Level = data.level;
			PlayTime = data.playTime;
		}
	}

	// Clear game data
	public void ClearGameData()
	{
		Health = 100;
		XP = 0;
		Score = 0;
		Coins = 0;
		Level = 1;
		PlayTime = 0f;

		string path = Application.persistentDataPath + "/savegame.json";
		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}
}

[System.Serializable]
public class SaveData
{
	public int health;
	public int xp;
	public int score;
	public int coins;
	public int level;
	public float playTime;
}
