module GameOfLife
    open System
    
    let grid = "
    00000000
    00110000
    00010000
    00110000
    00001000
    00000000
    00000000
    00000000
    "
    let generation (grid:string) =
        let size = 8
        let lines = grid.Split([|'\r';'\n'|])
        let lines = lines |> Array.filter (fun line -> line.Length > 0)
        let computeNeighbours x y =
            [-1,-1; 0,-1; 1,-1
             -1, 0;       1, 0
             -1, 1; 0, 1; 1, 1]
            |> List.map (fun (dx,dy) ->
                let x,y = x+dx, y+dy
                if x >=0 && x < size && y >= 0 && y < size &&
                   lines.[y].Chars(x) = '1'
                then 1
                else 0
            ) |> List.sum
    
        let life x y c =       
            match c, computeNeighbours x y with
            | '1' , 2 -> c
            |  _ , 3 -> '1'
            |  _ , _ -> '0'
        
        lines |> Array.mapi (fun y line ->
            let chars =
                line.ToCharArray() |> Array.mapi (fun x c ->
                    life x y c
                )
            String(chars)
        )
        |> String.concat "\r\n"
    
    printfn "%s" grid
    
    do System.Console.ReadLine() |> ignore
