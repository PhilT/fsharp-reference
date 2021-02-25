import * as energy from '../lib/energy_js_plugin.js'
import * as oimojs from '../lib/oimo_js_plugin.js'

const B = BABYLON
const Vector3 = B.Vector3
const canvas = document.getElementById('renderCanvas')

document.addEventListener('DOMContentLoaded', () => {
  window.engine = new B.Engine(canvas)
})

window.addEventListener('resize', function () {
  window.engine.resize()
})

document.addEventListener('click', (event) => {
  const engines = {
    cannon: { Plugin: B.CannonJSPlugin },
    oimo: { Plugin: B.OimoJSPlugin },
    ammo: { Plugin: null },
    energy: { Plugin: null },
    oimojs: { Plugin: null }
  }
  const engineNames = Object.keys(engines)
  const selectedEngine = event.target.id
  const engine = window.engine

  if (!engineNames.includes(selectedEngine)) return

  if (window.scene) {
    window.scene.dispose()
  }
  window.scene = new B.Scene(window.engine)
  const scene = window.scene
  scene.clearColor = new B.Color3(0.5, 0.5, 0.5)

  console.log(`Running ${selectedEngine}`)
  engineNames.forEach(engine => {
    const selected = engine === selectedEngine ? 'selected' : ''
    document.getElementById(engine).className = selected
  })

  if (selectedEngine === 'ammo') {
    window.AmmoPlugin.createWorld()
  } else if (selectedEngine === 'energy') {
    energy.init(window.scene)
  } else if (selectedEngine == 'oimojs') {
    window.world = oimojs.init()
  } else {
    scene.enablePhysics(new Vector3(0, -9.8, 0), new engines[selectedEngine].Plugin())
  }

  const SIZE = 30

  const camera = new B.ArcRotateCamera('', 0, 0, 0, new Vector3(0, 10, 0), scene)
  camera.setPosition(new Vector3(-40, 50, -50))
  camera.attachControl(canvas, true)

  // Sunlight
  const sun = new B.HemisphericLight('', new Vector3(0, 1, 0))
  sun.diffuse = new B.Color3(0.5, 0.5, 0.5)

  // Point light
  const light = new B.PointLight('Omni', new Vector3(-60, 60, 80), scene)

  // Matter
  const createBrick = () => {
    const matter = B.MeshBuilder.CreateBox('', { size: 1 }, scene)
    matter.convertToUnIndexedMesh()
    matter.setEnabled(false)
    return matter
  }

  // const flatten = array => array.reduce((acc, val) => acc.concat(val), [])
  const impostorOptions = {
    mass: 1,
    restitution: 0.0,
    friction: 1,
    disableBidirectionalTransformation: true
  }

  const brick = createBrick()
  brick.convertToUnIndexedMesh()
  const wall = []
  const bodies = []
  const startHeight = 0.5
  // Wall
  for (let y = 0; y < SIZE; y++) {
    for (let x = 0; x < SIZE; x++) {
      const instance = brick.createInstance()
      instance.position = new Vector3(x - (SIZE / 2), y + startHeight, 0)
      if (selectedEngine === 'ammo') {
        window.AmmoPlugin.createBox(instance, x - (SIZE / 2), y + startHeight, 0)
      } else if (selectedEngine === 'energy') {
        energy.createBox(instance, 1)
      } else if (selectedEngine === 'oimojs') {
        bodies.push(oimojs.createBox(window.world, instance, 1))
      } else {
        instance.physicsImpostor = new B.PhysicsImpostor(instance,
          B.PhysicsImpostor.BoxImpostor, impostorOptions, scene)
      }
      wall.push(instance)
    }
  }

  const reset = bricks => {
    bricks.forEach(brick => (brick.instance.position = new Vector3(brick.x - (SIZE / 2), brick.y + 5, 0)))
  }

  // reset(wall)

  // Ground
  const ground = B.MeshBuilder.CreateGround('', { height: 20000, width: 20000, subdivisions: 2 }, scene)
  const groundMaterial = new B.StandardMaterial()
  groundMaterial.diffuseTexture = new B.Texture('../grid.webp')
  groundMaterial.diffuseTexture.uScale = 1000
  groundMaterial.diffuseTexture.vScale = 1000
  ground.material = groundMaterial

  if (selectedEngine === 'ammo') {
    window.AmmoPlugin.createGround(20000, 20000)
  } else if (selectedEngine === 'energy') {
  } else if (selectedEngine === 'oimojs') {
    oimojs.createGround(world)
  } else {
    ground.physicsImpostor = new B.PhysicsImpostor(ground, B.PhysicsImpostor.BoxImpostor, { mass: 0, restitution: 1 }, scene)
  }

  const timeDiv = document.getElementById('time')
  const fpsDiv = document.getElementById('fps')
  const avgFpsDiv = document.getElementById('avgFps')
  const minFpsDiv = document.getElementById('minFps')
  const maxFpsDiv = document.getElementById('maxFps')
  let totalFps = 0
  let fpsCount = 0
  let minFps = Infinity
  let maxFps = 0
  const startTime = Date.now()

  window.setInterval(() => {
    let fps = engine.getFps()
    if (fps !== Infinity) totalFps += fps
    fpsCount += 1
    minFps = Math.min(minFps, fps)
    maxFps = Math.max(maxFps, fps)
  }, 500)

  if (selectedEngine === 'ammo') {
    scene.registerAfterRender(function () {
      window.AmmoPlugin.updatePositions(engine)
    })
  } else if (selectedEngine === 'energy') {
    energy.start()
  }

  const updatePositions = () => {
    wall.forEach((brick, i) => {
      const body = bodies[i]
      brick.position = body.getPosition()
      const { x, y, z, w } = body.getQuaternion()
      brick.rotationQuaternion = new B.Quaternion(x, y, z, w)
    })
  }

  let step = true

  engine.runRenderLoop(() => {
    scene.render()
    if (selectedEngine === 'oimojs' && step) {
      window.world.step()
      updatePositions()
    }
    step = !step
    timeDiv.innerHTML = `${((Date.now() - startTime) / 1000).toFixed()}s`
    fpsDiv.innerHTML = `${(engine.getFps()).toFixed()} FPS`
    avgFpsDiv.innerHTML = `${(totalFps / fpsCount).toFixed()} Avg`
    minFpsDiv.innerHTML = `${minFps.toFixed()} Min`
    maxFpsDiv.innerHTML = `${maxFps.toFixed()} Max`
  })

  console.log('all done.')
})
