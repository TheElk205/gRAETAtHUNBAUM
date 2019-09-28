using System;
using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    public class Waypoint : MonoBehaviour
    {
        public List<WaypointConnection> connections;

    }

    [Serializable]
    public class WaypointConnection
    {
        public float weight = 1.0f;
        public Waypoint pointIngTo;
    }
}