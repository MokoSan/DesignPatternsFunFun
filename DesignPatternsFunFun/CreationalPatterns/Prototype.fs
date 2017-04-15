(* Specify the kind of objects to create using a prototypical instance, and create new objects by copying this prototype. *)

module Prototype = 

    [<Literal>]
    let DesignPatternName = "Prototype"

    type IPrototype  =
        inherit System.ICloneable
        abstract Name : string 

    [<Sealed>]
    type PrototypeA( name : string ) =
        interface IPrototype with
            member __.Name    = name 
            member __.Clone() = PrototypeA( name ) :> obj

    [<Sealed>]
    type PrototypeB( name : string ) = 
        interface IPrototype with
            member __.Name    = name 
            member __.Clone() = PrototypeB( name ) :> obj

    let main() =
        let prototypeA : IPrototype = ( PrototypeA( "Link ") :> IPrototype ).Clone() :?> IPrototype
        printfn "%s: Creating new Prototype of Prototype A with Name: %s" DesignPatternName prototypeA.Name 
        let prototypeB : IPrototype = ( PrototypeB( "Zelda" ) :> IPrototype ).Clone() :?> IPrototype
        printfn "%s: Creating new Prototype of Prototype B with Name: %s" DesignPatternName prototypeB.Name 
        0