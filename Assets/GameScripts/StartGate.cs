using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGate : MonoBehaviour
{
	[SerializeField]
	private Timer timer = null;

	[SerializeField]
	private Transform startTarget = null;

	private GameObject rollerBall = null;

	void Start()
	{
		RollerBallMover[] rollers = FindObjectsByType<RollerBallMover>(FindObjectsSortMode.None);
		if (rollers != null && rollers.Length > 0)
		{
			for (int i = 0; i < rollers.Length; i++)
			{
                if (rollerBall == null)
                {
                    if (rollers[i].enabled)
                    {
                        rollerBall = rollers[i].gameObject;
                    }              
                }
                else if (rollers[i].enabled)
				{
                    rollers[i].enabled = false;
					Debug.Log("Multiple active RollerBallMover object. Randomly picked one to start with, make sure only one is active on game start.");
                }
					 
			}
		}

		if (rollerBall == null) 
		{
			Debug.Log("Could not find any active RollerBallMover objects. Make sure you have at least one active roller ball in the scene.");
		}

		// Align roller ball with start gate postion
		rollerBall.transform.position = startTarget.position;
		rollerBall.transform.rotation = startTarget.rotation;
	}

	private void OnTriggerExit(Collider other)
	{
		timer.timerStart();
	}
}
