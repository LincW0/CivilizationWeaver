using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class City
    {
        public static float dayToSecond = 0.3F;
        public static readonly float idealLifeSpan = 70;
        public GameObject gameObject { get; private set; }
        public float health { get; private set; }
        public float averageLifeSpan
        {
            get { return health * idealLifeSpan; }
            private set { throw new NotSupportedException("Unable to set average lifespan"); }
        }
        public float averageChildCount { get; private set; }
        public int population { get; private set; }

        public City(GameObject gameObject)
        {
            this.gameObject = gameObject;
            averageChildCount = 10;
            population = 1000;
            health = 1;
        }

        public void Update()
        {
            if (population>1 && UnityEngine.Random.value<population * averageChildCount * Time.deltaTime / dayToSecond / 365 / averageLifeSpan)
            {
                population++;
            }
            if(UnityEngine.Random.value < population * Time.deltaTime / dayToSecond / 365 / averageLifeSpan)
            {
                population--;
            }
            Debug.Log(population);
        }
    }
}
