using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	private float runningTime;
	private bool run;

	// Use this for initialization
	void Start () {
		runningTime = 0;
		run = false;
	}
	
	// Fixed Update is called quickly but not every frame
	// the timer doesn't need per frame accuracy so update
	// on FixedUpdate is more then fine
	void FixedUpdate () {
		if (run)
		{
			runningTime += Time.fixedDeltaTime;
		}
	}

	public void timerStart()
	{
		run = true;
	}

	public void timerStop()
	{
		run = false;
	}

	public void timerReset()
	{
		runningTime = 0;
	}

	public float getTimerTime()
	{
		return runningTime;
	}

	public bool isRunning()
	{
		return run;
	}
}
