module NumericalBenchmarks
    open System
       
    let multiply (i:int):float32 =
        let x = 1.1f * float32 (i &&& 0xFF)
        x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x
        
    let fibonacciRec (i:int):float32 =
        (Seq.unfold
            (fun (current, next) -> Some(current, (next, current + next)))
            (0, 1))
        |> Seq.take 50 |> Seq.head |> float32
    
    let fibonacciIterative (i:int):float32 =
        let mutable a = 0
        let mutable b = 1
        let mutable c = 0
        
        (seq { for j in 0 .. Int32.MaxValue do
                c <- a + b
                a <- b
                b <- c
                yield c })
        |> Seq.take 50 |> Seq.head |> float32