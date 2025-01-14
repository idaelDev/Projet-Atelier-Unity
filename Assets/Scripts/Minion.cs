﻿using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {


	public float reloadTime = 1f;
	private NavMeshAgent nav;
	private bool occupied;
	private Actor target;
	private float time;

	void Awake()
	{
		occupied = false;
		nav = gameObject.GetComponent<NavMeshAgent>();
		FindNextTarget();
	}

	void Update()
	{
		time += Time.deltaTime;
		if(!occupied)
		{
			FindNextTarget();
		}
		else if(nav.remainingDistance <= 1.5)
		{
			if(time >= reloadTime)
			{
                target.DoAction(this.gameObject);
				occupied = false;
				time = 0;
			}
		}
	}

	void FindNextTarget()
	{
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Pattern");
		float minDistance = 10000f;
		target = null;
		if(targets.Length > 0)
		{
			for(int i=0; i<targets.Length; i++)
			{
				float d = Vector3.Distance(targets[i].transform.position, transform.position);
				if(d < minDistance)
				{
					if(!targets[i].GetComponent<Pattern>().choosen)
					{
						minDistance = d;
						target = targets[i].GetComponent<Actor>();
					}
				}
			}
			if(target != null)
			{
				nav.destination = target.transform.position;
				occupied = true;
				target.GetComponent<Pattern>().choosen = true;
			}
		}
	}

}
