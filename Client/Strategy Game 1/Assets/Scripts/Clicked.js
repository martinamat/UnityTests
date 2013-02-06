#pragma strict

var texto : GUIText;
var nombre : String = "Edificio Bonito 3";

function Start () {

}

function Update () {

}

function OnMouseDown () {
    texto.text = "Selected: "+ this.nombre;
}