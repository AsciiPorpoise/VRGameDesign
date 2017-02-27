#pragma strict

/*
 * This maps a gameObject's rotation to a mobile device's sensor input using a basic integral filter.
 * TODO: Translate to C#
 */

public class MotionControl2 extends UnityEngine.Networking.NetworkBehaviour {
    private var sizeFilter: int = 15;
    private var filter: Vector3[];
    private var filterSum = Vector3.zero;
    private var posFilter: int = 0;
    private var qSamples: int = 0;

    function Start() {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Input.gyro.enabled = true;
    }
     
    function MovAverage(sample: Vector3): Vector3 {
        if (qSamples==0) filter = new Vector3[sizeFilter];
        filterSum += sample - filter[posFilter];
        filter[posFilter++] = sample;
        if (posFilter > qSamples) qSamples = posFilter;
        posFilter = posFilter % sizeFilter;
        return filterSum / qSamples;
    }
     
    function Update () {
        if(!isLocalPlayer) {
            return;
        }
        transform.rotation = Input.gyro.attitude;
        transform.up = -MovAverage(Input.acceleration.normalized);
        transform.rotation.z = -transform.rotation.z;
    }


}