module InvasionPercolation

open System
open System.IO
open C5

type FillOrResist = Resistance of int | Filled 
    
   
let flat2Darray array2D = 
            seq { for x in [0..(Array2D.length1 array2D) - 1] do 
                      for y in [0..(Array2D.length2 array2D) - 1] do 
                          yield array2D.[x, y] }
    


let matrixBuilder n seed r =
    let rnd = new Random(seed)
    let arr = Array2D.init<FillOrResist> n n (fun _ _ -> Resistance ((rnd.Next() + 1) % r))
    let center = n/2
    arr.[center, center] <- Filled
    arr

let findPrioQueue (matrixMask:FillOrResist[,]) n (queue:IntervalHeap<int*int*int>) =
    let comIndex =
        [|      0,-1;
         -1, 0;       1, 0;
                0, 1
        |]
    
    let q = queue
    let value,posx,posy = q.DeleteMin()
    let m = matrixMask
    m.[posx, posy] <- Filled
    
    for pair in comIndex do
        let x,y = fst pair + posx, snd pair + posy
        if x >= 0 && x < n && y >= 0 && y < n then 
            match m.[x,y] with
            | Resistance a ->
                    let res = (a,x,y)
                    if not (q.Exists (fun (l,m,n) -> l = a && m = x && n = y)) then q.Add res  |> ignore //We can ignore the add result
            | _ -> ()
    
    m,q
let rec invPerPrioHelper matMask n nfill queue =
    match nfill with
    | 0 -> matMask //Done
    | _ ->
        let m,q = findPrioQueue matMask n queue
        invPerPrioHelper m n (nfill - 1) q
        
        
let invasionPercolation n nfill dummy =
    let R = 5000
    let matrixMask = matrixBuilder n dummy R
    let p = new IntervalHeap<int*int*int>()
    p.Add((R*2,n/2, n/2)) |> ignore //Enqueue center element with a high value (and ignore the success result)
    invPerPrioHelper matrixMask n nfill p
    
let invPercoBenchmark n nfill dummy =
    let res = invasionPercolation n nfill dummy
    float32 res.Length //return *some* value so that the result isn't completely optimised away

