using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusSpawner : MonoBehaviour
{

    public GameObject[] bonusPrefabs;
    private int currentBonusToSpawn = 0;
    public AnimationCurve SpawnCurve;

	public GameObject bonusTurbo;    

    private GameObject selectedObjectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        //currentBonusToSpawn = Mathf.Clamp(Mathf.RoundToInt(bonusPrefabs.Length*CurveWeightedRandom(SpawnCurve)),0,bonusPrefabs.Length-1);
            
            currentBonusToSpawn=Random.Range(0,bonusPrefabs.Length);

			if ((GameManager.distancePassed+1)%30==0){
				selectedObjectToSpawn=bonusTurbo;
			}else{
				selectedObjectToSpawn=bonusPrefabs[currentBonusToSpawn];
            }
			

			GameObject go;
            Vector3 newpos;
			go = Instantiate (selectedObjectToSpawn) as GameObject;
            newpos=transform.position;
            go.transform.position=newpos;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


float CurveWeightedRandom(AnimationCurve curve) {
    return curve.Evaluate(Random.value);
}


}
