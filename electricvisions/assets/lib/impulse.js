var lastAcceleration = 0
var acceleration = 0
var velocity = 0
var force = 200
var mass = 1
var timeStep = 1 / 60
var averageAcceleration
var position = 0

for (var i = 0; i < 100; i++) {
  if (velocity < 50) {
    lastAcceleration = acceleration
    acceleration = force / mass
    averageAcceleration = (lastAcceleration + acceleration) / 2
    velocity += averageAcceleration * timeStep
  }

  position += velocity * timeStep +
    (0.5 * lastAcceleration * Math.pow(timeStep, 2))

  console.log(position)
}
