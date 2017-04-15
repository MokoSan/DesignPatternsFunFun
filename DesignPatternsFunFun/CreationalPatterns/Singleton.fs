(* Ensure a class has only one instance and provide a global point of access to it. *)

module Singleton =

    type Singleton private() = 
        static let mutable instance : Singleton Lazy = lazy( Singleton() )
        static member Instance      : Singleton      = instance.Value 
        member __.DoSomething()     : unit           = 
            printfn "Doing something from the Singleton"

    let main() =
        let singleton = Singleton.Instance
        singleton.DoSomething()