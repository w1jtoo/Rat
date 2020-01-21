# Rat

* No overloads
* No Inheritance

## Instantiation

`<type>::new <args>*`

## Basic Types

`int, string, bool, tuple, void, float, functions(may be)`

## Special Types

`Nil, Any, Optional`

## Args

* Separated by space `' '`
* If Argument may contain spaces it is better to enclose it parentheses

## Operators

`+, -, *, /, %, <<, >>, ^, &(and), |(or), not, is, in, All, =, ==, :: (no dots), match`

## Match

```python
match(<expr>):
    [<case> => <operators>+]*
    _ => <default>
```

## Type Definition

```python 
let type <typename>:
    ((<fields>[: <type>])+) where <field[i]> {in <{enum,collection}>} {or, and, setoperators (v, ^, |, ...)} <logical expr> 
 ```

## Extension

```python
let ext <typename>:
    <Function>
 ```

## Functions

```python
let {fn, parallel} <name> <arguments>* [=> <out_types>+]:
    <func1> {&, |} <func2> {&, |} <func3>
```
