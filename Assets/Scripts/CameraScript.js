#pragma strict

var target : Transform;
var smooth : float = 5;

function Update () {
    transform.position.x = Mathf.Lerp(transform.position.x,target.position.x,Time.deltaTime*smooth);
}