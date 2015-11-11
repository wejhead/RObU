# RObU
Resonant Objects in Unity 

Implementation of real-time modal synthesis algorithms as 
proposed by (http://www.cs.ubc.ca/~kvdoel/publications/thesis.pdf, among others)
in Unity. 

## Preprocessing

At the moment, some preprocessing is required to obtain optimal results. See Angelus (https://github.com/johncburnett/Angelus) for extracting resynthesis files from audio files. 

If you are an audiosculpt user, I also wrote a tool to parse audiosculpt partial tracking to a .ro file. 

Shape dependency and contanct-location dependency are still in the works, if you can help me out please let me know. You can see the work I have done so far in Preprocessing Tools/PythonMeshAnalysis. 

## Usage

Simply add robu.cs to any gameobject in your scene and assign it a .ro file (see Models directory) to resynthesize from. 
