(* Attach additional responsibilities to an object dynamically. 
   Decorators provide a flexible alternative to subclassing for extending functionality.*)

module Decorator =

    [<Literal>]
    let DesignPatternName = "Decorator"
    
    type IComponent =
        abstract member Operate : unit -> unit

    [<Sealed>]
    type ConcreteComponent( name : string ) = 
        member __.Name = name
        interface IComponent with
            member __.Operate() = 
                printfn "%s: Calling Operate from ConcreteComponent" DesignPatternName 

    [<AbstractClass>]
    type Decorator() =
        abstract member Operate             : unit       -> unit
        abstract member AdditionalOperation : unit       -> unit 
        abstract member Component           : IComponent with get 
        interface IComponent with
            member __.Operate() = __.Operate() 

    [<Sealed>]
    type ConcreteDecoratorA( componentToDecorate : IComponent ) as this =
        inherit Decorator()
        override this.Component = componentToDecorate
        override this.Operate() =
            this.AdditionalOperation()
            printfn "%s: Calling Operate from ConcreteDecoratorA" DesignPatternName
        override this.AdditionalOperation() =
            printfn "%s: Performing Additional Operation from ConcreteDecoratorA" DesignPatternName

    [<Sealed>]
    type ConcreteDecoratorB( componentToDecorate : IComponent ) as this =
        inherit Decorator()
        override this.Component = componentToDecorate
        override this.Operate() =
            this.AdditionalOperation()
            printfn "%s: Calling Operate from ConcreteDecoratorB" DesignPatternName
        override this.AdditionalOperation() =
            printfn "%s: Performing Additional Operation from ConcreteDecoratorB" DesignPatternName

    let main() = 
        let componentToDecorate = ConcreteComponent( "Pizza" )
        printfn "%s: Created Concrete Component Named %s" DesignPatternName componentToDecorate.Name
        ( componentToDecorate :> IComponent ).Operate() 

        printfn "%s: Created Decorator Component A" DesignPatternName 
        let decorationA         = ConcreteDecoratorA( componentToDecorate )  
        decorationA.Operate()

        printfn "%s: Created Decorator Component B based on Decoration Component A" DesignPatternName 
        let decorationB         = ConcreteDecoratorB( decorationA )
        decorationB.Operate()

        0