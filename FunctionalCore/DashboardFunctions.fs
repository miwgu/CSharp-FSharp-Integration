namespace FunctionalCore

open System.Collections.Generic
open CSharpApp.Models

module DashboardFunctions =

    let createDashboard
        (flatRecords: ResizeArray<FlatData>)
        (allEmployeeNames: ResizeArray<string>) : DashboardView =

        let flatRecordsByYear = Dictionary<string, List<FlatData>>()

        flatRecords
        |> Seq.groupBy (fun record -> record.YearString)
        |> Seq.iter (fun (year, records) ->
            flatRecordsByYear.Add(
                year,
                records
                |> Seq.sortBy (fun record -> record.Date)
                |> List<FlatData>
            ))

        let labelsByMonth = Dictionary<string, List<string>>()

        flatRecords
        |> Seq.groupBy (fun record -> record.YearMonthString)
        |> Seq.iter (fun (month, records) ->
            labelsByMonth.Add(
                month,
                records
                |> Seq.map (fun record -> record.DateString)
                |> Seq.distinct
                |> Seq.sort
                |> List<string>
            ))

        let labelsByYear = Dictionary<string, List<string>>()

        flatRecords
        |> Seq.groupBy (fun record -> record.YearString)
        |> Seq.iter (fun (year, records) ->
            labelsByYear.Add(
                year,
                records
                |> Seq.map (fun record -> record.DateString)
                |> Seq.distinct
                |> Seq.sort
                |> List<string>
            ))

        let dashboard = DashboardView()
        dashboard.FlatRecords <- flatRecords
        dashboard.FlatRecordsByYear <- flatRecordsByYear
        dashboard.AllEmployeeNames <- allEmployeeNames
        dashboard.LabelsByMonth <- labelsByMonth
        dashboard.LabelsByYear <- labelsByYear
        dashboard.AvailableMonths <-
            labelsByMonth.Keys
            |> Seq.sort
            |> List<string>
        dashboard.AvailableYears <-
            labelsByYear.Keys
            |> Seq.sort
            |> List<string>

        dashboard
