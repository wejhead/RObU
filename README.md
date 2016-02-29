# RObU
Physical modelling of Rigid-body sounds in Unity. 

Implementation of real-time modal synthesis algorithms as 
proposed by (http://www.cs.ubc.ca/~kvdoel/publications/thesis.pdf, among others)
in Unity. 

## Preprocessing

At the moment, some preprocessing is required to obtain optimal results. See Angelus (https://github.com/johncburnett/Angelus) for extracting resynthesis files from audio files. 

If you are an audiosculpt user, I also wrote a tool to parse audiosculpt partial tracking to a .ro file. 

Things I could use help on finishing:

1. Shape Dependency (natural frequency analysis, ideally this would happen on Awake)
2. Contact Location Dependency

## Usage

Simply add robu.cs to any gameobject in your scene and assign it a .ro file (see Models directory).
