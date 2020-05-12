# Rat

Rat is a high-level functional programming language with compilation to Microsoft Intermediate Language (MSIL). 

Main fature is chaining methods with | and & operatiors. For example:

```Rat
let div a, b = if b != 0 a / b else Err

print & (toString & (div | 0) (...))
```

Same rust example code:

```Rust
fn div(a: f32, b: f32) -> Result<f32> {
    match b {
        0 => Err(),
        _ => a / b
    }
fn main() {
    println!("{}", div(...).or_else(0));
    }
}
```

## Rat 0.1 features

Features are needed to bootstrapping.

### Fucntion

```Rat
let foo a, b = a + b
```

### Operations

To numerical types:

- binary: _+, *, /, %_
- unary: _-_
- ternary: _if_ __condition__ __then_branch__ _else_ __else_branch__

To functuins:

- | - __TODO description__
- & - __TODO description__
  
Extention block:

```Rat
Extention DotnetMath:
    Math.Cos
    Console.Abs

Extention DotnetMath:
    Console.Writeline
    Console.Readley
```
