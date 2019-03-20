module NumericalBenchmarks
    open System
    open System.Collections
       
    let multiply (i:int):float32 =
        let x = 1.1f * float32 (i &&& 0xFF)
        x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x
        
    let fibRecWrap no dummy =
        let rec fibonacciRec current next n =
            match n with
            | 0 -> current + next
            | _ -> fibonacciRec next (current + next) (n - 1)
        float32 (fibonacciRec 0 1 no)
        
    let fibIterWrap n dummy =
        let mutable a = 0
        let mutable b = 1
        let mutable c = 0
        
        for j in 2 .. n do
            c <- a + b
            a <- b
            b <- c
        float32 c
                
    let primes max dummy = 
        let array = new BitArray(max, true);
        let lastp = Math.Sqrt(float max) |> int
        for p in 2..lastp+1 do
            if array.Get(p) then
                for pm in p*2..p..max-1 do
                    array.Set(pm, false);
                    
        seq { for i in 2..max-1 do if array.Get(i) then yield i } |> Seq.last
        
    let randomizeArray n m dummy =
        let rnd = System.Random()
        Array2D.init n m (fun _ _  -> rnd.Next())