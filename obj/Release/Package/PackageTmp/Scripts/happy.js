// JavaScript source code
//Happy Numbers

function happyChecker(n) {
    var arr = [];
    var newNum = 0;
    //Ok split the number into a string then into an array
    var num = n.toString().split("");
    for (var i = 0; i < num.length; i++) {
        arr[i] = parseInt(num[i], 10);
    }
    //Ok square the numbers and add them to newNum
    for (var i = 0; i < arr.length; i++) {
        newNum += Math.pow(arr[i], 2);
    }
    //And here is a quick check to stop from hitting an endless loop
    if (newNum === 58 || newNum === 4 || newNum === 37) {
        return false;
    }
    if (newNum === 1) {
        return true;
    } else {
        return happyChecker(newNum);
    }
}
function disHappy(num) {
    var happy = document.getElementById('resultHappy');
    for (j = num; j < 20; j++) {
        if (happyChecker(j)) {
            happy.innerHTML += j + " is a Happy Number." + "<br>";
        }
    }
}