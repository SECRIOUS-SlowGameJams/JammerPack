using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    Texture2D simpleCursor;
    [SerializeField]
    Texture2D specialCursor;




    public bool spawnParticleOnClick = true;

    public bool spawnParticleOnMove = true;

    /// Serialized Fields for Editor
#pragma warning disable 0649
    private Texture2D currentCursor;
    [SerializeField]
    cursorHotSpotStyles cursorHotSpotStyle;
#pragma warning restore 0649



    public float spawnDistanceFromCamera;
    public bool EnableVFXonMove
    {
        get { return spawnParticleOnMove; }
        set { spawnParticleOnMove = value; }
    }

    public bool EnableVFXonClick
    {
        get { return spawnParticleOnClick; }
        set { spawnParticleOnClick = value; }
    }


    //it may be useful to know which was the previous cursor before changing it.
    //So by calling this at any point and storing it in a new variable before swapping, I can revert to it.
    public Texture2D PreviousCursor
    {
        get { return currentCursor; }
    }
    public GameObject ClickParticleSystem;
    public GameObject TrailParticleSystem;

    ///  Unity CallBacks Methods
    private void Awake()
    {
        //if there is no value set in the editor, load them from the default location
        if (simpleCursor == null)
        {
            simpleCursor = Resources.Load("Textures\\CursorTextures\\SimpleCursor") as Texture2D;
        }
        if (specialCursor == null)
        {
            specialCursor = Resources.Load("Textures\\CursorTextures\\SpecialCursor") as Texture2D;
        }
    }
    void Start()
    {
        //this is the default cursor
        SetDefaultCursor(simpleCursor);
    }

    private void Update()
    {
        if (spawnParticleOnClick)
        {
            GenerateParticleOnClick();
        }
        if (spawnParticleOnMove)
        {
            GenerateParticleOnMove();
        }
    }

    //this is public too, because I may want to expand on the cursor concept
    public void CursorChange(Texture2D cursorImage)
    {
        //Debug.Log("Cursor now is " + cursorImage.name + ".");
        Vector2 hotspot = DetermineHotSpot(cursorImage, cursorHotSpotStyle);
        Cursor.SetCursor(cursorImage, hotspot, CursorMode.Auto);
        currentCursor = cursorImage;
    }

    void SetDefaultCursor(Texture2D cursorImage)
    {
        //set it in run time as software cursor;
        CursorChange(cursorImage);
    }
    public void SimpleCursor()
    {
        CursorChange(simpleCursor);
    }

    public void SpecialCursor()
    {
        CursorChange(specialCursor);
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Cursor hidden.");
    }


    void GenerateParticle(GameObject particleSystem)
    {
        Instantiate(particleSystem, MouseCursorIn3DSpace(spawnDistanceFromCamera), Quaternion.identity, this.transform);
    }

    void GenerateParticleOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateParticle(ClickParticleSystem);
        }
    }

    void GenerateParticleOnMove()
    {
        float movementThreshold = 1;
        if (CheckForMouseMovement(movementThreshold))
        {
            //some code here
        }

    }

    Vector2 DetermineHotSpot(Texture2D cursorImage, cursorHotSpotStyles pointerHotSpotStyle)
    {
        Vector2 hotspot = new Vector2();
        // The offset from the top left of the texture to use as the target point (must be within the bounds of the cursor)
        switch (pointerHotSpotStyle)
        {
            case cursorHotSpotStyles.TopLeft:
                hotspot = new Vector2(0, 0);
                break;
            case cursorHotSpotStyles.TopRight:
                hotspot = new Vector2(cursorImage.width, 0);
                break;
            case cursorHotSpotStyles.BottomLeft:
                hotspot = new Vector2(0, -cursorImage.height);
                break;
            case cursorHotSpotStyles.BottomRight:
                hotspot = new Vector2(cursorImage.width, -cursorImage.height);
                break;
            default:
                //nothing to say here...
                break;
        }
        return hotspot;
    }


    bool CheckForMouseMovement(float thres)
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        float mouseMovementAbs = mouseInput.magnitude;
        if (mouseMovementAbs > thres)
            return true;
        else
            return false;
    }


    #region Mouse Cursor Coordinates in 3D Space
    //This works with camera rotation and even in perspective mode, but size is based on clip distance
    public static Vector3 MouseCursorIn3DSpace(float extraDistance)
    {
        return PointIn3DSpace(Input.mousePosition, extraDistance, false);
    }

    //offsets the given point by the given distance from the camera clip plane or the camera
    public static Vector3 PointIn3DSpace(Vector3 point, float extraDistance, bool includeNearPlane)
    {
        float cameraNearClipPlane = Camera.main.nearClipPlane;
        float pointDepthIn3D = 0;
        if (includeNearPlane)
        { pointDepthIn3D = cameraNearClipPlane + extraDistance; }
        else
        { pointDepthIn3D = extraDistance; }

        //i have pixels and world units in one vector.... jesus.
        Vector3 hybriPointPosition = new Vector3(point.x, point.y, pointDepthIn3D);
        Vector3 pointPositionInWorldUnits = Camera.main.ScreenToWorldPoint(hybriPointPosition);

        return pointPositionInWorldUnits;
    }

    public static void DebugDrawMousein3DSpace(float extraDistance)
    {
        //I want it in pixels this time.
        Vector3 mousePositionOnScreen = Input.mousePosition;

        Color LineColor = new Color();
        Color RayColor = new Color();

        if (WithinBoundsCheck(mousePositionOnScreen))
        {
            LineColor = Color.green;
            RayColor = Color.red;
        }
        else
        {
            LineColor = Color.blue;
            RayColor = Color.yellow;
        }

        Vector3 mousePositionIn3D = MouseCursorIn3DSpace(0);
        Debug.DrawLine(Camera.main.transform.position, mousePositionIn3D, LineColor, 1f);

        Vector3 direction = mousePositionIn3D - Camera.main.transform.position;
        Vector3 startPosition = mousePositionIn3D;
        //Vector3 startPosition = Camera.main.transform.position;

        Debug.DrawRay(startPosition, direction * 100, RayColor, 1f);
    }

    public static bool WithinBoundsCheck(Vector3 pointValue)
    {
        if (
            (pointValue.x < 0) || (pointValue.x > Screen.width)
            ||
            (pointValue.y < 0) || (pointValue.y > Screen.height)
            )
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    #endregion


   public bool debugMode;
    private void OnGUI()
    {
#if UNITY_EDITOR

       

    if (debugMode)
        {
            Rect mouseDebug = new Rect(10, 10, 300, 200);

            GUI.Box(mouseDebug, "Mouse debug");
            ////GUI.Button() only gets called every other second
            //isDebugging = GUI.Toggle(new Rect(20, 40, 200, 20), isDebugging, new GUIContent( "Enable debugging", "will show mouse coordinates"));

            GUI.Label(new Rect(20, 60, 400, 20), "Mouse position on screen:");

            GUI.Label(new Rect(40, 80, 400, 20),
                ("x-position: " + Input.mousePosition.x.ToString()));
            GUI.Label(new Rect(40, 100, 400, 20),
        "y-position: " + Input.mousePosition.y.ToString());


            GUI.Label(new Rect(20, 120, 400, 20), "Mouse position in 3D space from main camera:");

            GUI.Label(new Rect(40, 140, 400, 20),
               "x-position: " + MouseCursorIn3DSpace(0).x);
            GUI.Label(new Rect(40, 160, 400, 20),
        "y-position: " + MouseCursorIn3DSpace(0).y);

        }


#endif



    }


}

enum cursorHotSpotStyles
{
    TopRight,
    TopLeft,
    BottomRight,
    BottomLeft,
}
