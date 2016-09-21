using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class InstantiateFleet
{
  
    public class CreateFleet
    {

        public int id;
        public string name;
        public string shipClass;
        public string model;
        public float acceleration;
        public float maxSpeed;
        public float targetSpeed;
        public int level;
        public int maxCrew;
        public int firepower;
        public int horsepower;
        public int TransportCap;
        public double maintenance;
        public double cost;
        public int maxHealth;
        public float daysToBuild;
        public string rotateModel;
        public int rotateModelSize;
        public CreateFleet(int i, string n, string c, string m, string r, int ms, float a, float ma, float t, int l, int cre, int f, int h, int tra, double man, double cos, int hea, int d)
        {
            id = i;
            rotateModel = r;
            rotateModelSize = ms;
            name = n;
            shipClass = c;
            model = m;
            acceleration = a;
            maxSpeed = ma;
            targetSpeed = t;
            level = l;
            maxCrew = cre;
            TransportCap = tra;
            horsepower = h;
            maintenance = man;
            cost = cos;
            maxHealth = hea;
            daysToBuild = d;
            firepower = f;
        }
    }



    public CreateFleet getByID(int C)
    {
        for (int i = 0; i < Fleet.construction.Count; i++)
        {
            if (C == Fleet.construction[i].id)
            {
                return Fleet.construction[i];
            }
        }
        return null;
    }
    public CreateFleet getByIDMenial(int C)
    {
        for (int i = 0; i < Fleet.menialShip.Count; i++)
        {
            if (C == Fleet.menialShip[i].id)
            {
                return Fleet.menialShip[i];
            }
        }
        return null;
    }

    public void createAttributes()
    {

        Fleet.construction.Add(new CreateFleet(1, "Shuttle", "Cronos", "Ships/Cronos/CronosShuttle", "Ships/Cronos/Models/Shuttle", 5000, 4f, 7f, 7f, 1, 50, 1, 500, 500, 25000, 100000, 125, 50));
        Fleet.construction.Add(new CreateFleet(2, "Warpship", "Cronos", "Ships/Cronos/CronosWarpship", "Ships/Cronos/Models/Warpship", 3800, 3f, 6f, 6f, 1, 5, 0, 400, 0, 50000, 200000, 300, 65));
        Fleet.construction.Add(new CreateFleet(3, "Fighter", "Cronos", "Ships/Cronos/CronosFighter", "Ships/Cronos/Models/Fighter", 4000, 10f, 25f, 25f, 1, 10, 25, 750, 0, 100000, 500000, 500, 75));
        Fleet.construction.Add(new CreateFleet(4, "Battleship", "Cronos", "Ships/Cronos/CronosBattleship", "Ships/Cronos/Models/Battleship", 100, 1f, 5f, 5f, 1, 1000, 500, 200, 0, 1000000, 5000000, 5000, 125));
        Fleet.construction.Add(new CreateFleet(5, "Cruiser", "Cronos", "Ships/Cronos/CronosCruiser", "Ships/Cronos/Models/Cruiser", 160, .7f, 3f, 3f, 1, 10000, 1000, 150, 0, 2000000, 70000000, 10000, 155));
        Fleet.construction.Add(new CreateFleet(6, "Battlecruiser", "Cronos", "Ships/Cronos/CronosBattlecruiser", "Ships/Cronos/Models/BattleCruiser", 125, .2f, 1f, 1f, 1, 25000, 2500, 100, 0, 5000000, 90000000, 20000, 200));
        Fleet.menialShip.Add(new CreateFleet(1, "Colonyship", "Cronos", "Ships/Cronos/Colonyship", "Ships/Cronos/Models/Colonyship", 500, 2f, 3f, 3f, 1,15, 0, 500, 0, 100000, 100000, 300, 65));
        Fleet.menialShip.Add(new CreateFleet(2, "Tradeship", "Cronos", "Ships/Cronos/Tradeship", "Ships/Cronos/Models/Tradeship", 700, 2f, 3f, 1f, 1, 25, 0, 500, 0, 100000, 100000, 300, 56));


    }

}

