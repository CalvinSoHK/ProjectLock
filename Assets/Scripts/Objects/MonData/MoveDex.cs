using Core.AddressableSystem;
using Mon.MonGeneration;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utility;

namespace Mon.Moves
{
    /// <summary>
    /// Contains all move data
    /// </summary>
    public class MoveDex
    {
        /// <summary>
        /// Dictionary of move data.
        /// Key = Tag E.g. "FireType"
        /// Value = List of MoveData, all moves that are related to that key
        /// </summary>
        Dictionary<string, List<MoveData>> moveDict = new Dictionary<string, List<MoveData>>();

        private bool isReady = false;

        /// <summary>
        /// When true, the moveDict is loaded
        /// </summary>
        public bool IsReady { get { return isReady; } }

        /// <summary>
        /// Clears the dict of all lists
        /// </summary>
        /// <returns></returns>
        private async Task ClearDict()
        {
            moveDict.Clear();
        }

        /// <summary>
        /// Populates dex with all moves
        /// </summary>
        /// <returns></returns>
        public async Task LoadDex()
        {
            //Clear dict first
            await ClearDict();

            //Grab AddressablesManager to load all move data
            AddressablesManager addressableManager = Core.CoreManager.Instance.addressablesManager;
            IList<MoveData> moveIList = await addressableManager.LoadAddressablesByTag<MoveData>("MoveData");

            //Iterate through all loaded moves
            foreach(MoveData move in moveIList)
            {
                try
                {
                    await AddMoveToTags(move);
                }
                catch (MoveDexException e)
                {
                    Debug.LogError(e.Message);
                }
            }

            //Set as ready
            isReady = true;
        }

        /// <summary>
        /// Adds a given move to a given (singular) tag in the dictionary
        /// If it already is in the dictionary it will not be added
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private async Task AddMoveToTag(string tag, MoveData move)
        {
            if (moveDict.ContainsKey(tag))
            {
                List<MoveData> result = new List<MoveData>();
                if (moveDict.TryGetValue(tag, out result))
                {
                    if (!result.Contains(move))
                    {
                        result.Add(move);
#if DEBUG_ENABLED && MON_GEN
                        Debug.Log("Successfully added move: " + move.name + " to tag: " + tag);
#endif
                    }
                }
                else
                {
                    throw new MoveDexException("MoveDex Error: Unable to grab list of tag: " + tag);
                }
            }           
            else //Otherwise thsi tag hasn't been added before, add a new key and list
            {
                List<MoveData> moveList = new List<MoveData>();
                moveList.Add(move);
                moveDict.Add(tag, moveList);
#if DEBUG_ENABLED && MON_GEN
                Debug.Log("Successfully added move: " + move.name + " to tag: " + tag);
#endif
            }
        }

        /// <summary>
        /// Adds a given move to all relevant tags
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private async Task AddMoveToTags(MoveData move)
        {
            foreach(string tag in move.strictTags)
            {
                try
                {
                    await AddMoveToTag(tag, move);
                }
                catch(MoveDexException e)
                {
                    Debug.LogError(e.Message);
                }
                
            }

            foreach (string tag in move.optionalTags)
            {
                try
                {
                    await AddMoveToTag(tag, move);
                }
                catch (MoveDexException e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }

        /// <summary>
        /// Generates list of learn moves for a given generated mon using it's tags
        /// Gives it a number of moves by inputted int
        /// </summary>
        /// <param name="generatedMon"></param>
        /// <returns></returns>
        public async Task<List<LearnMoveData>> GenerateLearnMoves(GeneratedMon generatedMon, int numberOfMoves)
        {
            List<LearnMoveData> learnList = new List<LearnMoveData>();

            //Deck to shuffle and pull from
            Deck<MoveData> moveDeck = new Deck<MoveData>();
            
            //Adds all related moves to move deck
            foreach(string tag in generatedMon.assignedTags)
            {
                List<MoveData> relatedMoves = new List<MoveData>();
                if(moveDict.TryGetValue(tag, out relatedMoves))
                {
                    foreach (MoveData moveData in relatedMoves)
                    {
                        moveDeck.AddCard(moveData);
                    }
                }               
            }

            if(moveDeck.Count <= 0)
            {
                throw new MoveDexException("MoveDex Error: Generated move deck was empty for: " + generatedMon.name);
            }

            //Pull number of moves out of moveDeck and add to generatedMon's learn list.
            //Additionally pick a random level for it to learn those moves
            for(int i = 0; i < numberOfMoves; i++)
            {
                //Only attempt to do draws if the deck has cards
                if(moveDeck.Count != 0)
                {
                    moveDeck.ShuffleDeck();
                    MoveData data = moveDeck.DestructiveDraw();
                    moveDeck.RemoveAllOfCard(data);

                    //Check if the drawn move is a valid move to add to this mon (checking strict tags)
                    bool validMove = true;
                    foreach (string strictTag in data.strictTags)
                    {
                        if (!generatedMon.assignedTags.Contains(strictTag))
                        {
                            validMove = false;
                        }
                    }

                    if (validMove)
                    {
                        int level = Random.Range(data.minLevelRange, data.maxLevelRange);

                        LearnMoveData learnMove = new LearnMoveData(data, level);
                        learnList.Add(learnMove);
                    }
                    else
                    {
                        //Decrement i to keep drawing.
                        i--;
                    }
                }                 
            }

            if(learnList.Count == 0)
            {
                string errorMsg = "Error: Generated mon had no moves in learn list: " + generatedMon.name;
                errorMsg += "\n Full list of tags on mon is: ";
                foreach(string tag in generatedMon.assignedTags)
                {
                    errorMsg += "\n " + tag;
                }
                Debug.LogError(errorMsg);
            }

            learnList.Sort();

            //Set learnList first index to 0
            learnList[0] = new LearnMoveData(learnList[0].move, 1);

            return learnList;
        }
    }

    /// <summary>
    /// Exception for MoveDex
    /// </summary>
    public class MoveDexException : System.Exception
    {
        public MoveDexException(string msg) : base(msg)
        {

        }
    }
}


