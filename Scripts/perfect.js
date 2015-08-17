// JavaScript source code

//Checks whether a given number is perfect.
function perfectChecker(n) {
    var temp = 0;
    for (var x = 1; x <= n / 2; x++) {
        if (n % x === 0) {
            temp += x;
        }
    }
    if (temp === n) {
        return true;

    } else {
        return false;

    }
}

//Identify perfect numbers between 1 and 10000
function displayPerfect(num) {
    var displayHtml = document.getElementById('resultPerfect');
    for (p = num; p < 10000; p++) {
        if (perfectChecker(p)) {
            displayHtml.innerHTML += p + " is a Perfect Number" + "<br>";
        }
    }
}