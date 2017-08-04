// brickPlacer.projectile.cs
//
// Documentation: http://docs.garagegames.com/torque-3d/reference/classProjectileData.html#aa04e6f6051a59e35e61d4acd9c8d4be4

datablock ProjectileData(BrickPlacerProjectile)
{
	// Shape - todo, change later
	projectileShapeName = "art/shapes/weapons/shared/rocket.dts";
	size                = 0.1;
	
	// Damage
	// None, this isn't a weapon projectile.
	directDamage = 0;
	radiusDamage = 0;
	damageRadius = 0;
	areaImpulse  = 0;
	//damageType = "RocketDamage";

	// Velocity
	muzzleVelocity   = 100;
	velInheritFactor = 0.2;   // How much of player's velocity to inherit.
	isBallistic      = false; // Apply bullet physics?
	gravityMod       = 0.01;  // How much gravity affects projectile.

	// Lifetime
	lifetime  = 5000;
	fadeDelay = 4500;

	// Particles
	//particleEmitter      = BrickPlacerProjectileEmitter;           // Particle to emit as projectile moves.
	//splash               = RocketSplash;                           // Particle to emit when hitting water  
	//particleWaterEmitter = BrickPlacerProjectileEmitterUnderwater; // Particle to emit while projectile is traveling through water.
};

//datablock ParticleEmitterData(BrickPlacerProjectileEmitter) {
//	
//};

//datablock ParticleEmitterData(BrickPlacerProjectileEmitterUnderwater) {
//	
//};