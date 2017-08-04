//
// "Namespace" & Server inits
//

if(!isDefined(BuildSystem)) {
	new ScriptObject(BuildSystem);
}

if(!isDefined(PlantedBricks)){
	PlantedBricks.delete(); // todo: check if this works
}

function onServerDestroyed() {
	Parent::onServerDestroyed();
	
	if(isDefined(PlantedBricks)) {
		PlantedBricks.delete(); // todo: check if this works
	}
}

// 
// Global commands
//

// TODO: maybe move these two commands to somewhere that makes more sense? maybe to brick.cs...

$brickXY = 0.5;
$brickZ  = 0.2;

// Block Units > World Units
function convertBUtoWU(%BUVector) {
	return getWord(%BUVector, 0) * $brickXY SPC
	       getWord(%BUVector, 1) * $brickXY SPC
		   getWord(%BUVector, 2) * $brickZ;
}

// World Units > Block Units
function convertWUtoBU(%WUVector) {
	return getWord(%WUVector, 0) / $brickXY SPC
	       getWord(%WUVector, 1) / $brickXY SPC
		   getWord(%WUVector, 2) / $brickZ;
}



//
// Server commands
//

function serverCmdTransformSelection(%client, %pos, %scale, %rotation, %units) { }

function serverCmdMoveSelection(%client, %distance, %units) { 
	if(isObject(%client.ghostBrick)) { // TODO - check if client can move brick
		if(%units !$= "WU") { // TODO - detag/tag
			%distance = convertBUtoWU(%distance);
		}
		
		%ghostBrick = %client.ghostBrick;
		
		BuildSystem.moveGhostBrick(%ghostBrick, VectorAdd(%ghostBrick.position, %distance));
	}
}

function serverCmdScaleSelection(%client, %scale) { 
	
}

function serverCmdRotateSelection(%client, %rotation) {
	
}

function serverCmdDeleteSelection(%client) { }

function serverCmdPlantSelection(%client) { 
	if(isObject(%client.ghostBrick)) { // TODO - also check if client can plant
		BuildSystem.plantGhostBrick(%client.ghostBrick);
	}
}

function serverCmdDeselectSelection(%client) { }

//
// Client functions
//


//
// Ghost bricks
function BuildSystem::getGhostBrickOwner(%this, %ghostBrick) {
	
}

function BuildSystem::deleteGhostBrick(%this, %ghostBrick) {
	
}

function BuildSystem::createGhostBrick(%this, %position, %brickDef, %owner) {
	if(isObject(%owner.ghostBrick)) {
		%owner.ghostBrick.delete();
	}
		
	%owner.ghostBrick = new TSStatic() {
		shapeName  = %brickDef.ghostModel;
		scale      = %brickDef.brickScale;
		position   = %position;
		owner      = %owner;
		newGhost   = true; // If true, the brick was created for the purpose of block placement and should be deleted when deselected.
		definition = %brickDef;
	};
}

function BuildSystem::createGhostBrickFromExisting(%this, %location, %brick) {
	
}

function BuildSystem::transformGhostBrick(%this, %ghostBrick, %rotation) {
	
}

function BuildSystem::rotateGhostBrick(%this, %ghostBrick, %rotation) {
	
}

function BuildSystem::scaleGhostBrick(%this, %ghostBrick, %scale) {
	
}

function BuildSystem::moveGhostBrick(%this, %ghostBrick, %position) {
	// make brick move sound
	
	%ghostBrick.position = %position;
}

function BuildSystem::plantGhostBrick(%this, %ghostBrick) {
	// make brick plant sound
	
	new TSStatic() {
		shapeName = %ghostBrick.shapeName;
		scale     = %ghostBrick.scale;
		position  = %ghostBrick.position;
		owner     = %ghostBrick.owner;
		
		collisionType = %ghostBrick.definition.collisionType;
	};
}

function BuildSystem::canGhostSpawn(%this, %col, %pos) {
	return true;
}

function BuildSystem::canGhostPlant(%this, %ghostBrick) {
	return true;
}

function BuildSystem::clampPosToGrid(%this, %position) {
	return %this.clampPointToGrid(getWord(%position, 0), $brickXY) SPC
	       %this.clampPointToGrid(getWord(%position, 1), $brickXY) SPC
		   %this.clampPointToGrid(getWord(%position, 2), $brickZ);
}

function BuildSystem::clampPointToGrid(%this, %pos, %scale) {
	%lowerPos = %pos - mFMod(%pos, %scale); // The lowest position the brick can be snapped to.
	%upperPos = %lowerPos + %scale;     // The highest position the brick can be snapped to.
	
	if(%pos < 0) { // Subtraction of remainder with negative values are always bigger than they should be. This is a fix.	
		%lowerPos -= %scale;
		%upperPos -= %scale;
	}
	
	%lowerDif = mAbs(%pos - %lowerPos); // The distance between the current position and the lowest snap position.
	%upperDif = mAbs(%pos - %upperPos); // The distance between the current position and the highest snap position.
	
	%newPos = 0;
	
	if(%lowerDif < %upperDif) { // Snap brick to lowest position.
		%newPos = %lowerPos;
	} else { // Snap brick to highest position.
		%newPos = %upperPos;
	}
	
	return %newPos;
}

//
// Brickplacer Projectile 
//


function BrickPlacerProjectile::OnCollision(%this, %projectile, %col, %fade, %pos, %normal) {	
	%clampedPos = BuildSystem.clampPosToGrid(%pos);
	
	%client = %projectile.sourceObject.client;
	
	//%brick  = %client.selectedBrick; 
	%brick  = Brick1x1; 
	
	if(BuildSystem.canGhostSpawn(%col, %clampedPos)) {
		// todo: code that makes it spawn 0.5x0.5x0.333
		BuildSystem.createGhostBrick(%clampedPos, %brick, %client);
	}	
}

//package brickPlacerOverrides() {
//	
//};

//activatePackage(brickPlacerOverrides);