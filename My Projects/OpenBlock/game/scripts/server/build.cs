//
// Server commands
//

function serverCmdMoveSelection(%client, %distance) { }
function serverCmdScaleSelection(%client, %scale) { }
function serverCmdRotateSelection(%client, %rotation) { }
function serverCmdDeleteSelection(%client) { }
function serverCmdPlantSelection(%client) { }

//
// Client commands
//

function GameConnection::spawnSelection(%this, %location) {
	%player = %this.player;
	
	if(%this.selection) { // Player already has a selection.
		// unselect current selection
	} 
	
	// create new selection
	// set selection
}