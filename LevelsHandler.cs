using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsHandler : MonoBehaviour
{
	//	public static int levelcnt = 0;
	//	public GameObject[] levelsinteractables;
	//	int levelunlocked=0;

	//	public GameObject mainpanel;
	//	public GameObject levelpanel;

	public static LevelsHandler instance;
	public static int beginnerlvl;
	public static int interlvl;
	public static int advancelvl;

	public GameObject beginnerlevels;
//	public GameObject interlevels;
//	public GameObject advancelevels;

	public bool isbeginner = false;
	public bool isinter = false;
	public bool isadvance = false;

	public bool loadScene = false;

	[SerializeField]
	private int scene;
	[SerializeField]
	private Text loadingText;

	public GameObject loadingimg;

	public AudioSource btnsnd;

	public static bool _isTimeBased = false;
	public static bool _isMovesBased = false;

	// Use this for initialization
	void Start ()
	{

		instance = this;
		isbeginner = false;
		isinter = false;
		isadvance = false;
		loadScene = false;

		for (int i = 0; i <= PlayerPrefs.GetInt ("beginnerlvls"); i++) {
			if (i <= 119) {
				FindObjectOfType<getlvltxt>().level[i].GetComponent<Button> ().interactable = true;

			}
		}

	}
	
	// Update is called once per frame
	void Update ()
	{

//		totalscoretxt.text = "Total Coins: "+PlayerPrefs.GetInt("ScoreCoins").ToString ();

		// If the new scene has started loading...
		if (loadScene == true) {

			// ...then pulse the transparency of the loading text to let the player know that the computer is still working.
			loadingText.color = new Color (loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong (Time.time, 1));
		}

	}

	public void onclicklvl ()
	{
		Time.timeScale = 1; 
		if (AdmobController.Instance.IsInitLoad())
		{
			AdmobController.Instance.ShowInterstitialAd(1);
		}
		else
		{

			clickafterad();
		}

	}

	public void clickafterad()
	{
		if (loadScene == false)
		{

			// ...set the loadScene boolean to true to prevent loading a new scene more than once...
			loadScene = true;

			loadingimg.SetActive(true);
			// ...change the instruction text to read "Loading..."
			loadingText.text = "Loading...";

			// ...and start a coroutine that will load the desired scene.
			StartCoroutine(LoadNewScene());

		}
	}

	public void BeginnerLevelsInput (int l)
	{
		SoundManager.SM.ButtonClickSound();
		beginnerlvl = l;
		isbeginner = true;
		onclicklvl ();

		print (beginnerlvl);
	}


	public void InterLevelsInput (int l)
	{

		interlvl = l;
		isinter = true;
		onclicklvl ();

	}


	public void AdvanceLevelsInput (int l)
	{

		advancelvl = l;
		isadvance = true;
		onclicklvl ();

	}


	// The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
	IEnumerator LoadNewScene ()
	{

		// This line waits for 3 seconds before executing the next line in the coroutine.
		// This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
		yield return new WaitForSeconds (3);

		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = Application.LoadLevelAsync (scene);

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) {
			yield return null;
		}

	}
}
