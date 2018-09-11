using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
	public DataGame dataGame;
	public GameManager gameManager;
	public PlayerControl playControl;
	public Text scorePlay;
	public Text coinPlay;
	public Text scoreOver;
	public Text bestOver;
	public Animator animaUI;

	// Use this for initialization
	void Awake ()
	{
		animaUI = GetComponent<Animator> ();
	}
		
	void Start ()
	{
		animaUI.SetBool ("start", true);
		SetCoin ();
	}

	public void ButtonPlay ()
	{
		animaUI.SetBool ("start", false);
		animaUI.SetBool ("play", true);
	}

	public void GameOver ()
	{
		animaUI.SetBool ("play", false);
		animaUI.SetBool ("gameOver", true);
		if (!PlayerPrefs.HasKey ("best") || PlayerPrefs.GetInt ("best") < dataGame.score) {
			PlayerPrefs.SetInt ("best", dataGame.score);
		}
		scoreOver.text = "Score : " + dataGame.score.ToString ();
		bestOver.text = "Best : " + PlayerPrefs.GetInt ("best").ToString ();
		//UltimateSevice.Instances.SumitService (dataGame.score);
//		if (Random.Range (0, 3) % 3 == 1) {
//			UltimateAds.Instances.ShowFull ();
//		}
	}

	public void ButtonRestart ()
	{
		animaUI.SetBool ("gameOver", false);
		animaUI.SetBool ("play", true);
	}

	public void ButtoShop ()
	{
		animaUI.SetBool ("shop", true);
	}

	public void BackShop ()
	{
		animaUI.SetBool ("shop", false);
	}

	public void SetScore ()
	{
		scorePlay.text = dataGame.score.ToString ();
	}

	public void SetCoin ()
	{
		coinPlay.text = dataGame.coin.ToString ();
	}

	public void Pingcoin ()
	{
		animaUI.SetTrigger ("ping");
	}
}
