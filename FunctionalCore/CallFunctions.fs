namespace FunctionalCore

open System.Collections.Generic
open CSharpApp.Models

module CallDataService =

    let getAllEmployeeNames (calls: ResizeArray<Call>) : ResizeArray<string> =
        calls
        |> Seq.collect (fun c -> c.Employees)
        |> Seq.map (fun e -> e.Name)
        |> Seq.distinct
        |> Seq.sort
        |> ResizeArray

    let private flatCallData (allNames: seq<string>) (call: Call) : seq<FlatData> =
        let dailyCalls =
            call.Employees
            |> Seq.map (fun e -> e.Name, e.Calls)
            |> dict

        allNames
        |> Seq.map (fun name ->
            let calls =
                match dailyCalls.TryGetValue name with
                | true, v -> v
                | _ -> 0
            FlatData(Date = call.Date, Name = name, Calls = calls))

    let getFlatRecords (calls: seq<Call>) (allNames: seq<string>) : ResizeArray<FlatData> =
        calls
        |> Seq.collect (flatCallData allNames)
        |> Seq.sortBy (fun f -> f.Date)
        |> ResizeArray