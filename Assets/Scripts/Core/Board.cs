﻿using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

	public Transform m_emptySprite;
	public int m_height = 30;
	public int m_width = 30;

	public int m_header = 8;

	// Use 2-D array to store the data for every grid
	Transform[,] m_grid;


	// Awake is called when script is loaded (Awake is a pre-start, anything you want to run before start)
	void Awake() {
		m_grid = new Transform[m_width, m_height];
	}

	// Start is called when script is enabled before any calls to update happen
	// Use this for initialization
	void Start () {
		DrawEmptyCells();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DrawEmptyCells() {

		// Check to see if we assigned prefab to emptySprite
		if(m_emptySprite != null) {
		// By using nested for-loop, it should hit every grid of the board
		// Outer Loop that increment through the rows
			for(int y = 0; y < m_height; y++) {

				// Inner Loop that counts through each column
				for(int x = 0; x < m_width; x++) {

					// It handles the creation of each square
					Transform clone;

					//Vector3 takes one counter from x and one counter from y
					clone = Instantiate(m_emptySprite, new Vector3(x, y, 0), Quaternion.identity) as Transform;
					clone.name = "Board Space ( x = " + x.ToString() + " , y =" + y.ToString() + ")";
					clone.transform.parent = transform;
				}
			}
		}
		else {
			
			Debug.Log("WARNING! Please assign the emptySprite object!");
		}
	}
}
