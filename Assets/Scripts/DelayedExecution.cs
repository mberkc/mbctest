using System;
using System.Collections;
using UnityEngine;

public class DelayedExecution : MonoBehaviour {

	/*
	 * 
	 * 
	 * Implement the function 'Do' below so that it can be called from any context.
	 * You want to pass it a function and a float 'delay'. After 'delay' seconds, the function is to be executed.
	 * You can create as many additional functions as you need.
	 * Assume that this class needs to be a 'MonoBehaviour', so don't change that.
	 * 
	 * 
	 */

	public static DelayedExecution Instance;

	void Awake () {
		Instance = this;
	}

	public static void Do (float delay) {
		if (Instance) {
			Instance.Invoke ("DelayedInvoke", delay);
			//Instance.StartCoroutine (DelayedCoroutine (delay));
		}

	}

	void DelayedInvoke () {
		print ("Delayed Invoke");
	}

	static IEnumerator DelayedCoroutine (float delay) {
		yield return new WaitForSeconds (delay);
		print ("Delayed Coroutine");
	}

}