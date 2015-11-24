(**
- title : F# Das Typsystem
- description : Einführung in das Typsystem von F#
- author : Nasser Brake
- theme : night
- transition : none

***
## F# 
## Das Typsystem
 
#### Nasser Brake
#### http://www.nasserbrake.de
#### https://www.github.com/nasserbrake

***
### Tuple

Eine Menge von Elementen 

- Müssen nicht vom gleichen Typ sein
- Die Reihenfolge ist entscheidend
- Ist ein ad-hoc Typ: keine benannter

' Auch wenn ich einen Typ definieren kann, kann ich einzelne Werte diesen Typen nicht zuordnen.  Die einzige Zuordnung ist den Bestandteilen und deren Reihenfolge

---
### Tuple: Construction

- Kann beliebig viele Elemente beinhalten
- Construction erolgt mittels Komma (mit und ohne Klammern)
- Multiplikation von (mindestens) zwei Domains: Kartesiche Summe

*)

type IntTuple = int * int // Jeder int 'mal' jeder int
let intTuple  = 1,1

type IntStringTuple = int * string // Jeder int 'mal' jeder string
let intStringTuple  = 1,"string"

type TripleIntTuple = int * int * int
let tripleIntTuple = 1,2,3

(**

---
### Tuple: Komposition

Aus einfachen Typen lassen sich komplexere erstellen

*)

type Complex = float * float
let complex  = 1.0,1.0

type Komposition = IntStringTuple * Complex
let komposition  = intStringTuple,complex

(**

---
### Tuple: Deconstruction
Ein Tuple kann in seinen Bestandteilen zerlegt werden

*)
let complex' = 1.0,2.0
let c',c'' = complex'
let komposition' = intStringTuple,complex'
let k',k'' = komposition'

(** <div style="display: none" > *)
(*** define-output:complex ***)
printf "c' = %A | c'' = %A" c' c''
(*** define-output:komposition ***)
printf "k' = %A | k'' = %A" k' k''
(** </div> *)

(*** include-output: complex ***)
(*** include-output: komposition ***)

(**
' In F# ist es üblich Varianten von einem Wert mittels '' zu kennzeichnen
' Klammern sind nicht notwendig, dienen der Abgrenzung und Klarheit

---
### Tuple: Strukturelle Gleichheit

- Typ und Reihnfolge: sind Zwei Werte vergelichbar?
- Werte: sind zwei Werte gleich?

*)
let equal = complex' = complex

(** <div style="display: none" > *)
(*** define-output:Type-Equality ***)
printf "Gleich = %b" equal
(** </div> *)

(*** include-output: Type-Equality ***)

(**
' Typ Definition ist hilfreich bei Signaturen

---
### Tuple: Pattern Matching

*)
let matchComplex c = 
    match c with
    | 0.0,0.0 -> "0.0,0.0"
    | 1.0,1.0 -> "0.0,0.0"
    | 1.0,2.0 -> "1.0,1.0"
    | _,_ -> "sonst"

let result   = matchComplex complex
let result'  = matchComplex complex'
let result'' = matchComplex (2.0,2.0) // Brauche jetzt Klammern!

(** <div style="display: none" > *)
(*** define-output:Tuple-PatternMatching ***)
printf "result = %A | " result 
printf "result' = %A | " result'
printf "result'' = %A " result''
(** </div> *)
(*** include-output: Tuple-PatternMatching ***)

(**
' Fehlnde Fälle werden vom Compiler festgestellt und angezeigt

---

(**



---

***
  
### Funktionale Sprachen: kein Trend

- Imperative (Fortran, Cobol) 50er Jahre
- Funktionale (Lisp) 50er Jahre
- Objektorientierte (Smalltalk, Java, C#) 70er Jahre

' In einer Industrie voller trends!
' F# ist eine Sprache in der ML Familie

*** 

### Funktionale Ideen

- Trennung von Daten und Verhalten
- Funktionen sind normale Typen, können als Eingabe, Ausgabe verwendet werden
- Kompsition: light-weight Typen verbinden zu neuen composite-types
- Immutability: Keine Variablen sondern Ausdrucke

' Manche dieser Ideen werden auch in anderen Sprachen gepflegt, z.B. Immutability. 

***
    
### Funktionales C#

- C# 3.0 führt funktionale Elemente ein: Lambdas und Linq
- Func<> als Parameter verwenden (Strategy Pattern)
- Immutability macht das code verständlich
    
' Alle Bibliotheken aus .net stehen zur Verfügung
' Feststellung: funktionale Konstrukte können Code vereinfachen, noch mehr wenn der Syntax dies unterstützt
' Warum dann nicht F# probieren? DAS ORIGINAL!ss

***

### F# 

- Functions as first-class elements
- Immutable per default
- Algebraic Data Types
- Functional-first

' Function-first heißt mir stehen alle Elemente der OO aus .net zur Verfügung, kann APIs erstellen die von C#/VB.NET aus angesprochen werden können
' Mutability erfodert anpassungen, ist also möglich
' Funktionen müssen nicht als delegates definiert werden
' Die ADT werden wir heute untersuchen
' ADT's in F# können aber auch Verhalten haben, es ja auch eine functional first Sprache!

***

### Syntax Highlighting

#### F# (with tooltips)

*)
let a = 5
let factorial x = [1..x] |> List.reduce (*)
let c = factorial a
(**
`c` is evaluated for you
*)
(*** include-value: c ***)
(**

---

#### More F#

*)
[<Measure>] type sqft
[<Measure>] type dollar
let sizes = [|1700<sqft>;2100<sqft>;1900<sqft>;1300<sqft>|]
let prices = [|53000<dollar>;44000<dollar>;59000<dollar>;82000<dollar>|]
(**

#### `prices.[0]/sizes.[0]`

*)
(*** include-value: prices.[0]/sizes.[0] ***)
(**

---

#### C#

    [lang=cs]
    using System;


    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, world!");
        }
    }


---

#### JavaScript

    [lang=js]
    function copyWithEvaluation(iElem, elem) {
      return function (obj) {
          var newObj = {};
          for (var p in obj) {
              var v = obj[p];
              if (typeof v === "function") {
                  v = v(iElem, elem);
              }
              newObj[p] = v;
          }
          if (!newObj.exactTiming) {
              newObj.delay += exports._libraryDelay;
          }
          return newObj;
      };
    }

---

#### Haskell

    [lang=haskell]
    recur_count k = 1 : 1 : zipWith recurAdd (recur_count k) (tail (recur_count k))
            where recurAdd x y = k * x + y

    main = do
      argv <- getArgs
      inputFile <- openFile (head argv) ReadMode
      line <- hGetLine inputFile
      let [n,k] = map read (words line)
      printf "%d\n" ((recur_count k) !! (n-1))


*code from [NashFP/rosalind](https://github.com/NashFP/rosalind/blob/master/mark_wutka%2Bhaskell/FIB/fib_ziplist.hs)*

---

### SQL

    [lang=sql]
    select *
    from
      (select 1 as Id union all select 2 union all select 3) as X
    where Id in (@Ids1, @Ids2, @Ids3)

*sql from [Dapper](https://code.google.com/p/dapper-dot-net/)*

***

**Bayes' Rule in LaTeX**

$ \Pr(A|B)=\frac{\Pr(B|A)\Pr(A)}{\Pr(B|A)\Pr(A)+\Pr(B|\neg A)\Pr(\neg A)} $

***

### The Reality of a Developer's Life

**When I show my boss that I've fixed a bug:**

![When I show my boss that I've fixed a bug](http://www.topito.com/wp-content/uploads/2013/01/code-07.gif)

**When your regular expression returns what you expect:**

![When your regular expression returns what you expect](http://www.topito.com/wp-content/uploads/2013/01/code-03.gif)

*from [The Reality of a Developer's Life - in GIFs, Of Course](http://server.dzone.com/articles/reality-developers-life-gifs)*

***
- data-background : images/fsharp128.png
- data-background-repeat : repeat
- data-background-size : 100px

### Slides Properties

- http://fsprojects.github.io/FsReveal/formatting.html#Slide-properties

***

### Speaker Notes

- Press `s` to see a speaker note

' this is a speaker note
' and here we have another one

*)
