// JavaScript source code
function armstrongChecker(n) {
    var arr = [];
    var newNum = 0;
    //Take n and split the digits into single numbers
    var num = n.toString().split("");
    for (var i = 0; i < num.length; i++) {
        arr[i] = parseInt(num[i], 10);
    }
    //Now cube the numbers and add them together
    for (var i = 0; i < arr.length; i++) {
        newNum += Math.pow(arr[i], 3);
    }
    //Here we check to see if the newNum matches the original num
    if (newNum === n) {
        return true;
    } else {
        return false;
    }
}
function disArmstrong(num) {
    var armstrong = document.getElementById('resultArmstrong');
    for (j = num; j < 1000; j++) {
        if (armstrongChecker(j)) {
            armstrong.innerHTML += j + " is an Armstrong Number." + "<br>";
        }
    }
}