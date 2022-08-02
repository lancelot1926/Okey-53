using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DragTile : MonoBehaviour
{

    private Vector3 mOffset;
    private float mZCoord;
    public bool onBottom;
    GameHandler gmHandler;
    public TextMeshProUGUI text;
    public Image icon;
    public string number;
    public string color;
    public bool isItJoker;
    private void Start()
    {
        gmHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        if (number == "J")
        {
            isItJoker = true;
            number = gmHandler.OkeyNum;
            color = gmHandler.OkeyColor;
            
        }
        
    }
    private void Update()
    {
        if (transform.parent.tag=="PlaceHolder")
        {
            if (transform.parent.childCount >= 2)
            {
                GameObject otherTile = transform.parent.GetChild(0).gameObject;
                int x = gmHandler.PlaceHolders.IndexOf(otherTile.transform.parent);
                if (x < 15)
                {

                    otherTile.transform.parent = gmHandler.PlaceHolders[x + 1];
                    otherTile.transform.position = otherTile.transform.parent.position;
                }
                if (x == 15)
                {
                    otherTile.transform.parent = gmHandler.PlaceHolders[0];
                    otherTile.transform.position = otherTile.transform.parent.position;
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.L)&&transform.parent.tag=="PlaceHolder")
        {
            transform.position = transform.parent.position;

        }

    }
    private void OnMouseDown()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out RaycastHit hitInfo, 20f);
        /*if (hitInfo.transform.tag == "Floor")
        {
            onBottom = false;
        }
        if(hitInfo.transform.tag == "IstakaBottom")
        {
            onBottom = true;
        }*/
        if (gmHandler.PlaceHolders.IndexOf(transform.parent) <= 7)
        {
            onBottom = false;
        }
        if (gmHandler.PlaceHolders.IndexOf(transform.parent) >= 8)
        {
            onBottom = true;
        }
        transform.position = transform.parent.position;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void FloorCheck()
    {
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hitInfoDown,20f);
        
        //Debug.Log(hitInfoDown.transform.tag);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * (hitInfoDown.distance), Color.red);
        if (hitInfoDown.transform.tag == "Floor")
        {
            if (onBottom == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.6f);
                
            }
            
        }
        if (hitInfoDown.transform.tag == "IstakaBottom")
        {
            if (onBottom == false)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.6f);
                
            }
            
            


        }
        /*if (hitInfoUp.transform.tag != "Top")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);

        }*/
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
        FloorCheck();
    }
    private void OnMouseUp()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hitInfo, 20f);
        if (transform.parent.tag == "PlaceHolder")
        {
            if (gmHandler.PlaceHolders.IndexOf(transform.parent) <= 7)
            {
                onBottom = false;
            }
            if (gmHandler.PlaceHolders.IndexOf(transform.parent) >= 8)
            {
                onBottom = true;
            }
            transform.position = transform.parent.position;

        }
        if (transform.parent.tag == "PassingTileColldier")
        {
            transform.parent = gmHandler.passedTilesLocation1;
            transform.position = gmHandler.passedTilesLocation1.position;
            transform.eulerAngles = new Vector3(90, 0, 0);
            gmHandler.passedTilesbyPlayer1.Add(gameObject);

        }

    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (number == gmHandler.OkeyNum && color == gmHandler.OkeyColor)
            {
                transform.eulerAngles = new Vector3(-40, 180, 0);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlaceHolder")
        {
            transform.parent = other.transform;
            /*if (other.transform.childCount>=2)
            {
                GameObject otherTile = other.transform.GetChild(0).gameObject;
                int x = gmHandler.PlaceHolders.IndexOf(other.transform);
                if (x < 15)
                {

                    otherTile.transform.parent = gmHandler.PlaceHolders[x + 1];
                    otherTile.transform.position = otherTile.transform.parent.position;
                }
                if (x==15)
                {
                    otherTile.transform.parent = gmHandler.PlaceHolders[0];
                    otherTile.transform.position = otherTile.transform.parent.position;
                }

                Debug.Log(x);
            }*/

        }
        if (other.tag == "PassingTileColldier")
        {
            transform.parent = other.transform;
            

        }
    }
}
