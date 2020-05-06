using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {


	public static bool drawDebug = false;
	

	public static Color levelColor = Color.white;
	
	public static int distancePassed = 0;
	public static int score = 0;
	public static int hiscore = 0;
	public static int crystals = 0;
	public static int level = 1;
	public static float levelLenght = 1.0f;
	public static float levelPassed = 0.0f;
	public static bool playerDead = false;
	public static bool playerFinish = false;
	public static bool inCinematic = true;
	public static bool minibossActive = false;
	public static String CustomString1="not set";

	public static Vector3 playerPosition;

	public static float controlX=0.0f;


	private static Transform playerTransform;

	private static GameObject myGameOverCanvas;
	private static GameObject myScoreCanvas;

	public static int gameRestarts=0;

	private static GameObject guiCurrentLevelText;
	private static GameObject guiNextLevelText;

	private static GameObject guiLevelProgress;


	public static bool useGyro=false;
	// Use this for initialization
	void Awake(){


		if (!SPlayerPrefs.HasKey("O_Controls")){
			SPlayerPrefs.SetInt("O_Controls",1);
		}

		if (!SPlayerPrefs.HasKey("O_Snd")){
			SPlayerPrefs.SetInt("O_Snd",1);
		}


		if (!SPlayerPrefs.HasKey("Crystals")){
			SPlayerPrefs.SetInt("Crystals",0);
		}else{
			crystals = SPlayerPrefs.GetInt("Crystals");
		}

		if (!SPlayerPrefs.HasKey("HiScore")){
			SPlayerPrefs.SetInt("HiScore",0);
		}else{
			hiscore = SPlayerPrefs.GetInt("HiScore");
		}

		if (!SPlayerPrefs.HasKey("Level")){
			SPlayerPrefs.SetInt("Level",level);
		}else{
			level = SPlayerPrefs.GetInt("Level");
		}
			
	}
	void Start () {
		
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		GameObject sSndButtonOnImg,sSndButtonOffImg,sGyroImg,sTouchImg;
		sSndButtonOnImg = GameObject.Find("sSndBtnON");
		sSndButtonOffImg = GameObject.Find("sSndBtnOFF");
		sGyroImg = GameObject.Find("sGyroImg");
		sTouchImg = GameObject.Find("sTouchImg");
		Color tempOn,tempOff,tempG,tempT;
		tempOn = sSndButtonOnImg.GetComponent<Image>().color;
		tempOff = sSndButtonOffImg.GetComponent<Image>().color;
		tempG = sGyroImg.GetComponent<Image>().color;
		tempT = sTouchImg.GetComponent<Image>().color;



		if (SPlayerPrefs.GetInt("O_Snd")==0){
			AudioListener.volume=0.0f;
			tempOn.a=0;
			tempOff.a=1;			
		}else{
			AudioListener.volume=1.0f;
			tempOn.a=1;
			tempOff.a=0;			
		}

		if (SPlayerPrefs.GetInt("O_Controls")==1){
			useGyro=false;
			tempG.a=0;
			tempT.a=0.6f;			
		}else{
			useGyro=true;
			tempG.a=0.6f;
			tempT.a=0;			
		}

		sSndButtonOnImg.GetComponent<Image>().color= tempOn;
		sSndButtonOffImg.GetComponent<Image>().color= tempOff;
		sGyroImg.GetComponent<Image>().color= tempG;
		sTouchImg.GetComponent<Image>().color= tempT;



		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		myGameOverCanvas = GameObject.FindGameObjectWithTag("GameoverScreen");
		myGameOverCanvas.SetActive(false);
		myScoreCanvas = GameObject.FindGameObjectWithTag("ScoreScreen");
		guiCurrentLevelText = GameObject.FindGameObjectWithTag("guiCurrL");
		guiNextLevelText = GameObject.FindGameObjectWithTag("guiNextL");
		guiLevelProgress = GameObject.Find("pbar_front");
		guiCurrentLevelText.GetComponent<Text>().text=level.ToString();
		guiNextLevelText.GetComponent<Text>().text=(level+1).ToString();

		myScoreCanvas.SetActive(false);



	
		distancePassed = 0;
		playerDead=false;
		playerFinish=false;
		inCinematic=true;
		minibossActive = false;
		crystals = SPlayerPrefs.GetInt("Crystals");



				myScoreCanvas.SetActive(true);
				Invoke("AllowGameplay",1.0f);

		//Invoke("AllowGameplay",1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		playerPosition = playerTransform.transform.position;
		CustomString1=(levelPassed*1.0f/levelLenght*1.0f).ToString();
		//guiLevelProgress.GetComponent<Image>().fillAmount=Mathf.Lerp(guiLevelProgress.GetComponent<Image>().fillAmount,((levelPassed*1.0f+1)/levelLenght*1.0f),0.01f);
		guiLevelProgress.GetComponent<Image>().fillAmount=levelPassed/levelLenght;
	}


	void AllowGameplay(){
		inCinematic=false;
	}


	static public void GameOver(){
		myGameOverCanvas.SetActive(true);
		gameRestarts+=1;
	}



	static public void finishLevel(){
		level++;
		if (score>hiscore){
			hiscore=score;
		}
		SPlayerPrefs.SetInt("HiScore", hiscore);
		SPlayerPrefs.SetInt("Level", level);
		SPlayerPrefs.SetInt("Crystals", crystals);
		SPlayerPrefs.Save();
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void Restart(String none){
		score=0;
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	static public void earnCrystal(){
		crystals+=1;
		
	}

	void IncrementCrystal(){
		
	}




	static public void RewardedSkipped(){
		score=0;
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
	static public void RewardedSuccess(){
				SPlayerPrefs.SetInt("Crystals", crystals);
				SPlayerPrefs.Save();
				SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}



	public void ChangeOpt(String optName){
		
		
		if (optName=="sound"){
			GameObject sSndButtonOnImg,sSndButtonOffImg;
			sSndButtonOnImg = GameObject.Find("sSndBtnON");
			sSndButtonOffImg = GameObject.Find("sSndBtnOFF");
			Color tempOn,tempOff;
			tempOn = sSndButtonOnImg.GetComponent<Image>().color;
			tempOff = sSndButtonOffImg.GetComponent<Image>().color;		
			if (SPlayerPrefs.GetInt("O_Snd")==1){
				SPlayerPrefs.SetInt("O_Snd",0);
				AudioListener.volume=0.0f;
				tempOn.a=0;
				tempOff.a=1;		
			}else{
				SPlayerPrefs.SetInt("O_Snd",1);
				AudioListener.volume=1.0f;
				tempOn.a=1;
				tempOff.a=0;		
			}
			sSndButtonOnImg.GetComponent<Image>().color= tempOn;
			sSndButtonOffImg.GetComponent<Image>().color= tempOff;			
		}

		if (optName=="controls"){
			GameObject sGyroImg,sTouchImg;
			sGyroImg = GameObject.Find("sGyroImg");
			sTouchImg = GameObject.Find("sTouchImg");
			Color tempG,tempT;
			tempG = sGyroImg.GetComponent<Image>().color;
			tempT = sTouchImg.GetComponent<Image>().color;

			if (SPlayerPrefs.GetInt("O_Controls")==1){
				SPlayerPrefs.SetInt("O_Controls",2);
				useGyro=true;
				tempG.a=0.6f;
				tempT.a=0;			
			}else{
				SPlayerPrefs.SetInt("O_Controls",1);
				useGyro=false;
				tempG.a=0;
				tempT.a=0.6f;			
			}
			sGyroImg.GetComponent<Image>().color= tempG;
			sTouchImg.GetComponent<Image>().color= tempT;
		}


		
	}



}
