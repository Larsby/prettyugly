using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;
	//Static instance of GameManager which allows it to be accessed by any other script.
	private int level = 1;
	private int prettiesOut = 0;
	private static Text levelCompleted = null;
	private static Text levelFailed = null;
	private static Text percentage = null;
	private static Text percentageText;
	private static Text LifeCounterText = null;
	private static Text AvailableMoves = null;
	private static Button exit;
	private int numberOfFoodToFail = -1;
	private int fail = 0;
	private int activeLevel = 0;
	private int prettiesInScene = 0;
	PlayerController pusher = null;
	//Awake is always called before any Start functions
	private bool ready = false;
	private bool useMoves = false;
	public bool showUI = true;


	static int numberOfBalls = 0;
	void Awake ()
	{
		
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);    
		
		DontDestroyOnLoad (gameObject);

		ConnectToUI ();

		ShowUI ();

		InitGame ();
	}
	public void RegisterBall() {
		numberOfBalls++;
	}
	public void DeRegisterBall() {
		numberOfBalls--;
		if(numberOfBalls<=0) {
			Debug.Log("All balls done");
		//	LoadNextLevel();
		}
	}
	public bool IsReady ()
	{
		return ready;
	}

	public void AspectSet ()
	{
		ready = true;
	}
	public void UseMoves(bool use) {
		useMoves = use;
		ShowUI ();
	}
	public Quaternion GetPusherRotation ()
	{
		ConnectPusher ();
		return  pusher.transform.rotation;
	}

	public Vector3 GetPusherPosition ()
	{
		ConnectPusher ();
		return pusher.transform.position;
	}

	public bool IsPlayerAiming ()
	{
		if(PlayerController.instance)
		return PlayerController.instance.IsAiming ();
		return false;
	}
	public void GameOver() {
		Debug.Log("Game over!");
		GameObject.FindWithTag("Blob").SetActive(false);
		PusherOut();

	}
	void ConnectPusher ()
	{
		if (pusher == null) {
			pusher = GameObject.FindWithTag ("Blob").GetComponent<PlayerController> ();
		}	
	}

	void ConnectToUI ()
	{
		if (showUI == false) return;
		ready = false;
		ConnectPusher ();
		GameObject uiC = GameObject.Find ("UI(Clone)");
		if (uiC == null) {
			uiC = (GameObject)Instantiate (Resources.Load ("UI"));
		}
		if (uiC && levelCompleted == null) {
			levelCompleted = GameObject.Find ("LevelCompleted").GetComponent<Text> ();
			levelFailed = GameObject.Find ("LevelFailed").GetComponent<Text> ();
			levelCompleted.gameObject.SetActive (false);
			percentage = GameObject.Find ("Percentage").GetComponent<Text> ();
			percentageText = GameObject.Find ("PercentageText").GetComponent<Text> ();
			levelFailed.gameObject.SetActive (false);
			LifeCounterText = GameObject.Find ("LifeCounter").GetComponent<Text> ();
			exit = GameObject.Find ("Exit").GetComponent<Button> ();
		}
	
			GameObject obj = GameObject.Find ("AvailableMoves");
		if (obj != null) {
			AvailableMoves = obj.GetComponent<Text> ();
		} else {
			AvailableMoves = null;
		}

	}

	public void ShowUI ()
	{
		if (showUI == false) return;
		percentageText.gameObject.SetActive (true);
		percentage.gameObject.SetActive (true);
		LifeCounterText.gameObject.SetActive (true);
		exit.gameObject.SetActive (true);
		if (useMoves) {
			AvailableMoves.gameObject.SetActive (true);
		} else {
			if (AvailableMoves != null) {
				AvailableMoves.gameObject.SetActive (false);
			}
		}

	}

	public void HideUI ()
	{
		if (showUI == false) return;
		levelFailed.gameObject.SetActive (false);
		levelCompleted.gameObject.SetActive (false);
		percentageText.gameObject.SetActive (false);
		percentage.gameObject.SetActive (false);
		LifeCounterText.gameObject.SetActive (false);
		exit.gameObject.SetActive (false);
		AvailableMoves.gameObject.SetActive (false);
	}


	void OnLevelWasLoaded (int level)
	{
		FadeShaderColor.firstCombine = true;
		if (level == 0) {
			HideUI ();
		} else {
			/*
			levelFailed.gameObject.SetActive (false);
			levelCompleted.gameObject.SetActive (false);
			percentageText.gameObject.SetActive (true);
			percentage.gameObject.SetActive (true);
			LifeCounterText.gameObject.SetActive (true);
			exit.gameObject.SetActive (true);
			*/
		}
		fail = 0;
		prettiesOut = 0;
		prettiesInScene = GameObject.FindGameObjectsWithTag ("Pretty").Length;
		ready = false;

	}

	public void setMaxFood (int fail)
	{
		
		numberOfFoodToFail = fail;
		float perc = (float)((float)fail / (float)numberOfFoodToFail);
		float totalPercent = 100.0f - (perc * 100);
		if (totalPercent == 0.0f)
			totalPercent = 100.0f;
		double p = System.Math.Round (totalPercent, 1);
		string text = "" + p;
		if (p >= 0) {
			if (text.IndexOf (".") == -1) {
				text = text + ".0";
			}
			if (!percentage) {
				ConnectToUI ();
			}

			if (percentage)
				percentage.text = text + "";
		}
	}

	public void ActivatePrettyOutDetection ()
	{
		GameObject[] prettties = GameObject.FindGameObjectsWithTag ("Pretty");
		foreach (GameObject pretty in prettties) {
			TellGameMangerImOut imout = pretty.GetComponent<TellGameMangerImOut> ();
			if (imout == null) {
				imout = pretty.AddComponent<TellGameMangerImOut> ();
			}
			imout.enabled = true;
		}
	}

	public IEnumerator LevelSelectorWithDelay ()
	{
		yield  return new WaitForSeconds (2.0f);
		LoadLevelSelector ();

	}

	public IEnumerator LoadNextLevelWithDelay ()
	{
		yield  return new WaitForSeconds (2.0f);
		LoadNextLevel ();
	}

	public void removeFood ()
	{
		fail--;
		float perc = (float)((float)fail / (float)numberOfFoodToFail);
		float totalPercent = 100.0f - (perc * 100);
		double p = System.Math.Round (totalPercent, 1);
		string text = "" + p;
		if (p >= 0) {
			if (text.IndexOf (".") == -1) {
				text = text + ".0";
			}
			if (!percentage) {
				ConnectToUI ();
			}
			if (percentage)
				percentage.text = text + "";
		}
	}

	public void addFood ()
	{
		fail++;
		if (fail >= numberOfFoodToFail) {
			levelFailed.gameObject.SetActive (true);
			fail = 0;
			StartCoroutine (LevelSelectorWithDelay ());
			return;
		}

		float perc = (float)((float)fail / (float)numberOfFoodToFail);
		float totalPercent = 100.0f - (perc * 100);
		double p = System.Math.Round (totalPercent, 1);
		string text = "" + p;
		if (p >= 0) {
			if (text.IndexOf (".") == -1) {
				text = text + ".0";
			}
			if (!percentage) {
				ConnectToUI ();
			}
			if (percentage)
				percentage.text = text + "";
		}
	}

	public void SetActiveLevel (int activeLevel)
	{
		this.activeLevel = activeLevel;
	}

	public void LoadLevelSelector ()
	{
		MessureArea.instance.Clear ();
		SceneManager.LoadScene (0); 
	}
	public void LoadPrevLevel()
	{
		if (MessureArea.instance)
			MessureArea.instance.Clear();
		activeLevel = SceneManager.GetActiveScene().buildIndex;
		int sceneCount = SceneManager.sceneCountInBuildSettings;

		if ( activeLevel -1 >=0)
		{
			activeLevel--;
			SceneManager.LoadScene(activeLevel);
		}
		else
		{
			activeLevel = sceneCount-1;
			SceneManager.LoadScene(activeLevel);

		}
	}
	public void LoadNextLevel ()
	{
		if(MessureArea.instance)
		MessureArea.instance.Clear ();
		activeLevel = SceneManager.GetActiveScene ().buildIndex;
		int sceneCount = SceneManager.sceneCountInBuildSettings;

		if (sceneCount > activeLevel + 1) {
			activeLevel++;
			SceneManager.LoadScene (activeLevel); 
		} else {
			activeLevel = 0;
			SceneManager.LoadScene (0); 

		}
	}

	public void Update ()
	{
		if (ready && prettiesInScene == 0) {
			prettiesInScene = GameObject.FindGameObjectsWithTag ("Pretty").Length;
		}
	}

	public void Reload ()
	{
		MessureArea.instance.Clear ();
		SceneManager.LoadScene (activeLevel);
	}
	public void PusherMovesLeft(int moves) {
		AvailableMoves.text = "" + moves;
	}
	public void PusherLostLife (int life)
	{
		if (showUI == false) return;
		LifeCounterText.text = "" + life;
	}
	public void PusherMove() {
	
	}
	public void PusherOut ()
	{
		if (showUI == false) return;
		if (!levelFailed) {
			ConnectToUI ();
		}
		levelFailed.gameObject.SetActive (true);
		StartCoroutine (LevelSelectorWithDelay ());
	}

	public void PrettyOut ()
	{
		if (showUI == false) return;
		prettiesOut++;
		Debug.Log ("Pretty out!!!" + prettiesOut + " of total" + prettiesInScene);

		if (prettiesOut == prettiesInScene) {
			if (levelCompleted)
				levelCompleted.gameObject.SetActive (true);
			StartCoroutine (LoadNextLevelWithDelay ());
		}
	}
	//Initializes the game for each level.
	void InitGame ()
	{ 
		//	SceneManager.LoadScene (level);
		//	SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

 

}