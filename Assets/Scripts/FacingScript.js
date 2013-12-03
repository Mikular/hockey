#pragma strict

var left : System.Boolean;

var puck : Transform;

function Update () 
{
	if ((puck.position.x > transform.position.x && left)
		|| (puck.position.x < transform.position.x && !left))
	{
		transform.localScale.x *= -1;
		left = !left;
	}
}