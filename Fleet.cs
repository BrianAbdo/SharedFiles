using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Fleet
{
    public static List<InstantiateFleet.CreateFleet> construction = new List<InstantiateFleet.CreateFleet>();
    public static List<InstantiateFleet.CreateFleet> menialShip = new List<InstantiateFleet.CreateFleet>();
}

