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
	public int Health { get; set; } = 100;
	public int XP { get; set; } = 0;
	public int Score { get; set; } = 0;
	public int Coins { get; set; } = 0;
	public int Level { get; set; } = 1;
	public float PlayTime { get; set; } = 0f;

	// Singleton
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			ManagerCount++;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		HandleSceneNavigation();
		UpdatePlayTime();
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
