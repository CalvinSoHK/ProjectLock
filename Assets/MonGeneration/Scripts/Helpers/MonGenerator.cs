using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Mon.Enums;

namespace Mon.MonGeneration
{
    public class MonGenerator
    {
        MonGeneratorSettingsSO settings;

        //<ID number, to generated mon pairs>
        public Dictionary<int, GeneratedMon> monBase = new Dictionary<int, GeneratedMon>();

        //Consumed key IDs paired with the names in the data files.
        //Useful to have the name in case we have errors in our json files. 
        //We can check to see if we have duplicate keys with different names.
        Dictionary<int, string> consumedKeys = new Dictionary<int, string>();

        /// <summary>
        /// Resets monBase dictionary
        /// </summary>
        public void ClearMonBase()
        {
            monBase.Clear();
            consumedKeys.Clear();
        }

        /// <summary>
        /// Retrieves a mon with given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GeneratedMon GetMonByID(int id)
        {
            GeneratedMon mon = new GeneratedMon();
            monBase.TryGetValue(id, out mon);

            if(mon != null)
            {
                return mon;
            }
            else
            {
                Debug.LogError("Error, invalid ID. No Mon exists with that ID: " + id);
                return null;
            }
        }

        /// <summary>
        /// Checks if the id is a valid mon.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckValidID(int id)
        {
            GeneratedMon mon = new GeneratedMon();
            return monBase.TryGetValue(id, out mon);      
        }

        /// <summary>
        /// Generates all mons in KeysJSON
        /// </summary>
        /// <param name="keyObj"></param>
        public void GenerateMonsByKey(KeysJSON keyObj)
        {
            //Reset mon Base
            ClearMonBase();

            //Start counting IDs generated
            int ID = 0;

            DataReader dataReader = new DataReader();
            BaseMon curMon;
            foreach (string path in keyObj.keys)
            {
                //Load from path
                curMon = dataReader.ParseData(path);
                
                //Generate a Mon with that ID
                GeneratedMon[] monFamily = GenerateMonFamily(curMon);

                //Add each member of the family to the list, assign updated IDs
                for(int i = 0; i < monFamily.Length; i++)
                {
                    ID++;
                    monFamily[i].ID = ID;

                    if (i + 1 < monFamily.Length)
                    {
                        monFamily[i].next_evoID = ID + 1;
                    }

                    if (i - 1 >= 0)
                    {
                        monFamily[i].prev_evoID = ID - 1;
                    }

                    monBase.Add(ID, monFamily[i]);
                }
            }
            //Debug.Log("MonBase generated with : " + ID + " total IDs.");
        }

        /// <summary>
        /// Creates a whole family of mons.
        /// Determines evolutions, stages, stats, typing, etc.
        /// </summary>
        /// <param name="baseMon"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public GeneratedMon[] GenerateMonFamily(BaseMon baseMon)
        {
            //If we don't have the settings yet, grab it.
            if (settings == null)
            {
                settings = SOCache.GetScriptableObject<MonGeneratorSettingsSO>(SO.MonGeneratorSettingsSO);
            }

            //Pick a family to be used based off of this baseMon
            BaseMon[] baseFamilyList = PickFamily(baseMon);

            //Only try to make a family if it isn't an empty list.
            if (baseFamilyList.Length > 0)
            {
                //Grab family typing from json
                MonType familyPrimaryType = baseMon.requiredType;

                //Pick a secondary typing, can be monotyped.
                MonType familySecondaryType = PickSecondaryTyping(familyPrimaryType);

                //Pick growth type of the family
                MonGrowthType familyGrowthType = PickGrowthType();

                //Pick at what stage in the family it gains it's secondary typing.
                int secondTypeGainedStage = Random.Range(1, baseFamilyList.Length);

                //Pick family stats profile
                MonGenFamilyProfileSO familyProfile = settings.PickRandomFamilyProfile(baseFamilyList.Length);

                //Calculate family's max stats
                MonBaseStats maxStats = GenerateMaxStats(familyProfile);

                //Transform that into generated mons
                GeneratedMon[] familyList = new GeneratedMon[baseFamilyList.Length];

                //Translate BaseMon into GeneratedMon, applying anything that is certain at this point.
                for (int i = 0; i < baseFamilyList.Length; i++)
                {
                    //Fixed information
                    familyList[i] = TranslateMon(baseFamilyList[i]);
                    familyList[i].stage = (MonStage)(i + 1);
                    familyList[i].primaryType = familyPrimaryType;
                    familyList[i].growthType = familyGrowthType;

                    //Apply second typing if we are equal to or greater than the stage where we gain second typing.
                    if ((int)familyList[i].stage >= secondTypeGainedStage)
                    {
                        familyList[i].secondaryType = familySecondaryType;
                    }

                    //Pick stats
                    familyList[i].baseStats = CalculateStats(maxStats, familyProfile.GrabProfile(familyList[i].stage));
                }
                return familyList;
            }

            //Return empty list if invalid
            return new GeneratedMon[0];
        }

        /// <summary>
        /// Generates a mon from a base mon.
        /// Translates all the information that is guaranteed to be true every time.
        /// </summary>
        /// <param name="baseMon"></param>
        /// <returns></returns>
        public GeneratedMon TranslateMon(BaseMon baseMon)
        {
            GeneratedMon generatedMon = new GeneratedMon();
           
            //Copy name from BaseMon
            generatedMon.name = baseMon.name;

            //Copy required type from BaseMon
            generatedMon.primaryType = baseMon.requiredType;

            return generatedMon;
        }

        /// <summary>
        /// Pulls the whole family including the baseMon as BaseMon.
        /// </summary>
        /// <param name="baseMon"></param>
        /// <returns></returns>
        private List<BaseMon> ParseFamilyData(BaseMon baseMon)
        {
            //Pull full family
            List<BaseMon> fullFamily = new List<BaseMon>();
            fullFamily.Add(baseMon);

            //Debug.Log("Parsed: " + baseMon.name + " with ID: " + baseMon.key);

            DataReader dataReader = new DataReader();
            foreach (int key in baseMon.family)
            {
                BaseMon mon = dataReader.ParseData(key.ToString());
                fullFamily.Add(mon);
                //Debug.Log("Parsed: " + mon.name + " with ID: " + mon.key);
            }

            fullFamily.Sort();

            return fullFamily;
        }

        /// <summary>
        /// Picks a secondary typing for the mon.
        /// Has chance to be a monotype.
        /// </summary>
        /// <param name="primaryType"></param>
        /// <returns></returns>
        private MonType PickSecondaryTyping(MonType primaryType)
        {
            // Randomly pick if this pokemon will get a secondary type.
            if (Random.Range(0f, 1f) >= settings.monoTypingChance)
            {
                //If it is getting a second type, randomly pick one that isn't the primary type.
                PickRandomEnum<MonType> pickRandomEnum = new PickRandomEnum<MonType>();
                MonType pickedType = MonType.None;

                //Keep picking random type until it is not None and it is not the same as the primary type.
                while (pickedType == MonType.None || pickedType == primaryType)
                {
                    pickedType = pickRandomEnum.PickRandom();
                }

                return pickedType;
            }
            else
            {
                return MonType.None;
            }
        }
    
        /// <summary>
        /// Picks random growth type
        /// </summary>
        /// <returns></returns>
        private MonGrowthType PickGrowthType()
        {
            //If it is getting a second type, randomly pick one that isn't the primary type.
            PickRandomEnum<MonGrowthType> pickRandomEnum = new PickRandomEnum<MonGrowthType>();
            return pickRandomEnum.PickRandom();
        }

        /// <summary>
        /// Generates max stats for this family
        /// Based off of family profile
        /// </summary>
        /// <param name="familyProfile"></param>
        /// <returns></returns>
        private MonBaseStats GenerateMaxStats(MonGenFamilyProfileSO familyProfile)
        {
            //Initialize random generator
            Utility.GaussianRandom random = GaussianRandom.Instance;

            //Generate stat total
            double statTotal = random.RandomGaussian(familyProfile.statTotalAverage, familyProfile.statTotal_stddev);

            //Pick a weight per stat.
            double hp_weight = random.RandomGaussian(familyProfile.hpAverage, familyProfile.hpWeight_stddev);
            double def_weight = random.RandomGaussian(familyProfile.defAverage, familyProfile.defWeight_stddev);
            double sp_def_weight = random.RandomGaussian(familyProfile.spDefAverage, familyProfile.spDefWeight_stddev);
            double atk_weight = random.RandomGaussian(familyProfile.atkAverage, familyProfile.atkWeight_stddev);
            double sp_atk_weight = random.RandomGaussian(familyProfile.spAtkAverage, familyProfile.spAtkWeight_stddev);
            double speed_weight = random.RandomGaussian(familyProfile.speedAverage, familyProfile.speedWeight_stddev);

            //Sum up the weights, and distribute the total accordingly
            double sum = hp_weight + def_weight + sp_def_weight + atk_weight + sp_atk_weight + speed_weight;
            double part = statTotal / sum;

            //Set each stat by multiplying part with the weight, and rounding it.
            MonBaseStats stats = new MonBaseStats(
                Mathf.Clamp(Mathf.RoundToInt((float)(part * hp_weight)), 0, int.MaxValue),
                Mathf.Clamp(Mathf.RoundToInt((float)(part * def_weight)), 0, int.MaxValue),
                Mathf.Clamp(Mathf.RoundToInt((float)(part * sp_def_weight)), 0, int.MaxValue),
                Mathf.Clamp(Mathf.RoundToInt((float)(part * atk_weight)), 0, int.MaxValue),
                Mathf.Clamp(Mathf.RoundToInt((float)(part * sp_atk_weight)), 0, int.MaxValue),
                Mathf.Clamp(Mathf.RoundToInt((float)(part * speed_weight)), 0, int.MaxValue));

            return stats;
        }

        /// <summary>
        /// Calculates stats by applying profiles to the family's max stats
        /// </summary>
        /// <returns></returns>
        private MonBaseStats CalculateStats(MonBaseStats maxStats, MonGenProfileSO profile)
        {
            //Set each stat by multiplying part with the weight, and rounding it.
            MonBaseStats stats = new MonBaseStats(
                Mathf.Clamp(Mathf.RoundToInt((float)(maxStats.GetStat(MonStatType.HP) * profile.GetRandomStatPotential(MonStatType.HP))), 0, int.MaxValue),
                Mathf.Clamp(Mathf.RoundToInt((float)(maxStats.GetStat(MonStatType.DEF) * profile.GetRandomStatPotential(MonStatType.DEF))), 0, int.MaxValue),
                Mathf.Clamp(Mathf.RoundToInt((float)(maxStats.GetStat(MonStatType.SPDEF) * profile.GetRandomStatPotential(MonStatType.SPDEF))), 0, int.MaxValue),
                Mathf.Clamp(Mathf.RoundToInt((float)(maxStats.GetStat(MonStatType.ATK) * profile.GetRandomStatPotential(MonStatType.ATK))), 0, int.MaxValue),
                Mathf.Clamp(Mathf.RoundToInt((float)(maxStats.GetStat(MonStatType.SPATK) * profile.GetRandomStatPotential(MonStatType.SPATK))), 0, int.MaxValue),
                Mathf.Clamp(Mathf.RoundToInt((float)(maxStats.GetStat(MonStatType.SPEED) * profile.GetRandomStatPotential(MonStatType.SPEED))), 0, int.MaxValue));

            return stats;
        }
    
        /// <summary>
        /// Picks baseMon in a family to make the family that will be in this run of the game
        /// Returns an empty list if this family has already been generated.
        /// </summary>
        private BaseMon[] PickFamily(BaseMon baseMon)
        {
            //Pull full family
            List<BaseMon> fullFamily = ParseFamilyData(baseMon);

            //Add each member of the family to the consumeKeys list
            foreach (BaseMon mon in fullFamily)
            {
                //Try and grab this key from consumedKeys. If it is consumed we can skip.
                string usedName = "";
                if(consumedKeys.TryGetValue(mon.key, out usedName))
                {
                    return new BaseMon[0];
                }
                consumedKeys.Add(mon.key, mon.name);
                //Debug.Log("Consumed key: " + mon.key + " with name: " + mon.name);
            }

            //Determine if this family will be a 1, 2, or 3 stage family.
            int familySize = Random.Range(1, 4);

            //Create generatedMon list based on familySize
            BaseMon[] familyList = new BaseMon[familySize];

            switch (familySize)
            {
                //In the case of just 1, pick from any of the three.
                case 1: 
                    familyList[0] = fullFamily[Random.Range(0, 3)];
                    break;
                // In the case of 2, we have to pick if it will start from 1 or 2. 
                // 1 can go to 2 or 3. 2 will only go to 3. So three options.
                case 2: 
                    int option = Random.Range(1, 3);
                    switch (option)
                    {
                        case 1:
                            familyList[0] = fullFamily[0]; 
                            familyList[1] = fullFamily[1];
                            break;
                        case 2:
                            familyList[0] = fullFamily[0];
                            familyList[1] = fullFamily[2];
                            break;
                        case 3:
                            familyList[0] = fullFamily[1];
                            familyList[1] = fullFamily[2];
                            break;
                    }
                    break;
                //In the case of 3, just use all three in order.
                case 3:
                    familyList[0] = fullFamily[0];
                    familyList[1] = fullFamily[1];
                    familyList[2] = fullFamily[2];
                    break;
                default:
                    Debug.LogError("Invalid number of family members attempted: " + familySize);
                    break;
            }

            return familyList;
        }
    }
}