using UnityEngine;
using System;
using System.IO;

public class ModalModel
{
    public int activeFreqs;
    private int nPoints;
    public float freq_scale;
    public float damp_scale;
    public float amp_scale;
    public double[] f;
    public double[] d;
    public double[] a;

    public ModalModel(String file)
    {
        try
        {
            //read the file
            using (StreamReader sr = new StreamReader(file))
            {
                String line = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    //Debug.Log(line);
                    //conditionals
                    if (line == "nactive_freq:")
                    {
                        line = sr.ReadLine();
                        int j;
                        if (Int32.TryParse(line, out j))
                        {
                            activeFreqs = j;
                            f = new double[activeFreqs];
                            d = new double[activeFreqs];
                            a = new double[activeFreqs];
                        }
                        else
                        {
                            Debug.LogError("ParsingError-->nactive_freq");
                        }
                    }
                    else if (line == "frequency_scale:")
                    {
                        line = sr.ReadLine();
                        float j;
                        if (float.TryParse(line, out j))
                        {
                            freq_scale = j;
                        }
                        else
                        {
                            Debug.LogError("ParsingError-->freq_scale");
                        }
                    }
                    else if (line == "damping_scale:")
                    {
                        line = sr.ReadLine();
                        float j;
                        if (float.TryParse(line, out j))
                        {
                            damp_scale = j;
                        }
                        else
                        {
                            Debug.LogError("ParsingError-->damp_scale");
                        }
                    }
                    else if (line == "amplitude_scale:")
                    {
                        line = sr.ReadLine();
                        float j;
                        if (float.TryParse(line, out j))
                        {
                            amp_scale = j;
                        }
                        else
                        {
                            Debug.LogError("ParsingError-->amp_scale");
                        }
                    }
                    else if (line == "frequencies:")
                    {
                        for (int i = 0; i < activeFreqs; i++)
                        {
                            line = sr.ReadLine();
                            double j;
                            if (double.TryParse(line, out j))
                            {
                                f[i] = j;
                            }
                            else
                            {
                                Debug.LogError("ParsingError-->frequencies");
                            }
                        }

                    }
                    else if (line == "dampings:")
                    {
                        for (int i = 0; i < activeFreqs; i++)
                        {
                            line = sr.ReadLine();
                            double j;
                            if (double.TryParse(line, out j))
                            {
                                d[i] = j;
                            }
                            else
                            {
                                Debug.LogError("ParsingError-->dampings");
                            }
                        }
                    }
                    else if (line == "amplitudes[point][freq]:")
                    {
                        for (int i = 0; i < activeFreqs; i++)
                        {
                            line = sr.ReadLine();
                            double j;
                            if (double.TryParse(line, out j))
                            {
                                a[i] = j;
                            }
                            else
                            {
                                Debug.LogError("ParsingError-->dampings");
                            }
                        }
                    }
                    else line = sr.ReadLine();

                }
                
                
            }
            if (f.Length != d.Length && d.Length != a.Length) Debug.LogError("inconsistent list length for f, d, and a");
        }
        catch (Exception e)
        {
            Debug.LogError("The file could not be read");
            Debug.LogError(e);
        }
        
        
    }
}
