using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// reference to our board
	Board m_gameBoard;

	// reference to our spawner 
	Spawner m_spawner;

	// Create a variable for currently active shape
	Shape m_activeShape;

	// Make 1 second by default
	float m_dropInterval = 1f;

	// float and represents a game time when the next event is going to happen
	float m_timeToDrop;


	// Use this for initialization
	void Start () 
	{
		
		// find spawner and board with GameObject.FindWithTag plus GetComponent; make sure you tag your objects correctly
		//m_gameBoard = GameObject.FindWithTag("Board").GetComponent<Board>();
		//m_spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();

		// find spawner and board with generic version of GameObject.FindObjectOfType, slower but less typing
		m_gameBoard = GameObject.FindObjectOfType<Board>();
		m_spawner = GameObject.FindObjectOfType<Spawner>();


		if (m_spawner)
		{
			if (m_activeShape == null) {

				m_activeShape = m_spawner.SpawnShape();
			}

			m_spawner.transform.position = Vectorf.Round(m_spawner.transform.position);
		}
			

		if (!m_gameBoard)
		{
			Debug.LogWarning("WARNING!  There is no game board defined!");
		}

		if (!m_spawner)
		{
			Debug.LogWarning("WARNING!  There is no spawner defined!");
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		// if we don't have a spawner or gameBoard just don't run the game
		if (!m_gameBoard || !m_spawner)
		{
			return;
		}

		if(Time.time >  m_timeToDrop) {

			m_timeToDrop = Time.time + m_dropInterval;
			//if there is an active shape 
			if (m_activeShape) {

				// shape landing
				m_activeShape.MoveDown();
				m_gameBoard.StoreShapeInGrid(m_activeShape);
				//validate the shape's new position after we move it down by using is valid position

				if(!m_gameBoard.IsValidPosition (m_activeShape)) {

					m_activeShape.MoveUp();
				}
			}
		}


	}
}
