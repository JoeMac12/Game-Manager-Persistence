using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	// Display text
	public TextMeshProUGUI healthText;
	public TextMeshProUGUI xpText;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI coinsText;
	public TextMeshProUGUI levelText;
	public TextMeshProUGUI playTimeText;
	public TextMeshProUGUI managerCountText;
	public TextMeshProUGUI currentSceneText;

	public Button saveButton;
	public Button loadButton;

	// Buttons
	private void Start()
	{
		saveButton.onClick.AddListener(SaveGame);
		loadButton.onClick.AddListener(LoadGame);
	}

	private void Update()
	{
		UpdateUI();
	}

	// Update UI text
	private void UpdateUI()
	{
		healthText.text = $"Health: {GameManager.Instance.Health}";
		xpText.text = $"XP: {GameManager.Instance.XP}";
		scoreText.text = $"Score: {GameManager.Instance.Score}";
		coinsText.text = $"Coins: {GameManager.Instance.Coins}";
		levelText.text = $"Level: {GameManager.Instance.Level}";
		playTimeText.text = $"Play Time: {GameManager.Instance.PlayTime:F2}s";
		managerCountText.text = $"Game Managers: {GameManager.ManagerCount}";
		currentSceneText.text = $"Current Scene: {GameManager.Instance.CurrentSceneName}";
	}

	private void SaveGame()
	{
		GameManager.Instance.SaveGame();
	}

	private void LoadGame()
	{
		GameManager.Instance.LoadGame();
	}
}
