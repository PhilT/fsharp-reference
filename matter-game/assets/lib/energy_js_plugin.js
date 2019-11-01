let energy
let world
let contact
let space

export function init (scene) {
  var contactFlagMode = null
  var gravityVector = new BABYLON.Vector3(0, -9.8, 0)

  if (energy) energy.destroySimulation()
  energy = new Energy(scene, gravityVector, Energy.QUICK_STEP, contactFlagMode, 1, 20)
  world = energy.dxWorld
  contact = energy.dxContactgroup
  space = energy.dxSpace

  energy.setTimeStepAndNumStep(0.033, 1)
  energy.setMaxContact(3)
  // TODO explain
  energy.dWorldSetAutoDisableFlag(1)
  energy.dWorldSetAutoDisableAverageSamplesCount(10)
  energy.dWorldSetAutoDisableLinearThreshold(0.30)
  energy.dWorldSetAutoDisableAngularThreshold(0.30)
  energy.addStaticPlane(0, 0, 1, 0)
}

export function createBox (box, mass) {
  energy.addDynamicObject(box, Energy.BOX, mass)
  // mu appears to be defined by Energy
  energy.dBodySetSurfaceParameter(box, mu, 500)
}

export function start () {
  energy.startSimulation()
}
