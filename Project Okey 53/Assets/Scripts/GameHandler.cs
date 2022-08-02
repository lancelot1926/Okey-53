using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameHandler : MonoBehaviour
{

    public List<Transform> PlaceHolders;
    public GameObject Tile;
    public Transform drawableLocation;
    public List<GameObject> player1Pool;
    public List<GameObject> player2Pool;
    public List<GameObject> player3Pool;
    public List<GameObject> player4Pool;
    int x = 0;

    public Transform player2Istaka;
    public Transform player3Istaka;
    public Transform player4Istaka;
    [SerializeField]
    private List<Tiles> tileList;
    
    public GameObject OkeyIndicator;
    [SerializeField]
    private Transform IndicatorLocation;

    public List<GameObject> createdTiles;
    public Transform passedTilesLocation1;
    public List<GameObject> passedTilesbyPlayer1;
    public string OkeyNum;
    public string OkeyColor;
    private State state;
    private enum State
    {
        Player1Turn,
        Player2Turn,
        Player3Turn,
        Player4Turn,
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //spawning tiles and adjusting numbers with colors
        for (int x=0;x<tileList.Count;x++)
        {
            GameObject SpawnedTile = Instantiate(Tile, drawableLocation);
            SpawnedTile.transform.eulerAngles = new Vector3(90, 0, 0);
            if (x >= 1)
            {
                SpawnedTile.transform.position = new Vector3(drawableLocation.GetChild(0).position.x, drawableLocation.GetChild(x - 1).position.y + 0.1f, drawableLocation.GetChild(0).position.z);
                
            }
            createdTiles.Add(SpawnedTile);
            for (int y = 0; y < createdTiles.Count; y++)
            {
                
                createdTiles[y].GetComponent<DragTile>().text.SetText(tileList[y].tileNumber);
                createdTiles[y].GetComponent<DragTile>().number = tileList[y].tileNumber;
                //createdTiles[y].GetComponent<DragTile>().text.color = tileList[y].color;
                switch (tileList[y].colorName)
                {
                    case "Red":
                        createdTiles[y].GetComponent<DragTile>().text.color = Color.red;
                        createdTiles[y].GetComponent<DragTile>().icon.color = Color.red;
                        createdTiles[y].GetComponent<DragTile>().color = "Red";
                        break;
                    case "Black":
                        createdTiles[y].GetComponent<DragTile>().text.color = Color.black;
                        createdTiles[y].GetComponent<DragTile>().icon.color = Color.black;
                        createdTiles[y].GetComponent<DragTile>().color = "Black";
                        break;
                    case "Blue":
                        createdTiles[y].GetComponent<DragTile>().text.color = Color.blue;
                        createdTiles[y].GetComponent<DragTile>().icon.color = Color.blue;
                        createdTiles[y].GetComponent<DragTile>().color = "Blue";
                        break;
                    case "Green":
                        createdTiles[y].GetComponent<DragTile>().text.color = Color.green;
                        createdTiles[y].GetComponent<DragTile>().icon.color = Color.green;
                        createdTiles[y].GetComponent<DragTile>().color = "Green";
                        break;

                }

            }
        }
        OkayIndicatorFinder();
        for (int y = 0; y < 8; y++)
        {
            int random=Random.Range(0, createdTiles.Count);
            player1Pool.Add(createdTiles[random]);
            createdTiles.Remove(createdTiles[random]);
        }
        for (int y = 0; y < 7; y++)
        {
            int random = Random.Range(0, createdTiles.Count);
            createdTiles[random].transform.parent = player2Istaka;
            createdTiles[random].transform.position = player2Istaka.position;
            player2Pool.Add(createdTiles[random]);
            createdTiles.Remove(createdTiles[random]);
            
        }

        for (int y = 0; y < 7; y++)
        {
            int random = Random.Range(0, createdTiles.Count);
            createdTiles[random].transform.parent = player3Istaka;
            createdTiles[random].transform.position = player3Istaka.position;
            player3Pool.Add(createdTiles[random]);
            createdTiles.Remove(createdTiles[random]);
            
        }
        for (int y = 0; y < 7; y++)
        {
            int random = Random.Range(0, createdTiles.Count);
            createdTiles[random].transform.parent = player4Istaka;
            createdTiles[random].transform.position = player4Istaka.position;
            player4Pool.Add(createdTiles[random]);
            createdTiles.Remove(createdTiles[random]);
            
        }
        for (int z = 0; z < player1Pool.Count; z++)
        {
            player1Pool[z].transform.parent = PlaceHolders[z];
            player1Pool[z].transform.eulerAngles = new Vector3(40, 0, 0);
            player1Pool[z].transform.position = player1Pool[z].transform.parent.position;
        }
        //AiThink(player2Pool);
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int x = 1; x < createdTiles.Count; x++)
        {
            GameObject Tile = createdTiles[x];
            createdTiles[x].transform.position = drawableLocation.position;
            Tile.transform.position = new Vector3(createdTiles[x].transform.position.x, createdTiles[x-1].transform.position.y + 0.1f, createdTiles[x].transform.position.z);


        }
    }


    public void DrawFromPool()
    {
        for(int x = 0; x < PlaceHolders.Count; x++)
        {
            if (PlaceHolders[x].childCount==0)
            {
                int topTileIndex = createdTiles.Count - 1;
                createdTiles[topTileIndex].transform.parent = PlaceHolders[x];
                createdTiles[topTileIndex].transform.position = createdTiles[topTileIndex].transform.parent.position;
                createdTiles[topTileIndex].transform.eulerAngles = new Vector3(40, 0, 0);
                player1Pool.Add(createdTiles[topTileIndex]);
                createdTiles.Remove(createdTiles[topTileIndex]);
                
                break;
            }
        }
    }
    public void DrawFromPlayer()
    {

    }

    public void AiPlaying()
    {
        switch (state)
        {
            case State.Player2Turn:
                

                break;

            
            
            
            case State.Player3Turn:
                break;
            
            
            case State.Player4Turn:
                break;
        }
    }

    public void AiThink(List<GameObject> pool)
    {
        List<GameObject> BlueGroup = new List<GameObject>();
        List<GameObject> RedGroup = new List<GameObject>();
        List<GameObject> GreenGroup = new List<GameObject>();
        List<GameObject> BlackGroup = new List<GameObject>();
        List<GameObject> Ones = new List<GameObject>();
        List<GameObject> Threes = new List<GameObject>();
        List<GameObject> Fives = new List<GameObject>();
        List<GameObject> Sevens = new List<GameObject>();
        List<GameObject> Nines = new List<GameObject>();
        List <List<GameObject>> numberLists = new List<List<GameObject>>();      


        bool hasOkey = new bool();
        GameObject Okey;
        //Organise Tiles As Groups In Two Types
        for (int x = 0; x < pool.Count; x++)
        {
            switch (pool[x].GetComponent<DragTile>().color)
            {
                case "Blue":
                    
                    BlueGroup.Add(pool[x]);
                    break;
                case "Black":
                    BlackGroup.Add(pool[x]);
                    break;
                case "Red":
                    RedGroup.Add(pool[x]);
                    break;
                case "Green":
                    GreenGroup.Add(pool[x]);
                    break;
            }

            switch (pool[x].GetComponent<DragTile>().number)
            {
                case "1":
                    Ones.Add(pool[x]);
                    break;
                case "3":
                    Threes.Add(pool[x]);
                    break;
                case "5":
                    Fives.Add(pool[x]);
                    break;
                case "7":
                    Sevens.Add(pool[x]);                    
                    break;
                case "9":
                    Nines.Add(pool[x]);
                    break;
            }
        }

        //Find it has Okey or not
        for(int x = 0; x < pool.Count; x++)
        {
            if(pool[x].GetComponent<DragTile>().isItJoker==false&& pool[x].GetComponent<DragTile>().color==OkeyColor&& pool[x].GetComponent<DragTile>().number == OkeyNum)
            {
                hasOkey = true;
                Okey = pool[x];
            }
            
        }
        numberLists.Add(Ones);
        numberLists.Add(Threes);
        numberLists.Add(Fives);
        numberLists.Add(Sevens);
        numberLists.Add(Nines);

        for (int y = 0; y < numberLists.Count; y++)
        {
            PossibilityCheckNums(numberLists[y]);
        }

        Debug.Log("Ones: "+Ones.Count);
        Debug.Log("Threes: " + Threes.Count);
        Debug.Log("Fives: " + Fives.Count);
        Debug.Log("Sevens: " + Sevens.Count);
        Debug.Log("Nines: " + Nines.Count);
    }
    public void PossibilityCheckNums(List<GameObject> list)
    {

    }
    public void OkayIndicatorFinder()
    {
        while (OkeyIndicator == null)
        {
            int ran = Random.Range(0, createdTiles.Count);
            if (createdTiles[ran].GetComponent<DragTile>().number != "J")
            {
                OkeyIndicator = createdTiles[ran];
                OkeyIndicator.transform.parent = IndicatorLocation;
                OkeyIndicator.transform.position = IndicatorLocation.transform.position;
                OkeyIndicator.transform.eulerAngles = new Vector3(0, 0, 0);
                createdTiles.Remove(OkeyIndicator);
            }
            
        }
        switch (OkeyIndicator.GetComponent<DragTile>().number)
        {
            case "1":
                OkeyNum = "3";
                OkeyColor = OkeyIndicator.GetComponent<DragTile>().color;
                break;
            case "3":
                OkeyNum = "5";
                OkeyColor = OkeyIndicator.GetComponent<DragTile>().color;
                break;
            case "5":
                OkeyNum = "7";
                OkeyColor = OkeyIndicator.GetComponent<DragTile>().color;
                break;
            case "7":
                OkeyNum = "9";
                OkeyColor = OkeyIndicator.GetComponent<DragTile>().color;
                break;
            case "9":
                OkeyNum = "1";
                OkeyColor = OkeyIndicator.GetComponent<DragTile>().color;
                break;
        }
        
    }
}
