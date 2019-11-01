export function init () {
  return new OIMO.World({
    timestep: 1 / 30,
    iterations: 6,
    broadphase: 3, // 1 brute force, 2 sweep and prune, 3 volume tree
    worldscale: 1, // scale full world
    random: true, // randomize sample
    info: false, // calculate statistic or not
    gravity: [0, -9.8, 0]
  })
}

export function createBox (world, box, mass) {
  return world.add({
    type: 'box', // type of shape : sphere, box, cylinder
    size: [mass, mass, mass], // size of shape
    pos: [box.position.x, box.position.y, box.position.z], // start position in degree
    rot: [0, 0, 0], // start rotation in degree
    move: true, // dynamic or statique
    density: mass,
    friction: 0.2,
    restitution: 0,
    belongsTo: 1, // The bits of the collision groups to which the shape belongs.
    collidesWith: 0xffffffff // The bits of the collision groups with which the shape collides.
  })
}

export function createGround (world) {
  return world.add({
    type: 'box', // type of shape : sphere, box, cylinder
    size: [20000, 1, 20000], // size of shape
    pos: [0, 0, 0], // start position in degree
    rot: [0, 0, 0], // start rotation in degree
    move: false, // dynamic or statique
    density: 1,
    friction: 0.2,
    restitution: 1,
    belongsTo: 1, // The bits of the collision groups to which the shape belongs.
    collidesWith: 0xffffffff // The bits of the collision groups with which the shape collides.
  })
}
