exec("./brick.cs");

if(isDefined(BrickDefinitions)){
	BrickDefinitions.delete(); // todo: check if this works
}
	
// Register brick definitions

new SimGroup(BrickDefinitions);

exec("./brick1x1.cs");
BrickDefinitions.add(Brick1x1);


function onServerDestroyed() {
	Parent::onServerDestroyed();
	
	if(isDefined(BrickDefinitions)) {
		BrickDefinitions.delete(); // todo: check if this works
	}
}
