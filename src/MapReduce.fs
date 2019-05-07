module MapReduce

open System
    
let numbers = [| "13.37"; "42.42"; "741.897"; "989.981"; "168.186" |]

let mapReduceArray (dummy:int) : float32 =
    let result = numbers |> Array.map Single.Parse |> Array.sum
    result + float32 dummy
    
let mapReduceSeq (dummy:int) : float32 =
    let result = numbers |> Seq.map Single.Parse |> Seq.sum
    result + float32 dummy
    
    
type Temperature = Celsius of float32 | Fahrenheit of float32 | Kelvin of float32
let temperatures = [| Celsius(12.5f); Celsius(65.4f); Fahrenheit(123.32f); Celsius(37.5f); Fahrenheit(100.0f); Fahrenheit(98.7f); Kelvin(1.0f) |]

let toKelvin (temp:Temperature) =
    match temp with
    | Celsius c -> c + 273.15f
    | Fahrenheit f -> ((f - 32.0f) / 1.8f) + 273.15f
    | Kelvin k -> k
    
let mapReduceUnions dummy =
    let result = temperatures |> Array.map toKelvin |> Array.sum
    result + float32 dummy
