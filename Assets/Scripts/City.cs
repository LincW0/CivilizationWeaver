using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class City
    {
        public static readonly float idealLifeSpan = 80;
        public GameObject GameObject { get; protected set; }
        private float baseHealth;
        public float Health { get { return baseHealth * (FoodQuantity == 0 ? 0.002F : 1); } }
        public float AverageLifeSpan { get { return Health * idealLifeSpan; } }
        public float AverageChildCount { get; protected set; }
        protected float populationContinuous;
        public long Population => (long)populationContinuous;
        public string Name { get; set; }
        protected float deathCountContinuous;
        public long DeathCount => (long)deathCountContinuous;
        public List<CityConponent> CityConponents { get; protected set; }
        public float FoodStorageCapacity { get; protected set; }
        public float FoodQuantity { get; protected set; }
        private GameTime timePanel;
        public City(GameObject gameObject, string name)
        {
            GameObject = gameObject;
            AverageChildCount = 10;
            populationContinuous = 1000;
            deathCountContinuous = 0;
            baseHealth = 0.25F;
            Name = name;
            CityConponents = new List<CityConponent>();
            timePanel = GameObject.Find("TimePanel").GetComponent<GameTime>();
        }
        protected void UpdateFoodStorage()
        {
            FoodStorageCapacity = 0;
            foreach (CityConponent cityConponent in CityConponents)
            {
                if (cityConponent is FoodStorage foodStorage)
                {
                    FoodStorageCapacity += foodStorage.Capacity;
                }
            }
        }

        public void AddFood(float quantity)
        {
            if (quantity > FoodStorageCapacity - FoodQuantity)
            {
                quantity = FoodStorageCapacity - FoodQuantity;
                //TODO: notify player.
            }
            FoodQuantity += quantity;
            foreach (CityConponent cityConponent in CityConponents)
            {
                if (cityConponent is FoodStorage foodStorage)
                {
                    float storing = Mathf.Min(foodStorage.Capacity - foodStorage.StoredQuantity, quantity);
                    foodStorage.StoredQuantity += storing;
                    if (storing == quantity) break;
                    quantity -= storing;
                }
            }
        }

        public void ConsumeFood(float quantity)
        {
            if (quantity > FoodQuantity)
            {
                throw new ArgumentOutOfRangeException("Not enough food: " + FoodQuantity.ToString() + " - " + quantity.ToString() + " = Not enough.");
            }
            FoodQuantity -= quantity;
            foreach (CityConponent cityConponent in CityConponents)
            {
                if (cityConponent is FoodStorage foodStorage)
                {
                    float consuming = Mathf.Min(foodStorage.StoredQuantity, quantity);
                    foodStorage.StoredQuantity -= consuming;
                    if (consuming == quantity) break;
                    quantity -= consuming;
                }
            }
        }

        public void Update()
        {
            populationContinuous += Population * AverageChildCount * (UnityEngine.Random.value + 0.5F) * Time.deltaTime / timePanel.DayToSecond / 365 / idealLifeSpan;
            float deathInFrame = Population * (UnityEngine.Random.value + 0.5F) * Time.deltaTime / timePanel.DayToSecond / 365 / AverageLifeSpan;
            populationContinuous -= deathInFrame;
            deathCountContinuous += deathInFrame;
            ConsumeFood(Mathf.Min(FoodQuantity, Population * Time.deltaTime));
            Debug.Log(Population);
            Debug.Log(DeathCount);
        }
    }
    public class BasicSettlement : City
    {
        public BasicSettlement(GameObject gameObject) : base(gameObject, "Basic Settlement")
        {
            CityConponents.Add(new FoodPile(this));
            UpdateFoodStorage();
            AddFood(10000);
        }
    }
    public class CityConponent
    {
        protected City city;
        virtual public string Name { get; protected set; }
        virtual public float Space { get; protected set; }
        public CityConponent(City city)
        {
            this.city = city;
        }
    }
    public class FoodStorage : CityConponent
    {
        virtual public float Capacity { get; protected set; }
        virtual public float StoredQuantity { get; set; }
        public FoodStorage(City city) : base(city)
        {
            StoredQuantity = 0;
        }
    }
    public class FoodPile : FoodStorage
    {
        public FoodPile(City city) : base(city)
        {
            Name = "Food Pile";
            Capacity = 10000;
            Space = 1;
        }
    }
}
