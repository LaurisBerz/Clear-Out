using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleGenerator : MonoBehaviour {

	public ObjectPooler collectiblePool;

	public float distanceBetweenCollectibles;
	public float chanceMoreCollectiblesWillSpawn;

	public void SpawnCollectible (Vector3 startPosition) 
	{
		if (Random.Range (0f, 100f) < chanceMoreCollectiblesWillSpawn) {
			GameObject collectible1 = collectiblePool.GetPooledObject ();
			collectible1.transform.position = startPosition;
			collectible1.SetActive (true); 
		}

		if (Random.Range (0f, 100f) < chanceMoreCollectiblesWillSpawn) {
			GameObject collectible2 = collectiblePool.GetPooledObject ();
			collectible2.transform.position = new Vector3 (startPosition.x - distanceBetweenCollectibles, startPosition.y, startPosition.z);
			collectible2.SetActive (true);
		}

		if (Random.Range (0f, 100f) < chanceMoreCollectiblesWillSpawn) {
			GameObject collectible3 = collectiblePool.GetPooledObject ();
			collectible3.transform.position = new Vector3 (startPosition.x + distanceBetweenCollectibles, startPosition.y, startPosition.z);
			collectible3.SetActive (true);
		}
	}
}
