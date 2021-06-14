using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SECRIOUS.Utilities;

/*
 This script assumes an orthographic camera that moves on the x-axis.
 It tiles a seamless sprite in accordance with the camera movement.
 Please place your prefab as a child of the gameObject in the appropriate position.
 It must have a SpriteRenderer Component attached with the desired Sprite.
 WARNING: Assign the prefab slot from the project ASSET panel, NOT from the Prefab instance in your scene.
 All subsequent sprites will be tiled on the same y and z value, with different x values.
 Optional:
 If the chosen sprite does not fill the initial viewport, the sprite will also be tiled to fill it.
 */

public class InfinitePlatformTiler : MonoBehaviour
    {

    /// Public Properties
    [Tooltip("This script assumes an orthographic Camera.")]
    public Camera Camera;
    [Tooltip("Please select this from the asset panel and place an instance of it appropriately in your scene under this gameobject.")]
    public GameObject tilePrefab;
    public Transform tileContainer;

    ///  private Fields
    float tileLengthInUnits;
    Vector3 referenceCameraPosition;
    GameObject currentSpawn;
    Transform[] currentTiles;

    
        ///  Unity CallBacks Methods
    void Awake()
    {
        currentTiles = tileContainer.FetchAllChildrenT();
        referenceCameraPosition = Camera.transform.position;
        tileLengthInUnits = tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Start()
    {
        //may optionally also fill the viiew in case the chosen sprite is smaller than the viewport
        InitializeView();
    }

    void Update()
    {
        ObserveCameraMovement();
    }


    //editor methods
    [ContextMenu("CenterStartingTile")]
    void CenterStartingTile()
    {
        currentTiles = tileContainer.FetchAllChildrenT();
        currentTiles[0].transform.position = new Vector3(Camera.transform.position.x, currentTiles[0].transform.position.y, currentTiles[0].transform.position.z);
    }

    [ContextMenu("CenterCamera")]
    void CenterCamera()
    {
        currentTiles = tileContainer.FetchAllChildrenT();
        Camera.transform.position = new Vector3(currentTiles[0].transform.position.x, Camera.transform.position.y, Camera.transform.position.z);
    }

    ///  Private Methods
    float CalculateViewWidth()
    { 
        float screenRatio = Screen.width / (float)Screen.height;
        if (Camera.orthographic)
        {
            float ViewWidth;
            float CameraHeight = 2 * Camera.orthographicSize;
            ViewWidth = screenRatio * CameraHeight;
            return ViewWidth;
        }
        else
        {
            Debug.LogError("Please choose an orthographic camera.");
            return 0;
        }
    }


    int CalculateInitialTileAmountToFillScreen(float cameraViewPortWidthInUnits)
    {
       float amount = cameraViewPortWidthInUnits / tileLengthInUnits;
       //better overshoot  
       int integerAmount = Mathf.CeilToInt(amount);
        //remove existing tiles from calculations
       int amountOfTilesToAdd = integerAmount - currentTiles.Length;
        //in case it is an odd number, I need to make it even to tile symmetrically
       if (amountOfTilesToAdd / 2 != 0)
            amountOfTilesToAdd += 1;
       // I still needs one extra at each side for initialization
              amountOfTilesToAdd += 2;
            return amountOfTilesToAdd;
    }

     void InitializeView()
    {
        float cameraViewPortWidthInUnits = CalculateViewWidth();
 
        int amountOfTileSpritesToFillViewPort = CalculateInitialTileAmountToFillScreen(cameraViewPortWidthInUnits);
        //WARNING: assumes symmetrical tiling
        int amountOfTilesToFillEachSide = amountOfTileSpritesToFillViewPort / 2;

        AssignCurrentTile(Direction.right);
        MultiSpawn(amountOfTilesToFillEachSide, Direction.right);
        AssignCurrentTile(Direction.left);
        MultiSpawn(amountOfTilesToFillEachSide, Direction.left);

        void MultiSpawn(int amount, Direction direction)
        {
            for (int i = 0; i < amount; i++)
            {
                Spawn(direction);
            }
        }
    }



    void ObserveCameraMovement()
    {
        if ((Camera.transform.position.x - referenceCameraPosition.x) >= tileLengthInUnits)
        {
            referenceCameraPosition = Camera.transform.position;
            Debug.Log("Spawning new tile");
            AssignCurrentTile(Direction.right);
            Spawn(Direction.right);
            RemoveSpawn(Direction.right);
        }
        else if ((Camera.transform.position.x - referenceCameraPosition.x) <= -tileLengthInUnits)
        {
            referenceCameraPosition = Camera.transform.position;
            Debug.Log("Spawning new tile");
            AssignCurrentTile(Direction.left);
            Spawn(Direction.left);
            RemoveSpawn(Direction.left);
        }
    }

    void AssignCurrentTile(Direction direction)
    {
        if (direction == Direction.right)
        {
            currentSpawn = currentTiles[currentTiles.Length - 1].gameObject;
        }
        else
            currentSpawn = currentTiles[0].gameObject;
    }

    int leftcounter;
    int rightcounter;
    void Spawn(Direction tileDirection)
    {
        Vector3 spawningPoint;
        if (tileDirection == Direction.right)
        {  
            spawningPoint = currentSpawn.transform.position + new Vector3(tileLengthInUnits, 0, 0);
            currentSpawn = Instantiate(tilePrefab, spawningPoint, Quaternion.identity, tileContainer.transform);
            rightcounter++;
            currentSpawn.name = tilePrefab.name + "_Right_" + rightcounter;
            currentSpawn.transform.SetAsLastSibling();
        }
        else
        { 
            spawningPoint = currentSpawn.transform.position - new Vector3(tileLengthInUnits, 0, 0);
            currentSpawn = Instantiate(tilePrefab, spawningPoint, Quaternion.identity, tileContainer);
            leftcounter++;
            currentSpawn.name = tilePrefab.name + "_Left_" + leftcounter;
            currentSpawn.transform.SetAsFirstSibling();
        }
        currentTiles = tileContainer.FetchAllChildrenT();
    }


    void RemoveSpawn(Direction direction)
    {
        if (direction == Direction.right)
        {
            Destroy(currentTiles[0].gameObject);
            leftcounter--;
        }
        else
        {
            Destroy(currentTiles[currentTiles.Length - 1].gameObject);
            rightcounter--;
        }

        StartCoroutine(UpdateContents());

        IEnumerator UpdateContents()
        {
            yield return new WaitForFixedUpdate();
            currentTiles = tileContainer.FetchAllChildrenT();
        }
    }


    enum Direction
    {
        left,
        right
    }

}
