module NumericalBenchmarks
    open System
       
    let multiply (i:int):float32 =
        let x = 1.1f * float32 (i &&& 0xFF)
        x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x
        
    
    
        
    let fibRecWrap n =
        let rec fibonacciRec current next n =
            match n with
            | 0 -> current + next
            | _ -> fibonacciRec next (current + next) (n - 1)
        float32 (fibonacciRec 0 1 200)
        
    let fibIterWrap n =
        let fibonacciIterative n =
            let mutable a = 0
            let mutable b = 1
            let mutable c = 0
            
            for j in 2 .. n do
                c <- a + b
                a <- b
                b <- c
            c
        float32 (fibonacciIterative 200)
        
    let sieve_primes top_number = 
        let numbers = [ yield 2
                        for i in 3..2..top_number -> i ]
        let rec sieve ns = 
            match ns with
            | [] -> []
            | x::xs when x*x > top_number -> ns
            | x::xs -> x::sieve (List.filter(fun y -> y%x <> 0) xs)
        sieve numbers 