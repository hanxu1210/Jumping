using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public enum GameState
	{
		start,
		play,
		gameOver
	}

	public DataGame dataGame;
	public PlayerControl player;
	public UiManager uiManager;
	public CameraControl cam;
	public GameState gameState = GameState.start;
	public Vector2 deltaPlatform;
	Animation animaLisPlatform;

	Transform listPlatform;

	void Awake ()
	{
		listPlatform = GetComponent<Transform> ();
		animaLisPlatform = GetComponent<Animation> ();
		animaLisPlatform.enabled = false;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
		if ((Input.GetMouseButtonDown(0) || KeyboardHandler.IsOkButtonDown()) && !WasAButton())
		{
			TouchGame ();
		}
	}

	private bool WasAButton()
	{
		UnityEngine.EventSystems.EventSystem ct
		= UnityEngine.EventSystems.EventSystem.current;

		if (! ct.IsPointerOverGameObject() ) return false;
		if (! ct.currentSelectedGameObject ) return false;
		if (ct.currentSelectedGameObject.GetComponent<Button>() == null )
			return false;

		return true;
	}

	public void TouchGame ()
	{
		if (gameState == GameState.play) {
			if (animaLisPlatform.enabled) {
				animaLisPlatform.enabled = false;
				cam.enabled = true;
			}
			player.Jump ();
		} 
		// else if (gameState == GameState.start) {
		// 	PlayGame ();
		// 	uiManager.ButtonPlay ();
		// }
		// else if (gameState == GameState.gameOver) {
		// 	RestartGame ();
		// 	uiManager.ButtonRestart ();
		// }
	}

	public void PlayGame ()
	{
		player.gameObject.SetActive (true);
		animaLisPlatform.gameObject.SetActive (true);
		animaLisPlatform.enabled = true;
		if (animaLisPlatform.isPlaying) {
			animaLisPlatform.Stop ();
		}
		animaLisPlatform.Play ();
		dataGame.platformActive = Random.Range (0, dataGame.platform.Length);
		for (int i = 0; i < listPlatform.childCount; i++) {
			Platform platform = listPlatform.GetChild (i).GetComponent<Platform> ();
			platform.enabled = true;
			platform.SetRadius ();
		}
		cam.Start ();
		player.Start ();
	}

	public void RestartGame ()
	{
		gameState = GameState.start;
		PlayGame ();
	}

	public void BeginPlay ()                               // chay xong animation BeginGame;      
	{
		gameState = GameState.play;
		player.enabled = true;
		SetPlatform ();
	}

	public void SetPlatform ()
	{
		listPlatform.gameObject.SetActive (true);
		for (int i = 0; i < listPlatform.childCount; i++) {
			Platform platform = listPlatform.GetChild (i).GetComponent<Platform> ();
			platform.enabled = true;
			platform.ResetIndex ();
			if (i >= 3) {
				platform.deltaPlatform = deltaPlatform;
				platform.SetPlatform ();
			}
		}
	}

#if DEBUG_VERSION
	// 检查按键信息 start
	public Text keycodeText1;
	public Text keycodeText2;
	private int keyTextIndex = 0;
	void OnGUI()
	{
		if (Input.anyKeyDown)
		{
			Event e = Event.current;
			if (e.isKey)
			{
				if (keyTextIndex == 0)
				{
					keycodeText1.text ="按下的键值：" + e.keyCode.ToString();
					Debug.Log(keycodeText1.text);
					keyTextIndex++;
				}
				else if(keyTextIndex == 1)
				{
					keycodeText2.text ="按下的键值：" + e.keyCode.ToString();
					Debug.Log(keycodeText2.text);
					keyTextIndex = 0;
				}
			}
		}
	}
	// 检查按键信息 end
#endif
	// public bool IsOKButtonPressed()
	// {
	// 	if (Input.GetKeyDown (KeyCode.JoystickButton0))
	// 	{
	// 		return true;
	// 	}
	// 	return false;
	// }
}
