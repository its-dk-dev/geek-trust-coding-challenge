using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace GeekTrust
{
    public class Program
    {
        //Set global variables
        public static Dictionary<string, int> DictionaryOfTrainABogiesWithDistance = new Dictionary<string, int>();
        public static Dictionary<string, int> DictionaryOfTrainBBogiesWithDistance = new Dictionary<string, int>();
        public static string locationWhereBothTrainsMeet = "HYB", ENGINE = "ENGINE", JOURNEY_ENDED = "JOURNEY_ENDED";
         
        //Initiallize all required data with constructor
        public Program()
        {
            //Set the routes with station code and distances 
            //Train A routes
            DictionaryOfTrainABogiesWithDistance.Add("CHN",0);
            DictionaryOfTrainABogiesWithDistance.Add("SLM",350);
            DictionaryOfTrainABogiesWithDistance.Add("BLR",550);
            DictionaryOfTrainABogiesWithDistance.Add("KRN",900);
            DictionaryOfTrainABogiesWithDistance.Add("HYB",1200);
            DictionaryOfTrainABogiesWithDistance.Add("NGP",1600);
            DictionaryOfTrainABogiesWithDistance.Add("ITJ",1900);
            DictionaryOfTrainABogiesWithDistance.Add("BPL",2000);
            DictionaryOfTrainABogiesWithDistance.Add("AGA",2500);
            DictionaryOfTrainABogiesWithDistance.Add("NDL",2700);

            //Train B routes
            DictionaryOfTrainBBogiesWithDistance.Add("TVC",0);
            DictionaryOfTrainBBogiesWithDistance.Add("SRR",300);
            DictionaryOfTrainBBogiesWithDistance.Add("MAQ",600);
            DictionaryOfTrainBBogiesWithDistance.Add("MAO",1000);
            DictionaryOfTrainBBogiesWithDistance.Add("PNE",1400);
            DictionaryOfTrainBBogiesWithDistance.Add("HYB",2000);
            DictionaryOfTrainBBogiesWithDistance.Add("NGP",2400);
            DictionaryOfTrainBBogiesWithDistance.Add("ITJ",2700);
            DictionaryOfTrainBBogiesWithDistance.Add("BPL",2800);
            DictionaryOfTrainBBogiesWithDistance.Add("PTA",3800);
            DictionaryOfTrainBBogiesWithDistance.Add("NJP",4200);
            DictionaryOfTrainBBogiesWithDistance.Add("GHY",4700);
        }

        public List<Tuple<string, int>> getSingleTrainBogiesOrder (List<string> bogiesOfTrain)
        {
            List<Tuple<string,int>> orderOfBogiesForTrain  = new List<Tuple<string, int>>();
            
            //First element in the input list is always train name and second is always ENGINE, We don't need to order first 2 element
            bogiesOfTrain.RemoveRange(0, 2);
            //Add first ENGINE and then go for order 
            orderOfBogiesForTrain.Insert(0, new Tuple<string,int>(ENGINE, 0));

            foreach(string bogie in bogiesOfTrain)
            {
                //Read a boogie attached station and calculate distance from source
                int bogieDistanceFromSourceInTrainA = 0, bogieDistanceFromSourceInTrainB = 0;
                if(DictionaryOfTrainABogiesWithDistance.Count > 0 && DictionaryOfTrainABogiesWithDistance.ContainsKey(bogie))
                {
                    bogieDistanceFromSourceInTrainA  = DictionaryOfTrainABogiesWithDistance[bogie];
                }

                if(DictionaryOfTrainBBogiesWithDistance.Count > 0 && DictionaryOfTrainBBogiesWithDistance.ContainsKey(bogie))
                {
                    bogieDistanceFromSourceInTrainB  = DictionaryOfTrainBBogiesWithDistance[bogie];
                }

                bool bogieFoundInTrainAAfterMeet = false, bogieFoundInTrainBAfterMeet = false;
                if(bogieDistanceFromSourceInTrainA > 0 &&  DictionaryOfTrainABogiesWithDistance[locationWhereBothTrainsMeet] <= bogieDistanceFromSourceInTrainA)
                {
                    bogieFoundInTrainAAfterMeet = true;
                }

                if(bogieDistanceFromSourceInTrainB > 0 && DictionaryOfTrainBBogiesWithDistance[locationWhereBothTrainsMeet] <= bogieDistanceFromSourceInTrainB)
                {
                    bogieFoundInTrainBAfterMeet = true;
                }

                //List<Tuple<CODE,DISTANCE>> for trains A while arriving at HYB 
                //Boogie Attached Station Found in both trains routes, then take nearest distance
                if(bogieFoundInTrainAAfterMeet && bogieFoundInTrainBAfterMeet)
                {
                    int nearestDistance = bogieDistanceFromSourceInTrainA > bogieDistanceFromSourceInTrainB ? bogieDistanceFromSourceInTrainB : bogieDistanceFromSourceInTrainA;
                    orderOfBogiesForTrain.Add(new Tuple<string, int>(bogie, nearestDistance));
                }
                else if(!bogieFoundInTrainAAfterMeet && !bogieFoundInTrainBAfterMeet) { }
                else
                {
                    if(bogieFoundInTrainAAfterMeet)
                    {
                        orderOfBogiesForTrain.Add(new Tuple<string, int>(bogie, bogieDistanceFromSourceInTrainA));
                    }
                    else
                    {
                        orderOfBogiesForTrain.Add(new Tuple<string, int>(bogie, bogieDistanceFromSourceInTrainB));
                    }
                }
            }
            return orderOfBogiesForTrain;
        }
        
        public List<Tuple<string, int>> mergeBogies(List<Tuple<string,int>> TrainABogiesOrder, List<Tuple<string,int>> TrainBBogiesOrder)
        {
            //The remaining bogies from location where both train meet are attached in the descending order of distances
            TrainABogiesOrder.AddRange(TrainBBogiesOrder);
            TrainABogiesOrder.Sort(
                (t1, t2) => {
                    int res = t1.Item2.CompareTo(t2.Item2);
                    return res == 0 ? res : t2.Item2.CompareTo(t1.Item2);
                }
            );
            
            //Remove bogie where both train meet together and engine of both train because in descending order it get put at the end
            TrainABogiesOrder.RemoveAll(a=>a.Item1 == locationWhereBothTrainsMeet || a.Item1 == ENGINE);

            //First, both the engines are attached
            TrainABogiesOrder.Insert(0, new Tuple<string,int>(ENGINE, 0));
            TrainABogiesOrder.Insert(1, new Tuple<string,int>(ENGINE, 0));

            return TrainABogiesOrder;
        }

        public void displayBogieOrder(List<Tuple<string, int>> TrainABogiesOrder, bool isBothTrainOrder = false)
        {
            int engineCount = isBothTrainOrder ? 2: 1;

            if(TrainABogiesOrder.Count > engineCount)
            { 
                foreach(Tuple<string, int> item in TrainABogiesOrder)
                {
                    Console.Write (item.Item1+ " ");
                }
            }
            else
            {
                Console.Write (JOURNEY_ENDED);
            }
        }
        
        static void Main(string[] args)
        {
            Program classObj = new Program();
            string[] inputData = File.ReadAllLines(args[0]);

            List<string> trainABogies = inputData[0].Split(' ').ToList();
            List<string> trainBBogies = inputData[1].Split(' ').ToList();

            //Create List<Tuple<CODE,DISTANCE>> for both the trains while arriving at location where both trains meet
            //Skip all the stations comes before at location where both trains meet
            List<Tuple<string,int>> TrainABogiesOrder = classObj.getSingleTrainBogiesOrder(trainABogies);
            List<Tuple<string,int>> TrainBBogiesOrder = classObj.getSingleTrainBogiesOrder(trainBBogies);

            //Display bogie order of arrival of Train A at location where both trains meet
            Console.Write ("\nARRIVAL TRAIN_A ");
            classObj.displayBogieOrder(TrainABogiesOrder);

            //Get order of bogies for train AB
            Console.Write ("\nARRIVAL TRAIN_B ");
            classObj.displayBogieOrder(TrainBBogiesOrder);

            //Merge bogies once both train arrived at location where both trains meet
            classObj.mergeBogies(TrainABogiesOrder, TrainBBogiesOrder);

            //Display both train AB's departure bogie order
            Console.Write ("\nDEPARTURE TRAIN_AB ");
            classObj.displayBogieOrder(TrainABogiesOrder, true);
        }
    }
}
