using UnityEngine;
using System.Collections;

public class treespawner : MonoBehaviour 
{
	public GameObject Tree;
	public float spawntime = 4f;
	public Transform[] spawnlocations; 


	// Use this for initialization
	void Start () 
	{
		InvokeRepeating ("Spawn", spawntime, spawntime);
	}

	// Update is called once per frame
	void Update () 
	{


	}
	void Spawn()
	{
		int spawnlocationIndex = Random.Range (0, spawnlocations.Length);

		Instantiate (Tree, spawnlocations [spawnlocationIndex].position, spawnlocations [spawnlocationIndex].rotation);
	}
}
