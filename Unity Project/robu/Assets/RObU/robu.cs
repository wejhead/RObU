using UnityEngine;
using System;

/// <summary>
/// resyntheizes sound of rigidbodies based of the modal data (.ro) file given
/// to do:
/// shape dependency
/// contact location dependency
/// </summary>

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class robu : MonoBehaviour {

    #region public 
    public string modalDataFileName;
    public int numModes = 20;
    public double amp = 0.1;
    public double frequencyScale = 1;
    public double dampingScale = 1;
    public double externalDamping = 2;
    public double ampScale = 1;
    #endregion

    #region private
    private ModalModel modalModel;
    private int bufSize;
    private float[] buffer;
    private int samplerate;
    private double t = 0;
    private double phase;
    private double F;
    private double K = 0;
    private double tpi = 2 * Math.PI;
    private Rigidbody rb;
    private float distanceFromBounds;
    private Collider coll;
    #endregion

    void Awake()
    {
        //setup audio
        samplerate = AudioSettings.outputSampleRate;
        //Debug.Log("sample rate: " + samplerate);

        //instantiate modal model
        modalDataFileName = "./Assets/Scripts/resonanceModels/"+modalDataFileName;
        modalModel = new ModalModel(modalDataFileName);
        if(numModes > modalModel.activeFreqs)
        {
            Debug.LogError(gameObject.name+": desired number of modes exceeds available, clamped at "+ modalModel.activeFreqs);
            numModes = modalModel.activeFreqs;
        }

        //frequency driven by the transform scale
        frequencyScale = 1/
            ((transform.localScale.x
            + transform.localScale.y 
            + transform.localScale.z)/3);

        //find rigidbody
        rb = gameObject.GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Extrapolate;

        coll = gameObject.GetComponent<Collider>();
        Application.runInBackground = true;
    }

    #region collision_handling
    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.layer != 8)
        {
            if (c.impulse.magnitude > 10f)
            {
                // cap F at 10 to account for unnatural glitches that may cause it to get super large
                F = 10f;
            }
            else
            {
                F = c.impulse.magnitude;
            }
            K = t;
        }  
    }

    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.layer != 8)
        {
            F = F / externalDamping;
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.layer != 8)
        {
            K = t;
        }
    }
    #endregion

    void OnAudioFilterRead(float[] data, int channels)
    {
        //write to the audio buffer
        for (int i = 0; i < data.Length; i += channels)
        {
            //calculate sample
            double temp = 0;
            //pre bake some variables
            double tk = t - K;

            //need to update to account for impact location
            for (int k = 0; k < numModes; k++)
            {
                // damped oscillators
                temp += (ampScale * modalModel.a[k]) *
                    (Math.Exp(-modalModel.d[k] * dampingScale * tk)) *
                    (Math.Cos(tpi* tk * modalModel.f[k] * frequencyScale)
                    * F);
            }
            //write to audio buffer
            data[i] = (float)temp * (float)amp;
            if (channels == 2) data[i + 1] = data[i];
            t += 1D/samplerate;
        }
    }
}
