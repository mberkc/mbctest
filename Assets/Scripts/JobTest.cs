using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

/*
 *
 * JobTest scene runs very slow because of the repeated dummy math operation below. Implement the for loop below, using parallelized Unity jobs and Burst compiler to gain performance
 * If the 'count' is too large for your machine to handle, you can decrease it.
 * 
 */

public class JobTest : MonoBehaviour {

	[SerializeField]
	private bool useJob = false;

	private int count = 1000000;

	private float[] values;

	NativeArray<float> values_Job;

	MathOperationJob mathOperationJob;

	JobHandle jobHandle;

	void Start () {
		values = new float[count];
	}

	void Update () {

		if (useJob) {

			values_Job = new NativeArray<float> (count, Allocator.Persistent);
			mathOperationJob = new MathOperationJob () {
				values = values_Job
			};

			jobHandle = mathOperationJob.Schedule (values_Job.Length, 64);
			jobHandle.Complete ();
			values_Job.Dispose ();

		} else {
			for (int i = 0; i < values.Length; i++) {
				values[i] = Mathf.Sqrt (Mathf.Pow (values[i] + 1.75f, 2.5f + i)) * 5 + 2f;
			}
		}

	}

	struct MathOperationJob : IJobParallelFor {
		public NativeArray<float> values;
		public void Execute (int index) {
			float temp = values[index];
			values[index] = Mathf.Sqrt (Mathf.Pow (temp + 1.75f, 2.5f + index)) * 5 + 2f;
		}
	}

}