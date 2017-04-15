(* Convert the interface of a class into another interface clients expect. 
Adapter lets classes work together that couldn't otherwise because of incompatible interfaces. *)

module Adapter = 

    [<Literal>]
    let DesignPatternName = "Adapter"

    type ITarget = 
        abstract member Respond : string -> unit

    [<Sealed>]
    type Adaptee() =
        member __.Response( gameName : string ) = 
            printfn "%s: Fulfilling Request from Adaptee with name: %s." DesignPatternName gameName
            match gameName with 
            | "BattleToads" -> printfn "%s: Do you have BattleToads?" DesignPatternName
            | "Duke Nukem"  -> printfn "%s: Time to Kick Ass && Chew Bubble Gum and I am all out of gum" DesignPatternName 
            | _             -> printfn "%s: Unidentified Name of Game" DesignPatternName

    [<Sealed>]
    type Adapter() =
        let adaptee : Adaptee = Adaptee()
        
        interface ITarget with
            member __.Respond( gameName : string ) =
                adaptee.Response( gameName )

    let main() = 
        let adapter : Adapter = Adapter()

        printfn "%s: Calling 'BattleToads' from the Adaptee." DesignPatternName 
        (adapter :> ITarget).Respond( "BattleToads" )

        printfn "%s: Calling 'Duke Nukem' from the Adaptee." DesignPatternName 
        (adapter :> ITarget).Respond( "Duke Nukem" )

        printfn "%s: Calling 'Doom' from the Adaptee." DesignPatternName 
        (adapter :> ITarget).Respond( "Doom" )

        0