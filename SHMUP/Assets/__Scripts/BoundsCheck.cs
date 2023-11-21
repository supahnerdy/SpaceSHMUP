using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    [System.Flags]
    public enum eScreenLocs {
        onScreen = 0, // 0000 in binary
        offRight = 1, // 0001 in binary
        offLeft = 2, // 0010 in binary
        offUp = 4, // 0100 in binary
        offDown = 8 // 1000 in binary
    }
    public enum bType { center, inset, outset };
    [Header("Camera Boundaries")]
    public float camWidth;
    public float camHeight;
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    //public bool isOnScreen = true;

    [Header("GameObject Boundaries")]
    public bType boundsType = bType.center;
    public float radius = 1.0f;
    public bool keepOnScreen = true;
    // Start is called before the first frame update
    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Find the checkRadius that will enable center, inset, or outset
        float checkRadius = 0;
        if (boundsType == bType.inset)
            checkRadius = -radius;
        if (boundsType == bType.outset)
            checkRadius = radius;

        Vector3 pos = transform.position;
        screenLocs = eScreenLocs.onScreen;
        //isOnScreen = true;
        // Restrict the X position to camWidth
        if (pos.x > camWidth + checkRadius) {
            pos.x = camWidth + checkRadius;
            screenLocs |= eScreenLocs.offRight;
            //isOnScreen = false;
        }
        if (pos.x < -camWidth - checkRadius) {
            pos.x = -camWidth - checkRadius;
            screenLocs |= eScreenLocs.offLeft;
            //isOnScreen = false;
        }
        // Restrict the Y position to camHeight
        if (pos.y > camHeight + checkRadius) {
            pos.y = camHeight + checkRadius;
            screenLocs |= eScreenLocs.offUp;
            //isOnScreen = false;
        }
        if (pos.y < -camHeight - checkRadius) {
            pos.y = -camHeight - checkRadius;
            screenLocs |= eScreenLocs.offDown;
            //isOnScreen = false;
        }

        if (keepOnScreen && !isOnScreen) {
            transform.position = pos;
            screenLocs = eScreenLocs.onScreen;
            //isOnScreen = true;
        }
    }

    public bool isOnScreen {
        get { return (screenLocs == eScreenLocs.onScreen);}
    }

    public bool LocIs(eScreenLocs checkLoc) {
        if (checkLoc == eScreenLocs.onScreen) return isOnScreen;
        return ( (screenLocs & checkLoc) == checkLoc);
    }
}
