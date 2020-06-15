using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 
 * 
 * Complete the functions below.  
 * For sure, they don't belong in the same class. This is just for the test so ignore that.
 * 
 * 
 */

public static class Utility {

	public static GameObject[] GetObjectsWithName (string name) {

		/*
		 * 
		 *	Return all objects in the scene with the specified name. Don't think about performance, do it in as few lines as you can. 
		 * 
		 */

		List<GameObject> objectsInScene = new List<GameObject> ();
		foreach (GameObject go in Resources.FindObjectsOfTypeAll (typeof (GameObject))) {
			if (go.hideFlags != HideFlags.None)
				continue;
			if (PrefabUtility.GetPrefabType (go) == PrefabType.Prefab || PrefabUtility.GetPrefabType (go) == PrefabType.ModelPrefab)
				continue;
			if (go.name == name)
				objectsInScene.Add (go);
		}

		// Another option to just return root objects
		//Scene currentScene = SceneManager.GetActiveScene ();
		//foreach (GameObject go in currentScene.GetRootGameObjects ())
		//	if (go.name == name)
		//		objectsInScene.Add (go);

		return objectsInScene.ToArray ();
	}

	public static bool CheckCollision (Ray ray, float maxDistance, int layer) {
		/*
		 * 
		 *	Perform a raycast using the ray provided, only to objects of the specified 'layer' within 'maxDistance' and return if something is hit. 
		 * 
		 */

		// layer to layerMask conversion
		int layerMask = 1 << layer;
		return Physics.Raycast (ray, maxDistance, layerMask);
	}

	public static Vector2[] GeneratePoints (int size) {

		/*
		 * Generate 'size' number of random points, making sure they are distributed as evenly as possible (Trying to achieve maximum distance between every neighbor).
		 * Boundary corners are (0, 0) and (1, 1). (Point (1.2, 0.45) is not valid because it's outside the boundaries. )
		 * Is there a known algorithm that achieves this?
		 */

		// Source: https://programming.guide/random-point-within-circle.html
		// Applied it to square(points outside bounds recalculated)

		float a, r, x, y, minDistance;
		int i = 0;
		float radius = Mathf.Sqrt (2);

		//
		minDistance = 2f / (3 * Mathf.Sqrt (size));

		Vector2[] points = new Vector2[size];
		while (i < size) {
			a = 2 * Mathf.PI * UnityEngine.Random.Range (0f, 1f);
			r = radius * Mathf.Sqrt (UnityEngine.Random.Range (0f, 1f));
			x = r * Mathf.Cos (a);
			y = r * Mathf.Sin (a);

			// If points is inside square
			if (Mathf.Abs (x) <= 1 && Mathf.Abs (y) <= 1) {
				// Conversion from bounds (-1,1) to (0,1)
				Vector2 point = new Vector2 (x + 1, y + 1) / 2;

				// Checking if point is not closer to others than minDistance
				for (int j = 0; j < i; j++) {
					if (Vector2.Distance (point, points[j]) < minDistance)
						break;
					if (j + 1 == i) {
						points[i] = point;
						i++;
					}
				}
				if (i == 0) {
					points[i] = point;
					i++;
				}
			}
		}

		return points;
	}

	public static Texture2D GenerateTexture (int width, int height, Color color) {

		/*
		 * Create a Texture2D object of specified 'width' and 'height', fill it with 'color' and return it. Do it as performant as possible.
		 */

		Texture2D texture = new Texture2D (width, height, TextureFormat.RGBA32, false);

		// If these are not default
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;

		int size = width * height;
		Color[] fillColorArray = new Color[size];
		for (int i = 0; i < size; i++)
			fillColorArray[i] = color;
		texture.SetPixels (fillColorArray);
		texture.Apply ();

		return texture;
	}

}