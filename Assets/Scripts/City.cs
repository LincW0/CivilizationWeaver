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
        protected float baseHealth;
        public float Health { get { return baseHealth * (NotEnoughFood ? 0.002F : 1); } }
        public float AverageLifeSpan { get { return Health * idealLifeSpan; } }
        public float AverageChildCount { get; protected set; }
        protected float populationContinuous;
        public long Population => (long)populationContinuous;
        public string Name { get; set; }
        protected float deathCountContinuous;
        public long DeathCount => (long)deathCountContinuous;
        public List<CityComponent> CityComponents { get; protected set; }
        public float FoodStorageCapacity { get; protected set; }
        public float FoodQuantity { get; protected set; }
        public readonly GameTime TimePanel;
        public readonly float TotalSpace;
        public float SpaceTaken { get; protected set; }
        public bool NotEnoughFood { get; protected set; }
        public City(GameObject gameObject, string name, float totalSpace)
        {
            GameObject = gameObject;
            deathCountContinuous = 0;
            Name = name;
            CityComponents = new List<CityComponent>();
            TimePanel = GameObject.Find("TimePanel").GetComponent<GameTime>();
            TotalSpace = totalSpace;
            NotEnoughFood = false;
        }
        protected void UpdateFoodStorage()
        {
            FoodStorageCapacity = 0;
            foreach (CityComponent cityConponent in CityComponents)
            {
                if (cityConponent is FoodStorage foodStorage)
                {
                    FoodStorageCapacity += foodStorage.Capacity;
                }
            }
        }

        protected void UpdateSpaceTaken()
        {
            SpaceTaken = 0;
            foreach (CityComponent cityConponent in CityComponents)
            {
                SpaceTaken += cityConponent.Space;
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
            foreach (CityComponent cityComponent in CityComponents)
            {
                if (cityComponent is FoodStorage foodStorage)
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
            foreach (CityComponent cityComponent in CityComponents)
            {
                if (cityComponent is FoodStorage foodStorage)
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
            populationContinuous += Population * AverageChildCount * (UnityEngine.Random.value + 0.5F) * Time.deltaTime / TimePanel.DayToSecond / 365 / idealLifeSpan;
            float deathInFrame = Population * (UnityEngine.Random.value + 0.5F) * Time.deltaTime / TimePanel.DayToSecond / 365 / AverageLifeSpan;
            populationContinuous -= deathInFrame;
            deathCountContinuous += deathInFrame;
            if (FoodQuantity <= Population * Time.deltaTime / TimePanel.DayToSecond)
            {
                NotEnoughFood = true;
            }
            else
            {
                NotEnoughFood = false;
            }
            ConsumeFood(Mathf.Min(FoodQuantity, Population * Time.deltaTime / TimePanel.DayToSecond));
            foreach (CityComponent cityComponent in CityComponents)
            {
                cityComponent.Update();
            }
            //Debug.Log(Population);
            //Debug.Log(DeathCount);
        }
    }
    public class BasicSettlement : City
    {
        public BasicSettlement(GameObject gameObject) : base(gameObject, "Basic Settlement", 3)
        {
            AverageChildCount = 10;
            populationContinuous = 100;
            baseHealth = 0.25F;
            CityComponents.Add(new FoodPile(this));
            UpdateFoodStorage();
            AddFood(10000);
            CityComponents.Add(new GatheringArea(this));
            UpdateSpaceTaken();
        }
    }
    public class CityComponent
    {
        protected City city;
        virtual public string Name { get; protected set; }
        virtual public float Space { get; protected set; }
        virtual public void Update()
        {

        }
        public CityComponent(City city)
        {
            this.city = city;
        }
    }
    public class FoodStorage : CityComponent
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
    public class FoodProduction : CityComponent
    {
        public readonly float BaseProduction;
        virtual public float Production { get { return BaseProduction; } }
        public FoodProduction(City city, float baseProduction) : base(city)
        {
            BaseProduction = baseProduction;
        }
        public override void Update()
        {
            city.AddFood(Production * Time.deltaTime / city.TimePanel.DayToSecond);
        }
    }
    public class GatheringArea : FoodProduction
    {
        public GatheringArea(City city) : base(city, 50)
        {
            Name = "Gathering Area";
            Space = 1;
        }
    }
}
