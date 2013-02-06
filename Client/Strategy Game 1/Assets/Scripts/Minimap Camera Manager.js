#pragma strict

function Start () {
	this.transform.position.x = 1000;
	this.transform.position.y = 1000;
	this.transform.position.z = 1000;
	this.transform.eulerAngles.x = 90;	
	this.camera.orthographic = true;
	this.camera.orthographicSize = 1000;
	this.camera.farClipPlane = 1100;
	this.camera.rect = Rect (-0.8, -0.65, 1, 1);
	this.camera.depth = 1;
}

function Update () {

}