using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBridge : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    public Transform StartPoint { get => _startPoint; }
    [SerializeField] private Transform _endPoint;
    public Transform EndPoint { get => _endPoint; }

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    [SerializeField] private float ropeSegLen = 0.25f;
    public float RepoSegLen { get => ropeSegLen; }
    [SerializeField] private int segmentLength = 35;
    public float SegmentLength { get => segmentLength; }
    [SerializeField] private float lineWidth = 0.1f;

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = StartPoint.position;

        for (int i = 0; i < segmentLength; i++)
        {
            ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    // Update is called once per frame
    void Update() => DrawRope();
    private void FixedUpdate() => Simulate();

    public void ChangeEndPoint(Transform transform)
    {
        _endPoint = transform;
    }

    public void ChangeStartPoint(Transform transform)
    {
        _startPoint = transform;
    }

    public void SetPoints(Transform firstConnecion, Transform secondConnection)
    {
        _startPoint = firstConnecion;
        _endPoint = secondConnection;
    }
    private void Simulate()
    {
        // SIMULATION
        Vector3 forceGravity = new Vector3(0f, -1f, 0f);

        for (int i = 1; i < segmentLength; i++)
        {
            RopeSegment firstSegment = ropeSegments[i];
            Vector3 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
            ApplyConstraint();
    }

    private void ApplyConstraint()
    {
        //Constrant to First Point 
        RopeSegment firstSegment = ropeSegments[0];
        firstSegment.posNow = StartPoint.position;
        ropeSegments[0] = firstSegment;


        //Constrant to Second Point 
        RopeSegment endSegment = ropeSegments[ropeSegments.Count - 1];
        endSegment.posNow = EndPoint.position;
        ropeSegments[ropeSegments.Count - 1] = endSegment;

        for (int i = 0; i < segmentLength - 1; i++)
        {
            RopeSegment firstSeg = ropeSegments[i];
            RopeSegment secondSeg = ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - ropeSegLen);
            Vector3 changeDir = Vector3.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector3 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    private void DrawRope()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[segmentLength];
        for (int i = 0; i < segmentLength; i++)
        {
            ropePositions[i] = ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector3 posNow;
        public Vector3 posOld;

        public RopeSegment(Vector3 pos)
        {
            posNow = pos;
            posOld = pos;
        }
    }
}
