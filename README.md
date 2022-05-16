# geektrustcodingchallenge
Geek trust coding challenge


**Problem statement**

**Context**
There are 2 super fast trains, Train A and Train B. Train A travels from Chennai to New Delhi. Train B travels from Trivandrum to Guwahati.


Passengers can board these trains only at the source station.
The trains have only reserved bogies and each bogie will only have passengers to a specific station.
When the train arrives at a station, the entire bogie with passengers is detached from the train, and the train continues its journey.
The routes with station code and distances of each station from originating station are as follows:

**(STATION (CODE) - DISTANCE ):**

_Train A	Train B_
CHENNAI (CHN) - 0	TRIVANDRUM (TVC) -0
SALEM (SLM) - 350	SHORANUR (SRR) - 300
BANGALORE (BLR) - 550	MANGALORE (MAQ) - 600
KURNOOL (KRN) - 900	MADGAON (MAO) - 1000
HYDERABAD (HYB) - 1200	PUNE (PNE) - 1400
NAGPUR (NGP) - 1600	HYDERABAD (HYB) - 2000
ITARSI (ITJ) - 1900	NAGPUR (NGP) - 2400
BHOPAL (BPL) - 2000	ITARSI (ITJ) - 2700
AGRA (AGA) - 2500	BHOPAL (BPL) - 2800
NEW DELHI (NDL) - 2700	PATNA (PTA) - 3800
NEW JALPAIGURI (NJP) - 4200
GUWAHATI (GHY) - 4700

**The Merger**
During a part of their journey, these trains follow the same route and travel as one train - Train AB.

Trains start from their respective source stations and meet at Hyderabad.
Trains travel as TrainAB from Hyderabad till Bhopal as a single train.
From Bhopal the trains travel again as two independent trains, Train A and Train B.
Train A can have passengers in the route for Train B and vice versa.
  Eg: People can board from Chennai in Train A and travel to Guwahati.

**Merging Rules**
First, both the engines are attached.
The remaining bogies from Hyderabad are attached in the descending order of distances they have to travel further from Hyderabad.
When the merged train reaches a station, the bogie for that station will be the last one and it can be detached quickly.
The Goal
Given the initial bogie order of both trains, your program should print :


The bogie order of arrival of Train A and Train B at Hyderabad
Train AB's departure bogie order from Hyderabad
💡Pro tip: Aim to get the Readability and Correctness (I/O) badges - your profile can go forward with most companies with those 2 badges. The rest can be improved upon later!

**Assumptions**
The passengers board only from the source station.
If there are no passenger bogies to travel from Hyderabad station, then train should stop there. In such a case it should print JOURNEY_ENDED
The distances are in kilometers.
If there are multiple bogies with same station as its destination, then they can be arranged next to each other when the Train AB leaves Hyderabad.
