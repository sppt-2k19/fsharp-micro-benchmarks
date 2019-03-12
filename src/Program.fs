module Main
    open NoMutateBenchmarks
    open NumericalBenchmarks
    open GameOfLife
    

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
        printfn "%s,%.3f,%.3f,%i" msg mean standardDeviation count
        msg, mean, standardDeviation, count
    
//    let runIterations func iterations count =
//        let reducer acc elem =
//            let started = DateTime.UtcNow
//            for i in 0 .. count do
//                    func i |> ignore
//            let runningTime = DateTime.UtcNow.Subtract(started).TotalMilliseconds * 1000000.0
//            let time = runningTime / float count
//            let deltaTime, deltaTimeSquared, _ = acc
//            deltaTime + time, deltaTimeSquared + (time * time), runningTime
//        List.fold reducer (0.0, 0.0, 0.0) [0 .. iterations]
//    let rec recursiveBenchmark msg func iterations minTime count =
//        let deltaTime, deltaTimeSquared, runningTime = runIterations func iterations count
//        
//        if runningTime < minTime && count < Int32.MaxValue / 2 then
//            recursiveBenchmark msg func iterations minTime count * 2
//        else 
//            let mean = deltaTime / float iterations
//            let standardDeviation = sqrt (abs (deltaTimeSquared - mean * mean * float iterations) / float (iterations - 1))
//            printfn "%s,%f,%f,%i" msg mean standardDeviation count
//            msg, mean, standardDeviation, count
                
    
    [<EntryPoint>]
    let main argv =
        let iterations = 5
        let minTime = float (250 * 1000000)
        let mutable results = []
        
//        let startRecursiveBench msg func iterations minTime = recursiveBenchmark msg func iterations minTime 1
        let runBenchmark bench msg func = bench msg func iterations minTime
        
        let (<<) l r = results <- results @ [ r ]
        
        
        printfn "Iterative Mark8 benchmark"
        results << runBenchmark iterativeBenchmark "FibonacciRec" fibRecWrap
        results << runBenchmark iterativeBenchmark "FibonacciRec" fibRecWrap
        results << runBenchmark iterativeBenchmark "FibonacciRec" fibRecWrap
        results << runBenchmark iterativeBenchmark "FibonacciIter" fibIterWrap
        results << runBenchmark iterativeBenchmark "FibonacciIter" fibIterWrap
        results << runBenchmark iterativeBenchmark "FibonacciIter" fibIterWrap
        
        results << runBenchmark iterativeBenchmark "ScaleVector2D" scaleVector2D
        results << runBenchmark iterativeBenchmark "ScaleVector3D" scaleVector3D 
        results << runBenchmark iterativeBenchmark "MultiplyVector2D" multiplyVector2D
        results << runBenchmark iterativeBenchmark "MultiplyVector3D" multiplyVector3D
        results << runBenchmark iterativeBenchmark "TranslateVector2D" translateVector2D  
        results << runBenchmark iterativeBenchmark "TranslateVector3D" translateVector3D
        results << runBenchmark iterativeBenchmark "SubtractVector2D" subtractVector2D
        results << runBenchmark iterativeBenchmark "SubtractVector3D" subtractVector3D
        results << runBenchmark iterativeBenchmark "LengthVector2D" lengthVector2D
        results << runBenchmark iterativeBenchmark "LengthVector3D" lengthVector3D
        results << runBenchmark iterativeBenchmark "DotProductVector2D" dotProductVector2D  
        results << runBenchmark iterativeBenchmark "DotProductVector3D" dotProductVector3D
        
//        printfn "\nRecursive Mark8 benchmark"
//        results << runBenchmark startRecursiveBench "ScaleVector2D" scaleVector2D
//        results << runBenchmark startRecursiveBench "ScaleVector3D" scaleVector3D
//        results << runBenchmark startRecursiveBench "MultiplyVector2D" multiplyVector2D
//        results << runBenchmark startRecursiveBench "MultiplyVector3D" multiplyVector3D
//        results << runBenchmark startRecursiveBench "TranslateVector2D" translateVector2D
//        results << runBenchmark startRecursiveBench "TranslateVector3D" translateVector3D
//        results << runBenchmark startRecursiveBench "SubtractVector2D" subtractVector2D
//        results << runBenchmark startRecursiveBench "SubtractVector3D" subtractVector3D
//        results << runBenchmark startRecursiveBench "LengthVector2D" lengthVector2D
//        results << runBenchmark startRecursiveBench "LengthVector3D" lengthVector3D
//        results << runBenchmark startRecursiveBench "DotProductVector2D" dotProductVector2D
//        results << runBenchmark startRecursiveBench "DotProductVector3D" dotProductVector3D
        
        
        
        printfn "\n%O" results
        0 // return an integer exit code
