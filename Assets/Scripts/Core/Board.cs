using UnityEngine;
using System.Collections;


public class Board : MonoBehaviour {

	// a SpriteRenderer that will be instantiated in a grid to create our board
	public Transform m_emptySprite;
	// the height of the board

	public int m_height = 30;
		// width of the board

	public int m_width = 10;
	public int m_header = 10;

	// Use 2-D array to store the data for every grid

	Transform[,] m_grid;

	// Awake is called when script is loaded (Awake is a pre-start, anything you want to run before start)

	void Awake()
	{
		m_grid = new Transform[m_width,m_height];
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

		if (m_emptySprite)
		{
			// By using nested for-loop, it should hit every grid of the board
			// Outer Loop that increment through the rows
			for (int y = 0; y < m_height - m_header; y++)
			{

				// Inner Loop that counts through each column
				for (int x = 0; x < m_width; x++) 
				{
					// It handles the creation of each square
					Transform clone;

					//Vector3 takes one counter from x and one counter from y
					clone = Instantiate(m_emptySprite, new Vector3(x, y, 0), Quaternion.identity) as Transform;
										// names the empty squares for organizational purposes

					clone.name = "Board Space ( x = " + x.ToString() +  " , y =" + y.ToString() + " )"; 
							// parents all of the empty squares to the Board object

					clone.transform.parent = transform;
				}
			}
		}
	}


}
