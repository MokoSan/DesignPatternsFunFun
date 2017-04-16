(* Use sharing to support large numbers of fine-grained objects efficiently. *)

module Flyweight =

    open System.Collections.Generic

    [<Literal>]
    let DesignPatternName = "Flyweight"

    type IFlyweight = 
        abstract member Name    : string with get
        abstract member Operate : unit -> unit 

    type ConcreteFlyweight( name : string ) =
        interface IFlyweight with
            member __.Name = name
            member __.Operate() = 
                printfn "%s: Operate Called from ConcreteFlyweight" DesignPatternName

    [<Sealed>]
    type FlyweightFactory() =
        let mutable flyweights : Map<string, ConcreteFlyweight> = Map.empty

        member __.GetFlyweightByName( name : string ) : IFlyweight = 
            match Map.tryFind name flyweights with
            | Some v -> v :> IFlyweight 
            | None   -> 
                let newConcreteFlyweight = ConcreteFlyweight( name )
                printfn "%s: Creating a new Flyweight with name: %s" DesignPatternName name
                flyweights <- Map.add name newConcreteFlyweight flyweights
                newConcreteFlyweight :> IFlyweight

    let main() = 

        printfn "%s: Creating the Flyweight factory" DesignPatternName
        let flyweightFactory = FlyweightFactory()

        printfn "%s: Calling Get Flyweight By Name for name: FlyweightA" DesignPatternName
        let flyweightA = flyweightFactory.GetFlyweightByName( "FlyweightA" )

        printfn "%s: Calling Get Flyweight By Name for name: FlyweightB" DesignPatternName
        let flyweightB = flyweightFactory.GetFlyweightByName( "FlyweightB" )

        printfn "%s: Calling Get Flyweight By Name for name: FlyweightA" DesignPatternName
        let flyweightA' = flyweightFactory.GetFlyweightByName( "FlyweightA" )

        printfn "%s: Checking if the object reference of the two FlyweightAs are the same" DesignPatternName
        let areEqual = obj.ReferenceEquals( flyweightA, flyweightA' )
        printfn "%s: The references are equal: %b" DesignPatternName areEqual

        0