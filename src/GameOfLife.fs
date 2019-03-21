module GameOfLife

open System

let defaultGameOfLifeGrid = "
10000
00110
00101
"

let iterateGrid (grid:string) =
    let lines = grid.Split([|'\r';'\n'|], StringSplitOptions.RemoveEmptyEntries)
    let width = lines.[0].Length
    let height = lines.Length
    let computeNeighbours x y =
        [-1,-1; 0,-1; 1,-1
         -1, 0;       1, 0
         -1, 1; 0, 1; 1, 1]
        |> List.map (fun (dx,dy) ->
            let nx,ny = x+dx, y+dy
            if nx >=0 && nx < width && ny >= 0 && ny < height &&
               lines.[ny].Chars(nx) = '1'
            then 1
            else 0
        ) |> List.sum

    let life x y c =       
        match c, computeNeighbours x y with
        | '1' ,2 ->  c
        |  _ , 3 -> '1'
        |  _ , _ -> '0'
    
    
    let newLines = lines |> Array.mapi (fun y line ->
        let chars = line.ToCharArray()
        let values = Array.mapi (fun x c -> life x y c) chars
        String(values)
    )
    String.Join("\r\n", newLines)
    
let iterateGameOfLifeTimes times x =
    let mutable grid = defaultGameOfLifeGrid
    for i in 0..times do grid <- iterateGrid grid
    0.0f