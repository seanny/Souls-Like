using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    [Serializable]
    public class WaterData
    {
        public GameObject waterParentPrefab;
        public GameObject waterObjectPrefab;
        public List<GameObject> waterObjects;

        public float waterHeight = 0;

        public int maxWater = 10;

        public GameObject waterParent;
    }


    public class Water : MonoBehaviour
    {
        public WaterData waterData;

        private void Start()
        {
            if(waterData == null)
            {
                waterData = new WaterData();
            }
            CreateParentObject();
            CreateWater();
        }

        private void CreateWater()
        {
            for (int i = 0; i < waterData.maxWater; i++)
            {
                for (int y = 0; y < waterData.maxWater; y++)
                {
                    GameObject water = Instantiate(waterData.waterObjectPrefab);
                    water.transform.position = new Vector3(i * 10f, waterData.waterHeight, y * 10f);
                    water.transform.SetParent(waterData.waterParent.transform);
                }
            }
        }

        private void CreateParentObject()
        {
            waterData.waterParent = Instantiate(waterData.waterParentPrefab);
            waterData.waterParent.transform.position = Vector3.zero;
        }
    }
}
