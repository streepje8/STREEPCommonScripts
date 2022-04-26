// ----------------------------------------------------------------------------
// CameraDolly
//
// Author: streep
// Date:   24/04/2022
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScrollMode
{
    LockedSmooth,
    LockedHard,
    FreeSmooth,
    FreeHard
}
public class CameraDolly : MonoBehaviour
{
    public ScrollMode scrollMode = ScrollMode.LockedSmooth;
    public Transform target;
    public CameraTrack track;
    public bool showVisual = false;
    public float smoothAmount = 0.1f;
    
    [HideInInspector]public Vector3 pointA;
    [HideInInspector]public Vector3 pointB;

    private struct trackingPoint
    {
        public float distance;
        public Vector3 point;
        public trackingPoint(float distance, Vector3 point) { this.distance = distance; this.point = point; }
    }
    void Start()
    {
        if(target == null)
        {
            Debug.LogWarning("CameraDolly has no target assigned!", this);
            enabled = false;
        }
        if (track == null)
        {
            Debug.LogWarning("CameraDolly has no track assigned!", this);
            enabled = false;
        }
        RecalculatePoints();
    }
    void Update()
    {
        if (track.points?.Count > 1)
        {
            RecalculatePoints();
            switch (scrollMode)
            {
                case ScrollMode.LockedSmooth:
                    transform.position = Vector3.Lerp(transform.position, new Vector3(pointA.x, pointA.y, transform.position.z), (1f/smoothAmount) * Time.deltaTime);
                    break;
                case ScrollMode.LockedHard:
                    transform.position = new Vector3(pointA.x, pointA.y, transform.position.z);
                    break;
                case ScrollMode.FreeSmooth:
                    if (pointB.y != pointA.y) //Note that this check fails on diagonal scrolls
                    {
                        //Vertical scroll
                        transform.position = Vector3.Lerp(transform.position, new Vector3(pointA.x, Mathf.Clamp(target.position.y, Mathf.Min(pointA.y, pointB.y), Mathf.Max(pointA.y, pointB.y)), transform.position.z), (1f / smoothAmount) * Time.deltaTime);
                    } else
                    {
                        //Horizontal scroll
                        transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(target.position.x, Mathf.Min(pointA.x, pointB.x), Mathf.Max(pointA.x, pointB.x)), pointA.y, transform.position.z), (1f / smoothAmount) * Time.deltaTime);
                    }
                    break;
                case ScrollMode.FreeHard:
                    if (pointB.y != pointA.y) //Note that this check fails on diagonal scrolls
                    {
                        //Vertical scroll
                        transform.position = new Vector3(pointA.x, Mathf.Clamp(target.position.y,Mathf.Min(pointA.y,pointB.y),Mathf.Max(pointA.y,pointB.y)), transform.position.z);
                    }
                    else
                    {
                        //Horizontal scroll
                        transform.position = new Vector3(Mathf.Clamp(target.position.x, Mathf.Min(pointA.x, pointB.x), Mathf.Max(pointA.x, pointB.x)), pointA.y, transform.position.z);
                    }
                    break;
            }
        }
    }

    public void RecalculatePoints()
    {
        if (track.points.Count > 1)
        {
            List<trackingPoint> points = new List<trackingPoint>();
            foreach (Vector3 point in track.points)
            {
                points.Add(new trackingPoint(Vector3.Distance(point, target.transform.position), point));
            }
            points.Sort(delegate (trackingPoint t1, trackingPoint t2) { return (t1.distance.CompareTo(t2.distance)); });
            pointA = points[0].point + new Vector3(0, 0.5f, 0);
            float leastDiff = 999f;
            for(int i = 1; i < 3; i++)
            {
                if(points.Count > i)
                {
                    float diff = Mathf.Abs(Vector3.Dot((target.position - pointA).normalized, (points[i].point - target.position).normalized) - Vector3.Dot((target.position - pointA).normalized, (pointB - pointA).normalized));
                    //Debug.Log(points[i].point + ">> " + diff);
                    if (diff < leastDiff)
                    {
                        pointB = points[i].point + new Vector3(0,0.5f,0);
                        leastDiff = diff; 
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (showVisual)
        {
            if (track != null && track.points?.Count > 0) //Nullcheck here since the start function has not ran yet
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(track.points[0] + new Vector3(0, 0.5f, 10), 0.5f);
                for (int i = 1; i < track.points.Count; i++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(track.points[i - 1] + new Vector3(0, 0.5f, 10), track.points[i] + new Vector3(0, 0.5f, 10));
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(track.points[i] + new Vector3(0, 0.5f, 10), 0.5f);
                    Gizmos.color = Color.white;
                }
            }
        }
    }
}
