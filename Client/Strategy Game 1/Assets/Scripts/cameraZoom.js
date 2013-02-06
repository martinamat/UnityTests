#pragma strict

function Start () {

}

function Update () {

	if(Input.GetButtonDown("Zoomin"))
    {
    	if ( transform.camera.orthographicSize > 100)
    		transform.camera.orthographicSize -= 100;    		
    }
    
    if(Input.GetButtonDown("Zoomout"))
    {
    	if ( transform.camera.orthographicSize < 400)
    		transform.camera.orthographicSize += 100;    		
    }
    
    if(Input.GetButton("AngleUp"))
    {
    	if( transform.eulerAngles.x < 60 )
    		transform.eulerAngles.x += 10 * Time.deltaTime;    		
    }
    
    if(Input.GetButton("AngleDown"))
    {
    	if( transform.eulerAngles.x > 10 )
    		transform.eulerAngles.x -= 10 * Time.deltaTime;    		
    }
    
    if(Input.GetButton("ResetCam"))
    {
    	transform.camera.orthographicSize = 300;
    	transform.eulerAngles.x = 30;    		
    }

}