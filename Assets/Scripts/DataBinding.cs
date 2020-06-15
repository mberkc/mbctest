using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * 
 * Assume that you have created the class 'Data' for some reason and you are processing some data inside it. The output is float 'data'.
 * Then you realized you have to show this variable on the screen via a UnityEngine.Text, using the class 'TextSetter' you have written.
 * Data and TextSetter classes have no means of communication. They can't use references of each other.
 * In addition, you liked the TextSetter class a lot and want to use it in different places with different types of data later on. You want to generalize your technique.
 * 
 * Writing a global manager class that handles the classes below is not an option.
 * 
 * Static access to data classes is not the answer.
 * 
 * How would you solve this? Is there a behavioural pattern that seems to be the answer?
 * 
 * You can implement anything you wish.
 * 
 * Your solution doesn't actually have to work, just make sure your solution and intentions are clear conceptually.
 * 
 * 
 */

// Mediator Design Pattern	

public class Data : MonoBehaviour {

	private float data = 0f;

	IMediator mediator;

	public void SetMediator (IMediator mediator) {
		this.mediator = mediator;
	}

	private void Update () {
		data += Time.deltaTime * 5f;
		if (mediator != null)
			mediator.Publish (data);
	}

}

public class TextSetter : MonoBehaviour, IMediator {

	[SerializeField] private Text text;

	public void Publish (float data) {
		text.text = "" + data;
	}

}

public interface IMediator {
	void Publish (float data);
}