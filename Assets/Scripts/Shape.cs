using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {

	public static bool isSelected = false;
	public bool unSelected = true;
	public bool isMovable = false;
	public Board board;

	public int placeID;

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		userInput ();
		FindMatch (0);
		FindMatch (1);
		FindMatch (2);
		FindMatch (3);
	}

	public void Select()
	{
		isSelected = true;
		isMovable = true;
		transform.localScale += new Vector3(0.1F, 0.1F, 0);
	}

	public void DeSelect()
	{
		isSelected = false;
		isMovable = false;
		unSelected = true;
		transform.localScale -= new Vector3(0.1F, 0.1F, 0);
	}

	public void OnMouseDown() 
	{
		if (isSelected == true && unSelected == false ) {
			DeSelect ();
			unSelected = true;
		}

		else if (isSelected == false) {
			Select ();
			unSelected = false;
		}
		//Debug.Log (isSelected.ToString());

	}
		

	public void Swap(int direction)
	{
		if (direction == 0)  //up
		{
			if (placeID < 64) 
			{
				//Debug.Log (placeID);
				GameObject other = board.GetComponent<Board> ().GetPlace (placeID + 8);

				Sprite tmpSprite = other.GetComponent<SpriteRenderer> ().sprite;

				other.GetComponent<SpriteRenderer> ().sprite = this.GetComponent<SpriteRenderer> ().sprite;

				this.GetComponent<SpriteRenderer> ().sprite = tmpSprite;
			}

		}

		if (direction == 1) //Left 
		{ 	
			if (placeID == 0 || placeID == 8 || placeID == 16 ||
			    placeID == 24 || placeID == 32 || placeID == 40 ||
			    placeID == 48 || placeID == 56 || placeID == 64 || placeID == 72) 
			{
				return;
			}

				
				GameObject other = board.GetComponent<Board> ().GetPlace (placeID - 1);

				Sprite tmpSprite = other.GetComponent<SpriteRenderer> ().sprite;

				other.GetComponent<SpriteRenderer> ().sprite = this.GetComponent<SpriteRenderer> ().sprite;

				this.GetComponent<SpriteRenderer> ().sprite = tmpSprite;

		}


		if (direction == 2) //down
		{
			if (placeID <= 7) 
			{
				return;
			}
				GameObject other = board.GetComponent<Board> ().GetPlace (placeID - 8);

				Sprite tmpSprite = other.GetComponent<SpriteRenderer> ().sprite;

				other.GetComponent<SpriteRenderer> ().sprite = this.GetComponent<SpriteRenderer> ().sprite;

				this.GetComponent<SpriteRenderer> ().sprite = tmpSprite;

		}

		if (direction == 3)  //right
		{
			if (placeID == 7 || placeID == 15 || placeID == 23 || 
				placeID == 31 || placeID == 39 || placeID == 47 ||
				placeID == 55 || placeID == 63 || placeID == 71) 
			{
				return;
			}
			GameObject other = board.GetComponent<Board> ().GetPlace (placeID + 1);

			Sprite tmpSprite = other.GetComponent<SpriteRenderer> ().sprite;

			other.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer> ().sprite;

			this.GetComponent<SpriteRenderer> ().sprite = tmpSprite;

		}
	}

	public void userInput()
	{
		if (isMovable == true) 
		{
			
			if (Input.GetKeyDown (KeyCode.W)) {
				Swap (0); // up
				FindMatch (0);
				DeSelect ();
			} else if (Input.GetKeyDown (KeyCode.A)) {
				Swap (1); // left
				FindMatch (1);
				DeSelect ();
			} else if (Input.GetKeyDown (KeyCode.S)) {
				Swap (2); // down
				FindMatch (2);
				DeSelect ();
			} else if (Input.GetKeyDown (KeyCode.D)) {
				Swap (3); // right
				FindMatch (3);
				DeSelect ();
			} else if (Input.GetKeyDown (KeyCode.K)) {
				//FindMatch ();
				DeSelect ();
			}
		}
	}

	private List<GameObject> Nearby(GameObject currentPlace, List<GameObject> matchList)
	{
		List<GameObject> isMatch = new List<GameObject> ();

		int cPid = currentPlace.GetComponent<Shape> ().placeID;

		Sprite cPsprite = currentPlace.GetComponent<SpriteRenderer> ().sprite;

		//Up
		if (matchList.Contains (board.GetComponent<Board> ().GetPlace (cPid + 8)) || cPid + 8 > 71) {
		} else {
				if (board.GetComponent<Board> ().GetPlace (cPid + 8).GetComponent<SpriteRenderer> ().sprite == cPsprite) {
				isMatch.Add (board.GetComponent<Board> ().GetPlace (cPid + 8));
			}
		}

		//Left
		if (matchList.Contains (board.GetComponent<Board> ().GetPlace (cPid - 1)) || 
			cPid - 1 == -1 || cPid - 1 == 7 || cPid - 1 == 15 ||
			cPid - 1 == 23 || cPid - 1 == 31 || cPid - 1 == 39 ||
			cPid - 1 == 47 || cPid - 1 == 55 || cPid - 1 == 63 || cPid - 1 == 71) {
		} else {
			if (board.GetComponent<Board> ().GetPlace (cPid - 1).GetComponent<SpriteRenderer> ().sprite == cPsprite) {
				isMatch.Add (board.GetComponent<Board> ().GetPlace (cPid - 1));
			}
		}

		//Right
		if (matchList.Contains (board.GetComponent<Board> ().GetPlace (cPid + 1)) ||

			cPid + 1 == 0 || cPid + 1 == 8 || cPid + 1 == 16 ||
			cPid + 1 == 24 || cPid + 1 == 32 || cPid + 1 == 40 ||
			cPid + 1 == 48 || cPid + 1 == 56 || cPid + 1 == 64 || cPid + 1 == 72) {
		} else {
			if (board.GetComponent<Board> ().GetPlace (cPid + 1).GetComponent<SpriteRenderer> ().sprite == cPsprite) {
				isMatch.Add (board.GetComponent<Board> ().GetPlace (cPid + 1));
			}
		}
			
		//Down
		if (matchList.Contains (board.GetComponent<Board> ().GetPlace (cPid - 8)) || cPid - 8 < 0) {
		} else {
			if (board.GetComponent<Board> ().GetPlace (cPid - 8).GetComponent<SpriteRenderer> ().sprite == cPsprite) {
				isMatch.Add (board.GetComponent<Board> ().GetPlace (cPid - 8));
			}
		}

		return isMatch;

	}

	/*
	private List<GameObject> Nearby(GameObject currentPlace, List<GameObject> matchList)
	{
		List<GameObject> isMatch = new List<GameObject> ();

		//Up
		if (matchList.Contains (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID + 8)) || currentPlace.GetComponent<Shape> ().placeID + 8 > 71) {
		} else {
			if (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID + 8).GetComponent<SpriteRenderer> ().sprite == currentPlace.GetComponent<SpriteRenderer> ().sprite) {
				isMatch.Add (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID + 8));
			}
		}

		//Left
		if (matchList.Contains (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID - 1)) || 
			currentPlace.GetComponent<Shape> ().placeID - 1 == -1 || currentPlace.GetComponent<Shape> ().placeID - 1 == 7 || currentPlace.GetComponent<Shape> ().placeID - 1 == 15 ||
			currentPlace.GetComponent<Shape> ().placeID - 1 == 23 || currentPlace.GetComponent<Shape> ().placeID - 1 == 31 || currentPlace.GetComponent<Shape> ().placeID - 1 == 39 ||
			currentPlace.GetComponent<Shape> ().placeID - 1 == 47 || currentPlace.GetComponent<Shape> ().placeID - 1 == 55 || currentPlace.GetComponent<Shape> ().placeID - 1 == 63 || currentPlace.GetComponent<Shape> ().placeID - 1 == 71) {
		} else {
			if (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID - 1).GetComponent<SpriteRenderer> ().sprite == currentPlace.GetComponent<SpriteRenderer> ().sprite) {
				isMatch.Add (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID - 1));
			}
		}

		//Right
		if (matchList.Contains (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID + 1)) ||

			currentPlace.GetComponent<Shape> ().placeID + 1 == 0 || currentPlace.GetComponent<Shape> ().placeID + 1 == 8 || currentPlace.GetComponent<Shape> ().placeID + 1 == 16 ||
			currentPlace.GetComponent<Shape> ().placeID + 1 == 24 || currentPlace.GetComponent<Shape> ().placeID + 1 == 32 || currentPlace.GetComponent<Shape> ().placeID + 1 == 40 ||
			currentPlace.GetComponent<Shape> ().placeID + 1 == 48 || currentPlace.GetComponent<Shape> ().placeID + 1 == 56 || currentPlace.GetComponent<Shape> ().placeID + 1 == 64 || currentPlace.GetComponent<Shape> ().placeID + 1 == 72) {
		} else {
			if (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID + 1).GetComponent<SpriteRenderer> ().sprite == currentPlace.GetComponent<SpriteRenderer> ().sprite) {
				isMatch.Add (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID + 1));
			}
		}

		//Down
		if (matchList.Contains (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID - 8)) || currentPlace.GetComponent<Shape> ().placeID - 8 < 0) {
		} else {
			if (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID - 8).GetComponent<SpriteRenderer> ().sprite == currentPlace.GetComponent<SpriteRenderer> ().sprite) {
				isMatch.Add (board.GetComponent<Board> ().GetPlace (currentPlace.GetComponent<Shape> ().placeID - 8));
			}
		}

		return isMatch;

	}
	*/

	public void FindMatch (int direction)
	{
		List<GameObject> matchList = new List<GameObject> ();
		List<GameObject> matches = new List<GameObject> ();
		List<GameObject> matches2 = new List<GameObject> ();

		switch (direction) {
		default:
			break;
		case 0:
			matches.Add (board.GetComponent<Board>().GetPlace(placeID + 8));
			break;

		case 1:
			matches.Add (board.GetComponent<Board>().GetPlace(placeID - 1));
			break;

		case 2:
			matches.Add (board.GetComponent<Board>().GetPlace(placeID - 8));
			break;

		case 3:
			matches.Add (board.GetComponent<Board>().GetPlace(placeID + 1));
			break;
		}


		//matches.Add (board.GetComponent<Board>().GetPlace(placeID));
		//matches.AddRange (Nearby (board.GetComponent<Board>().GetPlace(placeID), matchList));

		do 
		{
			matches.AddRange(matches2);
			matches2.Clear();

			foreach (var match in matches) 
			{
				matches2.AddRange (Nearby (match, matchList));
				matchList.Add (match);
			}
			matches.Clear();

			//Debug.Log("Matches2 " + matches2.Count);

		} while (matches2.Count != 0);

		if (matchList.Count >= 3) 
		{
			int color = 5;

			if (matchList[1].GetComponent<SpriteRenderer>().sprite == board.shapes[0]) {
				color = 0;
			}else if (matchList[0].GetComponent<SpriteRenderer>().sprite == board.shapes[1]) {
				color = 1;
			}else if (matchList[0].GetComponent<SpriteRenderer>().sprite == board.shapes[2]) {
				color = 2;
			}else if (matchList[0].GetComponent<SpriteRenderer>().sprite == board.shapes[3]) {
				color = 3;
			}

			clearMatch (matchList);

			board.SetScore (matchList.Count, color);

			board.BoardDrop ();
			board.BoardDrop ();
			board.BoardDrop ();
			board.BoardDrop ();
			board.BoardDrop ();
			board.BoardDrop ();
			board.BoardDrop ();
			board.BoardDrop ();
			board.BoardDrop ();
			board.Fill ();
		}


	}

	public void clearMatch(List<GameObject> matchList)
	{
		foreach (var x in matchList) {
			x.GetComponent<SpriteRenderer> ().sprite = null;
		}

	}


}
