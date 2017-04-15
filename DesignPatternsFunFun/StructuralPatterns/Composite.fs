(* Compose objects into tree structures to represent part-whole hierarchies. 
Composite lets clients treat individual objects and compositions of objects uniformly. *)

module Composite = 

    [<Literal>]
    let DesignPatternName = "Composite"
    
    type IComponent = 
        abstract Name    : string with get 
        abstract Add     : IComponent -> unit
        abstract Remove  : IComponent -> unit
        abstract Display : unit       -> unit 

    [<Sealed>]
    type Leaf( name : string ) =
        let mutable name = name
        interface IComponent with
            member __.Name                     = name 
            member __.Add( _ : IComponent )    =  
                failwithf "%s: Cannot Add to a Leaf." DesignPatternName 
            member __.Remove( _ : IComponent ) =  
                failwithf "%s: Cannot Remove from a Leaf." DesignPatternName 
            member __.Display()                = 
                printfn "%s: Name of Leaf: %s" DesignPatternName name 

    [<Sealed>]
    type Composite( name : string ) = 
        let mutable components : list<IComponent> = list.Empty 
        interface IComponent with 
            member val Name = name with get 
            member __.Add( componentToAdd : IComponent )       =
                printfn "%s: Adding %s to components" DesignPatternName componentToAdd.Name
                components <- components @ [ componentToAdd; ]

            member __.Remove( componentToRemove : IComponent ) =
                printfn "%s: Removing %s from components" DesignPatternName componentToRemove.Name
                let rec remove ( list : list<IComponent> ) : list<IComponent> = 
                    match list with
                    | []                                           -> [] 
                    | x :: xs when x.Name = componentToRemove.Name -> 
                        printfn "%s: Successfully removing %s from the components" DesignPatternName componentToRemove.Name
                        xs 
                    | x :: xs -> x :: remove xs 
                components <- remove components
            member __.Display() =
                printfn "%s: Displaying all Leafs of the Composite:" DesignPatternName
                components |> List.iter ( fun x -> printf "%s " x.Name )
                printf "\n"
    let main() =
        let composite = Composite("Composite")

        printfn "%s: Creating Leaf 1 and adding to Composite." DesignPatternName
        let leaf1     = Leaf( "Leaf1" )
        ( composite :> IComponent ).Add( leaf1 )

        printfn "%s: Creating Leaf 2 and adding to Composite." DesignPatternName
        let leaf2     = Leaf( "Leaf2" )
        ( composite :> IComponent ).Add( leaf2 )
        ( composite :> IComponent ).Display()

        printfn "%s: Creating Leaf 3 and adding to Composite." DesignPatternName
        let leaf3     = Leaf( "Leaf3" )
        ( composite :> IComponent ).Add( leaf3 )
        ( composite :> IComponent ).Display()

        printfn "%s: Removing Leaf 2 from Composite." DesignPatternName
        ( composite :> IComponent ).Remove( leaf2 )
        ( composite :> IComponent ).Display()
        
        0