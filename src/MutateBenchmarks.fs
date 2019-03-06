module MutateBenchmarks 
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
        let mutable v2 = new Vector2(float32 i)
        v1 <- v1 * v2
        v1.Length()
        
    let multiplyVector3D i =
        let mutable v1 = new Vector3(1.0f)
        let mutable v2 = new Vector3(float32 i)
        v1 <- v1 * v2
        v1.Length()
        
    let translateVector2D i =
        let mutable v1 = new Vector2(1.0f);
        let mutable v2 = new Vector2(float32 i);
        let v3 = v1 + v2
        v3.Length()
        
    let translateVector3D i =
        let mutable v1 = new Vector3(1.0f);
        let mutable v2 = new Vector3(float32 i);
        let v3 = v1 + v2
        v3.Length()
        
    let subtractVector2D i =
        let mutable v1 = new Vector2(float32 i)
        let v2 = new Vector2(1.0f)
        v1 <- v1 - v2
        v1.Length()
        
    let subtractVector3D i =
        let mutable v1 = new Vector3(float32 i)
        let v2 = new Vector3(1.0f)
        v1 <- v1 - v2
        v1.Length()
        
    let lengthVector2D i =
        let v1 = new Vector2(float32 i)
        v1.Length()
        
    let lengthVector3D i =
        let v1 = new Vector3(float32 i)
        v1.Length()
        
    let dotProductVector2D i =
        let v1 = new Vector2(1.0f)
        let v2 = new Vector2(float32 i)
        Vector2.Dot(v1, v2)
        
    let dotProductVector3D i =
        let v1 = new Vector3(1.0f)
        let v2 = new Vector3(float32 i)
        Vector3.Dot(v1, v2)
        
    let rec sieve = function
        | (p::xs) -> p :: sieve [ for x in xs do if x % p > 0 then yield x ]
        | []      -> []
        
    let multiply i =
        let x = 1.1f * float32 (i &&& 0xFF)
        x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x
        
    let fibonacci =
        Seq.unfold
            (fun (current, next) -> Some(current, (next, current + next)))
            (0, 1)
    
    let fibonacci2 digits =
        let mutable a = 0
        let mutable b = 1
        let mutable c = 0
        
        for j in 0 .. digits do
            c <- a + b
            a <- b
            b <- c
        c