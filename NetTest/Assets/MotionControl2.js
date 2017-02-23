#pragma strict

private var sizeFilter: int = 15;
private var filter: Vector3[];
private var filterSum = Vector3.zero;
private var posFilter: int = 0;
private var qSamples: int = 0;
     
function Start() {
    Screen.orientation = ScreenOrientation.LandscapeLeft;
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
    transform.up = -MovAverage(Input.acceleration.normalized);
    transform.rotation.z = -transform.rotation.z;
}