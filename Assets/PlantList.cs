using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

[CreateAssetMenu(fileName = "PlantList", menuName = "Plant System/Plant List")]
public class PlantList : ScriptableObject
{
    public List<Plant> plants = new List<Plant>();

    private void OnValidate()
    {
        ImportPlant();
        foreach (var plant in plants)
        {
            plant.SetUp();
        }

        plants = plants.OrderBy(plant => plant.rating).ThenByDescending(plant => plant.probability).ToList();
    }

    [ContextMenu("Import Plants from CSV")]
    private void ImportPlant()
    {
        string filename = "Assets/plants.csv";

        if (!File.Exists(filename))
        {
            Debug.LogError($"CSV file not found at {filename}");
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        plants.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split(';');

            if (fields.Length != 7)
            {
                Debug.LogWarning($"Invalid line at {i + 1}: Expected 7 fields, got {fields.Length}");
                continue;
            }

            try
            {
                Plant newPlant = new Plant
                {
                    name = fields[0],
                    attack = int.Parse(fields[1]),
                    durability = int.Parse(fields[2]),
                    speed = int.Parse(fields[3]),
                    defense = int.Parse(fields[4]),
                    rarity = (Rarity)System.Enum.Parse(typeof(Rarity), fields[5]),
                    type = (Type)System.Enum.Parse(typeof(Type), fields[6])
                };

                newPlant.SetUp();
                plants.Add(newPlant);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error parsing line {i + 1}: {e.Message}");
            }
        }
    }
}
