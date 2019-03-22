module Main
    open System.IO
    open NumericalBenchmarks
    open GameOfLife
    open InvasionPercolation

    open System
    let iterativeBenchmark msg func iterations minTime =
        let mutable count = 1
        let mutable runningTime = 0.0
        let mutable deltaTime = 0.0
        let mutable deltaTimeSquared = 0.0
        
        while (runningTime < minTime && count < Int32.MaxValue / 2) do
            count <- count * 2
            deltaTime <- 0.0
            deltaTimeSquared <- 0.0 
            for j in 0 .. iterations do
                let started = DateTime.UtcNow
                for i in 0 .. count do
                    func i |> ignore
                runningTime <- DateTime.UtcNow.Subtract(started).TotalMilliseconds * 1000000.0
                let time = runningTime / float count
                deltaTime <- deltaTime + time
                deltaTimeSquared <- deltaTimeSquared + time * time
        let mean = deltaTime / float iterations
        let standardDeviation = sqrt (abs (deltaTimeSquared - mean * mean * float iterations) / float (iterations - 1))
        printfn "%20s,%20.3f,%20.3f,%20i" msg mean standardDeviation count
        msg, mean, standardDeviation, count
    
    
    [<EntryPoint>]
    let main argv =
        
        let p = 10
        
        
        let iterations = 5
        let minTime = float (250 * 1000000)
        let mutable results = []
        let (<<) l r = results <- results @ [ r ]
        
        let runBenchmark bench msg func = bench msg func iterations minTime
        let toString resultTuple =
            let (name, mean, dev, count) = resultTuple
            sprintf "%s,%.3f,%.3f,%i" name mean dev count
        
//        printfn "Iterative Mark8 benchmark - no mutate"        
//        results << runBenchmark iterativeBenchmark "ScaleVector2D" NoMutateBenchmarks.scaleVector2D
//        results << runBenchmark iterativeBenchmark "ScaleVector3D" NoMutateBenchmarks.scaleVector3D 
//        results << runBenchmark iterativeBenchmark "MultiplyVector2D" NoMutateBenchmarks.multiplyVector2D
//        results << runBenchmark iterativeBenchmark "MultiplyVector3D" NoMutateBenchmarks.multiplyVector3D
//        results << runBenchmark iterativeBenchmark "TranslateVector2D" NoMutateBenchmarks.translateVector2D  
//        results << runBenchmark iterativeBenchmark "TranslateVector3D" NoMutateBenchmarks.translateVector3D
//        results << runBenchmark iterativeBenchmark "SubtractVector2D" NoMutateBenchmarks.subtractVector2D
//        results << runBenchmark iterativeBenchmark "SubtractVector3D" NoMutateBenchmarks.subtractVector3D
//        results << runBenchmark iterativeBenchmark "LengthVector2D" NoMutateBenchmarks.lengthVector2D
//        results << runBenchmark iterativeBenchmark "LengthVector3D" NoMutateBenchmarks.lengthVector3D
//        results << runBenchmark iterativeBenchmark "DotProductVector2D" NoMutateBenchmarks.dotProductVector2D  
//        results << runBenchmark iterativeBenchmark "DotProductVector3D" NoMutateBenchmarks.dotProductVector3D
//        
//        File.WriteAllText("no-mutate-results.csv", "Test,Mean,Deviation,Count\n" + String.Join('\n', (List.map toString results)))
        results <- []
        
        printfn "Iterative Mark8 benchmark - mutate"
        results << runBenchmark iterativeBenchmark "Sestoft Multiply" multiply
        results << runBenchmark iterativeBenchmark "Primes" (primes 100)
        results << runBenchmark iterativeBenchmark "RandomizeArray" (randomizeArray 4 4)
        results << runBenchmark iterativeBenchmark "GameOfLife" (iterateGameOfLifeTimes 4)
        results << runBenchmark iterativeBenchmark "InvasionPercolation" (invasionPercolation 5 10)
        results << runBenchmark iterativeBenchmark "FibonacciRecursive" (fibRecWrap 150)
        results << runBenchmark iterativeBenchmark "FibonacciIterative" (fibIterWrap 150)
        
        results << runBenchmark iterativeBenchmark "ScaleVector2D" MutateBenchmarks.scaleVector2D
        results << runBenchmark iterativeBenchmark "ScaleVector3D" MutateBenchmarks.scaleVector3D 
        results << runBenchmark iterativeBenchmark "MultiplyVector2D" MutateBenchmarks.multiplyVector2D
        results << runBenchmark iterativeBenchmark "MultiplyVector3D" MutateBenchmarks.multiplyVector3D
        results << runBenchmark iterativeBenchmark "TranslateVector2D" MutateBenchmarks.translateVector2D  
        results << runBenchmark iterativeBenchmark "TranslateVector3D" MutateBenchmarks.translateVector3D
        results << runBenchmark iterativeBenchmark "SubtractVector2D" MutateBenchmarks.subtractVector2D
        results << runBenchmark iterativeBenchmark "SubtractVector3D" MutateBenchmarks.subtractVector3D
        results << runBenchmark iterativeBenchmark "LengthVector2D" MutateBenchmarks.lengthVector2D
        results << runBenchmark iterativeBenchmark "LengthVector3D" MutateBenchmarks.lengthVector3D
        results << runBenchmark iterativeBenchmark "DotProductVector2D" MutateBenchmarks.dotProductVector2D  
        results << runBenchmark iterativeBenchmark "DotProductVector3D" MutateBenchmarks.dotProductVector3D
        
        File.WriteAllText("mutate-results.csv", "Test,Mean,Deviation,Count\n" + String.Join('\n', (List.map toString results)))
        
        0
