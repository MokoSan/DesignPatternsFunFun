(* Decouple an abstraction from its implementation so that the two can vary independently. *)

module Bridge = 

    [<Literal>]
    let DesignPatternName = "Bridge"

    type IImplementator = 
        abstract member Operate : unit -> unit

    [<Sealed>]
    type ConcreteImplementorA() =
        interface IImplementator with
            member __.Operate() =
                printfn "%s: Operate called from ConcreteImplementorA" DesignPatternName

    [<Sealed>]
    type ConcreteImplementorB() =
        interface IImplementator with
            member __.Operate() =
                printfn "%s: Operate called from ConcreteImplementorB" DesignPatternName

    type IBridge =
        abstract member Implementor : IImplementator with get, set 
        abstract member Operator    : unit -> unit

    [<Sealed>]
    type ConcreteBridge( initialImplementor : IImplementator ) =
        let mutable implementor : IImplementator = initialImplementor  

        interface IBridge with
            member __.Implementor 
                with get() = implementor 
                and set(value) = 
                    if value = implementor then ()
                    else implementor <- value

            member this.Operator() = 
                implementor.Operate()

    let main() =
        let implementorA = ConcreteImplementorA() 
        let bridge       = ConcreteBridge( implementorA ) 

        printfn "%s: Setting the implementor as ConcreteImplementorA." DesignPatternName
        ( bridge :> IBridge ).Operator()

        printfn "%s: Setting the implementor as ConcreteImplementorB." DesignPatternName
        let implementorB = ConcreteImplementorB() 
        ( bridge :> IBridge ).Implementor <- ( implementorB :> IImplementator )
        ( bridge :> IBridge ).Operator()

        0