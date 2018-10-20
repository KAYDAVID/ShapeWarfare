using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{

    public GameObject placeholder;
    public List<GameObject> placeholders = new List<GameObject>();
    public List<GameObject> TDplaceholders = new List<GameObject>();
    public List<Sprite> shapes = new List<Sprite>();
    //public int score = 0;

    private int maxX = 8;
    private int minX = 1;
    private int maxY = 4;
    private int minY = -4;

    public Slider blue;
    public Slider red;
    public Slider yellow;
    public Slider white;

    // Use this for initialization
    void Start()
    {
        createPlaceholders();
        Fill();
    }

    // Update is called once per frame
    void Update()
    {

    }
//####################################-3Match-###################################################

    private void createPlaceholders()
    {
        int num = 0;
        for (int i = minY; i <= maxY; i++)
        {
            for (int j = minX; j <= maxX; j++)
            {
                Vector2 pos = new Vector2(j, i);

                GameObject newP = (GameObject)Instantiate(placeholder, pos, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                newP.GetComponent<Shape>().placeID = num;
                placeholders.Add(newP);

                num++;
            }
        }
    }

    public void Fill()
    {
        for (int i = 0; i < placeholders.Count; i++)
        {
            if (placeholders[i].GetComponent<SpriteRenderer>().sprite == null)
            {
                SpriteRenderer s = placeholders[i].GetComponent<SpriteRenderer>();

                List<Sprite> possibleShapes = new List<Sprite>();
                possibleShapes.AddRange(shapes);

                if (i > 1)
                {
                    possibleShapes.Remove(placeholders[i - 1].GetComponent<SpriteRenderer>().sprite);
                    //Debug.Log (placeholders [i - 1].GetComponent<SpriteRenderer>().sprite.name);
                }

                if (i > 8)
                {
                    possibleShapes.Remove(placeholders[i - 8].GetComponent<SpriteRenderer>().sprite);
                }

                s.sprite = possibleShapes[Random.Range(0, possibleShapes.Count)];

            }

        }
    }

    public GameObject GetPlace(int id)
    {
        if (id <= 71 && id >= 0)
        {
            //Debug.Log (placeholders.Count);
            return placeholders[id];
        }

        return null;

    }

    public void DropDown(GameObject currentPlace)
    {
        if (currentPlace.GetComponent<Shape>().GetComponent<SpriteRenderer>().sprite != null)
        {
            return;
        }

        //Debug.Log ("drop");
        currentPlace.GetComponent<Shape>().Swap(0);

    }

    public void BoardDrop()
    {
        foreach (var x in placeholders)
        {
            DropDown(x);
        }

    }

    public void SetScore(int score, int color) // 0 blue, 1 red, 2 yellow, 3 white
    {
        switch (color)
        {
            default:
                break;
            case 0:
                blue.value += score;
                break;
            case 1:
                red.value += score;
                break;
            case 2:
                yellow.value += score;
                break;
            case 3:
                white.value += score;
                break;
        }

    }

//#######################################-TD-#####################################################

    private void TDcreatePlaceholders()
    {
        int num = 0;
        for (int i = minY; i <= maxY; i++)
        {
            for (int j = minX; j <= maxX; j++)
            {
                Vector2 pos = new Vector2(j, i);

                GameObject newP = (GameObject)Instantiate(placeholder, pos, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
                newP.GetComponent<Shape>().placeID = num;
                TDplaceholders.Add(newP);

                num++;
            }
        }
    }

}
