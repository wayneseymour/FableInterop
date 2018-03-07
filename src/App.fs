module FableInterop

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser

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

type IJQuery = interface end


module JQuery = 

  [<Emit("window['$']($0)")>]
  let select (selector: string) : IJQuery = jsNative


  [<Emit("window['$']($0)")>]
  let ready (handler: unit -> unit) : unit = jsNative
  
  [<Emit("$2.css($0, $1)")>]
  let css (prop: string) (value: string) (el: IJQuery) : IJQuery = jsNative
  
  [<Emit("$1.addClass($0)")>]
  let addClass (className: string) (el: IJQuery) : IJQuery = jsNative
  
  [<Emit("$1.click($0)")>]
  let click (handler: obj -> unit)  (el: IJQuery) : IJQuery = jsNative

JQuery.ready (fun () ->
   let div = JQuery.select "#main"
 
   div
   |> JQuery.css "background-color" "red"
   |> JQuery.click (fun ev -> console.log("Clicked"))
   |> JQuery.addClass "fancy-class"
   |> ignore
)
