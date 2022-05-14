using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeHandler : MonoBehaviour
{
    [SerializeField]
    bool myIsRightEdge;
    [SerializeField]
    Transform[] myHangingTransforms;
    [SerializeField]
    Transform[] myStandUpTransforms;

    Vector2 myHangposition;
    Vector2 myStandupPosition;
    void Awake()
    {
        if (myIsRightEdge)
        {
            myHangposition = myHangingTransforms[0].position;
            myStandupPosition = myStandUpTransforms[0].position;
        }
        else
        {
            myHangposition = myHangingTransforms[1].position;
            myStandupPosition = myStandUpTransforms[1].position;
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetEdgeSide()
    {
        return myIsRightEdge;
    }

    public Vector2 GetHangingposition()
    {
        return myHangposition;
    }

    public Vector2 GetStandUpPosition()
    {
        return myStandupPosition;
    }
}
