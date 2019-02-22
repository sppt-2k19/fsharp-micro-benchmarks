﻿module main

open System
open NoMutateBenchmarks

    
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
    let mutable result = 0.0f
    
    result <- result + benchmark "ScaleVector2D" iterations maxTime scaleVector2D
    result <- result + benchmark "ScaleVector3D" iterations maxTime scaleVector3D
    result <- result + benchmark "MultiplyVector2D" iterations maxTime multiplyVector2D
    result <- result + benchmark "MultiplyVector3D" iterations maxTime multiplyVector3D
    result <- result + benchmark "TranslateVector2D" iterations maxTime translateVector2D
    result <- result + benchmark "TranslateVector3D" iterations maxTime translateVector3D
    result <- result + benchmark "SubtractVector2D" iterations maxTime subtractVector2D
    result <- result + benchmark "SubtractVector3D" iterations maxTime subtractVector3D
    result <- result + benchmark "LengthVector2D" iterations maxTime lengthVector2D
    result <- result + benchmark "LengthVector3D" iterations maxTime lengthVector3D
    result <- result + benchmark "DotProductVector2D" iterations maxTime dotProductVector2D
    result <- result + benchmark "DotProductVector2D" iterations maxTime dotProductVector3D
    
    0 // return an integer exit code
