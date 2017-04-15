(* Provide an interface for creating families of related or dependent objects without specifying their concrete classes. *)

[<AbstractClass>]
type AbstractProductA() = 
    abstract member Name : string with get 

[<Sealed>]
type ConcreteProductA( name : string ) = 
    inherit AbstractProductA()
    override __.Name = name 

[<AbstractClass>]
type AbstractProductB() = 
    abstract member Name : string with get

[<Sealed>]
type ConcreteProductB( name : string ) =
    inherit AbstractProductB()
    override __.Name = name

[<AbstractClass>]
type AbstractFactory() = 
    abstract member CreateProductA : string -> AbstractProductA 
    abstract member CreateProductB : string -> AbstractProductB 

[<Sealed>]
type ConcreteFactory() =
    inherit AbstractFactory()
    override __.CreateProductA( name : string ) = 
        ConcreteProductA( name : string ) :> AbstractProductA
    override __.CreateProductB( name : string ) =
        ConcreteProductB( name : string ) :> AbstractProductB

let main() =
    let factory : AbstractFactory = ConcreteFactory() :> AbstractFactory 
    printfn "Created an instance of an Abstract Factory.\n"
    let productA : AbstractProductA = factory.CreateProductA("Product A") 
    printfn "Created Product A.\n"
    let productB : AbstractProductB = factory.CreateProductB("Product B")
    printfn "Created Product B.\n"
    0