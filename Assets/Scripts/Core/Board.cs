using UnityEngine;
using System.Collections;


public class Board : MonoBehaviour {

	// a SpriteRenderer that will be instantiated in a grid to create our board
	public Transform m_emptySprite;

	// the height of the board
	public int m_height = 30;

	// width of the board
	public int m_width = 10;

	// number of rows where we won't have grid lines at the top
	public int m_header = 8;

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

	bool IsWithinBoard(int x, int y) {
		// it will return true when x value is between 0 and width of the board or y value is 0 or positive.
		// Don't need to check the top part as the pieces always travel downward.

		return (x >= 0 && x < m_width && y >= 0 ); 
	}

	bool IsOccupied(int x, int y, Shape shape)
	{
		// it checks the 2-dimensional grid array, returns true, only if the grid space contains that's not
		// nothing, and has the parent that is from different shape object
		// returns false if it is empty and also is not empty and it belongs to existing shape that we are testing
		return (m_grid[x,y] !=null && m_grid[x,y].parent != shape.transform);
	}
	// method that takes shape as an argument 
	// loops through all the shape squares that's within board
	public bool IsValidPosition(Shape shape) {

		// for each loop: loop through each individual square of the shape
		foreach (Transform child in shape.transform)
		{
			Vector2 pos = Vectorf.Round(child.position);			// if any of those squares fails the condition, then the shape is in invalid position
			if(!IsWithinBoard((int) pos.x, (int) pos.y)) {

				// then return false

				return false;
			}
			// Each child square will have 2 checks, 
			// verify that each square is both on the board.
			// and not on a square that's been taken
			if (IsOccupied((int) pos.x, (int) pos.y, shape))
			{
				return false;
			}
		}
		return true;
			
	}	

	// draw our empty board with our empty sprite object
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

	// Store the shape in board's grid array
	public void StoreShapeInGrid(Shape shape) {

		// Make sure the shape isn't nothing
		if(shape == null) {

			return;
		}

		// it will loop through the shape and 
		// on each child square we will find the rounded x, y coordinate that 
		// We will store child square into the grid
		// The array index comes from the x and y coordinate
		// We must use (int) for explicit cast, because pos.x and pos.y are float, and
		// the array index is an integer
		// You potentially lose the value converting from float to an integer, so
		// c# makes you cast it explicitly.   
		foreach (Transform child in shape.transform)
		{
			Vector2 pos = Vectorf.Round(child.position);
			m_grid[(int) pos.x, (int) pos.y] = child;
		}
	}

}
