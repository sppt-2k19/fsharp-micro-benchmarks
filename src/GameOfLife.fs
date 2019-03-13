module GameOfLife

open System

let defaultGameOfLifeGrid = "
0100000
0001100
0001010
"

let iterateGameOfLife (grid:string) =
    let lines = grid.Split([|'\r';'\n'|], StringSplitOptions.RemoveEmptyEntries)
    let lines = lines |> Array.filter (fun line -> line.Length > 0)
    let width = lines.[0].Length
    let height = lines.Length
    let computeNeighbours x y =
        [-1,-1; 0,-1; 1,-1
         -1, 0;       1, 0
         -1, 1; 0, 1; 1, 1]
        |> List.map (fun (dx,dy) ->
            let x, y = x + dx, y + dy
            if x >= 0 && x < width && y >= 0 && y < height && lines.[y].Chars(x) = '1'
                then 1
                else 0
        ) |> List.sum

    let life y x c =       
        match c, computeNeighbours x y with
        | '1', 2 ->  c
        |  _ , 3 -> '1'
        |  _ , _ -> '0'
    
    
    lines |> Array.mapi (fun y line -> line.ToCharArray() |> Array.mapi (life y) |> String) |> String.concat "\r\n"
    
    
let iterateGameOfLifeTimes times x =
    let mutable grid = defaultGameOfLifeGrid
    for i in 0..times do grid <- iterateGameOfLife grid
    0.0f