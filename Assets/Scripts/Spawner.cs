using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour 
{
	public static Spawner spawner;
	public ObjectCache[] caches;
	public Dictionary<GameObject, bool> activeCachedObjects;

	void Start ()
	{
		spawner = this;
		int amount = 0;
		
		for(int i = 0; i < caches.Length; i++) {
			caches[i].Initialize();
			amount += caches[i].cacheSize;
		}
		
		activeCachedObjects = new Dictionary<GameObject, bool> ();
	}

	[System.Serializable]
	public class ObjectCache 
	{
		public GameObject prefab;
		public int cacheSize = 10;

		private GameObject[] objects;
		private int cacheIndex = 0;

		public void Initialize()
		{
			objects = new GameObject[cacheSize];

			for(var i = 0; i < cacheSize; i++) {
				objects[i] = Instantiate (prefab) as GameObject;
				objects[i].SetActive (false);
				objects[i].name = objects[i].name + i;
			}
		}

		public GameObject GetNextObjectInCache ()
		{
			GameObject obj = null;

			for (int i = 0; i < cacheSize; i++) {
				obj = objects[cacheIndex];

				if(obj.activeInHierarchy == false) {
					break;
				}

				cacheIndex = (cacheIndex + 1) % cacheSize;
			}

			if(obj.activeInHierarchy == true) {
				Spawner.Destroy(obj);
			}

			cacheIndex = (cacheIndex + 1) % cacheSize;
			return obj;
		}
	}

	public static GameObject Spawn (GameObject prefab, Vector3 position, Quaternion rotation)
	{
		ObjectCache cache = null;

		if(spawner) {
			for (int i = 0; i < spawner.caches.Length; i++) {
				if (spawner.caches[i].prefab == prefab) {
					cache = spawner.caches[i];
				}
			}
		}

		if(cache == null) {
			return Instantiate (prefab, position, rotation) as GameObject;
		}

		GameObject obj = cache.GetNextObjectInCache ();

		obj.transform.position = position;
		obj.transform.rotation = rotation;
		obj.SetActive (true);
		spawner.activeCachedObjects [obj] = true;

		return obj;
	}

	public static void Destroy (GameObject objectToDestroy)
	{
		if(spawner && spawner.activeCachedObjects.ContainsKey (objectToDestroy)) {
			objectToDestroy.SetActive (false);
			spawner.activeCachedObjects[objectToDestroy] = false;
		} else {
			objectToDestroy.SetActive(false);
		}
	}
}