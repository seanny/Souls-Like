using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    [Serializable]
    public class WaterData
    {
        public GameObject waterParent;
        public GameObject waterObjectPrefab;
        public List<GameObject> waterObjects;

        public float waterHeight = 0;

        public int maxWater = 10;
    }


    public class Water : MonoBehaviour
    {
        public WaterData waterData = new WaterData();

        private void Start()
        {
            CreateWater();
        }

        /// <summary>
        /// Create Water
        /// </summary>
        private void CreateWater()
        {
            for (int i = 0; i < waterData.maxWater; i++)
            {
                for (int y = 0; y < waterData.maxWater; y++)
                {
                    GameObject water = Instantiate(waterData.waterObjectPrefab, new Vector3(i * 10f, waterData.waterHeight, y * 10f), Quaternion.identity, waterData.waterParent.transform);
                    waterData.waterObjects.Add(water);
                }
            }
        }
    }
}
