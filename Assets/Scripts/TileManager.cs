using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {


	public GameObject[] tilePrefabs;
	public GameObject finishPrefab;
	public GameObject emptyPrefab;


	public int maxLevelLength = 10;
	private int passedTiles=0;
	private int generatedTiles=0;

	private int currentTileToSpawn = 0;

	public int[] LevelTreshold;

	private int CurrentTreshold=0;
	private Transform playerTransform;

	private float spawnZ= 0.0f;
	private float tileLenght=20f;
	private int amntOfTilesOnScreen=12;
	private float safeZone=55.0f;
	private List<GameObject> activeTiles;


	private int currentTileSelector=2;

	private int startTiles=0;

	public AnimationCurve SpawnCurve;


	public Color[] bgColors;
	public Color[] tubeColors;

	private GameObject myCamera;

	// Use this for initialization
	void Start () {

		if (GameManager.levelColor == Color.white){
			GameManager.levelColor = bgColors[Random.Range(0,bgColors.Length-1)];
		}

		if(Random.Range(0,10)>6){
			GameManager.levelColor = bgColors[Random.Range(0,bgColors.Length-1)];
		}
		

		myCamera = GameObject.Find("MainCamera");
		myCamera.GetComponent<Camera>().backgroundColor = GameManager.levelColor;
		RenderSettings.fogColor = GameManager.levelColor;

		//if (GameManager.level>=LevelTreshold.Length){
		maxLevelLength=maxLevelLength+Mathf.RoundToInt((GameManager.level/2));
		//}
		activeTiles = new List<GameObject>();
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		for (int i = 0; i<amntOfTilesOnScreen;i++){
			SpawnTile();
		}
			GameManager.levelLenght=maxLevelLength*20.0f;
			//GameManager.levelPassed=passedTiles;		
		//minibossNexTileSpawn = 5;
		Invoke("changeTile",5);

		
	}
	
	// Update is called once per frame
	void Update () {

		if (playerTransform.position.z - safeZone > (spawnZ - amntOfTilesOnScreen * tileLenght)){
			
				SpawnTile();
				DeleteTile();
		}
		
	}

	private void SpawnTile(int prefabIndex = -1){

		if (GameManager.level>=LevelTreshold.Length){
			CurrentTreshold=tilePrefabs.Length-1;
		}else{
			CurrentTreshold=LevelTreshold[GameManager.level];
		}

			if (startTiles<2){
				startTiles=startTiles+1;
				currentTileToSpawn = 0;
			}else{


				currentTileToSpawn = Random.Range(0,CurrentTreshold);
				//	currentTileToSpawn = Mathf.Clamp(Mathf.RoundToInt(tilePrefabs.Length*CurveWeightedRandom(SpawnCurve)),0,tilePrefabs.Length-1);
				
			}
		

		//GameManager.CustomString1="tiles from 0 to "+currentTileSelector.ToString();


		if (generatedTiles==maxLevelLength){
			GameObject go;
			go = Instantiate (finishPrefab) as GameObject;
			go.transform.SetParent(transform);
			go.transform.position = Vector3.forward *spawnZ;
			spawnZ += tileLenght;
			activeTiles.Add(go);
		}
		
		if (generatedTiles<maxLevelLength){
			generatedTiles++;
			GameObject go;
			go = Instantiate (tilePrefabs[currentTileToSpawn]) as GameObject;
			go.transform.SetParent(transform);
			go.transform.position = Vector3.forward *spawnZ;
			spawnZ += tileLenght;
			activeTiles.Add(go);
		}

	


	

	}


	private void DeleteTile(){
		Destroy(activeTiles[0]);
		activeTiles.RemoveAt(0);
		passedTiles++;
			
			//GameManager.levelPassed=passedTiles;
	}

	private void changeTile(){



		currentTileToSpawn = Random.Range(0,currentTileSelector);
		Invoke("changeTile",Random.Range(5,10));
	}







float CurveWeightedRandom(AnimationCurve curve) {
    return curve.Evaluate(Random.value);
}





}
