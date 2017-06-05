using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    private float[] platformWidths;
	private float[] platformHeights;
    private int platformSelector;

    public ObjectPooler[] theObjectPools;

    private float minHeigth;
    public Transform maxHeightPoint;
    private float maxHeight;

    public float maxHeightChange;
    private float heightChange;

	private CollectibleGenerator theCollectibleGenerator;
	public float randomCollectibleThreshold = 75f;

	public ObjectPooler[] theObstaclePools;
	public ObjectPooler obstaclePool;
	private int obstacleSelector;
	public float randomObstacleThreshold;

    void Start () {

        platformWidths = new float[theObjectPools.Length];
		platformHeights = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

		for (int i = 0; i < theObjectPools.Length; i++)
		{
			platformHeights[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.y;
		}

        minHeigth = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

		theCollectibleGenerator = FindObjectOfType<CollectibleGenerator> ();

	}
	
	void Update () {
		
        if(transform.position.x < generationPoint.position.x)
        {

            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();


            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if(heightChange < minHeigth)
            {
                heightChange = minHeigth;
            }

            Vector3 platformPosition = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);
            transform.position = platformPosition;

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;

            newPlatform.SetActive(true);

			if (Random.Range (0f, 100f) < randomCollectibleThreshold) 
			{
				theCollectibleGenerator.SpawnCollectible (new Vector3 (transform.position.x, transform.position.y + 5f, transform.position.z));
			}

			if (Random.Range (0f, 100f) < randomObstacleThreshold) 
			{
				obstacleSelector = Random.Range(0, theObstaclePools.Length);
				GameObject newObstacle = theObstaclePools[obstacleSelector].GetPooledObject();
				//GameObject newObstacle = obstaclePool.GetPooledObject();

				float obstacleXPosition = Random.Range (-platformWidths [platformSelector] / 2 + 1f, platformWidths [platformSelector] / 2 - 1f);
				float obstacleYPosition = platformHeights [platformSelector] / 2;

				Vector3 obstaclePosition = new Vector3 (obstacleXPosition, obstacleYPosition, 0f);

				newObstacle.transform.position = transform.position + obstaclePosition;
				newObstacle.transform.rotation = transform.rotation;
				newObstacle.SetActive (true);
			}

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, transform.position.y, transform.position.z);

        }
	}
}
