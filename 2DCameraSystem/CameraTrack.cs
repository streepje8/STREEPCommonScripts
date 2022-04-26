// ----------------------------------------------------------------------------
// CameraTrack
//
// Author: streep
// Date:   24/04/2022
// ----------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraTrack", menuName = "Custom/SpiritStorm/CameraTrack", order = 100)]
public class CameraTrack : ScriptableObject
{
    public List<Vector3> points = new List<Vector3>();
}
