module Main
    open System.IO
    open NumericalBenchmarks
    open GameOfLife
    open InvasionPercolation
    open MapReduce
    open SestoftBenchmark

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
        let bbbb = Benchmark.Mark8
        let runBenchmark msg func = Benchmark.Mark8 (msg, (new Func<int, float32>(func)), iterations, minTime)
        let toString resultTuple =
            let struct(name, mean, dev, count, dummy) = resultTuple
            sprintf "%s,%.3f,%.3f,%i" name mean dev count
        
        results <- []
        
        printfn "Iterative Mark8 benchmark - mutate"
        results << runBenchmark "MapReduce Array" mapReduceArray
        results << runBenchmark "MapReduce Array" mapReduceArray
        results << runBenchmark "MapReduce Array" mapReduceArray
        results << runBenchmark "MapReduce Array" mapReduceArray
        results << runBenchmark "MapReduce Array" mapReduceArray
        
        results << runBenchmark "MapReduce Seq" mapReduceSeq
        results << runBenchmark "MapReduce Seq" mapReduceSeq
        results << runBenchmark "MapReduce Seq" mapReduceSeq
        results << runBenchmark "MapReduce Seq" mapReduceSeq
        results << runBenchmark "MapReduce Seq" mapReduceSeq
        
        results << runBenchmark "MapReduce Unions" mapReduceUnions
        results << runBenchmark "MapReduce Unions" mapReduceUnions
        results << runBenchmark "MapReduce Unions" mapReduceUnions
        results << runBenchmark "MapReduce Unions" mapReduceUnions
        results << runBenchmark "MapReduce Unions" mapReduceUnions
        
        results << runBenchmark "Sestoft Multiply" multiply
        results << runBenchmark "Sestoft Multiply" multiply
        results << runBenchmark "Sestoft Multiply" multiply
        results << runBenchmark "Sestoft Multiply" multiply
        results << runBenchmark "Sestoft Multiply" multiply
        
        results << runBenchmark "Primes" (primes 100)
        results << runBenchmark "Primes" (primes 100)
        results << runBenchmark "Primes" (primes 100)
        results << runBenchmark "Primes" (primes 100)
        results << runBenchmark "Primes" (primes 100)
        
        results << runBenchmark "RandomizeArray" (randomizeArray 4 4)
        results << runBenchmark "RandomizeArray" (randomizeArray 4 4)
        results << runBenchmark "RandomizeArray" (randomizeArray 4 4)
        results << runBenchmark "RandomizeArray" (randomizeArray 4 4)
        results << runBenchmark "RandomizeArray" (randomizeArray 4 4)
        
        results << runBenchmark "GameOfLife" (iterateGameOfLifeTimes 4)
        results << runBenchmark "GameOfLife" (iterateGameOfLifeTimes 4)
        results << runBenchmark "GameOfLife" (iterateGameOfLifeTimes 4)
        results << runBenchmark "GameOfLife" (iterateGameOfLifeTimes 4)
        results << runBenchmark "GameOfLife" (iterateGameOfLifeTimes 4)
        
        results << runBenchmark "InvasionPercolation" (invPercoBenchmark 5 10)
        results << runBenchmark "InvasionPercolation" (invPercoBenchmark 5 10)
        results << runBenchmark "InvasionPercolation" (invPercoBenchmark 5 10)
        results << runBenchmark "InvasionPercolation" (invPercoBenchmark 5 10)
        results << runBenchmark "InvasionPercolation" (invPercoBenchmark 5 10)
        
        results << runBenchmark "FibonacciRecursive" (fibRecWrap 150)
        results << runBenchmark "FibonacciRecursive" (fibRecWrap 150)
        results << runBenchmark "FibonacciRecursive" (fibRecWrap 150)
        results << runBenchmark "FibonacciRecursive" (fibRecWrap 150)
        results << runBenchmark "FibonacciRecursive" (fibRecWrap 150)
        
        results << runBenchmark "FibonacciIterative" (fibIterWrap 150)
        results << runBenchmark "FibonacciIterative" (fibIterWrap 150)
        results << runBenchmark "FibonacciIterative" (fibIterWrap 150)
        results << runBenchmark "FibonacciIterative" (fibIterWrap 150)
        results << runBenchmark "FibonacciIterative" (fibIterWrap 150)
        
        results << runBenchmark "ScaleVector2D" MutateBenchmarks.scaleVector2D
        results << runBenchmark "ScaleVector2D" MutateBenchmarks.scaleVector2D
        results << runBenchmark "ScaleVector2D" MutateBenchmarks.scaleVector2D
        results << runBenchmark "ScaleVector2D" MutateBenchmarks.scaleVector2D
        results << runBenchmark "ScaleVector2D" MutateBenchmarks.scaleVector2D
        
        results << runBenchmark "ScaleVector3D" MutateBenchmarks.scaleVector3D 
        results << runBenchmark "ScaleVector3D" MutateBenchmarks.scaleVector3D 
        results << runBenchmark "ScaleVector3D" MutateBenchmarks.scaleVector3D 
        results << runBenchmark "ScaleVector3D" MutateBenchmarks.scaleVector3D 
        results << runBenchmark "ScaleVector3D" MutateBenchmarks.scaleVector3D
        
        results << runBenchmark "MultiplyVector2D" MutateBenchmarks.multiplyVector2D
        results << runBenchmark "MultiplyVector2D" MutateBenchmarks.multiplyVector2D
        results << runBenchmark "MultiplyVector2D" MutateBenchmarks.multiplyVector2D
        results << runBenchmark "MultiplyVector2D" MutateBenchmarks.multiplyVector2D
        results << runBenchmark "MultiplyVector2D" MutateBenchmarks.multiplyVector2D
        
        results << runBenchmark "MultiplyVector3D" MutateBenchmarks.multiplyVector3D
        results << runBenchmark "MultiplyVector3D" MutateBenchmarks.multiplyVector3D
        results << runBenchmark "MultiplyVector3D" MutateBenchmarks.multiplyVector3D
        results << runBenchmark "MultiplyVector3D" MutateBenchmarks.multiplyVector3D
        results << runBenchmark "MultiplyVector3D" MutateBenchmarks.multiplyVector3D
        
        results << runBenchmark "TranslateVector2D" MutateBenchmarks.translateVector2D  
        results << runBenchmark "TranslateVector2D" MutateBenchmarks.translateVector2D  
        results << runBenchmark "TranslateVector2D" MutateBenchmarks.translateVector2D  
        results << runBenchmark "TranslateVector2D" MutateBenchmarks.translateVector2D  
        results << runBenchmark "TranslateVector2D" MutateBenchmarks.translateVector2D
        
        results << runBenchmark "TranslateVector3D" MutateBenchmarks.translateVector3D
        results << runBenchmark "TranslateVector3D" MutateBenchmarks.translateVector3D
        results << runBenchmark "TranslateVector3D" MutateBenchmarks.translateVector3D
        results << runBenchmark "TranslateVector3D" MutateBenchmarks.translateVector3D
        results << runBenchmark "TranslateVector3D" MutateBenchmarks.translateVector3D
        
        results << runBenchmark "SubtractVector2D" MutateBenchmarks.subtractVector2D
        results << runBenchmark "SubtractVector2D" MutateBenchmarks.subtractVector2D
        results << runBenchmark "SubtractVector2D" MutateBenchmarks.subtractVector2D
        results << runBenchmark "SubtractVector2D" MutateBenchmarks.subtractVector2D
        results << runBenchmark "SubtractVector2D" MutateBenchmarks.subtractVector2D
        
        results << runBenchmark "SubtractVector3D" MutateBenchmarks.subtractVector3D
        results << runBenchmark "SubtractVector3D" MutateBenchmarks.subtractVector3D
        results << runBenchmark "SubtractVector3D" MutateBenchmarks.subtractVector3D
        results << runBenchmark "SubtractVector3D" MutateBenchmarks.subtractVector3D
        results << runBenchmark "SubtractVector3D" MutateBenchmarks.subtractVector3D
        
        results << runBenchmark "LengthVector2D" MutateBenchmarks.lengthVector2D
        results << runBenchmark "LengthVector2D" MutateBenchmarks.lengthVector2D
        results << runBenchmark "LengthVector2D" MutateBenchmarks.lengthVector2D
        results << runBenchmark "LengthVector2D" MutateBenchmarks.lengthVector2D
        results << runBenchmark "LengthVector2D" MutateBenchmarks.lengthVector2D
        
        results << runBenchmark "LengthVector3D" MutateBenchmarks.lengthVector3D
        results << runBenchmark "LengthVector3D" MutateBenchmarks.lengthVector3D
        results << runBenchmark "LengthVector3D" MutateBenchmarks.lengthVector3D
        results << runBenchmark "LengthVector3D" MutateBenchmarks.lengthVector3D
        results << runBenchmark "LengthVector3D" MutateBenchmarks.lengthVector3D
        
        results << runBenchmark "DotProductVector2D" MutateBenchmarks.dotProductVector2D  
        results << runBenchmark "DotProductVector2D" MutateBenchmarks.dotProductVector2D  
        results << runBenchmark "DotProductVector2D" MutateBenchmarks.dotProductVector2D  
        results << runBenchmark "DotProductVector2D" MutateBenchmarks.dotProductVector2D  
        results << runBenchmark "DotProductVector2D" MutateBenchmarks.dotProductVector2D
        
        results << runBenchmark "DotProductVector3D" MutateBenchmarks.dotProductVector3D
        results << runBenchmark "DotProductVector3D" MutateBenchmarks.dotProductVector3D
        results << runBenchmark "DotProductVector3D" MutateBenchmarks.dotProductVector3D
        results << runBenchmark "DotProductVector3D" MutateBenchmarks.dotProductVector3D
        results << runBenchmark "DotProductVector3D" MutateBenchmarks.dotProductVector3D
        
        File.WriteAllText("../results/results.csv", "Test,Mean,Deviation,Count\n" + String.Join('\n', (List.map toString results)))
        
        0
