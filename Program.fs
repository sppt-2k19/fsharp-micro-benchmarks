// Learn more about F# at http://fsharp.org

open System
open System
open System.Numerics

let scaleVector2D scalar =
    let mutable v1 = new Vector2(1.0f)
    v1 <- v1 * float32 scalar
    v1.Length()

let scaleVector3D scalar =
    let mutable v1 = new Vector3(1.0f)
    v1 <- v1 * float32 scalar
    v1.Length()

let multiplyVector2D i =
    let mutable v1 = new Vector2(1.0f)
    let mutable v2 = new Vector2(1.0f)
    v1 <- v1 * v2
    v1.Length()
    
let multiplyVector3D i =
    let mutable v1 = new Vector3(1.0f)
    let mutable v2 = new Vector3(1.0f)
    v1 <- v1 * v2
    v1.Length()
    
let translateVector2D i =
    let mutable v1 = new Vector2(1.0f);
    v1.X <- v1.X + i
    v1.Length()
    
let translateVector3D i =
    let mutable v1 = new Vector3(1.0f);
    v1.X <- v1.X + i
    v1.Length()
    
let subtractVector2D i =
    let v1 = new Vector2(1.0f)
    let v2 = new Vector2(1.0f)
    v1 <- v1 - v2
    
let benchmark msg iterations minTime func =
    let mutable count = 1
    let mutable totalCount = 0
    let mutable dummy = 0.0f
    let mutable runningTime = 0.0f
    let mutable deltaTime = 0.0f
    let mutable deltaTimeSquared = 0.0f
    
    while (runningTime < minTime && count < Int32.MaxValue / 2) do
        count <- count * 2
        deltaTime <- 0.0f
        deltaTimeSquared <- 0.0f
        for j = 0 to iterations do
            let timer = System.Diagnostics.Stopwatch.StartNew()
            for i = 0 to count do
                dummy <- dummy + func i
            runningTime <- float32 timer.ElapsedTicks * 100.0f
            let time = runningTime / (float32 count)
            deltaTime <- deltaTime + time
            deltaTimeSquared <- deltaTime * deltaTime
            totalCount <- totalCount + count
    let mean = deltaTime / float32 iterations
    let standardDeviation = sqrt (deltaTimeSquared - mean * mean * float32 iterations) / float32 (iterations - 1)
    printfn "%s,%f,%f,%i" msg mean standardDeviation count
    dummy / float32 totalCount
    
    
    
[<EntryPoint>]
let main argv =
    let iterations = 5
    let maxTime = float32 (250 * 1000000)
    
    let sv2d = benchmark "ScaleVector2D" iterations maxTime scaleVector2D
    let sv3d = benchmark "ScaleVector3D" iterations maxTime scaleVector3D
    let mv2d = benchmark "MultiplyVector2D" iterations maxTime multiplyVector2D
    let mv3d = benchmark "MultiplyVector3D" iterations maxTime multiplyVector3D
    
    printfn "%O %O %O %O" sv2d sv3d mv2d mv3d
    0 // return an integer exit code
