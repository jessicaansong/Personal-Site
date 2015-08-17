// JavaScript source code
function sumOfFive() {
    var n1 = parseInt(document.getElementById('sum_1').value);//Convert it to numbers
    var n2 = parseInt(document.getElementById('sum_2').value);
    var n3 = parseInt(document.getElementById('sum_3').value);
    var n4 = parseInt(document.getElementById('sum_4').value);
    var n5 = parseInt(document.getElementById('sum_5').value);
    var result = (n1 + n2 + n3 + n4 + n5);
    var display = document.getElementById('sum');
    display.innerHTML = "The sum is " + result;
}
