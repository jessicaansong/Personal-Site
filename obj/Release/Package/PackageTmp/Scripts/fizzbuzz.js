function fizzbuzz() {
            var result = document.getElementById('result');

            for (i = 1; i <= 100; i++) {
                if (i % 15 == 0) {
                    result.innerHTML += "FizzBuzz" + " ";
                } else if (i % 3 == 0) {
                    result.innerHTML += "Fizz" + " ";
                } else if (i % 5 == 0) {
                    result.innerHTML += "Buzz" + " ";
                } else {
                    result.innerHTML += i + " ";
                }
            }
}

