namespace FunctionalCore

open CSharpApp.Models

module CallDataService =

    let getAllEmployeeNames (calls: ResizeArray<Call>) : ResizeArray<string> =
        calls
        |> Seq.collect (fun c -> c.Employees)
        |> Seq.map (fun e -> e.Name)
        |> Seq.distinct
        |> Seq.sort
        |> ResizeArray