#pragma strict

var speed : float =400;
var topCam : Transform;
var defaultTopCamPosition: float = 800.0;

//Se puede controlar el zoom manejando el Size de la camara
//La rotacion completa voy a ver despues
//Efectos para ver desde mas arriba o mas pegado al suelo con la rotacion en x


function Start () {

	//Inicializo el objeto al que mira la camara en el 0,0,0
	this.transform.position.x = 0;	
	this.transform.position.y = 0;
	this.transform.position.z = 0;
	
	//Muevo la camara a esa posicion pero mas arriba
	topCam.transform.position.x = 0;	
	topCam.transform.position.y = defaultTopCamPosition;	
	topCam.transform.position.z = 0;	
	topCam.camera.orthographic = true;

}

function Update() // The name matter. update() won't work.
{
    // By using GetAxis, we use the InputManager. "Horizontal" is q/d or left/right and can be modified.
    //float h = Input.GetAxis( "Horizontal" ); // That's C#, silly me
    var h : float = Input.GetAxis( "Horizontal" );

    // Let's check if the input is pressed. Left is -1, right +1, nothing is 0
    if( Mathf.Abs( h ) > 0.0 )
    {
        // Now, we can move the cube by accessing the Transform component, which contains the matrix
        // To make the movement independant from the frame rate, we use Time.deltaTime.
        transform.position.x += h * Time.deltaTime * speed/2;
        transform.position.z -= h * Time.deltaTime * speed/2;        
    }
    
    var v : float = Input.GetAxis( "Vertical" );
    
    if( Mathf.Abs( v ) > 0.0 )
    {
        // Now, we can move the cube by accessing the Transform component, which contains the matrix
        // To make the movement independant from the frame rate, we use Time.deltaTime.
        transform.position.x += v * Time.deltaTime * speed;
        transform.position.z += v * Time.deltaTime * speed;
    }
 
    
    
}