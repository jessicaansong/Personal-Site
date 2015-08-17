// JavaScript source code
function fizzbuzz2() {
    var x = document.getElementById("x").value
    var y = document.getElementById("y").value
    var fizzbuzz_result = document.getElementById('fizzbuzz_result'); 
    //alert(x + " " + y)
    for (i = 1; i <= 100; i++) {
        if (i % (x*y) == 0) {
            fizzbuzz_result.innerHTML += "FizzBuzz" + " ";
        } else if (i % x == 0) {
            fizzbuzz_result.innerHTML += "Fizz" + " ";
        } else if (i % y == 0) {
            fizzbuzz_result.innerHTML += "Buzz" + " ";
        } else {
            fizzbuzz_result.innerHTML += i + " ";
        }
    }
}