module FableInterop

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser
open System

console.log("Fable is up and running...YO!");

[<Emit("$0 === undefined")>]
let isUndefined (x: 'a) : bool = jsNative

[<Emit("undefined")>]
let undefined : obj = jsNative

console.log(isUndefined 5) // false
console.log(isUndefined "") // false
console.log(isUndefined [||]) // false
console.log(isUndefined (undefined)) // true

[<Emit("$0 + $1")>]
let add (x: int) (y: int) = jsNative

console.log(add 5 10) // console.log(5 + 10)


[<Emit("isNaN($0)")>]
let isNaN (x: 'a) = jsNative
console.log(isNaN (log -2.0)) // true

[<Emit("Math.random()")>]
let getRandom() : float = jsNative
console.log(getRandom())

// [<Emit("isNaN(+$0) ? null : (+$0)")>]
// let parseFloat (input : string) : float option = jsNative

[<Emit("(x => isNaN(x) ? null : x)(+$0)")>]
let parseFloat (input : string) : float option = jsNative

match parseFloat "5x" with                 
| Some result -> console.log(result)       //  Parsing fails as it should
| None -> console.log("No result found")   //  logs "No result found"


type AddTimeProps = 
  abstract current : DateTime with get, set
  abstract amount : int with get, set
  abstract unit : string with get, set

let parameter = createEmpty<AddTimeProps>
parameter.current <- DateTime.Now
parameter.amount <- 20
parameter.unit <- "days"

console.log(parameter) 