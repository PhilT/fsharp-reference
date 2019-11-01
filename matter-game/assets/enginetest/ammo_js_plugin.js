Ammo().then(function (Ammo) {
  window.Ammo = Ammo

  const collisionConfiguration = new Ammo.btDefaultCollisionConfiguration()
  const overlappingPairCache = new Ammo.btDbvtBroadphase()
  const solver = new Ammo.btSequentialImpulseConstraintSolver()
  const dispatcher = new Ammo.btCollisionDispatcher(collisionConfiguration)
  let dynamicsWorld
  let bodies = []

  const createGround = (width, depth) => {
    var groundShape = new Ammo.btBoxShape(new Ammo.btVector3(width, 1, depth)) // as yet do not know range of possible imposter shapes
    var groundTransform = new Ammo.btTransform() // need to set a new transformation in order to move, rotate or scale an Ammo shape
    groundTransform.setIdentity() // set transformation to identity then can reset as needed
    groundTransform.setOrigin(new Ammo.btVector3(0, -1, 0)) // To get top of groundShape to same position as ground need -1 to place top on XZ plane then -6

    /* Set Physics properties of ground */
    var groundMass = 0
    var groundLocalInertia = new Ammo.btVector3(0, 0, 0)
    var groundMotionState = new Ammo.btDefaultMotionState(groundTransform)
    var groundRBInfo = new Ammo.btRigidBodyConstructionInfo(groundMass, groundMotionState, groundShape, groundLocalInertia)
    var impostor = new Ammo.btRigidBody(groundRBInfo)

    /* Add ground to world */
    dynamicsWorld.addRigidBody(impostor)
  }

  /* Create master imposter for a box */
  var boxShape = new Ammo.btBoxShape(new Ammo.btVector3(0.5, 0.5, 0.5)) // Again half side for BJS box

  const createBox = (box, x, y, z) => {
    var startTransform = new Ammo.btTransform()
    startTransform.setIdentity()
    var mass = 1
    var localInertia = new Ammo.btVector3(0, 0, 0)
    boxShape.calculateLocalInertia(mass, localInertia)

    var boxMotionState = new Ammo.btDefaultMotionState(startTransform)
    var boxRBInfo = new Ammo.btRigidBodyConstructionInfo(mass, boxMotionState, boxShape, localInertia)
    var boxImpostor = new Ammo.btRigidBody(boxRBInfo)

    dynamicsWorld.addRigidBody(boxImpostor)

    // reset position
    var origin = boxImpostor.getWorldTransform().getOrigin()
    origin.setX(x)
    origin.setY(y)
    origin.setZ(z)
    boxImpostor.activate()

    bodies.push({boxImpostor, box})
  }

  var transform = new Ammo.btTransform() // transform variable

  /* run imposter simulation per frame
  transferring imposter positions and orientation to boxes */

  const updatePositions = engine => {
    dynamicsWorld.stepSimulation(engine.getDeltaTime(), 2)
    bodies.forEach(({boxImpostor, box}) => {
      boxImpostor.getMotionState().getWorldTransform(transform)
      var origin = transform.getOrigin()
      var rotation = transform.getRotation()
      box.position.x = origin.x()
      box.position.y = origin.y()
      box.position.z = origin.z()
      var quaternion = new BABYLON.Quaternion(0, 0, 0, 0)
        .copyFromFloats(rotation.x(), rotation.y(), rotation.z(), rotation.w())
      box.rotation = quaternion.toEulerAngles()
    })
  }

  const createWorld = () => {
    if (dynamicsWorld) Ammo.destroy(dynamicsWorld)
    bodies = []

    dynamicsWorld = new Ammo.btDiscreteDynamicsWorld(
      dispatcher, overlappingPairCache, solver, collisionConfiguration
    )
    dynamicsWorld.setGravity(new Ammo.btVector3(0, -9.8, 0))
  }

  window.AmmoPlugin = {
    createGround,
    createBox,
    updatePositions,
    createWorld
  }
})
