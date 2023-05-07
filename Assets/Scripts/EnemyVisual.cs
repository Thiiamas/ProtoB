using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyVisual : MonoBehaviour, IEnemyVisual
{
    public class KochLineSide
    {
        public List<KochLineAlive> KochLines = new List<KochLineAlive>();
        public GameObject go;
    }
    public Enemy enemy;
    public enum Shape
    {
        Triangle,
        Square,
        Pentagon,
        Hexagon,
        Heptagon,
        Octagon
    }
    public Shape shape = new Shape();
    public int tiersCount = 1;
    public List<GameObject> tierList = new List<GameObject>();
    
    public Dictionary<GameObject,List<KochLineSide>> tiersSideList = new Dictionary<GameObject,List<KochLineSide>>();

    private void Start()
    {
        //get component
        enemy = transform.parent.GetComponent<Enemy>();
        //Tiers construction
        tiersCount = transform.childCount;
        if(transform.childCount == 0)
        {
            for (int i = 0; i < tiersCount; i++)
            {
                GameObject child = new GameObject("Tiers" + i);
                tierList.Add(child);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                tierList.Add(child);
            }
        }

        //Side constructions
        for(int i = 0; i < tierList.Count; i++)
        {
            GameObject tier = tierList[i];
            int faceNumber = GetTiersSides();
            List<KochLineSide> sideList = new List<KochLineSide>();
            if (tier.transform.childCount != faceNumber)
            {
                Debug.Log("Tiers have no child, have to create one (to implement)");
                for(int y = 0; y < faceNumber; y++)
                {
                    GameObject face = new GameObject("Face" + y);
                    face.AddComponent<LineRenderer>();
                    face.AddComponent<KochLineAlive>();
                }
            }
            else
            {
                for (int y = 0; y < tier.transform.childCount; y++)
                {
                    GameObject child = tier.transform.GetChild(y).gameObject;
                    List<KochLineAlive> kochLines = new List<KochLineAlive>();
                    for(int x = 0; x < child.transform.childCount; x++)
                    {
                        kochLines.Add(child.transform.GetChild(x).gameObject.GetComponent<KochLineAlive>());
                    }
                    KochLineSide side = new KochLineSide()
                    {
                        KochLines = kochLines,
                        go = child,
                    };
                    sideList.Add(side);
                }
            }
            tiersSideList[tier] = sideList;
        }
        foreach (KeyValuePair<GameObject, List<KochLineSide>> kv in tiersSideList)
        {
            foreach (KochLineSide side in kv.Value)
            {
                foreach(KochLineAlive kla in side.KochLines)
                {
                    enemy.SuscribeToRefreshEvent(kla.HandleRefreshEvent);
                }
                //Debug.Log(kv.Key.name + " : " + side.go.name);
            }

        }



    }

    private void Awake()
    {
        
    }

    int GetTiersSides()
    {
        int sideNumber = 0;
        switch (shape)
        {
            case Shape.Square:
                sideNumber = 6;
                break;
            default:
                Debug.Log("getTiersSides() not implemented");
                break;
        }
        return sideNumber;
    }
}
