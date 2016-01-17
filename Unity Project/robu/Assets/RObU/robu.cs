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
[RequireComponent(typeof(MeshProcessing))]

public class robu : MonoBehaviour {

    #region public variables
	//public bool ShapeDependent;
    public string modalDataFileName;
	public int numPartials = 20;
    public double frequencyScale = 1;
    public double internalDamping = 1;
    public double externalDamping = 2;
    public double ampScale = 1;
    #endregion

    #region private variables
    private ModalModel modalModel;
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
	private MeshFilter mf;
	private Mesh mesh;
    #endregion

	#region on awake
    void Awake()
    {
		Application.runInBackground = true;

        //setup audio
        samplerate = AudioSettings.outputSampleRate;
        //Debug.Log("sample rate: " + samplerate);

		//find mesh
		mf = gameObject.GetComponent<MeshFilter> ();
		mesh = mf.mesh;

		//find rigidbody
		rb = gameObject.GetComponent<Rigidbody>();
		rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // important thing to write about - what about these modes makes them preferable? 
		rb.interpolation = RigidbodyInterpolation.Extrapolate; // see above

		//find collider reference 
		coll = gameObject.GetComponent<Collider>();

        //instantiate modal model
		//if (ShapeDependent) {
		//	modalModel = new ModalModel (mesh);
		//} else {
			modalDataFileName = "./Assets/RObU/resonanceModels/" + modalDataFileName;
			modalModel = new ModalModel (modalDataFileName);

			//frequency driven by the transform scale
			frequencyScale = 1/
				((transform.localScale.x
				  + transform.localScale.y 
				  + transform.localScale.z)/3);
		//}

		//error handling
		if(numPartials > modalModel.activeFreqs)
        {
            Debug.LogError(gameObject.name+": desired number of modes exceeds available, clamped at "+ modalModel.activeFreqs);
			numPartials = modalModel.activeFreqs;
        }

    }
	#endregion

    #region collision handling
    void OnCollisionEnter(Collision c)
    {
    	if (c.relativeVelocity.magnitude > 10f)
        {
        // cap F at 10 to account for unnatural glitches that may cause it to get super large
        	F = 10f;
        }
        else
        {
			F = c.relativeVelocity.magnitude;
        }
        K = t;  
    }

    void OnCollisionStay()
    {

    	F = F / externalDamping;
    }

    void OnCollisionExit()
    {
        K = t;
    }
    #endregion

	#region synthesis
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
			for (int n = 0; n < numPartials; n++)
            {
                // damped oscillators
                temp += ((float)ampScale * modalModel.a[n]) *
                    (Math.Exp(-modalModel.d[n] * internalDamping * tk)) *
                    (Math.Cos(tpi* tk * modalModel.f[n] * frequencyScale)
                    * F);
            }
            //write to audio buffer
            data[i] = (float)temp;
            if (channels == 2) data[i + 1] = data[i];
            t += 1D/samplerate;
        }
    }
	#endregion
}
