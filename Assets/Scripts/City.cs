using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class City
    {
        public static float dayToSecond = 0.3F;
        public static readonly float idealLifeSpan = 70;
        public GameObject gameObject { get; private set; }
        public float health { get; private set; }
        public float averageLifeSpan{get { return health * idealLifeSpan; }}
        public float averageChildCount { get; private set; }
        public float populationContinuous { get; private set; }
        public int population { get { return (int)populationContinuous; }}
        public string name { get; set; }
        public City(GameObject gameObject, string name)
        {
            this.gameObject = gameObject;
            averageChildCount = 10;
            populationContinuous = 1000;
            health = 1;
            this.name = name;
        }

        public void Update()
        {
            populationContinuous += population * averageChildCount * (UnityEngine.Random.value + 0.5F) * Time.deltaTime / dayToSecond / 365 / averageLifeSpan;
            populationContinuous -= population * (UnityEngine.Random.value + 0.5F) * Time.deltaTime / dayToSecond / 365 / averageLifeSpan;
            Debug.Log(population);
        }
    }
}
