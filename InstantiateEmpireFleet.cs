using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class InstantiateEmpireFleet:MonoBehaviour
{
    int shipNum = 1;
    
    public class UnderConstruction {

       public InstantiateFleet.CreateFleet f;
        public int id;
        public float days;
        public UnderConstruction(InstantiateFleet.CreateFleet s, int i, float d)
        {
            id = i;
            f = s;
            days = d;
        }
    }


    public class CreateConstruction
    {

        public int id;
        public string name;
        public string shipClass;
        public string model;
        public float acceleration;
        public float maxSpeed;
        public float targetSpeed;
        public int level;
        public int crew;
        public int maxCrew;
        public int firepower;
        public int horsepower;
        public int TransportCap;
        public int marines;
        public int army;
        public double maintenance;
        public double cost;
        public int health;
        public int maxHealth;
        public float daysToBuild;
        public GameObject shipholder;
        public string type;
        public Empire emp;
        public string rotateModel;
        public InstantiateFleet.CreateFleet under;
        public enum shipAction
        {
            Docked,
            Docking,
            Moving,
            Waypoint,
            Patrol,
            Drifting,
            Fighting,
            Sieging,
            SustainingGate,
            Warping
        }
        public SolarSystem system;
        public shipAction shipaction = shipAction.Docked;
        public CreateConstruction(UnderConstruction f, Empire e)
        {
            float modifier = 0;

            under = f.f;
            emp = e;
            system = emp.getHomeworldPlanet().getSystem();
            InstantiateFleet fleet = new InstantiateFleet();
            id = f.id;
            name = f.f.name + "-" +id;
           
            type = f.f.name;
            model = f.f.model;
            acceleration = f.f.acceleration;
            maxSpeed = f.f.maxSpeed;
            targetSpeed = f.f.targetSpeed;
            level = f.f.level;
            maxCrew = f.f.maxCrew;
            firepower = f.f.firepower * (f.f.level +1);
            horsepower = f.f.horsepower;
            TransportCap = f.f.TransportCap * (f.f.level + 1); ;
            maintenance = f.f.maintenance * (f.f.level + 1);

            if (emp.hasAttribute(32))
            {
                float mod2 = Attributes.getAttribute(32).attrValue2;

                maintenance += maintenance * mod2;
            }

            cost = f.f.cost * (f.f.level + 1);
            maxHealth = f.f.maxHealth * (f.f.level + 1);

            if (emp.hasAttribute(66))
            {
                float mod2 = Attributes.getAttribute(66).attrValue;

                maxHealth += Mathf.RoundToInt(maxHealth * mod2);
            }
            daysToBuild = f.f.daysToBuild;
            health = maxHealth;
            crew = maxCrew;
            marines = 0;
            army = 0;
            rotateModel = f.f.rotateModel;
            shipClass = f.f.shipClass;
           
        }
        public void Start()
        {
            Debug.Log(model);
            shipholder = Instantiate(Resources.Load(model) as GameObject);
			shipholder.GetComponent<Unit>().Init(this, emp);
            //shipholder.transform.SetParent(emp.getHomeworldPlanet().NavalYard.transform.FindChild("ShipyardBlue").transform);
            shipholder.transform.localRotation = Quaternion.LookRotation(Vector3.zero);
            //shipholder.transform.localPosition = /*(p.NavalYard.transform.localPosition) +*/ new Vector3(-.25f, 0, 1);
			shipholder.transform.SetParent(GameObject.Find("Ships").transform);
			shipholder.transform.position = emp.getHomeworldPlanet().NavalYard.transform.FindChild("ShipyardBlue").transform.position + new Vector3(-.25f, 0, 1);

            //shipholder.transform.SetParent(GameObject.Find("Ships").transform);
            //shipholder.transform.position = (p.NavalYard.transform.FindChild("ShipyardBlue").transform.position);
            //shipholder.transform.rotation = p.NavalYard.transform.FindChild("ShipyardBlue").transform.rotation;
            //shipholder.transform.SetParent(GameObject.Find("Ships").transform);

            //setPosition(/*getGameObject().transform.TransformPoint*/(p.NavalYard.transform.position) + new Vector3(-.25f, 0, 1));

        }
        public void launch()
        {
            if (getAction() == shipAction.Docked)
            {
                setAction(shipAction.Moving);
                shipholder.GetComponent<UnitAnimation>().startLaunch();
            }
             

        }
        
        public void goToLoc()
        {
            if(getAction() != shipAction.Docked)
            {
                GameObject.Find("Galaxy").GetComponent<UIChangeUI>().changeView();

                GameObject.Find("Galaxy").GetComponent<CustomBuild>().Militarymap.GetComponent<UpdatedMilitaryMapUI>().loadMilitaryMap();

                GameObject.Find("Galaxy").GetComponent<CustomBuild>().Milcamera.transform.position = new Vector3(shipholder.transform.position.x, shipholder.transform.position.y + 300, shipholder.transform.position.z);

            }

        }
        public void Destroy()
        {
            if (getAction() == shipAction.Docked)
            {
                emp.construction.Remove(this);
                DestroyImmediate(shipholder);
            }
        }
        public void Repair()
        {
            if (getAction() == shipAction.Docked)
            {
                emp.getPlayer().changeTreasury((double)((double)health/(double)maxHealth) * 5000000);

                health = maxHealth;
            }
        }
        public void levelUp()
        {
            if (getAction() == shipAction.Docked)
            {
                level = level + 1;
                emp.getPlayer().changeTreasury(-level * 5000000);
                firepower = under.firepower * (level + 1);
                TransportCap = under.TransportCap * (level + 1); ;
                maintenance = under.maintenance * (level + 1);
                maxHealth = under.maxHealth * (level + 1);
                health = maxHealth;
            }
        }
        public void setAction(CreateConstruction.shipAction ac)
        {
            shipaction = ac;
        }
        public shipAction getAction()
        {
            return shipaction;
        }
        public void setSystem(SolarSystem s)
        {
            system = s;
        }
        public SolarSystem getSystem()
        {
            return system;
        }
    }
    public void BuildShip(UnderConstruction f, Empire e)
    {
        InstantiateFleet fleet = new InstantiateFleet();

        CreateConstruction b = new CreateConstruction(f, e);
        e.construction.Add(b);
        b.Start();

        //e.underConstruction.Remove(f);
    }
    public void BuyShip(int i, Empire e)
    {
        InstantiateFleet fleet = new InstantiateFleet();
        float modifier = 0;
        float daysToBuild = fleet.getByID(i).daysToBuild;
        double cost = 0;

        if (fleet.getByID(i).name == "Cruiser")
        {
            if (e.hasAttribute(32))
            {
                return; // cannot build due to attribute 23 (Inferior Navy)
            }
        }

        if (fleet.getByID(i).name == "Warpship")
        {
            if (!e.getHomeworldPlanet().getConstruction(19))
            {
                return; // can't build due to not having a warp gate
            }
        }

        if (fleet.getByID(i).name == "Battlecruiser")
        {
            if (e.getHomeworldPlanet().getConstruction(3))
            {
                float mod2 = Construction.getConstruction(3).attrValue;

                modifier += mod2;
            }
        }

        if (e.hasAttribute(12))
        {
            float mod2 = Attributes.getAttribute(12).attrValue;
            modifier += mod2;
        }

        daysToBuild += (daysToBuild * modifier);
        modifier = 0;

        Debug.Log(e.getActiveNavy());
        //if (e.getActiveNavy() >= fleet.getByID(i).maxCrew)
        //{
            e.changeDeployedNavy(fleet.getByID(i).maxCrew, false);

            cost = fleet.getByID(i).cost;

            if (e.hasAttribute(47))
            {
                float mod2 = Attributes.getAttribute(47).attrValue;
                modifier += mod2;
            }

            if (e.hasAttribute(32))
            {
                float mod2 = Attributes.getAttribute(32).attrValue;
                modifier += mod2;
            }

            cost += (cost * modifier);

            e.getPlayer().changeTreasury(-cost);
            
            e.underConstruction.Add(new UnderConstruction(fleet.getByID(i), shipNum, daysToBuild));
            shipNum++;
        //}
    

    }
    public UnderConstruction getFirstOfType(int j, Empire e)
    {
        bool h = false;
        for (int i = 0; i < e.underConstruction.Count; i++)
        {
            if (!h)
            {
                if (j == e.underConstruction[i].f.id)
                {
                    h = true;

                    return e.underConstruction[i];
                }
            }
       
        }
        return null;
    }
    public UnderConstruction getByID(int j, Empire e)
    {
        for (int i = 0; i < e.underConstruction.Count; i++)
        {
            if (j == e.underConstruction[i].id)
            {
                return e.underConstruction[i];
            }
        }
        return null;
    }
    public int getUnderConstruction(int i, Empire e)
    {
        int j = 0;
        foreach (UnderConstruction c in e.underConstruction)
        {
            if (c.f.id == i)
            {
                j++;
            }
        }
        return j;
    }
    public float getUnderConstructionNext(int i, Empire e)
    {
        float j = 0;
        bool type = false;
        foreach (UnderConstruction c in e.underConstruction)
        {
            if (!type)
            {
                if (c.f.id == i)
                {
                    j = c.days;
                    type = true;
                }
            }

        }
        return j;
    }
    public void checkUnderConstructionDays(int i, Empire e)
    {
        if (getByID(i,e).days == 0)
        {
            e.underConstruction.Remove(getByID(i, e));
        }
    }

 
    public void createAttributes()
    {

    }

}

