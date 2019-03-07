module Main
    open NoMutateBenchmarks
    open NumericalBenchmarks
    

    open System
    let iterativeBenchmark(msg:string, func:int -> float32, iterations:int32, minTime:float): string * float * float * int * float =
        let mutable count = 1
        let mutable runningTime = 0.0
        let mutable deltaTime = 0.0
        let mutable deltaTimeSquared = 0.0
        let mutable result = 0.0f
        
        while (runningTime < minTime && count < Int32.MaxValue / 2) do
            count <- count * 2
            deltaTime <- 0.0
            deltaTimeSquared <- 0.0 
            for j in 0 .. iterations do
                let started = DateTime.UtcNow
                for i in 0 .. count do
                    result <- result + func i
                runningTime <- DateTime.UtcNow.Subtract(started).TotalMilliseconds * 1000000.0
                let time = runningTime / float count
                deltaTime <- deltaTime + time
                deltaTimeSquared <- deltaTimeSquared + time * time
        let mean = deltaTime / float iterations
        let standardDeviation = sqrt (abs (deltaTimeSquared - mean * mean * float iterations) / float (iterations - 1))
        printfn "%s,%f,%f,%i" msg mean standardDeviation count
        msg, mean, standardDeviation, count, result
    
    let runIterations func iterations count =
        let reducer acc elem =
            let started = DateTime.UtcNow
            let mutable result = 0.0
            for i in 0 .. count do
                result <- result + func i
            let runningTime = DateTime.UtcNow.Subtract(started).TotalMilliseconds * 1000000.0
            let time = runningTime / float count
            let deltaTime, deltaTimeSquared, _, _ = acc
            deltaTime + time, deltaTimeSquared + (time * time), runningTime, result
        List.fold reducer (0.0, 0.0, 0.0, 0.0) [0 .. iterations]
    
    let rec recursiveBenchmark (msg:string, func:int32 -> float, iterations:int32, minTime:float, count:int32): string * float * float * int * float =
        let deltaTime, deltaTimeSquared, runningTime, result = runIterations func iterations count
        
        if runningTime < minTime && count < Int32.MaxValue / 2 then
            recursiveBenchmark (msg, func, iterations, minTime, count * 2)
        else 
            let mean = deltaTime / float iterations
            let standardDeviation = sqrt (abs (deltaTimeSquared - mean * mean * float iterations) / float (iterations - 1))
            printfn "%s,%f,%f,%i" msg mean standardDeviation count
            msg, mean, standardDeviation, count, result
                
    
    [<EntryPoint>]
    let main argv =
        let iterations = 5
        let minTime = float (250 * 1000000)
        let mutable results = []
        
        let startRecursiveBench (msg:string, func:int -> float32, iterations:int32, minTime: float) = recursiveBenchmark (msg, func, iterations, minTime, 1)
        let runBenchmark (bench:string * (int -> float32) * int32 * float -> string * float * float * int * float32, msg:string, func:int->float32) =
            bench(msg, func, iterations, minTime)
        
        let (<<) l r = results <- results @ [ r ]
        
        
        printfn "Iterative Mark8 benchmark"
        results << runBenchmark (iterativeBenchmark, "FibonacciRec", fibonacciRec)
        results << runBenchmark (iterativeBenchmark, "FibonacciIter", fibonacciIterative)
        
        results << runBenchmark (iterativeBenchmark, "ScaleVector2D", scaleVector2D)
        results << runBenchmark (iterativeBenchmark, "ScaleVector3D", scaleVector3D)
        results << runBenchmark (iterativeBenchmark, "MultiplyVector2D", multiplyVector2D)
        results << runBenchmark (iterativeBenchmark, "MultiplyVector3D", multiplyVector3D)
        results << runBenchmark (iterativeBenchmark, "TranslateVector2D", translateVector2D)
        results << runBenchmark (iterativeBenchmark, "TranslateVector3D", translateVector3D)
        results << runBenchmark (iterativeBenchmark, "SubtractVector2D", subtractVector2D)
        results << runBenchmark (iterativeBenchmark, "SubtractVector3D", subtractVector3D)
        results << runBenchmark (iterativeBenchmark, "LengthVector2D", lengthVector2D)
        results << runBenchmark (iterativeBenchmark, "LengthVector3D", lengthVector3D)
        results << runBenchmark (iterativeBenchmark, "DotProductVector2D", dotProductVector2D)
        results << runBenchmark (iterativeBenchmark, "DotProductVector3D", dotProductVector3D)
        
        printfn "\nRecursive Mark8 benchmark"
        results << runBenchmark (startRecursiveBench, "ScaleVector2D", scaleVector2D)
        results << runBenchmark (startRecursiveBench, "ScaleVector3D", scaleVector3D)
        results << runBenchmark (startRecursiveBench, "MultiplyVector2D", multiplyVector2D)
        results << runBenchmark (startRecursiveBench, "MultiplyVector3D", multiplyVector3D)
        results << runBenchmark (startRecursiveBench, "TranslateVector2D", translateVector2D)
        results << runBenchmark (startRecursiveBench, "TranslateVector3D", translateVector3D)
        results << runBenchmark (startRecursiveBench, "SubtractVector2D", subtractVector2D)
        results << runBenchmark (startRecursiveBench, "SubtractVector3D", subtractVector3D)
        results << runBenchmark (startRecursiveBench, "LengthVector2D", lengthVector2D)
        results << runBenchmark (startRecursiveBench, "LengthVector3D", lengthVector3D)
        results << runBenchmark (startRecursiveBench, "DotProductVector2D", dotProductVector2D)
        results << runBenchmark (startRecursiveBench, "DotProductVector3D", dotProductVector3D)
        
        
        
        printfn "\n%O" results
        0 // return an integer exit code
